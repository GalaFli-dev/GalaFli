namespace GalaFli
{
    partial class SettingForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.ItemBox = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.isBackSpace = new System.Windows.Forms.CheckBox();
            this.isTabNumlock = new System.Windows.Forms.CheckBox();
            this.isIntegration = new System.Windows.Forms.CheckBox();
            this.isThreeZeros = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.T_Num = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ConfigureGroupBox = new System.Windows.Forms.GroupBox();
            this.T_A_ThreeZeros = new System.Windows.Forms.Label();
            this.T_B_Plus = new System.Windows.Forms.Label();
            this.T_B_BS = new System.Windows.Forms.Label();
            this.T_Tab = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.T_A_Zero = new System.Windows.Forms.Label();
            this.T_A_TwoZeros = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.T_A_Plus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.T_A_BS = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.T_B_UnionZeros = new System.Windows.Forms.Label();
            this.ConfigureGroupBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(388, 555);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(125, 34);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(523, 555);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(125, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ItemBox
            // 
            this.ItemBox.FormattingEnabled = true;
            this.ItemBox.Location = new System.Drawing.Point(10, 69);
            this.ItemBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ItemBox.Name = "ItemBox";
            this.ItemBox.Size = new System.Drawing.Size(606, 26);
            this.ItemBox.TabIndex = 2;
            this.ItemBox.Text = "デバイスを選択してください";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(10, 42);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 18);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "デバイス";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(10, 147);
            this.textBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(167, 18);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "テンキーのタイプ";
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(493, 108);
            this.buttonUpdate.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(125, 34);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "更新";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // isBackSpace
            // 
            this.isBackSpace.AutoSize = true;
            this.isBackSpace.Location = new System.Drawing.Point(10, 228);
            this.isBackSpace.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isBackSpace.Name = "isBackSpace";
            this.isBackSpace.Size = new System.Drawing.Size(222, 22);
            this.isBackSpace.TabIndex = 7;
            this.isBackSpace.Text = "右上はBackSpaceですか?";
            this.isBackSpace.UseVisualStyleBackColor = true;
            this.isBackSpace.CheckedChanged += new System.EventHandler(this.checkBack_CheckedChanged);
            // 
            // isTabNumlock
            // 
            this.isTabNumlock.AutoSize = true;
            this.isTabNumlock.Location = new System.Drawing.Point(10, 195);
            this.isTabNumlock.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isTabNumlock.Name = "isTabNumlock";
            this.isTabNumlock.Size = new System.Drawing.Size(167, 22);
            this.isTabNumlock.TabIndex = 8;
            this.isTabNumlock.Text = "左上はTabですか?";
            this.isTabNumlock.UseVisualStyleBackColor = true;
            this.isTabNumlock.CheckedChanged += new System.EventHandler(this.isTabNumlock_CheckedChanged);
            // 
            // isIntegration
            // 
            this.isIntegration.AutoSize = true;
            this.isIntegration.Location = new System.Drawing.Point(10, 261);
            this.isIntegration.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isIntegration.Name = "isIntegration";
            this.isIntegration.Size = new System.Drawing.Size(237, 22);
            this.isIntegration.TabIndex = 9;
            this.isIntegration.Text = "0と00・000は同じキーですか?";
            this.isIntegration.UseVisualStyleBackColor = true;
            this.isIntegration.CheckedChanged += new System.EventHandler(this.isIntegration_CheckedChanged);
            // 
            // isThreeZeros
            // 
            this.isThreeZeros.AutoSize = true;
            this.isThreeZeros.Enabled = false;
            this.isThreeZeros.Location = new System.Drawing.Point(8, 294);
            this.isThreeZeros.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isThreeZeros.Name = "isThreeZeros";
            this.isThreeZeros.Size = new System.Drawing.Size(134, 22);
            this.isThreeZeros.TabIndex = 10;
            this.isThreeZeros.Text = "0が3つですか?";
            this.isThreeZeros.UseVisualStyleBackColor = true;
            this.isThreeZeros.CheckedChanged += new System.EventHandler(this.isThreeZeros_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(287, 147);
            this.textBox3.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(167, 18);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "プレビュー";
            // 
            // T_Num
            // 
            this.T_Num.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_Num.BackColor = System.Drawing.SystemColors.Control;
            this.T_Num.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_Num.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_Num.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_Num.Location = new System.Drawing.Point(328, 195);
            this.T_Num.Margin = new System.Windows.Forms.Padding(3);
            this.T_Num.Name = "T_Num";
            this.T_Num.Size = new System.Drawing.Size(60, 48);
            this.T_Num.TabIndex = 23;
            this.T_Num.Text = "Num";
            this.T_Num.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label16.Location = new System.Drawing.Point(393, 195);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 48);
            this.label16.TabIndex = 38;
            this.label16.Text = "/";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfigureGroupBox
            // 
            this.ConfigureGroupBox.Controls.Add(this.T_A_ThreeZeros);
            this.ConfigureGroupBox.Controls.Add(this.T_B_Plus);
            this.ConfigureGroupBox.Controls.Add(this.T_B_BS);
            this.ConfigureGroupBox.Controls.Add(this.T_Tab);
            this.ConfigureGroupBox.Controls.Add(this.label19);
            this.ConfigureGroupBox.Controls.Add(this.label18);
            this.ConfigureGroupBox.Controls.Add(this.label17);
            this.ConfigureGroupBox.Controls.Add(this.label15);
            this.ConfigureGroupBox.Controls.Add(this.T_A_Zero);
            this.ConfigureGroupBox.Controls.Add(this.T_A_TwoZeros);
            this.ConfigureGroupBox.Controls.Add(this.label12);
            this.ConfigureGroupBox.Controls.Add(this.T_A_Plus);
            this.ConfigureGroupBox.Controls.Add(this.label9);
            this.ConfigureGroupBox.Controls.Add(this.label8);
            this.ConfigureGroupBox.Controls.Add(this.label7);
            this.ConfigureGroupBox.Controls.Add(this.T_A_BS);
            this.ConfigureGroupBox.Controls.Add(this.label5);
            this.ConfigureGroupBox.Controls.Add(this.label4);
            this.ConfigureGroupBox.Controls.Add(this.label3);
            this.ConfigureGroupBox.Controls.Add(this.label2);
            this.ConfigureGroupBox.Controls.Add(this.label1);
            this.ConfigureGroupBox.Controls.Add(this.label16);
            this.ConfigureGroupBox.Controls.Add(this.T_Num);
            this.ConfigureGroupBox.Controls.Add(this.textBox3);
            this.ConfigureGroupBox.Controls.Add(this.isThreeZeros);
            this.ConfigureGroupBox.Controls.Add(this.isIntegration);
            this.ConfigureGroupBox.Controls.Add(this.isTabNumlock);
            this.ConfigureGroupBox.Controls.Add(this.isBackSpace);
            this.ConfigureGroupBox.Controls.Add(this.buttonUpdate);
            this.ConfigureGroupBox.Controls.Add(this.textBox2);
            this.ConfigureGroupBox.Controls.Add(this.textBox1);
            this.ConfigureGroupBox.Controls.Add(this.ItemBox);
            this.ConfigureGroupBox.Controls.Add(this.panel1);
            this.ConfigureGroupBox.Location = new System.Drawing.Point(18, 18);
            this.ConfigureGroupBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ConfigureGroupBox.Name = "ConfigureGroupBox";
            this.ConfigureGroupBox.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ConfigureGroupBox.Size = new System.Drawing.Size(630, 528);
            this.ConfigureGroupBox.TabIndex = 5;
            this.ConfigureGroupBox.TabStop = false;
            // 
            // T_A_ThreeZeros
            // 
            this.T_A_ThreeZeros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_A_ThreeZeros.BackColor = System.Drawing.SystemColors.Control;
            this.T_A_ThreeZeros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_A_ThreeZeros.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_A_ThreeZeros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_A_ThreeZeros.Location = new System.Drawing.Point(393, 414);
            this.T_A_ThreeZeros.Margin = new System.Windows.Forms.Padding(3);
            this.T_A_ThreeZeros.Name = "T_A_ThreeZeros";
            this.T_A_ThreeZeros.Size = new System.Drawing.Size(60, 48);
            this.T_A_ThreeZeros.TabIndex = 59;
            this.T_A_ThreeZeros.Text = "000";
            this.T_A_ThreeZeros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.T_A_ThreeZeros.Visible = false;
            // 
            // T_B_Plus
            // 
            this.T_B_Plus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_B_Plus.BackColor = System.Drawing.SystemColors.Control;
            this.T_B_Plus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_B_Plus.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_B_Plus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_B_Plus.Location = new System.Drawing.Point(523, 306);
            this.T_B_Plus.Margin = new System.Windows.Forms.Padding(3);
            this.T_B_Plus.Name = "T_B_Plus";
            this.T_B_Plus.Size = new System.Drawing.Size(60, 48);
            this.T_B_Plus.TabIndex = 58;
            this.T_B_Plus.Text = "+";
            this.T_B_Plus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.T_B_Plus.Visible = false;
            // 
            // T_B_BS
            // 
            this.T_B_BS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_B_BS.BackColor = System.Drawing.SystemColors.Control;
            this.T_B_BS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_B_BS.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_B_BS.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_B_BS.Location = new System.Drawing.Point(523, 195);
            this.T_B_BS.Margin = new System.Windows.Forms.Padding(3);
            this.T_B_BS.Name = "T_B_BS";
            this.T_B_BS.Size = new System.Drawing.Size(60, 48);
            this.T_B_BS.TabIndex = 57;
            this.T_B_BS.Text = "BS";
            this.T_B_BS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.T_B_BS.Visible = false;
            // 
            // T_Tab
            // 
            this.T_Tab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_Tab.BackColor = System.Drawing.SystemColors.Control;
            this.T_Tab.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_Tab.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_Tab.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_Tab.Location = new System.Drawing.Point(328, 195);
            this.T_Tab.Margin = new System.Windows.Forms.Padding(3);
            this.T_Tab.Name = "T_Tab";
            this.T_Tab.Size = new System.Drawing.Size(60, 48);
            this.T_Tab.TabIndex = 56;
            this.T_Tab.Text = "Tab";
            this.T_Tab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.T_Tab.Visible = false;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label19.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label19.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label19.Location = new System.Drawing.Point(523, 360);
            this.label19.Margin = new System.Windows.Forms.Padding(3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(60, 102);
            this.label19.TabIndex = 55;
            this.label19.Text = "En";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label18.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label18.Location = new System.Drawing.Point(458, 414);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 48);
            this.label18.TabIndex = 54;
            this.label18.Text = ".";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label17.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label17.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label17.Location = new System.Drawing.Point(393, 360);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(60, 48);
            this.label17.TabIndex = 53;
            this.label17.Text = "2";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label15.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label15.Location = new System.Drawing.Point(458, 360);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 48);
            this.label15.TabIndex = 52;
            this.label15.Text = "3";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // T_A_Zero
            // 
            this.T_A_Zero.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_A_Zero.BackColor = System.Drawing.SystemColors.Control;
            this.T_A_Zero.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_A_Zero.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_A_Zero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_A_Zero.Location = new System.Drawing.Point(328, 414);
            this.T_A_Zero.Margin = new System.Windows.Forms.Padding(3);
            this.T_A_Zero.Name = "T_A_Zero";
            this.T_A_Zero.Size = new System.Drawing.Size(60, 48);
            this.T_A_Zero.TabIndex = 51;
            this.T_A_Zero.Text = "0";
            this.T_A_Zero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // T_A_TwoZeros
            // 
            this.T_A_TwoZeros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_A_TwoZeros.BackColor = System.Drawing.SystemColors.Control;
            this.T_A_TwoZeros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_A_TwoZeros.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_A_TwoZeros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_A_TwoZeros.Location = new System.Drawing.Point(393, 414);
            this.T_A_TwoZeros.Margin = new System.Windows.Forms.Padding(3);
            this.T_A_TwoZeros.Name = "T_A_TwoZeros";
            this.T_A_TwoZeros.Size = new System.Drawing.Size(60, 48);
            this.T_A_TwoZeros.TabIndex = 50;
            this.T_A_TwoZeros.Text = "00";
            this.T_A_TwoZeros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.Location = new System.Drawing.Point(458, 195);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 48);
            this.label12.TabIndex = 49;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // T_A_Plus
            // 
            this.T_A_Plus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_A_Plus.BackColor = System.Drawing.SystemColors.Control;
            this.T_A_Plus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_A_Plus.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_A_Plus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_A_Plus.Location = new System.Drawing.Point(523, 195);
            this.T_A_Plus.Margin = new System.Windows.Forms.Padding(3);
            this.T_A_Plus.Name = "T_A_Plus";
            this.T_A_Plus.Size = new System.Drawing.Size(60, 48);
            this.T_A_Plus.TabIndex = 48;
            this.T_A_Plus.Text = "+";
            this.T_A_Plus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label9.Location = new System.Drawing.Point(523, 252);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 48);
            this.label9.TabIndex = 47;
            this.label9.Text = "-";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(458, 252);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 48);
            this.label8.TabIndex = 46;
            this.label8.Text = "9";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(393, 252);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 48);
            this.label7.TabIndex = 45;
            this.label7.Text = "8";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // T_A_BS
            // 
            this.T_A_BS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_A_BS.BackColor = System.Drawing.SystemColors.Control;
            this.T_A_BS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_A_BS.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_A_BS.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_A_BS.Location = new System.Drawing.Point(523, 306);
            this.T_A_BS.Margin = new System.Windows.Forms.Padding(3);
            this.T_A_BS.Name = "T_A_BS";
            this.T_A_BS.Size = new System.Drawing.Size(60, 48);
            this.T_A_BS.TabIndex = 44;
            this.T_A_BS.Text = "BS";
            this.T_A_BS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(458, 306);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 48);
            this.label5.TabIndex = 43;
            this.label5.Text = "6";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(393, 306);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 48);
            this.label4.TabIndex = 42;
            this.label4.Text = "5";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(328, 306);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 48);
            this.label3.TabIndex = 41;
            this.label3.Text = "4";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(328, 252);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 48);
            this.label2.TabIndex = 40;
            this.label2.Text = "7";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(328, 360);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 48);
            this.label1.TabIndex = 39;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.T_B_UnionZeros);
            this.panel1.Location = new System.Drawing.Point(287, 176);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 322);
            this.panel1.TabIndex = 62;
            // 
            // T_B_UnionZeros
            // 
            this.T_B_UnionZeros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T_B_UnionZeros.BackColor = System.Drawing.SystemColors.Control;
            this.T_B_UnionZeros.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.T_B_UnionZeros.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.T_B_UnionZeros.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.T_B_UnionZeros.Location = new System.Drawing.Point(43, 238);
            this.T_B_UnionZeros.Margin = new System.Windows.Forms.Padding(3);
            this.T_B_UnionZeros.Name = "T_B_UnionZeros";
            this.T_B_UnionZeros.Size = new System.Drawing.Size(123, 48);
            this.T_B_UnionZeros.TabIndex = 61;
            this.T_B_UnionZeros.Text = "0";
            this.T_B_UnionZeros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.T_B_UnionZeros.Visible = false;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 604);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.ConfigureGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(690, 660);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(690, 660);
            this.Name = "SettingForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ConfigureGroupBox.ResumeLayout(false);
            this.ConfigureGroupBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ComboBox ItemBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox isBackSpace;
        private System.Windows.Forms.CheckBox isTabNumlock;
        private System.Windows.Forms.CheckBox isIntegration;
        private System.Windows.Forms.CheckBox isThreeZeros;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label T_Num;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox ConfigureGroupBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label T_A_Zero;
        private System.Windows.Forms.Label T_A_TwoZeros;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label T_A_Plus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label T_A_BS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label T_Tab;
        private System.Windows.Forms.Label T_B_BS;
        private System.Windows.Forms.Label T_B_Plus;
        private System.Windows.Forms.Label T_B_UnionZeros;
        private System.Windows.Forms.Label T_A_ThreeZeros;
        private System.Windows.Forms.Panel panel1;
    }
}

