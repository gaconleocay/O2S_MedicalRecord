using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.GUIGridView;
using DevExpress.XtraSplashScreen;
using O2S_MedicalRecord.GUI.ChucNang.HSBA_XN;

namespace O2S_MedicalRecord.GUI.ChucNang
{
    public partial class ucDSHoSoBenhAn : UserControl
    {
        #region Declaration

        #endregion

        #region Load
        private void ucDSHoSoBenhAn_XetNghiem_Load(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                XetNghiem_LoadDataDefault();
                XetNghiem_LoadThongTinVeXetNghiem(rowMecicalrecord);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void XetNghiem_LoadDataDefault()
        {
            try
            {
                gridControlDSPhieuXetNghiem.DataSource = null;
                gridControlChiTietPhieuXetNghiem.DataSource = null;
                btnNhapPhieuXetNghiem.Enabled = false;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void XetNghiem_LoadThongTinVeXetNghiem(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                string sqlPhieuPTT = "select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username as nguoichidinh, mbp.maubenhphamdatastatus, mbp.chandoan from maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid left join nhompersonnel nv on nv.userhisid=mbp.userid inner join serviceprice ser on ser.maubenhphamid=mbp.maubenhphamid where mbp.hosobenhanid='" + rowMecicalrecord.hosobenhanid + "' and mbp.maubenhphamgrouptype=0 and mbp.isdeleted=0 and ser.bhyt_groupcode in ('03XN','07KTC') group by mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username, mbp.maubenhphamdatastatus; ";
                gridControlDSPhieuXetNghiem.DataSource = condb.GetDataTable_HIS(sqlPhieuPTT);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custome
        private void gridViewChiTietPhieuXetNghiem_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "serviceprice_status")
                {
                    string val = Convert.ToString(gridViewChiTietPhieuXetNghiem.GetRowCellValue(e.RowHandle, "mrd_xn_serviceid"));
                    string mrd_xn_servicestatus = Convert.ToString(gridViewChiTietPhieuXetNghiem.GetRowCellValue(e.RowHandle, "mrd_xn_servicestatus"));
                    if (val != "0") //da nhap XetNghiem
                    {
                        if (mrd_xn_servicestatus == "0") //nhap
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                            e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                        }
                        else if (mrd_xn_servicestatus == "1")//da luu OK
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                        }
                        else if (mrd_xn_servicestatus == "2")//da in
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSPhieuXetNghiem_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion

        #region Events
        private void gridControlDSPhieuXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSPhieuXetNghiem.FocusedRowHandle;
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSPhieuXetNghiem.GetRowCellValue(rowHandle, "maubenhphamid").ToString());

                if (maubenhphamid != 0)
                {
                    string sqlgetdichvu = "select ROW_NUMBER () OVER (ORDER BY his_ser.servicepricename) as stt, his_ser.servicepriceid, his_ser.servicepricecode, his_ser.servicepricename, his_ser.soluong, his_ser.soluongbacsi, his_ser.departmentid, his_ser.departmentgroupid, his_ser.maubenhphamid, COALESCE(mps.mrd_xn_serviceid,0) as mrd_xn_serviceid, mps.mrd_xntemid, COALESCE(mps.mrd_xn_servicestatus,-1) as mrd_xn_servicestatus from dblink('myconn','select servicepriceid, servicepricecode, servicepricename, soluong, soluongbacsi, departmentid, departmentgroupid, maubenhphamid FROM serviceprice where maubenhphamid=" + maubenhphamid + " and bhyt_groupcode in (''03XN'',''07KTC'')') AS his_ser(servicepriceid integer, servicepricecode text, servicepricename text, soluong double precision, soluongbacsi double precision, departmentid integer, departmentgroupid integer, maubenhphamid integer) left join mrd_xn_service mps on mps.servicepriceid=his_ser.servicepriceid; ";
                    gridControlChiTietPhieuXetNghiem.DataSource = condb.GetDataTable_Dblink(sqlgetdichvu);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlChiTietPhieuXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTietPhieuXetNghiem.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "departmentid").ToString());

                if (this.SelectRowMedicalrecord.departmentid == departmentid && (this.SelectRowMedicalrecord.medicalrecordstatus == 0 || this.SelectRowMedicalrecord.medicalrecordstatus == 2))
                {
                    btnNhapPhieuXetNghiem.Enabled = true;
                }
                else
                {
                    btnNhapPhieuXetNghiem.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Lay du lieu de mo form nhap ket qua
        private void btnNhapPhieuXetNghiem_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietXetNghiem();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlChiTietPhieuXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietXetNghiem();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void ClickSelectRow_ChiTietXetNghiem()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                int hienthiform = 0; //0=khong hien thi form
                MrdXetNghiemServiceDTO mrdxnserviceDTO = new MrdXetNghiemServiceDTO();
                var rowHandle = gridViewChiTietPhieuXetNghiem.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "departmentid").ToString());
                long serviceprice_status = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "mrd_xn_servicestatus").ToString());

                if (this.SelectRowMedicalrecord.departmentid == departmentid && (this.SelectRowMedicalrecord.medicalrecordstatus == 0 || this.SelectRowMedicalrecord.medicalrecordstatus == 2))
                {
                    hienthiform = 1;
                    if (serviceprice_status == 2)
                    {
                        mrdxnserviceDTO.file_readonly = true;
                        btnNhapPhieuXetNghiem.Enabled = false;
                    }
                    else
                    {
                        mrdxnserviceDTO.file_readonly = false;
                        btnNhapPhieuXetNghiem.Enabled = true;
                    }
                }
                else
                {
                    mrdxnserviceDTO.file_readonly = true;
                    btnNhapPhieuXetNghiem.Enabled = false;
                }
                if (hienthiform == 1)
                {

                    mrdxnserviceDTO.mrd_xn_serviceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "mrd_xn_serviceid").ToString());
                    mrdxnserviceDTO.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                    mrdxnserviceDTO.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                    mrdxnserviceDTO.servicepricecode = gridViewChiTietPhieuXetNghiem.GetRowCellValue(rowHandle, "servicepricecode").ToString();
                    mrdxnserviceDTO.patientid = this.SelectRowMedicalrecord.patientid;
                    mrdxnserviceDTO.vienphiid = this.SelectRowMedicalrecord.vienphiid;
                    mrdxnserviceDTO.hosobenhanid = this.SelectRowMedicalrecord.hosobenhanid;
                    mrdxnserviceDTO.departmentgroupid = this.SelectRowMedicalrecord.departmentgroupid;
                    mrdxnserviceDTO.departmentid = this.SelectRowMedicalrecord.departmentid;

                    frmNhapPhieuThucHienXN frmNhap = new frmNhapPhieuThucHienXN(mrdxnserviceDTO);
                    frmNhap.ShowDialog();
                    gridControlDSPhieuXetNghiem_Click(null, null);
                }
                if (btnNhapPhieuXetNghiem.Enabled == false)
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.PHIEU_DUOC_TAO_BOI_KHOA_PHONG_KHAC);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void repositoryItemButtonXN_Edit_Click_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietXetNghiem();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        }
}
