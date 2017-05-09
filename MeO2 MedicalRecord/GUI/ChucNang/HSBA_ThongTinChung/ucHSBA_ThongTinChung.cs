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

namespace MSO2_MedicalRecord.GUI.ChucNang.HSBA_ThongTinChung
{
    public partial class ucHSBA_ThongTinChung : UserControl
    {
        #region Declaration
        //// khai báo 1 hàm delegate
        //public delegate void GetString(string thoigian);
        //// khai báo 1 kiểu hàm delegate
        //public GetString MyGetData;
        private long hosobenhanid { get; set; }
        private long medicalrecordid { get; set; }
        private DAL.ConnectDatabase conn = new DAL.ConnectDatabase();


        #endregion

        public ucHSBA_ThongTinChung()
        {
            InitializeComponent();
        }
        public ucHSBA_ThongTinChung(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.hosobenhanid = filterDTO.hosobenhanid;
            this.medicalrecordid = filterDTO.medicalrecordid;
        }


        #region Lable Clink Copy
        private void lblPatientCode_Click(object sender, EventArgs e)
         {
            lblPatientCode.Focus();
            //lblPatientCode.Select();
        }
        private void lblVienphiCode_Click(object sender, EventArgs e)
        {
            lblVienphiCode.Focus();
        }
        private void lblPatientName_Click(object sender, EventArgs e)
        {
            lblPatientName.Focus();
        }
        #endregion

        #region Load
        private void ucHSBA_ThongTinChung_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDuLieuVeMacDinh();
                LoadDataVeBenhNhan(this.medicalrecordid, this.hosobenhanid);
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
                lblTenKhoa.Text = "";
                lblTenPhong.Text = "";
                lbHosobenhanid.Text = "";
                lblMedicalRecord.Text = "";
                lblPatientCode.Text = "";
                lblPatientName.Text = "";
                lblVienphiCode.Text = "";
                lblNgaySinh.Text = "";
                lblDoiTuongBenhNhan.Text = "";
                lblGioiTinh.Text = "";
                lblDiaChi.Text = "";
                lblBhytCode.Text = "";
                lblThoiGianVaoKhoa.Text = "";
                lblThoiGianRaKhoa.Text = "";
                gridControlHSBALichSuIn.DataSource = null;
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        internal void LoadDataVeBenhNhan(long medicalrecordid, long hosobenhanid)
        {
            try
            {
                if (this.medicalrecordid != null && this.hosobenhanid != null)
                {
                    string sqlthongtin = "select degp.departmentgroupname, de.departmentname, hsba.hosobenhanid, mrd.medicalrecordcode, hsba.patientcode, hsba.patientname, vp.vienphicode, (case vp.doituongbenhnhanid when 1 then 'BHYT' when 2 then 'Viện phí' when 3 then 'Dịch vụ' when 4 then 'Người nước ngoài' when 5 then 'Miễn phí' when 6 then 'Hợp đồng' else '' end) as doituongbenhnhan, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, hsba.bhytcode, mrd.thoigianvaovien, mrd.thoigianravien from hosobenhan hsba inner join medicalrecord mrd on mrd.hosobenhanid=hsba.hosobenhanid inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid left join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid left join department de on de.departmentid=mrd.departmentid where mrd.medicalrecordid='" + medicalrecordid + "' and hsba.hosobenhanid='" + hosobenhanid + "'; ";
                    DataView dataThongTin = new DataView(conn.GetDataTable_HIS(sqlthongtin));
                    if (dataThongTin.Count > 0)
                    {
                        lblTenKhoa.Text = dataThongTin[0]["departmentgroupname"].ToString();
                        lblTenPhong.Text = dataThongTin[0]["departmentname"].ToString();
                        lbHosobenhanid.Text = dataThongTin[0]["hosobenhanid"].ToString();
                        lblMedicalRecord.Text = dataThongTin[0]["medicalrecordcode"].ToString();
                        lblPatientCode.Text = dataThongTin[0]["patientcode"].ToString();
                        lblPatientName.Text = dataThongTin[0]["patientname"].ToString();
                        lblVienphiCode.Text = dataThongTin[0]["vienphicode"].ToString();
                        lblDoiTuongBenhNhan.Text = dataThongTin[0]["doituongbenhnhan"].ToString();
                        lblDiaChi.Text = dataThongTin[0]["diachi"].ToString();
                        lblBhytCode.Text = dataThongTin[0]["bhytcode"].ToString();
                        lblThoiGianVaoKhoa.Text = dataThongTin[0]["thoigianvaovien"].ToString();
                        lblThoiGianRaKhoa.Text = dataThongTin[0]["thoigianravien"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion



        private void gridControlHSBALichSuIn_Click(object sender, EventArgs e)
        {

        }

        private void gridViewHSBALichSuIn_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
    }
}
