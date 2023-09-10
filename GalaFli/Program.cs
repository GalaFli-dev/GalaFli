using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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


            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem1 });

            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);

            // Create the NotifyIcon.
            this.notifyIcon1 = new NotifyIcon();

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon("icon.ico");



            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Visible = true;

            notifyIcon1.Text = "NotifyIconテスト";



            // コンテキストメニュー
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "&終了";
            toolStripMenuItem.Click += menuItem1_Click;
            contextMenuStrip.Items.Add(toolStripMenuItem);

            // デバッグ用
            ContextMenuStrip DebugcontextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem DebugtoolStripMenuItem = new ToolStripMenuItem();
            DebugtoolStripMenuItem.Text = "&デバッグ";
            //DebugtoolStripMenuItem.Click += new EventHandler(debug_Click(debug_OverlayForm));
            contextMenuStrip.Items.Add(DebugtoolStripMenuItem);


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
        private void menuItem1_Click(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            Application.Exit();

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