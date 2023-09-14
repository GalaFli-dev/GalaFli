using InputInterceptorNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Timers;
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



            try
            {
                //ファイルパスの指定(自身で用意した場所のパスを書く)
                string filePath = "./all_data.json";
                Debug.WriteLine("json読み込み完了");

                //ファイルを読み込み
                string json = File.ReadAllText(filePath);

                //JSONをクラスオブジェクト(インスタンス)に変換
                var jsonDataTemp = JsonSerializer.Deserialize<Data>(json);




                // 設定ファイルの読み込みが出来るかどうかで分岐
                //読み込みが出来ない場合はSettingFormを起動する
                //読み込みが出来た場合はOverlayFormを起動する
                if (tenkeySettings.deviceId == "none")
                { //読み込みが出来ないとdeviceIdがnoneになる(コンストラクタ参照)
                    new SettingForm(new NotifyIcon()).ShowDialog();
                }
                else
                {
                    // デバッグ用(読み込み確認)
                    Debug.WriteLine(tenkeySettings.deviceId);
                    Debug.WriteLine(tenkeySettings.isTab);
                    Debug.WriteLine(tenkeySettings.isBSUpper);
                    Debug.WriteLine(tenkeySettings.isZeroUnion);
                    Debug.WriteLine(tenkeySettings.isZeroThree);

                    OverlayForm always_overlay = new OverlayForm(tenkeySettings);

                    // form1にあるJson_LoadクラスにjsonDataを渡す
                    always_overlay.Json_Load(jsonDataTemp);


                    // OverlayForm を別スレッドで実行
                    Thread overlayThread = new Thread(() =>
                    {
                        Application.Run(always_overlay);
                    });
                    overlayThread.Start();
                    // inputThread を別スレッドで実行
                    Thread inputThread = new Thread(() =>
                    {
                        Application.Run(new input_Realtime(always_overlay));
                    });




                    inputThread.Start();

                    // Tasktray をメインスレッドで実行
                    Application.Run(new Tasktray());
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

        public Tasktray()
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

            notifyIcon1.Text = "Galafli";


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
            notifyIcon1.Visible = false;

            // Close the form, which closes the application.
            Environment.Exit(0);

        }

        private void SettingForm_Click(object Sender, EventArgs e)
        {
            // @Himonooにフォーム起動のコード書いてもらう。
            new SettingForm(notifyIcon1).ShowDialog();

        }

        private EventHandler debug_Click(OverlayForm a)
        {
            a.debug_post();
            throw new NotImplementedException();

        }
    }

    public class input_Realtime : Form
    {
        //0の数を判定する際に使用する30ミリ秒のタイマー
        static System.Timers.Timer zeroKeyTimer = new System.Timers.Timer(30);
        //0が押された回数を保持する変数
        static int zeroKeyPressCount;
        //入力受付から内部処理に渡す文字列
        string sendText = "";
        //テンキー設定のインタンス
        TenkeySettings tenkeySettings = new TenkeySettings();

        OverlayForm overlayForm;
        public input_Realtime(OverlayForm a)
        {
            overlayForm = a;

            //ドライバと接続を試行し、結果によって処理を分岐
            if (!InitializeDriver())
            {
                //接続できない場合はドライバをインストール
                InstallDriver();

                //プログラムを終了
                Environment.Exit(0);
            }
            else
            {
                //デバイスIDを格納するポインタのサイズ
                const int ID_BUFFER_SIZE = 500;
                //Interceptionのドライバと接続して使用できるようにする
                IntPtr context = InputInterceptor.CreateContext();
                //すべてのキーボード入力を受け付ける(マウスは除外)
                InputInterceptor.SetFilter(context, InputInterceptor.IsKeyboard, (int)KeyboardFilter.All);

                //0の数を判定する際に使用するタイマーの設定
                zeroKeyTimer.Elapsed += ZeroKeyTimerElapsed;
                zeroKeyTimer.AutoReset = false;

                //入力を受け付けて処理する無限ループ
                while (true)
                {
                    //ここで入力を受け付ける
                    int device = InputInterceptor.Wait(context);

                    //入力されたキーコードやキーのステータスを取得
                    Stroke stroke = new Stroke();
                    InputInterceptor.Receive(context, device, ref stroke, 1);

                    //入力されたデバイスのIDを取得
                    IntPtr idBuffer = Marshal.AllocHGlobal(ID_BUFFER_SIZE);
                    uint result = InputInterceptor.GetHardwareId(context, device, idBuffer, ID_BUFFER_SIZE);
                    string id = "";

                    if (result > 0)
                    {
                        char currentChar;
                        int offset = 0;

                        do
                        {
                            currentChar = (char)Marshal.ReadByte(idBuffer, offset);

                            if (currentChar != '\0')
                            {
                                id += currentChar;
                                offset += sizeof(char);
                            }

                        } while (currentChar != '\0');

                    }

                    //指定されたテンキーの入力かつNumLock以外の入力を処理し、処理されないものはOSに再送信
                    if (id.Equals(tenkeySettings.deviceId) && stroke.Key.Code != KeyCode.NumLock)
                    {
                        if (stroke.Key.State.ToString().Contains("Up"))
                        {
                            if (stroke.Key.Code == KeyCode.Numpad0 || stroke.Key.Code == KeyCode.Insert)
                            {
                                // 0,00,000キーが押されたらタイマーをスタート
                                StartZeroKeyTimer();
                            }
                            else
                            {
                                sendText = GetSendKeyName(stroke.Key.Code.ToString());
                                //内部処理関数
                                overlayForm.InternalProcess(sendText);
                            }
                        }
                    }
                    else
                    {
                        InputInterceptor.Send(context, device, ref stroke, 1);
                    }

                    Marshal.FreeHGlobal(idBuffer);
                }
            }
        }

        //30ミリ秒のタイマー
        private static void StartZeroKeyTimer()
        {
            if (zeroKeyPressCount == 0)
            {
                zeroKeyPressCount = 1;
            }
            else
            {
                zeroKeyPressCount++;
            }

            if (zeroKeyTimer.Enabled)
            {
                zeroKeyTimer.Stop();
            }

            zeroKeyTimer.Start();
        }

        //タイマーの時間内に0が送信された数を数えてキーを判別
        private void ZeroKeyTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (zeroKeyPressCount == 1)
            {
                sendText = GetSendKeyName(KeyCode.Numpad0.ToString());
                overlayForm.InternalProcess(sendText);
            }
            else if (zeroKeyPressCount == 2)
            {
                sendText = GetSendKeyName("Numpad00");
                overlayForm.InternalProcess(sendText);
            }
            else if (zeroKeyPressCount == 3)
            {
                sendText = GetSendKeyName("Numpad000");
                overlayForm.InternalProcess(sendText);
            }

            zeroKeyPressCount = 0;
        }

        //各種テンキーに対応させるやつ
        private string GetSendKeyName(string keyCode)
        {
            //設定のiniから読み込む予定
            Dictionary<string, bool> type = new Dictionary<string, bool>()
            {
                {"isTab", tenkeySettings.isTab}, {"isBsUpper", tenkeySettings.isBSUpper}, {"isZeroUnion", tenkeySettings.isZeroUnion}, {"isZeroThree", tenkeySettings.isZeroThree}
            };

            if (keyCode.Equals(KeyCode.Tab.ToString()) && type["isTab"])
            {
                return "T_tab";
            }
            else if (keyCode.Equals(KeyCode.NumpadDivide.ToString()) || keyCode.Equals(KeyCode.Slash.ToString()))
            {
                return "T_slash";
            }
            else if (keyCode.Equals(KeyCode.NumpadAsterisk.ToString()) || keyCode.Equals(KeyCode.PrintScreen.ToString()))
            {
                return "T_asterisk";
            }
            else if ((keyCode.Equals(KeyCode.NumpadMinus.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(KeyCode.Backspace.ToString()) && type["isBsUpper"]))
            {
                return "T_minus";
            }
            else if (keyCode.Equals(KeyCode.Numpad7.ToString()) || keyCode.Equals(KeyCode.Home.ToString()))
            {
                return "T7";
            }
            else if (keyCode.Equals(KeyCode.Numpad8.ToString()) || keyCode.Equals(KeyCode.Up.ToString()))
            {
                return "T8";
            }
            else if (keyCode.Equals(KeyCode.Numpad9.ToString()) || keyCode.Equals(KeyCode.PageUp.ToString()))
            {
                return "T9";
            }
            else if ((keyCode.Equals(KeyCode.NumpadPlus.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(KeyCode.NumpadMinus.ToString()) && type["isBsUpper"]))
            {
                return "T_plus";
            }
            else if (keyCode.Equals(KeyCode.Numpad4.ToString()) || keyCode.Equals(KeyCode.Left.ToString()))
            {
                return "T4";
            }
            else if (keyCode.Equals(KeyCode.Numpad5.ToString()))
            {
                return "T5";
            }
            else if (keyCode.Equals(KeyCode.Numpad6.ToString()) || keyCode.Equals(KeyCode.Right.ToString()))
            {
                return "T6";
            }
            else if ((keyCode.Equals(KeyCode.Backspace.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(KeyCode.NumpadPlus.ToString()) && type["isBsUpper"]))
            {
                return "T_bs";
            }
            else if (keyCode.Equals(KeyCode.Numpad1.ToString()) || keyCode.Equals(KeyCode.End.ToString()))
            {
                return "T1";
            }
            else if (keyCode.Equals(KeyCode.Numpad2.ToString()) || keyCode.Equals(KeyCode.Down.ToString()))
            {
                return "T2";
            }
            else if (keyCode.Equals(KeyCode.Numpad3.ToString()) || keyCode.Equals(KeyCode.PageDown.ToString()))
            {
                return "T3";
            }
            else if (keyCode.Equals(KeyCode.NumpadEnter.ToString()) || keyCode.Equals(KeyCode.Enter.ToString()))
            {
                return "T_enter";
            }
            else if ((keyCode.Equals(KeyCode.Numpad0.ToString()) && !type["isZeroUnion"]) || (keyCode.Equals(KeyCode.Insert.ToString()) && !type["isZeroUnion"]))
            {
                return "T0";
            }
            else if ((keyCode.Equals("Numpad000") && type["isZeroThree"]) || (keyCode.Equals("Numpad00") && !type["isZeroThree"]) || (keyCode.Equals(KeyCode.Numpad0.ToString()) && type["isZeroUnion"]) || (keyCode.Equals(KeyCode.Insert.ToString()) && type["isZeroUnion"]))
            {
                return "T000";
            }
            else if (keyCode.Equals(KeyCode.Delete.ToString()))
            {
                return "T_dot";
            }
            else
            {
                return "";
            }
        }

        //ドライバへの接続をし、結果を返す
        static Boolean InitializeDriver()
        {
            if (InputInterceptor.CheckDriverInstalled())
            {
                if (InputInterceptor.Initialize())
                {
                    return true;
                }
            }
            return false;
        }

        //ドライバをインストールするやつ。管理者権限が必要
        static void InstallDriver()
        {
            MessageBox.Show("動作に必要なドライバがインストールされていないためインストールします。");
            if (InputInterceptor.CheckAdministratorRights())
            {
                if (InputInterceptor.InstallDriver())
                {
                    MessageBox.Show("ドライバのインストールが完了しました。コンピュータを再起動してください。");
                }
                else
                {
                    MessageBox.Show("ドライバのインストールに失敗しました。再試行するかソフトウェアを再インストールしてください。");
                }
            }
            else
            {
                MessageBox.Show("ドライバのインストールに失敗しました。管理者権限で再度起動してください。");
            }
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