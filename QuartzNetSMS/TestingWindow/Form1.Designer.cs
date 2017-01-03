namespace TestingWindow
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSMS = new System.Windows.Forms.Label();
            this.picDB = new System.Windows.Forms.PictureBox();
            this.picReport = new System.Windows.Forms.PictureBox();
            this.picReceive = new System.Windows.Forms.PictureBox();
            this.picSMS = new System.Windows.Forms.PictureBox();
            this.btnTesting = new System.Windows.Forms.Button();
            this.lblFramework = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.picweb = new System.Windows.Forms.PictureBox();
            this.txtMoblie = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.picmobile = new System.Windows.Forms.PictureBox();
            this.lblerror_send = new System.Windows.Forms.Label();
            this.lblerror_receive = new System.Windows.Forms.Label();
            this.lblerror_report = new System.Windows.Forms.Label();
            this.lblerror_db = new System.Windows.Forms.Label();
            this.lblerror_web = new System.Windows.Forms.Label();
            this.lblerror_mobile = new System.Windows.Forms.Label();
            this.picwait = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReceive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picweb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picmobile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picwait)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "检测.NET Framework版本";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "检测本地数据库连接";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "检测接收短信状态接口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "检测接受短信接口";
            // 
            // lblSMS
            // 
            this.lblSMS.AutoSize = true;
            this.lblSMS.Location = new System.Drawing.Point(74, 85);
            this.lblSMS.Name = "lblSMS";
            this.lblSMS.Size = new System.Drawing.Size(101, 12);
            this.lblSMS.TabIndex = 6;
            this.lblSMS.Text = "检测发送短信接口";
            // 
            // picDB
            // 
            this.picDB.Location = new System.Drawing.Point(266, 208);
            this.picDB.Name = "picDB";
            this.picDB.Size = new System.Drawing.Size(16, 16);
            this.picDB.TabIndex = 12;
            this.picDB.TabStop = false;
            // 
            // picReport
            // 
            this.picReport.Location = new System.Drawing.Point(266, 162);
            this.picReport.Name = "picReport";
            this.picReport.Size = new System.Drawing.Size(16, 16);
            this.picReport.TabIndex = 13;
            this.picReport.TabStop = false;
            // 
            // picReceive
            // 
            this.picReceive.Location = new System.Drawing.Point(266, 122);
            this.picReceive.Name = "picReceive";
            this.picReceive.Size = new System.Drawing.Size(16, 16);
            this.picReceive.TabIndex = 14;
            this.picReceive.TabStop = false;
            // 
            // picSMS
            // 
            this.picSMS.Location = new System.Drawing.Point(266, 85);
            this.picSMS.Name = "picSMS";
            this.picSMS.Size = new System.Drawing.Size(16, 16);
            this.picSMS.TabIndex = 15;
            this.picSMS.TabStop = false;
            // 
            // btnTesting
            // 
            this.btnTesting.Location = new System.Drawing.Point(348, 452);
            this.btnTesting.Name = "btnTesting";
            this.btnTesting.Size = new System.Drawing.Size(75, 23);
            this.btnTesting.TabIndex = 11;
            this.btnTesting.Text = "检测";
            this.btnTesting.UseVisualStyleBackColor = true;
            this.btnTesting.Click += new System.EventHandler(this.btnTesting_Click);
            // 
            // lblFramework
            // 
            this.lblFramework.AutoSize = true;
            this.lblFramework.Location = new System.Drawing.Point(318, 40);
            this.lblFramework.Name = "lblFramework";
            this.lblFramework.Size = new System.Drawing.Size(23, 12);
            this.lblFramework.TabIndex = 16;
            this.lblFramework.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "检测本地webservice";
            // 
            // picweb
            // 
            this.picweb.Location = new System.Drawing.Point(266, 246);
            this.picweb.Name = "picweb";
            this.picweb.Size = new System.Drawing.Size(16, 16);
            this.picweb.TabIndex = 18;
            this.picweb.TabStop = false;
            // 
            // txtMoblie
            // 
            this.txtMoblie.Location = new System.Drawing.Point(272, 30);
            this.txtMoblie.Name = "txtMoblie";
            this.txtMoblie.Size = new System.Drawing.Size(150, 21);
            this.txtMoblie.TabIndex = 19;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(3, 70);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(101, 12);
            this.lblMobile.TabIndex = 20;
            this.lblMobile.Text = "测试发送手机短信";
            // 
            // picmobile
            // 
            this.picmobile.Location = new System.Drawing.Point(454, 63);
            this.picmobile.Name = "picmobile";
            this.picmobile.Size = new System.Drawing.Size(16, 16);
            this.picmobile.TabIndex = 21;
            this.picmobile.TabStop = false;
            // 
            // lblerror_send
            // 
            this.lblerror_send.AutoSize = true;
            this.lblerror_send.Location = new System.Drawing.Point(318, 88);
            this.lblerror_send.Name = "lblerror_send";
            this.lblerror_send.Size = new System.Drawing.Size(23, 12);
            this.lblerror_send.TabIndex = 22;
            this.lblerror_send.Text = "...";
            // 
            // lblerror_receive
            // 
            this.lblerror_receive.AutoSize = true;
            this.lblerror_receive.Location = new System.Drawing.Point(318, 126);
            this.lblerror_receive.Name = "lblerror_receive";
            this.lblerror_receive.Size = new System.Drawing.Size(23, 12);
            this.lblerror_receive.TabIndex = 22;
            this.lblerror_receive.Text = "...";
            // 
            // lblerror_report
            // 
            this.lblerror_report.AutoSize = true;
            this.lblerror_report.Location = new System.Drawing.Point(318, 166);
            this.lblerror_report.Name = "lblerror_report";
            this.lblerror_report.Size = new System.Drawing.Size(23, 12);
            this.lblerror_report.TabIndex = 22;
            this.lblerror_report.Text = "...";
            // 
            // lblerror_db
            // 
            this.lblerror_db.AutoSize = true;
            this.lblerror_db.Location = new System.Drawing.Point(318, 208);
            this.lblerror_db.Name = "lblerror_db";
            this.lblerror_db.Size = new System.Drawing.Size(23, 12);
            this.lblerror_db.TabIndex = 22;
            this.lblerror_db.Text = "...";
            // 
            // lblerror_web
            // 
            this.lblerror_web.AutoSize = true;
            this.lblerror_web.Location = new System.Drawing.Point(318, 250);
            this.lblerror_web.Name = "lblerror_web";
            this.lblerror_web.Size = new System.Drawing.Size(23, 12);
            this.lblerror_web.TabIndex = 22;
            this.lblerror_web.Text = "...";
            // 
            // lblerror_mobile
            // 
            this.lblerror_mobile.AutoSize = true;
            this.lblerror_mobile.Location = new System.Drawing.Point(488, 67);
            this.lblerror_mobile.Name = "lblerror_mobile";
            this.lblerror_mobile.Size = new System.Drawing.Size(23, 12);
            this.lblerror_mobile.TabIndex = 22;
            this.lblerror_mobile.Text = "...";
            // 
            // picwait
            // 
            this.picwait.Image = ((System.Drawing.Image)(resources.GetObject("picwait.Image")));
            this.picwait.Location = new System.Drawing.Point(138, 246);
            this.picwait.Name = "picwait";
            this.picwait.Size = new System.Drawing.Size(499, 239);
            this.picwait.TabIndex = 23;
            this.picwait.TabStop = false;
            this.picwait.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPwd);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.lblerror_mobile);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblMobile);
            this.panel1.Controls.Add(this.txtMoblie);
            this.panel1.Controls.Add(this.picmobile);
            this.panel1.Location = new System.Drawing.Point(76, 280);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 133);
            this.panel1.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "手机号";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "身份认证账号";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(165, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "身份证人密码";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(272, 60);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(150, 21);
            this.txtUser.TabIndex = 24;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(272, 87);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(150, 21);
            this.txtPwd.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 516);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picwait);
            this.Controls.Add(this.lblerror_web);
            this.Controls.Add(this.lblerror_db);
            this.Controls.Add(this.lblerror_report);
            this.Controls.Add(this.lblerror_receive);
            this.Controls.Add(this.lblerror_send);
            this.Controls.Add(this.picweb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblFramework);
            this.Controls.Add(this.picDB);
            this.Controls.Add(this.picReport);
            this.Controls.Add(this.picReceive);
            this.Controls.Add(this.picSMS);
            this.Controls.Add(this.btnTesting);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSMS);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检测环境";
            ((System.ComponentModel.ISupportInitialize)(this.picDB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReceive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picweb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picmobile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picwait)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSMS;
        private System.Windows.Forms.PictureBox picDB;
        private System.Windows.Forms.PictureBox picReport;
        private System.Windows.Forms.PictureBox picReceive;
        private System.Windows.Forms.PictureBox picSMS;
        private System.Windows.Forms.Button btnTesting;
        private System.Windows.Forms.Label lblFramework;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picweb;
        private System.Windows.Forms.TextBox txtMoblie;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.PictureBox picmobile;
        private System.Windows.Forms.Label lblerror_send;
        private System.Windows.Forms.Label lblerror_receive;
        private System.Windows.Forms.Label lblerror_report;
        private System.Windows.Forms.Label lblerror_db;
        private System.Windows.Forms.Label lblerror_web;
        private System.Windows.Forms.Label lblerror_mobile;
        private System.Windows.Forms.PictureBox picwait;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}

