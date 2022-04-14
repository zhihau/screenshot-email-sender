namespace LogMailSender
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bRun = new System.Windows.Forms.Button();
            this.bAbout = new System.Windows.Forms.Button();
            this.trTimer = new System.Windows.Forms.Timer(this.components);
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.lbLog = new System.Windows.Forms.ListBox();
            this.gbTimer = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudSec = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudHour = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.gbGmailSetting = new System.Windows.Forms.GroupBox();
            this.txtbToAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.gbTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).BeginInit();
            this.gbGmailSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // bRun
            // 
            this.bRun.Location = new System.Drawing.Point(343, 331);
            this.bRun.Name = "bRun";
            this.bRun.Size = new System.Drawing.Size(75, 23);
            this.bRun.TabIndex = 0;
            this.bRun.Text = "&Run";
            this.bRun.UseVisualStyleBackColor = true;
            this.bRun.Click += new System.EventHandler(this.bRun_Click);
            // 
            // bAbout
            // 
            this.bAbout.Location = new System.Drawing.Point(505, 331);
            this.bAbout.Name = "bAbout";
            this.bAbout.Size = new System.Drawing.Size(75, 23);
            this.bAbout.TabIndex = 1;
            this.bAbout.Text = "&About";
            this.bAbout.UseVisualStyleBackColor = true;
            this.bAbout.Click += new System.EventHandler(this.bAbout_Click);
            // 
            // trTimer
            // 
            this.trTimer.Tick += new System.EventHandler(this.trTimer_Tick);
            // 
            // niTray
            // 
            this.niTray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Visible = true;
            this.niTray.Click += new System.EventHandler(this.niTray_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.gbLog);
            this.panel1.Controls.Add(this.gbTimer);
            this.panel1.Controls.Add(this.gbGmailSetting);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 313);
            this.panel1.TabIndex = 2;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.lbLog);
            this.gbLog.Location = new System.Drawing.Point(10, 152);
            this.gbLog.Name = "gbLog";
            this.gbLog.Size = new System.Drawing.Size(546, 150);
            this.gbLog.TabIndex = 2;
            this.gbLog.TabStop = false;
            this.gbLog.Text = "Log";
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.HorizontalScrollbar = true;
            this.lbLog.ItemHeight = 12;
            this.lbLog.Location = new System.Drawing.Point(14, 21);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(518, 112);
            this.lbLog.TabIndex = 0;
            // 
            // gbTimer
            // 
            this.gbTimer.Controls.Add(this.label7);
            this.gbTimer.Controls.Add(this.nudSec);
            this.gbTimer.Controls.Add(this.label6);
            this.gbTimer.Controls.Add(this.nudMin);
            this.gbTimer.Controls.Add(this.label5);
            this.gbTimer.Controls.Add(this.nudHour);
            this.gbTimer.Controls.Add(this.label4);
            this.gbTimer.Location = new System.Drawing.Point(10, 95);
            this.gbTimer.Name = "gbTimer";
            this.gbTimer.Size = new System.Drawing.Size(546, 51);
            this.gbTimer.TabIndex = 1;
            this.gbTimer.TabStop = false;
            this.gbTimer.Text = "Timer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(271, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Sec";
            // 
            // nudSec
            // 
            this.nudSec.Location = new System.Drawing.Point(229, 20);
            this.nudSec.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudSec.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudSec.Name = "nudSec";
            this.nudSec.Size = new System.Drawing.Size(36, 22);
            this.nudSec.TabIndex = 5;
            this.nudSec.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Min";
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(152, 20);
            this.nudMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(36, 22);
            this.nudMin.TabIndex = 3;
            this.nudMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Hour";
            // 
            // nudHour
            // 
            this.nudHour.Location = new System.Drawing.Point(75, 20);
            this.nudHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.nudHour.Name = "nudHour";
            this.nudHour.Size = new System.Drawing.Size(36, 22);
            this.nudHour.TabIndex = 1;
            this.nudHour.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Send every:";
            // 
            // gbGmailSetting
            // 
            this.gbGmailSetting.Controls.Add(this.txtbToAddress);
            this.gbGmailSetting.Controls.Add(this.label3);
            this.gbGmailSetting.Controls.Add(this.txtbPassword);
            this.gbGmailSetting.Controls.Add(this.label2);
            this.gbGmailSetting.Controls.Add(this.txtbUserName);
            this.gbGmailSetting.Controls.Add(this.label1);
            this.gbGmailSetting.Location = new System.Drawing.Point(10, 10);
            this.gbGmailSetting.Name = "gbGmailSetting";
            this.gbGmailSetting.Size = new System.Drawing.Size(546, 79);
            this.gbGmailSetting.TabIndex = 0;
            this.gbGmailSetting.TabStop = false;
            this.gbGmailSetting.Text = "Gmail Setting";
            // 
            // txtbToAddress
            // 
            this.txtbToAddress.Location = new System.Drawing.Point(75, 47);
            this.txtbToAddress.Name = "txtbToAddress";
            this.txtbToAddress.Size = new System.Drawing.Size(180, 22);
            this.txtbToAddress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "To address:";
            // 
            // txtbPassword
            // 
            this.txtbPassword.Location = new System.Drawing.Point(352, 19);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(180, 22);
            this.txtbPassword.TabIndex = 3;
            this.txtbPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // txtbUserName
            // 
            this.txtbUserName.Location = new System.Drawing.Point(75, 19);
            this.txtbUserName.Name = "txtbUserName";
            this.txtbUserName.Size = new System.Drawing.Size(180, 22);
            this.txtbUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "User name:";
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(424, 331);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 3;
            this.bClear.Text = "&Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 366);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bAbout);
            this.Controls.Add(this.bRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Screenshot Email Sender";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.gbLog.ResumeLayout(false);
            this.gbTimer.ResumeLayout(false);
            this.gbTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHour)).EndInit();
            this.gbGmailSetting.ResumeLayout(false);
            this.gbGmailSetting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bRun;
        private System.Windows.Forms.Button bAbout;
        private System.Windows.Forms.Timer trTimer;
        private System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbGmailSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbUserName;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbToAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbTimer;
        private System.Windows.Forms.NumericUpDown nudHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudSec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.Button bClear;
    }
}

