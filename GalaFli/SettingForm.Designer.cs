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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ConfigureGroupBox = new System.Windows.Forms.GroupBox();
            this.isThreeZeros = new System.Windows.Forms.CheckBox();
            this.isIntegration = new System.Windows.Forms.CheckBox();
            this.isTabNumlock = new System.Windows.Forms.CheckBox();
            this.isBackSpace = new System.Windows.Forms.CheckBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ConfigureGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(233, 321);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(314, 321);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(365, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.Text = "デバイスを選択してください";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ConfigureGroupBox
            // 
            this.ConfigureGroupBox.Controls.Add(this.isThreeZeros);
            this.ConfigureGroupBox.Controls.Add(this.isIntegration);
            this.ConfigureGroupBox.Controls.Add(this.isTabNumlock);
            this.ConfigureGroupBox.Controls.Add(this.isBackSpace);
            this.ConfigureGroupBox.Controls.Add(this.buttonUpdate);
            this.ConfigureGroupBox.Controls.Add(this.textBox2);
            this.ConfigureGroupBox.Controls.Add(this.textBox1);
            this.ConfigureGroupBox.Controls.Add(this.comboBox1);
            this.ConfigureGroupBox.Location = new System.Drawing.Point(11, 12);
            this.ConfigureGroupBox.Name = "ConfigureGroupBox";
            this.ConfigureGroupBox.Size = new System.Drawing.Size(377, 303);
            this.ConfigureGroupBox.TabIndex = 5;
            this.ConfigureGroupBox.TabStop = false;
            // 
            // isThreeZeros
            // 
            this.isThreeZeros.AutoSize = true;
            this.isThreeZeros.Location = new System.Drawing.Point(6, 196);
            this.isThreeZeros.Name = "isThreeZeros";
            this.isThreeZeros.Size = new System.Drawing.Size(90, 16);
            this.isThreeZeros.TabIndex = 10;
            this.isThreeZeros.Text = "0が3つですか?";
            this.isThreeZeros.UseVisualStyleBackColor = true;
            // 
            // isIntegration
            // 
            this.isIntegration.AutoSize = true;
            this.isIntegration.Location = new System.Drawing.Point(7, 174);
            this.isIntegration.Name = "isIntegration";
            this.isIntegration.Size = new System.Drawing.Size(186, 16);
            this.isIntegration.TabIndex = 9;
            this.isIntegration.Text = "0と00・000が同じキーにありますか?";
            this.isIntegration.UseVisualStyleBackColor = true;
            // 
            // isTabNumlock
            // 
            this.isTabNumlock.AutoSize = true;
            this.isTabNumlock.Location = new System.Drawing.Point(7, 130);
            this.isTabNumlock.Name = "isTabNumlock";
            this.isTabNumlock.Size = new System.Drawing.Size(112, 16);
            this.isTabNumlock.TabIndex = 8;
            this.isTabNumlock.Text = "左上はTabですか?";
            this.isTabNumlock.UseVisualStyleBackColor = true;
            this.isTabNumlock.CheckedChanged += new System.EventHandler(this.isTabNumlock_CheckedChanged);
            // 
            // isBackSpace
            // 
            this.isBackSpace.AutoSize = true;
            this.isBackSpace.Location = new System.Drawing.Point(7, 152);
            this.isBackSpace.Name = "isBackSpace";
            this.isBackSpace.Size = new System.Drawing.Size(150, 16);
            this.isBackSpace.TabIndex = 7;
            this.isBackSpace.Text = "右上はBackSpaceですか?";
            this.isBackSpace.UseVisualStyleBackColor = true;
            this.isBackSpace.CheckedChanged += new System.EventHandler(this.checkBack_CheckedChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(296, 72);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "更新";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(6, 98);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 12);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "テンキーのタイプ";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(6, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 12);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "デバイス";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 354);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.ConfigureGroupBox);
            this.Name = "SettingForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ConfigureGroupBox.ResumeLayout(false);
            this.ConfigureGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox ConfigureGroupBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox isThreeZeros;
        private System.Windows.Forms.CheckBox isIntegration;
        private System.Windows.Forms.CheckBox isTabNumlock;
        private System.Windows.Forms.CheckBox isBackSpace;
    }
}

