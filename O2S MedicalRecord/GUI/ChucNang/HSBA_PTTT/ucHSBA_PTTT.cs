using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.GUIGridView;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_PTTT
{
    public partial class ucHSBA_PTTT : UserControl
    {
        #region Declaration
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        #endregion

        public ucHSBA_PTTT()
        {
            InitializeComponent();
        }

        public ucHSBA_PTTT(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.mecicalrecordCurrentDTO = filterDTO;
        }

        #region Load
        private void ucHSBA_PTTT_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuVeMacDinh();
                LoadThongTinVePhauThuatThuThuat();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDuLieuVeMacDinh()
        {
            try
            {
                gridControlDSPhieuPTTT.DataSource = null;
                gridControlChiTietPhieuPTTT.DataSource = null;
                btnNhapPhieuPTTT.Enabled = false;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVePhauThuatThuThuat()
        {
            try
            {
                string sqlPhieuPTT = "select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username as nguoichidinh, mbp.maubenhphamdatastatus, mbp.chandoan from maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid left join tools_tblnhanvien nv on nv.userhisid=mbp.userid inner join serviceprice ser on ser.maubenhphamid=mbp.maubenhphamid where mbp.hosobenhanid='" + this.mecicalrecordCurrentDTO.hosobenhanid + "' and mbp.maubenhphamgrouptype=4 and mbp.isdeleted=0 and ser.bhyt_groupcode in ('06PTTT','07KTC') group by mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username, mbp.maubenhphamdatastatus; ";
                gridControlDSPhieuPTTT.DataSource = condb.GetDataTable_HIS(sqlPhieuPTT);

            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region GridControl Maubenhpham
        private void gridViewChiTietPhieuPTTT_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "serviceprice_status")
                {
                    string val = Convert.ToString(gridViewChiTietPhieuPTTT.GetRowCellValue(e.RowHandle, "mrd_pttt_serviceid"));
                    string mrd_pttt_servicestatus = Convert.ToString(gridViewChiTietPhieuPTTT.GetRowCellValue(e.RowHandle, "mrd_pttt_servicestatus"));
                    if (val != "0") //da nhap PTTT
                    {
                        if (mrd_pttt_servicestatus == "0") //nhap
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                            e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                        }
                        else if (mrd_pttt_servicestatus == "1")//da luu OK
                        {
                            e.Handled = true;
                            Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                            e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                        }
                        else if (mrd_pttt_servicestatus == "2")//da in
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
        private void gridViewDSPhieuPTTT_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        #endregion
        private void gridControlDSPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSPhieuPTTT.FocusedRowHandle;
                long maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSPhieuPTTT.GetRowCellValue(rowHandle, "maubenhphamid").ToString());

                if (maubenhphamid != null && maubenhphamid != 0)
                {
                    string sqlgetdichvu = "select ROW_NUMBER () OVER (ORDER BY his_ser.servicepricename) as stt, his_ser.servicepriceid, his_ser.servicepricecode, his_ser.servicepricename, his_ser.soluong, his_ser.soluongbacsi, his_ser.departmentid, his_ser.departmentgroupid, his_ser.maubenhphamid, COALESCE(mps.mrd_pttt_serviceid,0) as mrd_pttt_serviceid, mps.mrd_pttttemid, COALESCE(mps.mrd_pttt_servicestatus,-1) as mrd_pttt_servicestatus from dblink('myconn','select servicepriceid, servicepricecode, servicepricename, soluong, soluongbacsi, departmentid, departmentgroupid, maubenhphamid FROM serviceprice where maubenhphamid=" + maubenhphamid + " and bhyt_groupcode in (''06PTTT'',''07KTC'')') AS his_ser(servicepriceid integer, servicepricecode text, servicepricename text, soluong double precision, soluongbacsi double precision, departmentid integer, departmentgroupid integer, maubenhphamid integer) left join mrd_pttt_service mps on mps.servicepriceid=his_ser.servicepriceid; ";
                    gridControlChiTietPhieuPTTT.DataSource = condb.GetDataTable_Dblink(sqlgetdichvu);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void gridControlChiTietPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTietPhieuPTTT.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "departmentid").ToString());

                if (this.mecicalrecordCurrentDTO.departmentid == departmentid && (this.mecicalrecordCurrentDTO.medicalrecordstatus == 0 || this.mecicalrecordCurrentDTO.medicalrecordstatus == 2))
                {
                    btnNhapPhieuPTTT.Enabled = true;
                }
                else
                {
                    btnNhapPhieuPTTT.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #region Lay du lieu de mo form nhap ket qua
        private void btnNhapPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietPTTT();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlChiTietPhieuPTTT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietPTTT();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void ClickSelectRow_ChiTietPTTT()
        {
            try
            {
                int hienthiform = 0; //0=khong hien thi form
                MrdPtttServiceDTO mrdptttserviceDTO = new MrdPtttServiceDTO();
                var rowHandle = gridViewChiTietPhieuPTTT.FocusedRowHandle;
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "departmentid").ToString());
                long serviceprice_status = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "mrd_pttt_servicestatus").ToString());

                if (this.mecicalrecordCurrentDTO.departmentid == departmentid && (this.mecicalrecordCurrentDTO.medicalrecordstatus == 0 || this.mecicalrecordCurrentDTO.medicalrecordstatus == 2))
                {
                    hienthiform = 1;
                    if (serviceprice_status == 2)
                    {
                        mrdptttserviceDTO.file_readonly = true;
                        btnNhapPhieuPTTT.Enabled = false;
                    }
                    else
                    {
                        mrdptttserviceDTO.file_readonly = false;
                        btnNhapPhieuPTTT.Enabled = true;
                    }
                }
                else
                {
                    mrdptttserviceDTO.file_readonly = true;
                    btnNhapPhieuPTTT.Enabled = false;
                }
                if (hienthiform == 1)
                {

                    mrdptttserviceDTO.mrd_pttt_serviceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "mrd_pttt_serviceid").ToString());
                    mrdptttserviceDTO.servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                    mrdptttserviceDTO.maubenhphamid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "maubenhphamid").ToString());
                    mrdptttserviceDTO.servicepricecode = gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "servicepricecode").ToString();
                    mrdptttserviceDTO.patientid = this.mecicalrecordCurrentDTO.patientid;
                    mrdptttserviceDTO.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                    mrdptttserviceDTO.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                    mrdptttserviceDTO.departmentgroupid = this.mecicalrecordCurrentDTO.departmentgroupid;
                    mrdptttserviceDTO.departmentid = this.mecicalrecordCurrentDTO.departmentid;

                    frmNhapPhieuThucHienPTTT frmNhap = new frmNhapPhieuThucHienPTTT(mrdptttserviceDTO);
                    frmNhap.ShowDialog();
                    gridControlDSPhieuPTTT_Click(null, null);
                }
                if (btnNhapPhieuPTTT.Enabled == false)
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.PHIEU_DUOC_TAO_BOI_KHOA_PHONG_KHAC);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        #endregion

        private void repositoryItemButtonEditView_Click(object sender, EventArgs e)
        {
            try
            {
                ClickSelectRow_ChiTietPTTT();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

    }
}
