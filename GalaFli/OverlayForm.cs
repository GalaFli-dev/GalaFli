using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

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

        private const int KEYDOWN = 0x0;          // キーを押す
        private const int KEYUP = 0x2;            // キーを離す
        private const int EXTENDEDKEY = 0x1;      // 拡張コード


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

        //cmd処理に使うための配列
        List<string> cmdList = new List<string>();

        //仮想キーコードオブジェクトを入れる配列
        List<INPUT> inp = new List<INPUT>();


        public Label lblMessage;


        public OverlayForm(TenkeySettings a)
        {


            TransparencyKey = Color.Gray;
            StartPosition = FormStartPosition.Manual;

            // 画面の幅と高さを取得
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            
            Width = 330;
            Height = 240;



            Location = new Point(screenWidth - this.Width - 25, screenHeight - this.Height - 25);

            Console.WriteLine(screenWidth);
            Console.WriteLine(screenHeight);

            Console.WriteLine(screenWidth - this.Width);
            Console.WriteLine(screenHeight - this.Height);

            Console.WriteLine(this.Width);
            Console.WriteLine(this.Height);



            InitializeComponent();



            T0.Visible = !a.isZeroUnion;
            T000.Visible = !a.isZeroUnion;
            T0_another.Visible = a.isZeroUnion;
            TopMost = true;

        }


        private void OverlayForm_Shown(object sender, EventArgs e)
        {
            TopMost = true;
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
            basisState = jsonData.data[3];
            currentState = basisState;

        }


        //内部処理のメイン関数
        public void InternalProcess(string keyCode)
        {
            Console.WriteLine(keyCode);

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
                    View_ShortcutKey(cmdList, "send");
                    Send(currentState.keys[keyCodeIndex]);
                    break;

                //画面遷移の場合
                case "transition":
                    Transition(currentState.keys[keyCodeIndex]);
                    break;

                //コマンドの場合
                case "cmd":
                    cmdList.Add(currentState.keys[keyCodeIndex].value[0][0]);
                    View_ShortcutKey(cmdList, "cmd");
                    break;

                //cmd配列初期化の場合
                case "delete":
                    View_ShortcutKey(cmdList, "delete");
                    cmdList.Clear();

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
            Console.WriteLine(basisState.name);

            switch (basisState.name)
            {
                //かな状態の時は全角
                case "kana_basis":

                    //全角状態にする
                    AddInput(KEYDOWN, "hiragana");
                    AddInput(KEYUP, "hiragana");

                    break;

                //英数字、数字、Fn、spの場合半角
                case "alpha_basis":
                case "num_basis":
                case "fn_basis":
                case "special_basis":

                    //まず、全角状態にする
                    AddInput(KEYDOWN, "hiragana");
                    AddInput(KEYUP, "hiragana");

                    //次に、半角状態にする（最終的に半角）
                    AddInput(KEYDOWN, "hankaku/zenkaku");
                    AddInput(KEYUP, "hankaku/zenkaku");

                    break;

                case "cmd_basis":

                    //cmd_basisで送信処理まで来るのは送信ボタンのみ
                    //cmdList内に値が入っている場合　かつ　送信ボタンの場合
                    if (cmdList.Count > 0)
                    {
                        //キーボード操作実行用のデータに仮想キーコードを格納
                        for (int i = 0; i < cmdList.Count; i++)
                        {
                            AddInput(KEYDOWN, cmdList[i]);
                            AddInput(KEYUP, cmdList[i]);
                        }
                    }

                    break;

                //Error
                default:
                    Console.WriteLine("Error → basisState.name = {0}", basisState.name);
                    break;
            }


            //コマンドが入力状態の時（down)
            if (cmdList.Count > 0)
            {
                //down
                for (int i = 0; i < cmdList.Count; i++)
                {
                    AddInput(KEYDOWN, cmdList[i]);
                }
            }


            //キーボード操作実行用のデータに仮想キーコードを格納
            if (key.value != null)
            {
                for (int i = 0; i < key.value.Length; i++)
                {
                    for (int k = 0; k < key.value[i].Length; k++)
                    {
                        //down
                        AddInput(KEYDOWN, key.value[i][k]);
                    }

                    for (int k = key.value[i].Length; k > 0; k--)
                    {
                        //up
                        AddInput(KEYUP, key.value[i][k - 1]);
                    }
                }
            }


            //コマンドが入力状態の時（up)
            if (cmdList.Count > 0)
            {
                //up
                for (int i = 0; i < cmdList.Count; i++)
                {
                    AddInput(KEYUP, cmdList[i]);
                }
            }

            //キーボード操作実行
            if (inp.Count > 0)
            {
                INPUT[] inpArray = inp.ToArray();
                SendInput(inp.Count, ref inpArray[0], Marshal.SizeOf(inp[0]));

            }
            //初期化
            inp.Clear();
            cmdList.Clear();




            //currentState（現在の画面）を basisState（デフォルトの画面）を上書き
            currentState = basisState;

            //currentState値を基に画面を構築する
            String[] TlabelText = new string[19];

            int index = 0; // 配列のインデックス

            foreach (var key_val in currentState.keys)
            {
                TlabelText[index] = key_val.text;
                index++;
            }
            Change_Tlabel(TlabelText);

            //debug
            Console.WriteLine(string.Join(", ", TlabelText));

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
            String[] TlabelText = new string[19];

            int index = 0; // 配列のインデックス

            foreach (var key_val in currentState.keys)
            {
                TlabelText[index] = key_val.text;
                index++;
            }
            Change_Tlabel(TlabelText);

            //debug
            Console.WriteLine(string.Join(", ", TlabelText));
            Console.WriteLine("currentState.name : {0}\nbasisState.name : {1}", currentState.name, basisState.name);
        }


        //仮想キーコード配列に値を入れる関数
        public void AddInput(int keyEvent, string value)
        {
            inp.Add(new INPUT
            {
                type = INPUT_KEYBOARD,
                ki = new KEYBDINPUT
                {
                    wVk = (short)VK[value],  // 仮想キーコード
                    wScan = (short)MapVirtualKey((short)VK[value], 0),
                    dwFlags = EXTENDEDKEY | keyEvent,
                    dwExtraInfo = 0,
                    time = 0
                }
            });
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

                T0_another.Text = TlabelText[17];

            }));
        }

        public void View_ShortcutKey(List<string> cmdList, String action)
        {
            Invoke(new Action(() =>
            {
                if (action == "cmd")
                {
                    foreach (string cmd in cmdList)
                    {
                        if (cmd.Contains("ctrl"))
                        {
                            viewer_Ctrl.Visible = true;
                        }
                        if (cmd.Contains("alt"))
                        {
                            viewer_Alt.Visible = true;
                        }
                        if (cmd.Contains("shift"))
                        {
                            viewer_Shift.Visible = true;
                        }
                        if (cmd.Contains("win"))
                        {
                            viewer_Win.Visible = true;
                        }
                    }
                }
                else
                {
                    viewer_Ctrl.Visible = false;
                    viewer_Alt.Visible = false;
                    viewer_Shift.Visible = false;
                    viewer_Win.Visible = false;
                }


            }));

        }


        public void debug_post()
        {
            // 1000ミリ秒スリープ
            System.Threading.Thread.Sleep(2000);

            //受け取ったキーコード(想定）
            string keyCode = "T9";

            //内部処理起動
            InternalProcess(keyCode);

        }

        private void T_tab_Click(object sender, EventArgs e)
        {

        }

        private void T_asterisk_another_Click(object sender, EventArgs e)
        {

        }

        private void T_tab_another_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {

        }

        private void fnHint_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }



}
