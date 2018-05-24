using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.GUIGridView;
using DevExpress.XtraSplashScreen;
using O2S_MedicalRecord.GUI.ChucNang.HSBA_CDHA;

namespace O2S_MedicalRecord.GUI.ChucNang
{
    public partial class ucDSHoSoBenhAn : UserControl
    {
        #region Declaration

        #endregion

        #region Load
        private void ucDSHoSoBenhAn_CDHA_Load(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                CDHA_LoadDataDefault();
                CDHA_LoadThongTinVeCDHA(rowMecicalrecord);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void CDHA_LoadDataDefault()
        {
            try
            {
                gridControlDSPhieuCDHA.DataSource = null;
                gridControlChiTietPhieuCDHA.DataSource = null;
                btnNhapPhieuCDHA.Enabled = false;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void CDHA_LoadThongTinVeCDHA(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                string sqlPhieuPTT = "select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username as nguoichidinh, mbp.maubenhphamdatastatus, mbp.chandoan from maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid left join nhompersonnel nv on nv.userhisid=mbp.userid inner join serviceprice ser on ser.maubenhphamid=mbp.maubenhphamid where mbp.hosobenhanid='" + rowMecicalrecord.hosobenhanid + "' and mbp.maubenhphamgrouptype=1 and mbp.isdeleted=0 and ser.bhyt_groupcode in ('04CDHA','07KTC','05TDCN') group by mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username, mbp.maubenhphamdatastatus; ";
                gridControlDSPhieuCDHA.DataSource = condb.GetDataTable_HIS(sqlPhieuPTT);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custome
        private void gridViewChiTietPhieuCDHA_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "serviceprice_status")
                {
                    string val = Convert.ToString(gridViewChiTietPhieuCDHA.GetRowCellValue(e.RowHandle, "mrd_cdha_serviceid"));
                    string mrd_cdha_servicestatus = Convert.ToString(gridViewChiTietPhieuCDHA.GetRowCellValue(e.RowHandle, "mrd_cdha_servicestatus"));
                    if (val != "0") //da nhap CDHA
                    {
                        if (mrd_cdha_servicestatus == "0") //nhap
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                            e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                        }
                        else if (mrd_cdha_servicestatus == "1")//da luu OK
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                        }
                        else if (mrd_cdha_servicestatus == "2")//da in
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
        private void gridViewDSPhieuCDHA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
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
        private void gridControlDSPhieuCDHA_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSPhieuCDHA.FocusedRowHandle;
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSPhieuCDHA.GetRowCellValue(rowHandle, "maubenhphamid").ToString());

                if (maubenhphamid != 0)
                {
                    string sqlgetdichvu = "select ROW_NUMBER () OVER (ORDER BY his_ser.servicepricename) as stt, his_ser.servicepriceid, his_ser.servicepricecode, his_ser.servicepricename, his_ser.soluong, his_ser.soluongbacsi, his_ser.departmentid, his_ser.departmentgroupid, his_ser.maubenhphamid, COALESCE(mps.mrd_cdha_serviceid,0) as mrd_cdha_serviceid, mps.mrd_cdhatemid, COALESCE(mps.mrd_cdha_servicestatus,-1) as mrd_cdha_servicestatus from dblink('myconn','select servicepriceid, servicepricecode, servicepricename, soluong, soluongbacsi, departmentid, departmentgroupid, maubenhphamid FROM serviceprice where maubenhphamid=" + maubenhphamid + " and bhyt_groupcode in (''04CDHA'',''05TDCN'',''07KTC'')') AS his_ser(servicepriceid integer, servicepricecode text, servicepricename text, soluong double precision, soluongbacsi double precision, departmentid integer, departmentgroupid integer, maubenhphamid integer) left join mrd_cdha_service mps on mps.servicepriceid=his_ser.servicepriceid; ";
                    gridControlChiTietPhieuCDHA.DataSource = condb.GetDataTable_Dblink(sqlgetdichvu);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlChiTietPhieuCDHA_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTietPhieuCDHA.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "departmentid").ToString());

                if (this.SelectRowMedicalrecord.departmentid == departmentid && (this.SelectRowMedicalrecord.medicalrecordstatus == 0 || this.SelectRowMedicalrecord.medicalrecordstatus == 2))
                {
                    btnNhapPhieuCDHA.Enabled = true;
                }
                else
                {
                    btnNhapPhieuCDHA.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Lay du lieu de mo form nhap ket qua
        private void btnNhapPhieuCDHA_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietCDHA();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlChiTietPhieuCDHA_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietCDHA();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void ClickSelectRow_ChiTietCDHA()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                int hienthiform = 0; //0=khong hien thi form
                MrdCDHAServiceDTO mrdcdhaserviceDTO = new MrdCDHAServiceDTO();
                var rowHandle = gridViewChiTietPhieuCDHA.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "departmentid").ToString());
                long serviceprice_status = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "mrd_cdha_servicestatus").ToString());

                if (this.SelectRowMedicalrecord.departmentid == departmentid && (this.SelectRowMedicalrecord.medicalrecordstatus == 0 || this.SelectRowMedicalrecord.medicalrecordstatus == 2))
                {
                    hienthiform = 1;
                    if (serviceprice_status == 2)
                    {
                        mrdcdhaserviceDTO.file_readonly = true;
                        btnNhapPhieuCDHA.Enabled = false;
                    }
                    else
                    {
                        mrdcdhaserviceDTO.file_readonly = false;
                        btnNhapPhieuCDHA.Enabled = true;
                    }
                }
                else
                {
                    mrdcdhaserviceDTO.file_readonly = true;
                    btnNhapPhieuCDHA.Enabled = false;
                }
                if (hienthiform == 1)
                {

                    mrdcdhaserviceDTO.mrd_cdha_serviceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "mrd_cdha_serviceid").ToString());
                    mrdcdhaserviceDTO.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                    mrdcdhaserviceDTO.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                    mrdcdhaserviceDTO.servicepricecode = gridViewChiTietPhieuCDHA.GetRowCellValue(rowHandle, "servicepricecode").ToString();
                    mrdcdhaserviceDTO.patientid = this.SelectRowMedicalrecord.patientid;
                    mrdcdhaserviceDTO.vienphiid = this.SelectRowMedicalrecord.vienphiid;
                    mrdcdhaserviceDTO.hosobenhanid = this.SelectRowMedicalrecord.hosobenhanid;
                    mrdcdhaserviceDTO.departmentgroupid = this.SelectRowMedicalrecord.departmentgroupid;
                    mrdcdhaserviceDTO.departmentid = this.SelectRowMedicalrecord.departmentid;

                    frmNhapPhieuThucHienCDHA frmNhap = new frmNhapPhieuThucHienCDHA(mrdcdhaserviceDTO);
                    frmNhap.ShowDialog();
                    gridControlDSPhieuCDHA_Click(null, null);
                }
                if (btnNhapPhieuCDHA.Enabled == false)
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
        private void repositoryItemButtonCDHA_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietCDHA();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }


        #endregion

    }
}
