using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GalaFli
{
    public partial class SettingForm : Form
    {
        const int HID_FORMAT_LENGTH = 21;//HIDの表示を一定の文字数にするための定数


        //iniファイルの読み書き用
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        bool initializeFlag = true;

        //設定ファイルがあるとき読み込んで現状の設定を反映させる
        TenkeySettings tenkeySettings = new TenkeySettings();

        NotifyIcon notifyIcon;

        public SettingForm(NotifyIcon a)
        {
            notifyIcon = a;
            InitializeComponent();
        }


        private void GetKeyboardSet(bool inF)
        {
            comboBox1.Items.Clear();
            // USBデバイス情報を取得するためのクエリを作成
            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0 AND PNPClass = 'Keyboard'";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection usbDevices = searcher.Get();
            int cnt = 1;
            foreach (ManagementObject usbDevice in usbDevices)
            {
                // クラスがUSBデバイスであるかどうかを確認
                string deviceName = usbDevice["Name"] != null ? usbDevice["Name"].ToString() : "N/A";
                string[] HIDs = usbDevice["HardwareID"] as string[];
                string HID = usbDevice["HardwareID"] != null ? HIDs[0].ToString() : "N/A";
                //更新されたデバイス一覧として弾く用のリストに突っ込む
                //デバイスIDはPIDまでの表示する
                if (HID.Length > HID_FORMAT_LENGTH)
                {
                    comboBox1.Items.Add(new KeyValuePair(cnt + ". " + deviceName + "(" + HID.Substring(0, HID_FORMAT_LENGTH) + "...)", HID));
                    cnt++;
                }

            }

            if (inF)
            {
                comboBox1.Text = comboBox1.Items[0].ToString();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetKeyboardSet(initializeFlag);
            initializeFlag = false;
            if (tenkeySettings.deviceId != "none")
            {
                foreach (KeyValuePair item in comboBox1.Items)
                {
                    if (item.Value == tenkeySettings.deviceId)
                    {
                        comboBox1.SelectedItem = item;
                        break;
                    }
                }
                isTabNumlock.Checked = tenkeySettings.isTab;
                isBackSpace.Checked = tenkeySettings.isBSUpper;
                isIntegration.Checked = tenkeySettings.isZeroUnion;
                isThreeZeros.Checked = tenkeySettings.isZeroThree;
            }
            if (!isIntegration.Checked)
            {
                isThreeZeros.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {






        }

        //保存ボタンの処理
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //デバイスが選択されていなかったらエラー
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("デバイスを選択してください", "エラー");
                return;
            }
            KeyValuePair saveDevice = (KeyValuePair)comboBox1.SelectedItem;

            bool[] ret = new bool[5];
            ret[0] = WritePrivateProfileString("Tenkey", "DeviceId", saveDevice.Value, ".\\TenkeySettings.ini");
            ret[1] = WritePrivateProfileString("Tenkey", "isTab", isTabNumlock.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[2] = WritePrivateProfileString("Tenkey", "isBSUpper", isBackSpace.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[3] = WritePrivateProfileString("Tenkey", "isZeroUnion", isIntegration.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[4] = WritePrivateProfileString("Tenkey", "isZeroThree", isThreeZeros.Checked.ToString(), ".\\TenkeySettings.ini");

            notifyIcon.Visible = false;
            Application.Restart();
            Environment.Exit(0);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            GetKeyboardSet(initializeFlag);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Restart();
            Environment.Exit(0);
        }

        private void checkBack_CheckedChanged(object sender, EventArgs e)
        {
            if (T_B_BS.Visible)
            {
                T_B_BS.Visible = false;
                T_B_Plus.Visible = false;
            }
            else
            {
                T_B_BS.Visible = true;
                T_B_Plus.Visible = true;
            }
        }

        private void isTabNumlock_CheckedChanged(object sender, EventArgs e)
        {
            if (T_Tab.Visible) T_Tab.Visible = false;
            else T_Tab.Visible = true;
        }

        private void T_B_BS_Click(object sender, EventArgs e)
        {

        }

        private void isIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (isIntegration.Checked)
            {
                T_B_UnionZeros.Visible = true;
                isThreeZeros.Enabled = false;
                isThreeZeros.Checked = false;

                T_A_Zero.Visible = false;
                T_A_ThreeZeros.Visible = false;
                T_A_TwoZeros.Visible = false;
            }
            else
            {
                T_B_UnionZeros.Visible = false;
                isThreeZeros.Enabled = true;

                T_A_Zero.Visible = true;
                T_A_TwoZeros.Visible = true;
            }
        }

        private void isThreeZeros_CheckedChanged(object sender, EventArgs e)
        {
            if (isThreeZeros.Checked)
            {
                T_A_ThreeZeros.Visible = true;
            }
            else
            {

                T_A_ThreeZeros.Visible = false;
            }
        }

        private void T_A_ThreeZeros_Click(object sender, EventArgs e)
        {

        }
    }

    public class KeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public KeyValuePair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Key;
        }



    }


}
