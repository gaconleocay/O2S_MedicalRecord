namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    partial class frmChonLoaiBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChonLoaiBenhAn));
            this.btnChonTaoBenhAn = new DevExpress.XtraEditors.SimpleButton();
            this.cboMauBenhAn = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.cboMauBenhAn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChonTaoBenhAn
            // 
            this.btnChonTaoBenhAn.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChonTaoBenhAn.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnChonTaoBenhAn.Appearance.Options.UseFont = true;
            this.btnChonTaoBenhAn.Appearance.Options.UseForeColor = true;
            this.btnChonTaoBenhAn.Image = global::O2S_MedicalRecord.Properties.Resources.plus_2_16;
            this.btnChonTaoBenhAn.Location = new System.Drawing.Point(210, 101);
            this.btnChonTaoBenhAn.Name = "btnChonTaoBenhAn";
            this.btnChonTaoBenhAn.Size = new System.Drawing.Size(100, 30);
            this.btnChonTaoBenhAn.TabIndex = 79;
            this.btnChonTaoBenhAn.Text = "Chọn";
            this.btnChonTaoBenhAn.Click += new System.EventHandler(this.btnChonTaoBenhAn_Click);
            // 
            // cboMauBenhAn
            // 
            this.cboMauBenhAn.Location = new System.Drawing.Point(127, 34);
            this.cboMauBenhAn.Name = "cboMauBenhAn";
            this.cboMauBenhAn.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMauBenhAn.Properties.Appearance.Options.UseFont = true;
            this.cboMauBenhAn.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMauBenhAn.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboMauBenhAn.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMauBenhAn.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mrd_hsbatemid", 10, "ID"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("mrd_hsbatemname", 50, "Tên mẫu bệnh án")});
            this.cboMauBenhAn.Properties.DropDownRows = 10;
            this.cboMauBenhAn.Properties.NullText = "";
            this.cboMauBenhAn.Properties.PopupSizeable = false;
            this.cboMauBenhAn.Size = new System.Drawing.Size(418, 26);
            this.cboMauBenhAn.TabIndex = 78;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelControl1.Location = new System.Drawing.Point(16, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(91, 19);
            this.labelControl1.TabIndex = 77;
            this.labelControl1.Text = "Loại bệnh án";
            // 
            // frmChonLoaiBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 162);
            this.Controls.Add(this.btnChonTaoBenhAn);
            this.Controls.Add(this.cboMauBenhAn);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(590, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 200);
            this.Name = "frmChonLoaiBenhAn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn loại bệnh án";
            this.Load += new System.EventHandler(this.frmChonLoaiBenhAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboMauBenhAn.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnChonTaoBenhAn;
        private DevExpress.XtraEditors.LookUpEdit cboMauBenhAn;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}