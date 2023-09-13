using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Interceptor;

namespace GalaFli
{

    public partial class OverlayForm : Form
    {

        //jsonのデータがすべて入るインスタンス
        Data jsonData = new Data();
        //現在の画面状態
        Statedata currentState = new Statedata();
        //デフォルトの画面状態
        Statedata basisState = new Statedata();

        //テンキー設定のインタンス
        TenkeySettings tenkeySettings = new TenkeySettings();

        //仮想キーコードを使うための宣言↓↓
        // マウスイベント(mouse_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // キーボードイベント(keybd_eventの引数と同様のデータ)
        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public short wVk;
            public short wScan;
            public int dwFlags;
            public int time;
            public int dwExtraInfo;
        };

        // ハードウェアイベント
        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        };

        // 各種イベント(SendInputの引数データ)
        [StructLayout(LayoutKind.Explicit)]
        private struct INPUT
        {
            [FieldOffset(0)] public int type;
            [FieldOffset(4)] public MOUSEINPUT mi;
            [FieldOffset(4)] public KEYBDINPUT ki;
            [FieldOffset(4)] public HARDWAREINPUT hi;
        };


        // キー操作、マウス操作をシミュレート(擬似的に操作する)
        [DllImport("user32.dll")]
        private extern static void SendInput(
            int nInputs, ref INPUT pInputs, int cbsize);


        // 仮想キーコードをスキャンコードに変換
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyA")]
        private extern static int MapVirtualKey(
            int wCode, int wMapType);


        private const int INPUT_MOUSE = 0;                  // マウスイベント
        private const int INPUT_KEYBOARD = 1;               // キーボードイベント
        private const int INPUT_HARDWARE = 2;               // ハードウェアイベント

        private const int KEYEVENTF_KEYDOWN = 0x0;          // キーを押す
        private const int KEYEVENTF_KEYUP = 0x2;            // キーを離す
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;      // 拡張コード


        //仮想キーコードが格納されているディクショナリー
        private Dictionary<string, int> VK = new Dictionary<string, int>() {
            {"leftclick",0x01},{"rightclick",0x02},{"ctrlpause",0x03},{"middleclick",0x04},{"bs",0x08},{"tab",0x09},{"enter",0x0D},
            {"shift",0x10},{"ctrl",0x11},{"alt",0x12},{"pause",0x13},{"shiftcapslock",0x14},{"althankaku/zenkaku",0x19},{"esc",0x1B},{"conversion",0x1C},{"noconversion",0x1D},
            {"space",0x20},{"pageup",0x21},{"pagedown",0x22},{"end",0x23},{"home",0x24},{"leftarrow",0x25},{"uparrow",0x26},{"rightarrow",0x27},{"down",0x28},{"printscreen",0x2C},{"insert",0x2D},{"delete",0x2E},
            {"0",0x30},{"1",0x31},{"2",0x32},{"3",0x33},{"4",0x34},{"5",0x35},{"6",0x36},{"7",0x37},{"8",0x38},{"9",0x39},
            {"a",0x41},{"b",0x42},{"c",0x43},{"d",0x44},{"e",0x45},{"f",0x46},{"g",0x47},{"h",0x48},{"i",0x49},{"j",0x4A},{"k",0x4B},{"l",0x4C},{"m",0x4D},{"n",0x4E},{"o",0x4F},
            {"p",0x50},{"q",0x51},{"r",0x52},{"s",0x53},{"t",0x54},{"u",0x55},{"v",0x56},{"w",0x57},{"x",0x58},{"y",0x59},{"z",0x5A},{"leftwin",0x5B},{"rightwin",0x5C},{"app",0x5D},
            {"F1",0x70},{"F2",0x71},{"F3",0x72},{"F4",0x73},{"F5",0x74},{"F6",0x75},{"F7",0x76},{"F8",0x77},{"F9",0x78},{"F10",0x79},{"F11",0x7A},{"F12",0x7B},{"F13",0x7C},{"F14",0x7D},{"F15",0x7E},{"F16",0x7F},
            {"F17",0x80},{"F18",0x81},{"F19",0x82},{"F20",0x83},{"F21",0x84},{"F22",0x85},{"F23",0x86},{"F24",0x87},
            {"numlock",0x90},{"scrolllock",0x91},
            {"leftshift",0xA0},{"rightshift",0xA1},{"leftctrl",0xA2},{"rightctrl",0xA3},{"leftalt",0xA4},{"rightalt",0xA5},{"soundmute",0xAD},{"sounddown",0xAE},{"soundup",0xAF},
            {":*",0xBA},{";+",0xBB},{",<",0xBC},{"-=",0xBD},{".>",0xBE},{"/?",0xBF},
            {"@`",0xC0},
            {"[{",0xDB},{"\\|",0xDC},{"]}",0xDD},{"^~",0xDE},
            {"\\_",0xE2},
            {"capslock",0xF0},{"hiragana",0xF2 },{"hankaku/zenkaku",0xF3 },{"althiragana",0xF5},
        };

        public Label lblMessage;

        static Timer zeroKeyTimer = new Timer(30);
        static int zeroKeyPressCount;
        static string sendText;

        public OverlayForm()
        {

            TransparencyKey = Color.Gray;
            StartPosition = FormStartPosition.Manual;

            // 画面の幅と高さを取得
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;



            Location = new Point(screenWidth - this.Width - 25, screenHeight - this.Height - 25);

            Console.WriteLine(screenWidth);
            Console.WriteLine(screenHeight);

            Console.WriteLine(screenWidth - this.Width);
            Console.WriteLine(screenHeight - this.Height);

            Console.WriteLine(this.Width);
            Console.WriteLine(this.Height);

            TopMost = true;

            InitializeComponent();

            const int ID_BUFFER_SIZE = 500;
            static IntPtr context = InterceptionDriver.CreateContext();

            InterceptionDriver.SetFilter(context, InterceptionDriver.IsKeyboard, (int)KeyboardFilterMode.All);

            zeroKeyTimer.Elapsed += ZeroKeyTimerElapsed;
            zeroKeyTimer.AutoReset = false;

            while(true)
            {
                int device = InterceptionDriver.Wait(context);

                Stroke stroke = new Stroke();
                InterceptionDriver.Receive(context, device, ref stroke, 1);

                IntPtr idBuffer = Marshal.AllocHGlobal(ID_BUFFER_SIZE);
                int result = InterceptionDriver.GetHardwareId(context, device, idBuffer, ID_BUFFER_SIZE);
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

                if (id.Equals(tenkeySettings.deviceId) && stroke.Key.Code != Keys.NumLock)
                {
                    if (stroke.Key.State.ToString().Contains("Up"))
                    {
                        if (stroke.Key.Code == Keys.Numpad0 || stroke.Key.Code == Keys.Insert)
                        {
                            // 0,00,000キーが押されたらタイマーをスタート
                            StartZeroKeyTimer();
                        }
                        else
                        {
                            sendText = GetSendKeyName(stroke.Key.Code.ToString());
                            //内部処理関数
                            Internalprocess(sendText);
                        }
                    }
                }
                else
                {
                    InterceptionDriver.Send(context, device, ref stroke, 1);
                }

                Marshal.FreeHGlobal(idBuffer);
            }

            InterceptionDriver.DestroyContext(context);
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
        private static void ZeroKeyTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (zeroKeyPressCount == 1)
            {
                sendText = GetSendKeyName(Keys.Numpad0.ToString());
                Internalprocess(sendText);
            }
            else if (zeroKeyPressCount == 2)
            {
                sendText = GetSendKeyName("Numpad00");
                Internalprocess(sendText);
            }
            else if (zeroKeyPressCount == 3)
            {
                sendText = GetSendKeyName("Numpad000");
                Internalprocess(sendText);
            }

            zeroKeyPressCount = 0;
        }

        //各種テンキーに対応させるやつ
        private static string GetSendKeyName(string keyCode)
        {
            //設定のiniから読み込む予定
            Dictionary<string, bool> type = new Dictionary<string, bool>()
            {
                {"isTab", tenkeySettings.isTab}, {"isBsUpper", tenkeySettings.isBSUpper}, {"isZeroUnion", tenkeySettings.isZeroUnion}, {"isZeroThree", tenkeySettings.isZeroThree}
            };

            if (keyCode.Equals(Keys.Tab.ToString()) && type["isTab"])
            {
                return "T_tab";
            }
            else if (keyCode.Equals(Keys.NumpadDivide.ToString()) || keyCode.Equals(Keys.ForwardSlashQuestionMark.ToString()))
            {
                return "T_slash";
            }
            else if (keyCode.Equals(Keys.NumpadAsterisk.ToString()) || keyCode.Equals(Keys.PrintScreen.ToString()))
            {
                return "T_asterisk";
            }
            else if ((keyCode.Equals(Keys.NumpadMinus.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(Keys.Backspace.ToString()) && type["isBsUpper"]))
            {
                return "T_minus";
            }
            else if (keyCode.Equals(Keys.Numpad7.ToString()) || keyCode.Equals(Keys.Home.ToString()))
            {
                return "T7";
            }
            else if (keyCode.Equals(Keys.Numpad8.ToString()) || keyCode.Equals(Keys.Up.ToString()))
            {
                return "T8";
            }
            else if (keyCode.Equals(Keys.Numpad9.ToString()) || keyCode.Equals(Keys.PageUp.ToString()))
            {
                return "T9";
            }
            else if ((keyCode.Equals(Keys.NumpadPlus.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(Keys.NumpadMinus.ToString()) && type["isBsUpper"]))
            {
                return "T_plus";
            }
            else if (keyCode.Equals(Keys.Numpad4.ToString()) || keyCode.Equals(Keys.Left.ToString()))
            {
                return "T4";
            }
            else if (keyCode.Equals(Keys.Numpad5.ToString()))
            {
                return "T5";
            }
            else if (keyCode.Equals(Keys.Numpad6.ToString()) || keyCode.Equals(Keys.Right.ToString()))
            {
                return "T6";
            }
            else if ((keyCode.Equals(Keys.Backspace.ToString()) && !type["isBsUpper"]) || (keyCode.Equals(Keys.NumpadPlus.ToString()) && type["isBsUpper"]))
            {
                return "T_bs";
            }
            else if (keyCode.Equals(Keys.Numpad1.ToString()) || keyCode.Equals(Keys.End.ToString()))
            {
                return "T1";
            }
            else if (keyCode.Equals(Keys.Numpad2.ToString()) || keyCode.Equals(Keys.Down.ToString()))
            {
                return "T2";
            }
            else if (keyCode.Equals(Keys.Numpad3.ToString()) || keyCode.Equals(Keys.PageDown.ToString()))
            {
                return "T3";
            }
            else if (keyCode.Equals(Keys.NumpadEnter.ToString()) || keyCode.Equals(Keys.Enter.ToString()))
            {
                return "T_enter";
            }
            else if ((keyCode.Equals(Keys.Numpad0.ToString()) && !type["isZeroUnion"]) || (keyCode.Equals(Keys.Insert.ToString()) && !type["isZeroUnion"]))
            {
                return "T0";
            }
            else if ((keyCode.Equals("Numpad000") && type["isZeroThree"]) || (keyCode.Equals("Numpad00") && !type["isZeroThree"]) || (keyCode.Equals(Keys.Numpad0.ToString()) && type["isZeroUnion"]) || (keyCode.Equals(Keys.Insert.ToString()) && type["isZeroUnion"]))
            {
                return "T000";
            }
            else if (keyCode.Equals(Keys.Delete.ToString()))
            {
                return "T_dot";
            }
            else
            {
                return "";
            }
        }

        protected override CreateParams CreateParams //クリック透過してくれるやつ
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        public void CloseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => this.Close()));
            }
            else
            {
                this.Close();
            }
        }


        public void Json_Load(GalaFli.Data jsonDataTemp)
        {
            try
            {
                //jsonDataTempにあるJSONをjsonData格納
                jsonData = jsonDataTemp;

            }
            catch (Exception ex)
            {
                // エラーメッセージ表示やログ出力などのエラーハンドリングを行う
                Debug.WriteLine("Form JSONファイルの読み込みエラー: " + ex.Message);
            }

            //初期値設定
            basisState = jsonData.data[0];
            currentState = basisState;

        }


        //内部処理のメイン関数
        public void Internalprocess(string keyCode)
        {

            //keysの中から、受け取ったkeyの場所を探し、その添え字を格納する変数
            int keyCodeIndex = 0;

            //keyの添え字を探す（ここは辞書型とかであらかじめ添え字の位置を特定していればいらないかもしれない。T7 : 0 みたいに）
            for (int i = 0; i < currentState.keys.Length; i++)
            {
                if (currentState.keys[i].keyname == keyCode)
                {
                    keyCodeIndex = i;
                    break;
                }
            }

            Console.WriteLine(currentState.keys[keyCodeIndex].action);
            //keyのactionの値によって処理を変える
            switch (currentState.keys[keyCodeIndex].action)
            {
                //送信の場合
                case "send":
                    Send(currentState.keys[keyCodeIndex]);
                    break;

                //画面遷移の場合
                case "transition":
                    Transition(currentState.keys[keyCodeIndex]);
                    break;

                //値がNULL（何もしない）場合
                case null:
                    break;

                //エラーの場合
                default:
                    Console.WriteLine("Error → currentState.keys[keyCodeIndex].action = {0}", currentState.keys[keyCodeIndex].action);
                    break;
            }

        }


        //送信関数
        public void Send(Key key)
        {
            //debug
            string temp = "";
            for (int i = 0; i < key.value.Length; i++)
            {
                for (int k = 0; k < key.value[i].Length; k++)
                {
                    temp += key.value[i][k];
                }
            }
            Console.WriteLine("送信成功");



            //全角半角を変更

            // キーボード操作実行用のデータ（全角半角）
            INPUT[] inp_ime;
            //命令要素数変数（全角半角）
            int num_ime;


            switch (basisState.name)
            {
                //かな状態の時は全角
                case "kana_basis":
                    num_ime = 2;
                    inp_ime = new INPUT[num_ime];

                    //全角状態にする
                    inp_ime[0].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[0].ki.wVk = (short)VK["hiragana"];
                    inp_ime[0].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[0].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp_ime[0].ki.dwExtraInfo = 0;
                    inp_ime[0].ki.time = 0;

                    inp_ime[1].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[1].ki.wVk = (short)VK["hiragana"];
                    inp_ime[1].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[1].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp_ime[1].ki.dwExtraInfo = 0;
                    inp_ime[1].ki.time = 0;

                    break;

                //英数字状態の時は半角
                case "alpha_basis":
                    num_ime = 4;
                    inp_ime = new INPUT[num_ime];

                    //半角状態にする

                    //まず、全角状態にする
                    inp_ime[0].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[0].ki.wVk = (short)VK["hiragana"];
                    inp_ime[0].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[0].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp_ime[0].ki.dwExtraInfo = 0;
                    inp_ime[0].ki.time = 0;

                    inp_ime[1].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[1].ki.wVk = (short)VK["hiragana"];
                    inp_ime[1].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[1].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp_ime[1].ki.dwExtraInfo = 0;
                    inp_ime[1].ki.time = 0;

                    //次に、半角状態にする
                    inp_ime[2].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[2].ki.wVk = (short)VK["hankaku/zenkaku"];
                    inp_ime[2].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[2].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp_ime[2].ki.dwExtraInfo = 0;
                    inp_ime[2].ki.time = 0;

                    inp_ime[3].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[3].ki.wVk = (short)VK["hankaku/zenkaku"];
                    inp_ime[3].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[3].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp_ime[3].ki.dwExtraInfo = 0;
                    inp_ime[3].ki.time = 0;

                    break;

                //エラーの場合は半角
                default:

                    Console.WriteLine("Error → basisState.name = {0}", basisState.name);

                    num_ime = 4;
                    inp_ime = new INPUT[num_ime];

                    //半角状態にする

                    //まず、全角状態にする
                    inp_ime[0].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[0].ki.wVk = (short)VK["hiragana"];
                    inp_ime[0].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[0].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp_ime[0].ki.dwExtraInfo = 0;
                    inp_ime[0].ki.time = 0;

                    inp_ime[1].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[1].ki.wVk = (short)VK["hiragana"];
                    inp_ime[1].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[1].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp_ime[1].ki.dwExtraInfo = 0;
                    inp_ime[1].ki.time = 0;

                    //次に、半角状態にする
                    inp_ime[2].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[2].ki.wVk = (short)VK["hankaku/zenkaku"];
                    inp_ime[2].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[2].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp_ime[2].ki.dwExtraInfo = 0;
                    inp_ime[2].ki.time = 0;

                    inp_ime[3].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][0]の値で辞書型VKから16進数を持ってきている
                    inp_ime[3].ki.wVk = (short)VK["hankaku/zenkaku"];
                    inp_ime[3].ki.wScan = (short)MapVirtualKey(inp_ime[0].ki.wVk, 0);
                    inp_ime[3].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp_ime[3].ki.dwExtraInfo = 0;
                    inp_ime[3].ki.time = 0;

                    break;
            }


            // キーボード操作実行（全角半角）
            SendInput(num_ime, ref inp_ime[0], Marshal.SizeOf(inp_ime[0]));



            //value内のデータを送信する

            // キーボード操作実行用のデータ
            INPUT[] inp;
            //命令要素数変数
            int num = 0;

            //配列の全要素数取得
            for (int i = 0; i < key.value.Length; i++)
            {
                for (int k = 0; k < key.value[i].Length; k++)
                {
                    num++;
                }
            }

            //DownとUpがあるため2倍
            num *= 2;

            inp = new INPUT[num];

            //キーボード操作実行用のデータの添え字用変数
            int index = 0;


            //キーボード操作実行用のデータに仮想キーコードを格納
            for (int i = 0; i < key.value.Length; i++)
            {
                for (int k = 0; k < key.value[i].Length; k++, index++)
                {
                    //down

                    inp[index].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][k]の値で辞書型VKから16進数を持ってきている
                    inp[index].ki.wVk = (short)VK[key.value[i][k]];
                    inp[index].ki.wScan = (short)MapVirtualKey(inp[0].ki.wVk, 0);
                    inp[index].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYDOWN;
                    inp[index].ki.dwExtraInfo = 0;
                    inp[index].ki.time = 0;
                }

                for (int k = key.value[i].Length; k > 0; k--, index++)
                {
                    //up

                    inp[index].type = INPUT_KEYBOARD;
                    //ここに仮想キーコードを入れる
                    //key.value[i][k-1]の値で辞書型VKから16進数を持ってきている
                    inp[index].ki.wVk = (short)VK[key.value[i][k - 1]];
                    inp[index].ki.wScan = (short)MapVirtualKey(inp[0].ki.wVk, 0);
                    inp[index].ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;
                    inp[index].ki.dwExtraInfo = 0;
                    inp[index].ki.time = 0;
                }
            }



            // キーボード操作実行
            SendInput(num, ref inp[0], Marshal.SizeOf(inp[0]));




            //currentState（現在の画面）を basisState（デフォルトの画面）を上書き
            currentState = basisState;

            //currentState値を基にデフォルト画面を構築する（ここは木下）

            //debug
            Console.WriteLine("currentState.name : {0}\nbasisState.name : {1}", currentState.name, basisState.name);
        }


        //画面遷移関数
        public void Transition(Key key)
        {
            //debug
            Console.WriteLine("遷移成功");


            //実際の処理

            //まず、currentState値を変更
            for (int i = 0; i < jsonData.data.Length; i++)
            {
                if (jsonData.data[i].name == key.value[0][0])
                {
                    currentState = jsonData.data[i];
                    break;
                }
            }

            //currentState（現在の画面）がデフォルト画面の場合、basisState（デフォルトの画面）を上書き
            if (currentState.basis)
            {
                basisState = currentState;
            }

            //currentState値を基に画面を構築する
            String[] GUIsend = new string[19];

            int index = 0; // 配列のインデックス

            foreach (var key_val in currentState.keys)
            {
                GUIsend[index] = key_val.text;
                index++;
            }
            Change_Tlabel(GUIsend);

            //debug
            Console.WriteLine(string.Join(", ", GUIsend));
            Console.WriteLine("currentState.name : {0}\nbasisState.name : {1}", currentState.name, basisState.name);
        }

        public void Change_Tlabel(String[] TlabelText)
        {
            Invoke(new Action(() =>
            {
                T_tab.Text = TlabelText[0];
                T_slash.Text = TlabelText[1];
                T_asterisk.Text = TlabelText[2];
                T_minus.Text = TlabelText[3];
                T7.Text = TlabelText[4];
                T8.Text = TlabelText[5];
                T9.Text = TlabelText[6];
                T_plus.Text = TlabelText[7];
                T4.Text = TlabelText[8];
                T5.Text = TlabelText[9];
                T6.Text = TlabelText[10];
                T_bs.Text = TlabelText[11];
                T1.Text = TlabelText[12];
                T2.Text = TlabelText[13];
                T3.Text = TlabelText[14];
                T_enter.Text = TlabelText[15];
                T0.Text = TlabelText[16];
                T000.Text = TlabelText[17];
                T_dot.Text = TlabelText[18];
            }));
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {

        }

        public void debug_post()
        {
            // 1000ミリ秒スリープ
            System.Threading.Thread.Sleep(2000);

            //受け取ったキーコード(想定）
            string keyCode = "T9";

            //内部処理起動
            Internalprocess(keyCode);

        }
    }



}
