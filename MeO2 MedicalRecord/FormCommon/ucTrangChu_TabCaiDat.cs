using MSO2_MedicalRecord.FormCommon.TabCaiDat;
using MSO2_MedicalRecord.FormCommon.TabTrangChu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSO2_MedicalRecord.FormCommon
{
    public partial class ucTrangChu : UserControl
    {
        private void navBarItemLicense_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucSettingLicense frmsuathoigianravien = new ucSettingLicense();
                frmsuathoigianravien.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmsuathoigianravien);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void navBarItemConnectDB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucSettingDatabase frmsuathoigianravien = new ucSettingDatabase();
                frmsuathoigianravien.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmsuathoigianravien);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void navBarItemListNguoiDung_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucQuanLyNguoiDung frmsuathoigianravien = new ucQuanLyNguoiDung();
                frmsuathoigianravien.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmsuathoigianravien);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void navBarItemListNhanVien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucDanhSachNhanVien frmsuathoigianravien = new ucDanhSachNhanVien();
                frmsuathoigianravien.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmsuathoigianravien);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void navBarItemListOption_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucCauHinhHeThong frmsuathoigianravien = new ucCauHinhHeThong();
                frmsuathoigianravien.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmsuathoigianravien);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void navBarItemMaHoaGiaiMa_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucMaHoaVaGiaiMa frmResult = new ucMaHoaVaGiaiMa();
                frmResult.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmResult);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void navBarItemNhatKySuKien_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucMaHoaVaGiaiMa frmResult = new ucMaHoaVaGiaiMa();
                frmResult.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmResult);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void navBarItemQLMayTram_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                panelCaiDatChiTiet.Controls.Clear();
                ucMaHoaVaGiaiMa frmResult = new ucMaHoaVaGiaiMa();
                frmResult.Dock = System.Windows.Forms.DockStyle.Fill;
                panelCaiDatChiTiet.Controls.Add(frmResult);
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }




    }
}
