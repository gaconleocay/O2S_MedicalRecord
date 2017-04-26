using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MSO2_MedicalRecord.Base;

namespace MSO2_MedicalRecord.FormCommon.TabTrangChu
{
    public partial class ucSettingLicense : UserControl
    {
        private string MaDatabase = String.Empty;
        private MSO2_MedicalRecord.Base.ConnectDatabase condb = new MSO2_MedicalRecord.Base.ConnectDatabase();

        public ucSettingLicense()
        {
            InitializeComponent();
        }

        private void ucSettingLicense_Load(object sender, EventArgs e)
        {
            try
            {
                HienThiThongTinVeLicense();
                LoadFormTaoLicense();
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void HienThiThongTinVeLicense()
        {
            try
            {
                MaDatabase = MSO2_MedicalRecord.FormCommon.DangKyBanQuyen.KiemTraLicense.LayThongTinMaDatabase();
                txtMaMay.Text = MaDatabase;
                txtMaMay.ReadOnly = true;
                //Load License tu DB ra
                string kiemtra_licensetag = "SELECT datakey, licensekey FROM mrd_license WHERE datakey='" + MaDatabase + "' limit 1;";
                DataView dv = new DataView(condb.getDataTable(kiemtra_licensetag));
                if (dv != null && dv.Count > 0)
                {
                    txtKeyKichHoat.Text = dv[0]["licensekey"].ToString();
                }
                txtKeyKichHoat.Focus();
                btnLicenseKiemTra_Click(null, null);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadFormTaoLicense()
        {
            try
            {
                if (SessionLogin.SessionUsercode == MSO2_MedicalRecord.Base.KeyTrongPhanMem.AdminUser_key)
                {
                    groupBoxTaoLicense.Visible = true;
                    txtTaoLicensePassword.Focus();
                    btnTaoLicenseTao.Enabled = false;
                    DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
                    DateTime denNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                    dtTaoLicenseKeyTuNgay.Value = tuNgay;
                    dtTaoLicenseKeyDenNgay.Value = denNgay;
                }
                else
                {
                    groupBoxTaoLicense.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #region License
        private void btnLicenseKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtKeyKichHoat.Text.Trim()))
                {
                    lblThoiGianSuDung.Text = FormCommon.DangKyBanQuyen.KiemTraLicense.KiemTraThoiHanLicense(txtKeyKichHoat.Text.Trim());
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Chưa nhập mã kích hoạt!");
                    frmthongbao.Show();
                    lblThoiGianSuDung.Text = "none";
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }

        }
        private void btnLicenseLuu_Click(object sender, EventArgs e)
        {
            try
            {
                //Luu key kich hoat vao DB
                string update_license = "UPDATE mrd_license SET licensekey='" + txtKeyKichHoat.Text.Trim() + "' WHERE datakey='" + MaDatabase + "' ;";
                if (condb.ExecuteNonQuery(update_license))
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao("Lưu mã kích hoạt thành công!");
                    frmthongbao.Show();
                }
                else
                {
                    ThongBao.frmThongBao frmthongbao = new ThongBao.frmThongBao(MSO2_MedicalRecord.Base.ThongBaoLable.CO_LOI_XAY_RA);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void btnLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtMaMay.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Tao License
        private void btnTaoLicenseTao_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaoLicenseMaMay.Text != "")
                {
                    string MaDatabaseVaThoiGianSuDung = "";
                    if (chkKhongThoiHan.Checked)
                    {
                        MaDatabaseVaThoiGianSuDung = MSO2_MedicalRecord.Base.KeyTrongPhanMem.SaltEncrypt + "$" + txtTaoLicenseMaMay.Text + "$" + Base.KeyTrongPhanMem.BanQuyenKhongThoiHan;
                    }
                    else
                    {
                        string datetungay = DateTime.ParseExact(dtTaoLicenseKeyTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                        string datedenngay = DateTime.ParseExact(dtTaoLicenseKeyDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                        MaDatabaseVaThoiGianSuDung = MSO2_MedicalRecord.Base.KeyTrongPhanMem.SaltEncrypt + "$" + txtTaoLicenseMaMay.Text + "$" + datetungay + "$" + datedenngay;
                    }
                    txtTaoLicenseMaKichHoat.Text = EncryptAndDecrypt.Encrypt(MaDatabaseVaThoiGianSuDung, true);
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnTaoLicenseCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.Clear();    //Clear if any old value is there in Clipboard        
                Clipboard.SetText(txtTaoLicenseMaKichHoat.Text); //Copy text to Clipboard
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void txtTaoLicensePassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //Kiem tra pass dung hay sai?
                    if (txtTaoLicensePassword.Text.Trim() == MSO2_MedicalRecord.Base.KeyTrongPhanMem.LayLicense_key && SessionLogin.SessionUsercode == MSO2_MedicalRecord.Base.KeyTrongPhanMem.AdminUser_key)
                    {
                        btnTaoLicenseTao.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void chkKhongThoiHan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkKhongThoiHan.Checked)
                {
                    dtTaoLicenseKeyTuNgay.Enabled = false;
                    dtTaoLicenseKeyDenNgay.Enabled = false;
                }
                else
                {
                    dtTaoLicenseKeyTuNgay.Enabled = false;
                    dtTaoLicenseKeyDenNgay.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }







    }
}
