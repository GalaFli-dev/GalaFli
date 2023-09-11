using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Forms;


namespace GalaFli
{


    //jsonから受け取ったすべてのデータが入るクラス（構造体）
    public class Data
    {
        public Statedata[] data { get; set; }
    }

    // 画面状態のクラス（構造体）
    public class Statedata
    {
        public string name { get; set; }
        public bool basis { get; set; }
        public Key[] keys { get; set; }
    }

    // Keyのクラス（構造体）
    public class Key
    {
        public string keyname { get; set; }
        public string action { get; set; }
        public string[][] value { get; set; }
        public string text { get; set; }
    }


    // デバイスIDとデバイス名を格納するクラス（構造体）
    public class TenkeySettings
    {

        //どこでインスタンス化しても読み込めるようにここにDllImportが書いてあります
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);
        public string deviceId { get; }//デバイスID
        public bool isTab { get; set; }//タブキーが左上か
        public bool isBSUpper { get; set; }//BSキーが上にあるか
        public bool isZeroUnion { get; set; }//0キーと000キーが一体型か
        public bool isZeroThree { get; set; }//0キーと000キーが別々か
        private uint read_flag { get; set; }//読み込みが出来たかどうかのフラグ(外部から呼び出さないけど戻り値で必要なため)
        //コンストラクタで読み込みを行うためインスタンス化するだけで中身の参照ができる
        public TenkeySettings()
        {
            StringBuilder sb = new StringBuilder(256);//読み込み用のバッファ

            //デバイスIDの読み込み
            read_flag = GetPrivateProfileString("Tenkey", "deviceId", "none", sb, Convert.ToUInt32(sb.Capacity), ".\\TenkeySettings.ini");
            this.deviceId = sb.ToString();
            //isTabの読み込み
            read_flag = GetPrivateProfileString("Tenkey", "isTab", "false", sb, Convert.ToUInt32(sb.Capacity), ".\\TenkeySettings.ini");
            this.isTab = Convert.ToBoolean(sb.ToString());
            //isBSUpperの読み込み
            read_flag = GetPrivateProfileString("Tenkey", "isBSUpper", "false", sb, Convert.ToUInt32(sb.Capacity), ".\\TenkeySettings.ini");
            this.isBSUpper = Convert.ToBoolean(sb.ToString());
            //isZeroUnionの読み込み
            read_flag = GetPrivateProfileString("Tenkey", "isZeroUnion", "false", sb, Convert.ToUInt32(sb.Capacity), ".\\TenkeySettings.ini");
            this.isZeroUnion = Convert.ToBoolean(sb.ToString());
            //isZeroThreeの読み込み
            read_flag = GetPrivateProfileString("Tenkey", "isZeroThree", "false", sb, Convert.ToUInt32(sb.Capacity), ".\\TenkeySettings.ini");
            this.isZeroThree = Convert.ToBoolean(sb.ToString());
        }
    }




    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TenkeySettings tenkeySettings = new TenkeySettings();

            OverlayForm always_overlay = new OverlayForm();

            try
            {
                //ファイルパスの指定(自身で用意した場所のパスを書く)
                string filePath = "./all_data.json";
                Debug.WriteLine("json読み込み完了");

                //ファイルを読み込み
                string json = File.ReadAllText(filePath);

                //JSONをクラスオブジェクト(インスタンス)に変換
                var jsonDataTemp = JsonSerializer.Deserialize<Data>(json);

                // form1にあるJson_LoadクラスにjsonDataを渡す
                always_overlay.Json_Load(jsonDataTemp);


                // 設定ファイルの読み込みが出来るかどうかで分岐
                //読み込みが出来ない場合はSettingFormを起動する
                //読み込みが出来た場合はOverlayFormを起動する
                if (tenkeySettings.deviceId == "none")
                { //読み込みが出来ないとdeviceIdがnoneになる(コンストラクタ参照)
                    new SettingForm().ShowDialog();
                }
                else
                {
                    // デバッグ用(読み込み確認)
                    Debug.WriteLine(tenkeySettings.deviceId);
                    Debug.WriteLine(tenkeySettings.isTab);
                    Debug.WriteLine(tenkeySettings.isBSUpper);
                    Debug.WriteLine(tenkeySettings.isZeroUnion);
                    Debug.WriteLine(tenkeySettings.isZeroThree);


                    // OverlayForm を別スレッドで実行
                    Thread overlayThread = new Thread(() =>
                    {
                        Application.Run(always_overlay);
                    });



                    overlayThread.Start();
                    //labelChange.Start();

                    // Tasktray をメインスレッドで実行
                    Application.Run(new Tasktray(always_overlay));
                }


            }

            catch (Exception ex)
            {
                // エラーメッセージ表示やログ出力などのエラーハンドリングを行う
                Debug.WriteLine("Program JSONファイルの読み込みエラー: " + ex.Message);
            }
        }
    }

    public class Tasktray : System.Windows.Forms.Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.MenuItem menuItem1;


        private System.ComponentModel.IContainer components;

        public Tasktray(OverlayForm debug_OverlayForm)
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();




            // Create the NotifyIcon.
            this.notifyIcon1 = new NotifyIcon();

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon("icon.ico");



            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Visible = true;

            notifyIcon1.Text = "NotifyIconテスト";


            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();


            // 設定
            ToolStripMenuItem settingItem = new ToolStripMenuItem();
            settingItem.Text = "&設定";
            settingItem.Click += SettingForm_Click;
            contextMenuStrip.Items.Add(settingItem);

            // アプリを終了コンテキストメニュー

            ToolStripMenuItem EixtItem = new ToolStripMenuItem();
            EixtItem.Text = "&終了";
            EixtItem.Click += EixtApp_Click;
            contextMenuStrip.Items.Add(EixtItem);




            notifyIcon1.ContextMenuStrip = contextMenuStrip;


            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;



        }

        protected override void Dispose(bool disposing)
        {
            // Clean up any components being used.
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        //private void notifyIcon1_DoubleClick(object Sender, EventArgs e)
        //{
        //    // Show the form when the user double clicks on the notify icon.

        //    // Set the WindowState to normal if the form is minimized.
        //    if (this.WindowState == FormWindowState.Minimized)
        //        this.WindowState = FormWindowState.Normal;

        //    // Activate the form.
        //    this.Activate();
        //}
        private void EixtApp_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            Application.Exit();

        }

        private void SettingForm_Click(object Sender, EventArgs e)
        {
            // ハラサワくんにフォーム起動のコード書いてもらう。
            new SettingForm().ShowDialog();

        }

        private EventHandler debug_Click(OverlayForm a)
        {
            a.debug_post();
            throw new NotImplementedException();

        }
    }

}

/*
・状態変数名定義
大きく分けて二つに分ける
    ・kana
    かな文字のデフォルト:kana_basis
    かな文字のあ行      :kana_row_a

    ・alpha(alphanumeric)
    英数字のデフォルト  :alpha_basis
    英数字のa行(a,b,c)  :alpha_row_a

・上記以外の変数名定義
    ・2単語以上の場合
    1つ目の最初の文字が小文字、2つ目以降の最初の文字は大文字
*/