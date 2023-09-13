using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace GalaFli
{
    public partial class SettingForm : Form
    {



        //iniファイルの読み書き用
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);


        
        public SettingForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // USBデバイス情報を取得するためのクエリを作成
            string query = "SELECT * FROM Win32_KeyBoard WHERE ConfigManagerErrorCode = 0";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection usbDevices = searcher.Get();
            

            foreach (ManagementObject usbDevice in usbDevices)
            {
                // クラスがUSBデバイスであるかどうかを確認
                
                
                string deviceName = usbDevice["Name"] != null ? usbDevice["Name"].ToString() : "N/A";
                string deviceId = usbDevice["DeviceID"] != null ? usbDevice["DeviceID"].ToString() : "N/A";

                //更新されたデバイス一覧として弾く用のリストに突っ込む
                comboBox1.Items.Add(new KeyValuePair(deviceName, deviceId));
                
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            // 選択されたUSBデバイスの情報を表示
            KeyValuePair selectedDevice = (KeyValuePair)comboBox1.SelectedItem;
            MessageBox.Show($"デバイス名: {selectedDevice.Key}\nデバイスID: {selectedDevice.Value}");
            
            


        }

        //保存ボタンの処理
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //デバイスが選択されていなかったらエラー
            if(comboBox1.SelectedItem == null)
            {
                MessageBox.Show("デバイスを選択してください", "エラー");
                return;
            }
            KeyValuePair saveDevice = (KeyValuePair)comboBox1.SelectedItem;

            bool[] ret = new bool[5];
            //Interceprtionで取得しているHIDの形式に変換して保存する
            //Win32_Keyboardで取得した"USB\VID_PID"の形式を"HID\VID_PID"に変換してWin32_PnPEntityに投げ、欲しいHIDを取得する
            if (Regex.IsMatch(saveDevice.Value, ".+VID.+PID.+"))
            {
                string HIDsearchperser = saveDevice.Value.ToString();
                string searchTerm = "PID"; // 置換対象の文字列
                int startIndex = HIDsearchperser.IndexOf(searchTerm);
                if (startIndex >= 0)
                {
                    int endIndex = startIndex + searchTerm.Length + 5; // "PID_" + 5文字以降のインデックス
                    string replacement = HIDsearchperser.Substring(0, endIndex);
                    HIDsearchperser = replacement;
                }
                else
                {
                    Console.WriteLine("置換対象が見つかりませんでした。");
                }


                HIDsearchperser = HIDsearchperser.Replace("USB", "HID");
                HIDsearchperser = HIDsearchperser.Replace("\\", "\\\\");
                Debug.WriteLine(HIDsearchperser);
                string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0 AND DeviceID LIKE '%" + HIDsearchperser + "%'";
                Debug.WriteLine(query);
                ManagementObjectSearcher sercher = new ManagementObjectSearcher(query);
                ManagementObjectCollection selectedDevices = sercher.Get();
                foreach (ManagementObject obj in selectedDevices)
                {
                    // ハードウェアIDを取得
                    string[] hardwareIDs = (string[])obj["HardwareID"];
                    if (hardwareIDs != null)
                    {
                        Debug.WriteLine("Hardware IDs:");
                        foreach (string hardwareID in hardwareIDs)
                        {
                            if (Regex.IsMatch(hardwareID, "HID.+VID.+PID.+REV")) {
                                ret[0] = WritePrivateProfileString("Tenkey", "deviceId", hardwareID, ".\\TenkeySettings.ini");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("このデバイスは使用できません、違うデバイスを選択して下さい", "エラー");
                return;
            }
            



            ret[1] = WritePrivateProfileString("Tenkey", "isTab", isTabNumlock.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[2] = WritePrivateProfileString("Tenkey", "isBSUpper", isBackSpace.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[3] = WritePrivateProfileString("Tenkey", "isZeroUnion", isIntegration.Checked.ToString(), ".\\TenkeySettings.ini");
            ret[4] = WritePrivateProfileString("Tenkey", "isZeroThree", isThreeZeros.Checked.ToString(), ".\\TenkeySettings.ini");



            Application.Restart();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            // USBデバイス情報を取得するためのクエリを作成
            string query = "SELECT * FROM Win32_USBHub WHERE ConfigManagerErrorCode = 0";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection usbDevices = searcher.Get();

            foreach (ManagementObject usbDevice in usbDevices)
            {
                // クラスがUSBデバイスであるかどうかを確認


                string deviceName = usbDevice["Name"] != null ? usbDevice["Name"].ToString() : "N/A";
                string deviceId = usbDevice["DeviceID"] != null ? usbDevice["DeviceID"].ToString() : "N/A";

                KeyValuePair device = new KeyValuePair(deviceName, deviceId);
                // コンボボックスにデバイス名とデバイスIDを追加
                
                comboBox1.Items.Add(device);

            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Restart();
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
            if(T_Tab.Visible)T_Tab.Visible = false;
            else T_Tab.Visible = true;
        }

        private void T_B_BS_Click(object sender, EventArgs e)
        {

        }

        private void isIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (isIntegration.Checked)
            {
                if (isThreeZeros.Checked)T_B_ThreeZeros.Visible = true;
                else T_B_twoZeros.Visible = true;
            }
            else
            {
                if (isThreeZeros.Checked)
                {
                    T_A_ThreeZeros.Visible = true;
                    T_B_ThreeZeros.Visible = false;
                    T_B_twoZeros.Visible = false;
                }
                else
                {
                    T_A_ThreeZeros.Visible = false;
                    T_B_ThreeZeros.Visible = false;
                    T_B_twoZeros.Visible = false;
                }
            }
        }

        private void isThreeZeros_CheckedChanged(object sender, EventArgs e)
        {
            if(isThreeZeros.Checked)
            {
                if (isIntegration.Checked)
                {
                    T_B_ThreeZeros.Visible = true;
                    T_B_twoZeros.Visible = false;
                }
                else T_A_ThreeZeros.Visible = true;
            }
            else
            {
                if(isIntegration.Checked)
                {
                    T_B_ThreeZeros.Visible = false;
                    T_B_twoZeros.Visible = true;
                }
                else T_A_ThreeZeros.Visible = false;
            }
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
