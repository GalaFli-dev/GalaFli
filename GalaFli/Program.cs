using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Forms;


namespace GalaFli
{
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


            // OverlayForm を別スレッドで実行
            Thread overlayThread = new Thread(() =>
            {
                Application.Run(always_overlay);
            });

            Thread labelChange = new Thread(() =>
            {
                String[] a = new string[] { "NL", " ", " ", " ", " ゛", " く", " ", " ", " き", " か", " け", " BS", " ", " こ", " ", " ", " ", " ", "" };
                always_overlay.Change_Tlabel(a);
            });


            overlayThread.Start();
            //labelChange.Start();
            // Tasktray をメインスレッドで実行
            Application.Run(new Tasktray());

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
    }

}
