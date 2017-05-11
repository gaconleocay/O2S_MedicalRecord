namespace O2S_MedicalRecord.GUI.FormCommon
{
    partial class frmConnectDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnectDB));
            this.btnDBLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDBKiemTra = new DevExpress.XtraEditors.SimpleButton();
            this.btnDBUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDBPort_HSBA = new DevExpress.XtraEditors.TextEdit();
            this.txtDBName_HSBA = new DevExpress.XtraEditors.TextEdit();
            this.txtDBPass_HSBA = new DevExpress.XtraEditors.TextEdit();
            this.txtDBUser_HSBA = new DevExpress.XtraEditors.TextEdit();
            this.txtDBHost_HSBA = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDBPort = new DevExpress.XtraEditors.TextEdit();
            this.txtDBName = new DevExpress.XtraEditors.TextEdit();
            this.txtDBPass = new DevExpress.XtraEditors.TextEdit();
            this.txtDBUser = new DevExpress.XtraEditors.TextEdit();
            this.txtDBHost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort_HSBA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName_HSBA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass_HSBA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser_HSBA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost_HSBA.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDBLuu
            // 
            this.btnDBLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBLuu.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBLuu.Appearance.Options.UseFont = true;
            this.btnDBLuu.Appearance.Options.UseForeColor = true;
            this.btnDBLuu.Image = global::O2S_MedicalRecord.Properties.Resources.save_16;
            this.btnDBLuu.Location = new System.Drawing.Point(352, 227);
            this.btnDBLuu.Name = "btnDBLuu";
            this.btnDBLuu.Size = new System.Drawing.Size(100, 40);
            this.btnDBLuu.TabIndex = 10;
            this.btnDBLuu.Text = "Lưu";
            this.btnDBLuu.Click += new System.EventHandler(this.tbnDBLuu_Click);
            // 
            // btnDBKiemTra
            // 
            this.btnDBKiemTra.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBKiemTra.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBKiemTra.Appearance.Options.UseFont = true;
            this.btnDBKiemTra.Appearance.Options.UseForeColor = true;
            this.btnDBKiemTra.Image = ((System.Drawing.Image)(resources.GetObject("btnDBKiemTra.Image")));
            this.btnDBKiemTra.Location = new System.Drawing.Point(227, 227);
            this.btnDBKiemTra.Name = "btnDBKiemTra";
            this.btnDBKiemTra.Size = new System.Drawing.Size(100, 40);
            this.btnDBKiemTra.TabIndex = 11;
            this.btnDBKiemTra.Text = "Kiểm Tra";
            this.btnDBKiemTra.Click += new System.EventHandler(this.btnDBKiemTra_Click);
            // 
            // btnDBUpdate
            // 
            this.btnDBUpdate.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDBUpdate.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDBUpdate.Appearance.Options.UseFont = true;
            this.btnDBUpdate.Appearance.Options.UseForeColor = true;
            this.btnDBUpdate.Image = global::O2S_MedicalRecord.Properties.Resources.checkmark_16;
            this.btnDBUpdate.Location = new System.Drawing.Point(476, 227);
            this.btnDBUpdate.Name = "btnDBUpdate";
            this.btnDBUpdate.Size = new System.Drawing.Size(100, 40);
            this.btnDBUpdate.TabIndex = 13;
            this.btnDBUpdate.Text = "Update DB";
            this.btnDBUpdate.Click += new System.EventHandler(this.btnDBUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDBPort_HSBA);
            this.groupBox1.Controls.Add(this.txtDBName_HSBA);
            this.groupBox1.Controls.Add(this.txtDBPass_HSBA);
            this.groupBox1.Controls.Add(this.txtDBUser_HSBA);
            this.groupBox1.Controls.Add(this.txtDBHost_HSBA);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.labelControl10);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(398, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 190);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database Hồ sơ bệnh án";
            // 
            // txtDBPort_HSBA
            // 
            this.txtDBPort_HSBA.EditValue = "";
            this.txtDBPort_HSBA.Location = new System.Drawing.Point(112, 55);
            this.txtDBPort_HSBA.Name = "txtDBPort_HSBA";
            this.txtDBPort_HSBA.Properties.AllowFocused = false;
            this.txtDBPort_HSBA.Properties.AllowMouseWheel = false;
            this.txtDBPort_HSBA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBPort_HSBA.Properties.Appearance.Options.UseFont = true;
            this.txtDBPort_HSBA.Size = new System.Drawing.Size(248, 22);
            this.txtDBPort_HSBA.TabIndex = 22;
            // 
            // txtDBName_HSBA
            // 
            this.txtDBName_HSBA.EditValue = "";
            this.txtDBName_HSBA.Location = new System.Drawing.Point(112, 149);
            this.txtDBName_HSBA.Name = "txtDBName_HSBA";
            this.txtDBName_HSBA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBName_HSBA.Properties.Appearance.Options.UseFont = true;
            this.txtDBName_HSBA.Size = new System.Drawing.Size(248, 22);
            this.txtDBName_HSBA.TabIndex = 21;
            // 
            // txtDBPass_HSBA
            // 
            this.txtDBPass_HSBA.EditValue = "";
            this.txtDBPass_HSBA.Location = new System.Drawing.Point(112, 117);
            this.txtDBPass_HSBA.Name = "txtDBPass_HSBA";
            this.txtDBPass_HSBA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBPass_HSBA.Properties.Appearance.Options.UseFont = true;
            this.txtDBPass_HSBA.Properties.PasswordChar = '*';
            this.txtDBPass_HSBA.Size = new System.Drawing.Size(248, 22);
            this.txtDBPass_HSBA.TabIndex = 20;
            // 
            // txtDBUser_HSBA
            // 
            this.txtDBUser_HSBA.EditValue = "";
            this.txtDBUser_HSBA.Location = new System.Drawing.Point(112, 85);
            this.txtDBUser_HSBA.Name = "txtDBUser_HSBA";
            this.txtDBUser_HSBA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBUser_HSBA.Properties.Appearance.Options.UseFont = true;
            this.txtDBUser_HSBA.Size = new System.Drawing.Size(248, 22);
            this.txtDBUser_HSBA.TabIndex = 19;
            // 
            // txtDBHost_HSBA
            // 
            this.txtDBHost_HSBA.EditValue = "";
            this.txtDBHost_HSBA.Location = new System.Drawing.Point(112, 25);
            this.txtDBHost_HSBA.Name = "txtDBHost_HSBA";
            this.txtDBHost_HSBA.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBHost_HSBA.Properties.Appearance.Options.UseFont = true;
            this.txtDBHost_HSBA.Size = new System.Drawing.Size(248, 22);
            this.txtDBHost_HSBA.TabIndex = 18;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl6.Location = new System.Drawing.Point(21, 152);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(84, 14);
            this.labelControl6.TabIndex = 17;
            this.labelControl6.Text = "Database name";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl7.Location = new System.Drawing.Point(50, 121);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(51, 14);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "Password";
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl8.Location = new System.Drawing.Point(74, 89);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "User";
            // 
            // labelControl9
            // 
            this.labelControl9.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl9.Location = new System.Drawing.Point(74, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(25, 14);
            this.labelControl9.TabIndex = 14;
            this.labelControl9.Text = "Host";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Location = new System.Drawing.Point(76, 59);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(23, 14);
            this.labelControl10.TabIndex = 13;
            this.labelControl10.Text = "Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDBPort);
            this.groupBox2.Controls.Add(this.txtDBName);
            this.groupBox2.Controls.Add(this.txtDBPass);
            this.groupBox2.Controls.Add(this.txtDBUser);
            this.groupBox2.Controls.Add(this.txtDBHost);
            this.groupBox2.Controls.Add(this.labelControl5);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox2.Location = new System.Drawing.Point(7, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 190);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database HIS";
            // 
            // txtDBPort
            // 
            this.txtDBPort.EditValue = "";
            this.txtDBPort.Location = new System.Drawing.Point(108, 47);
            this.txtDBPort.Name = "txtDBPort";
            this.txtDBPort.Properties.AllowFocused = false;
            this.txtDBPort.Properties.AllowMouseWheel = false;
            this.txtDBPort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBPort.Properties.Appearance.Options.UseFont = true;
            this.txtDBPort.Size = new System.Drawing.Size(248, 22);
            this.txtDBPort.TabIndex = 22;
            // 
            // txtDBName
            // 
            this.txtDBName.EditValue = "";
            this.txtDBName.Location = new System.Drawing.Point(108, 141);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBName.Properties.Appearance.Options.UseFont = true;
            this.txtDBName.Size = new System.Drawing.Size(248, 22);
            this.txtDBName.TabIndex = 21;
            // 
            // txtDBPass
            // 
            this.txtDBPass.EditValue = "";
            this.txtDBPass.Location = new System.Drawing.Point(108, 109);
            this.txtDBPass.Name = "txtDBPass";
            this.txtDBPass.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBPass.Properties.Appearance.Options.UseFont = true;
            this.txtDBPass.Properties.PasswordChar = '*';
            this.txtDBPass.Size = new System.Drawing.Size(248, 22);
            this.txtDBPass.TabIndex = 20;
            // 
            // txtDBUser
            // 
            this.txtDBUser.EditValue = "";
            this.txtDBUser.Location = new System.Drawing.Point(108, 77);
            this.txtDBUser.Name = "txtDBUser";
            this.txtDBUser.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBUser.Properties.Appearance.Options.UseFont = true;
            this.txtDBUser.Size = new System.Drawing.Size(248, 22);
            this.txtDBUser.TabIndex = 19;
            // 
            // txtDBHost
            // 
            this.txtDBHost.EditValue = "";
            this.txtDBHost.Location = new System.Drawing.Point(108, 17);
            this.txtDBHost.Name = "txtDBHost";
            this.txtDBHost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBHost.Properties.Appearance.Options.UseFont = true;
            this.txtDBHost.Size = new System.Drawing.Size(248, 22);
            this.txtDBHost.TabIndex = 18;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Location = new System.Drawing.Point(17, 144);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(84, 14);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "Database name";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(46, 113);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(51, 14);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Password";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Location = new System.Drawing.Point(70, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "User";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(70, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(25, 14);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Host";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(72, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "Port";
            // 
            // frmConnectDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 292);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDBUpdate);
            this.Controls.Add(this.btnDBKiemTra);
            this.Controls.Add(this.btnDBLuu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 330);
            this.Name = "frmConnectDB";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu Hình Cơ Sở Dữ Liệu";
            this.Load += new System.EventHandler(this.frmConnectDB_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort_HSBA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName_HSBA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass_HSBA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser_HSBA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost_HSBA.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDBHost.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnDBLuu;
        private DevExpress.XtraEditors.SimpleButton btnDBKiemTra;
        private DevExpress.XtraEditors.SimpleButton btnDBUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtDBPort_HSBA;
        private DevExpress.XtraEditors.TextEdit txtDBName_HSBA;
        private DevExpress.XtraEditors.TextEdit txtDBPass_HSBA;
        private DevExpress.XtraEditors.TextEdit txtDBUser_HSBA;
        private DevExpress.XtraEditors.TextEdit txtDBHost_HSBA;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.TextEdit txtDBPort;
        private DevExpress.XtraEditors.TextEdit txtDBName;
        private DevExpress.XtraEditors.TextEdit txtDBPass;
        private DevExpress.XtraEditors.TextEdit txtDBUser;
        private DevExpress.XtraEditors.TextEdit txtDBHost;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}