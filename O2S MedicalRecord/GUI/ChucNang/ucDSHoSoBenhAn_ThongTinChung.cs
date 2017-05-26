using DevExpress.XtraGrid.Views.Grid;
using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.ChucNang
{
    public partial class ucDSHoSoBenhAn : UserControl
    {
        #region Load Thong Tin Chung
        private void ucDSHoSoBenhAn_ThongTinChung_Load(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                ThongTinChung_LoadDataDefault();
                ThongTinChung_LoadThongTinHanhChinh(rowMecicalrecord);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void ThongTinChung_LoadDataDefault()
        {
            try
            {
                lblTenKhoa.Text = "";
                lblTenPhong.Text = "";
                txtHosobenhanid.Text = "";
                txtMedicalRecord.Text = "";
                txtPatientCode.Text = "";
                txtPatientName.Text = "";
                txtVienphiId.Text = "";
                txtNgaySinh.Text = "";
                txtDoiTuongBenhNhan.Text = "";
                txtGioiTinh.Text = "";
                txtDiaChi.Text = "";
                txtBhytCode.Text = "";
                txtThoiGianVaoKhoa.Text = "";
                txtThoiGianRaKhoa.Text = "";
                txtHanTheTuDen.Text = "";
                gridControlHSBALichSuIn.DataSource = null;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void ThongTinChung_LoadThongTinHanhChinh(MedicalrecordDTO rowMecicalrecord)
        {
            try
            {
                if (rowMecicalrecord.medicalrecordid != 0 && rowMecicalrecord.hosobenhanid != 0)
                {
                    string sqlthongtin = "select degp.departmentgroupname, de.departmentname, hsba.hosobenhanid, mrd.medicalrecordcode, hsba.patientcode, hsba.patientname, vp.vienphiid, (case vp.doituongbenhnhanid when 1 then 'BHYT' when 2 then 'Viện phí' when 3 then 'Dịch vụ' when 4 then 'Người nước ngoài' when 5 then 'Miễn phí' when 6 then 'Hợp đồng' else '' end) as doituongbenhnhan, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, hsba.gioitinhname, TO_CHAR(hsba.birthday, 'dd/mm/yyyy') as birthday, bh.bhytcode, (case when bh.bhytcode<>'' then (TO_CHAR(bh.bhytfromdate, 'Từ dd/mm/yyyy') || ' đến ' || TO_CHAR(bh.bhytutildate, 'dd/mm/yyyy')) end ) as hanthetuden, to_char(mrd.thoigianvaovien,'hh:mi:ss dd/mm/yyyy') as thoigianvaovien, (case when mrd.thoigianravien<>'0001-01-01 00:00:00' then to_char(mrd.thoigianravien,'hh:mi:ss dd/mm/yyyy') end) as thoigianravien from hosobenhan hsba inner join medicalrecord mrd on mrd.hosobenhanid=hsba.hosobenhanid inner join (select vienphiid, doituongbenhnhanid, hosobenhanid, bhytid from vienphi where hosobenhanid='" + rowMecicalrecord.hosobenhanid + "') vp on vp.hosobenhanid=hsba.hosobenhanid inner join bhyt bh on bh.bhytid=vp.bhytid left join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid left join department de on de.departmentid=mrd.departmentid and de.departmenttype in (2,3,9) where mrd.medicalrecordid='" + rowMecicalrecord.medicalrecordid + "' and hsba.hosobenhanid='" + rowMecicalrecord.hosobenhanid + "';  ";
                    DataView dataThongTin = new DataView(condb.GetDataTable_HIS(sqlthongtin));
                    if (dataThongTin.Count > 0)
                    {
                        lblTenKhoa.Text = dataThongTin[0]["departmentgroupname"].ToString();
                        lblTenPhong.Text = dataThongTin[0]["departmentname"].ToString();
                        txtHosobenhanid.Text = dataThongTin[0]["hosobenhanid"].ToString();
                        txtMedicalRecord.Text = dataThongTin[0]["medicalrecordcode"].ToString();
                        txtPatientCode.Text = dataThongTin[0]["patientcode"].ToString();
                        txtPatientName.Text = dataThongTin[0]["patientname"].ToString();
                        txtVienphiId.Text = dataThongTin[0]["vienphiid"].ToString();
                        txtNgaySinh.Text = dataThongTin[0]["birthday"].ToString();
                        txtGioiTinh.Text = dataThongTin[0]["gioitinhname"].ToString();
                        txtDoiTuongBenhNhan.Text = dataThongTin[0]["doituongbenhnhan"].ToString();
                        txtDiaChi.Text = dataThongTin[0]["diachi"].ToString();
                        txtBhytCode.Text = dataThongTin[0]["bhytcode"].ToString();
                        txtThoiGianVaoKhoa.Text = dataThongTin[0]["thoigianvaovien"].ToString();
                        txtThoiGianRaKhoa.Text = dataThongTin[0]["thoigianravien"].ToString();
                        txtHanTheTuDen.Text = dataThongTin[0]["hanthetuden"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
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
