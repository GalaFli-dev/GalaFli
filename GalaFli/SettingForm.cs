using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GalaFli
{
    public partial class SettingForm : Form
    {
        //HIDの表示を一定の文字数にするための定数(表示用)
        const int HID_FORMAT_LENGTH = 21;
        //iniファイルの書き込み用の関数
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        //読み込み時にコンボボックスにデバイスを表示するかどうかのフラグ
        bool initializeFlag = true;
        //設定ファイルがあるとき読み込んで現状の設定を反映させる
        TenkeySettings tenkeySettings = new TenkeySettings();
        // アイコン読み込みのための変数
        NotifyIcon notifyIcon;

        public SettingForm(NotifyIcon a)
        {
            notifyIcon = a;
            InitializeComponent();
        }
        //デバイス一覧を取得する関数
        private void GetKeyboardSet(bool inF)
        {
            //更新にも使うため初期化する
            ItemBox.Items.Clear();
            // USBデバイス情報を取得するためのクエリを作成
            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0 AND PNPClass = 'Keyboard'";
            // クエリを実行するためのオブジェクトを作成し実行
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection usbDevices = searcher.Get();

            //デバイス情報に番号をつけるためのカウンタ
            int cnt = 1;

            // 取得したデバイス情報を列挙
            foreach (ManagementObject usbDevice in usbDevices)
            {
                // deviceNameにAPIから取得したデバイス名を格納、なければN/A
                string deviceName = usbDevice["Name"] != null ? usbDevice["Name"].ToString() : "N/A";
                // PnPEntityから受け取れるHardWareIDを配列で取得
                string[] HIDs = usbDevice["HardwareID"] as string[];
                // InputIntercepterで取得できるデバイスIDに合わせたIDを格納
                string HID = usbDevice["HardwareID"] != null ? HIDs[0].ToString() : "N/A";
                //更新されたデバイス一覧として弾く用のリストに入れる
                //デバイスIDは「HID...PID_(数字4桁)」までの表示する
                if (HID.Length > HID_FORMAT_LENGTH)
                {
                    ItemBox.Items.Add(new KeyValuePair(cnt + ". " + deviceName + "(" + HID.Substring(0, HID_FORMAT_LENGTH) + "...)", HID));
                    cnt++;
                }
            }
            if (inF)
            {
                ItemBox.Text = ItemBox.Items[0].ToString();
            }
        }
        private void SettingForm_Load(object sender, EventArgs e)
        {
            GetKeyboardSet(initializeFlag);
            initializeFlag = false;
            //設定ファイルがあるとき読み込んで現状の設定を反映させる
            if (tenkeySettings.deviceId != "none")
            {
                foreach (KeyValuePair item in ItemBox.Items)
                {
                    //設定ファイルのデバイスIDと一致するものをコンボボックスで選択した状態にする
                    if (item.Value == tenkeySettings.deviceId)
                    {
                        ItemBox.SelectedItem = item;
                        break;
                    }
                }
                //チェックボックスに設定ファイルの内容を反映
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
        //保存ボタンの処理
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //デバイスが選択されていなかったらエラー
            if (ItemBox.SelectedItem == null)
            {
                MessageBox.Show("デバイスを選択してください", "エラー");
                return;
            }
            KeyValuePair saveDevice = (KeyValuePair)ItemBox.SelectedItem;

            //設定ファイルに書き込む
            bool[] ret = new bool[5];
            //WritePrivateProfileString関数を使用し書き込み
            ret[0] = WritePrivateProfileString("Tenkey", "DeviceId", saveDevice.Value, ".\\TenkeySettings.ini");
            ret[1] = WritePrivateProfileString("Tenkey", "isTab", isTabNumlock.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[2] = WritePrivateProfileString("Tenkey", "isBSUpper", isBackSpace.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[3] = WritePrivateProfileString("Tenkey", "isZeroUnion", isIntegration.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[4] = WritePrivateProfileString("Tenkey", "isZeroThree", isThreeZeros.Checked.ToString(), ".\\TenkeySettings.ini");

            //設定が変わったらアプリを再起動する
            notifyIcon.Visible = false;
            Application.Restart();
            Environment.Exit(0);
        }
        //更新ボタン
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            GetKeyboardSet(initializeFlag);
        }
        //キャンセルボタン
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Restart();
            Environment.Exit(0);
        }
        //チェックボックスが押された時のプレビューの変更
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
    }
    //コンボボックスにデバイス名とデバイスIDを格納するためのクラス
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
