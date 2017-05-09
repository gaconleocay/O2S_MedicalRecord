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
using MSO2_MedicalRecord.DTO;

namespace MSO2_MedicalRecord.GUI.ChucNang.HSBA_PTTT
{
    public partial class ucHSBA_PTTT : UserControl
    {
        #region Declaration
        //private long hosobenhanid { get; set; }
        //private long medicalrecordid { get; set; }
        //private long departmentid { get; set; }
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        private DAL.ConnectDatabase conn = new DAL.ConnectDatabase();

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
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDuLieuVeMacDinh()
        {
            try
            {
                gridControlDSPhieuPTTT.DataSource = null;
                gridControlChiTietPhieuPTTT.DataSource = null;
                btnNhapPhieuPTTT.Enabled = false;
                btnXemPhieuPTTT.Enabled = false;
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVePhauThuatThuThuat()
        {
            try
            {
                //Load phieu PTTT
                string sqlPhieuPTT = "select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username as nguoichidinh, mbp.maubenhphamdatastatus from maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid left join tools_tblnhanvien nv on nv.userhisid=mbp.userid where mbp.hosobenhanid='" + this.mecicalrecordCurrentDTO.hosobenhanid + "' and mbp.maubenhphamgrouptype=4 and mbp.isdeleted=0; ";
                gridControlDSPhieuPTTT.DataSource = conn.GetDataTable_HIS(sqlPhieuPTT);

            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
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
                    string sqlgetdichvu = "select ROW_NUMBER () OVER (ORDER BY ser.servicepricename) as stt, ser.servicepriceid, ser.servicepricecode, ser.servicepricename, ser.soluong, ser.soluongbacsi, ser.departmentid, ser.departmentgroupid from serviceprice ser where ser.maubenhphamid='" + maubenhphamid + "';";
                    gridControlChiTietPhieuPTTT.DataSource = conn.GetDataTable_HIS(sqlgetdichvu);

                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
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

        private void btnNhapPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form nhap pttt
                frmNhapPhieuThucHienPTTT frmNhap = new frmNhapPhieuThucHienPTTT(this.mecicalrecordCurrentDTO);
                frmNhap.ShowDialog();
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnXemPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form nhap pttt
                frmNhapPhieuThucHienPTTT frmNhap = new frmNhapPhieuThucHienPTTT(this.mecicalrecordCurrentDTO);
                frmNhap.ShowDialog();
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void gridControlChiTietPhieuPTTT_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewChiTietPhieuPTTT.FocusedRowHandle;
                long servicepriceid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "servicepriceid").ToString());
                long departmentid = Utilities.Util_TypeConvertParse.ToInt64(gridViewChiTietPhieuPTTT.GetRowCellValue(rowHandle, "departmentid").ToString());

                if (this.mecicalrecordCurrentDTO.departmentid == departmentid && (this.mecicalrecordCurrentDTO.medicalrecordstatus==0 ) || this.mecicalrecordCurrentDTO.medicalrecordstatus==2)
                {
                    this.mecicalrecordCurrentDTO.servicepriceid = servicepriceid;
                    this.mecicalrecordCurrentDTO.departmentid = departmentid;

                    if (servicepriceid == 1) //da tao phieu nhap PTTT
                    {
                        btnNhapPhieuPTTT.Enabled = false;
                        btnXemPhieuPTTT.Enabled = true;
                    }
                    else
                    {
                        btnNhapPhieuPTTT.Enabled = true;
                        btnXemPhieuPTTT.Enabled = false;
                    }
                }
                else
                {
                    btnNhapPhieuPTTT.Enabled = false;
                    btnXemPhieuPTTT.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
    }
}
