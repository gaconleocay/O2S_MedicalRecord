using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.ChucNang.HoiChan
{
    public partial class frmHoiChan_NhapDuLieu : Form
    {
        #region Khai bao
        private int currentloaihoichan { get; set; }
        private MedicalrecordDTO currentMedicalrecord { get; set; }
        private MrdHsbaHoiChanDTO currentHsbaHoiChan { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();


        #endregion
        public frmHoiChan_NhapDuLieu()
        {
            InitializeComponent();
        }

        public frmHoiChan_NhapDuLieu(int loaihoichan, MedicalrecordDTO rowMedicalrecord, MrdHsbaHoiChanDTO rowHsbaHoiChan)
        {
            try
            {
                InitializeComponent();
                this.currentloaihoichan = loaihoichan;
                this.currentMedicalrecord = rowMedicalrecord;
                this.currentHsbaHoiChan = rowHsbaHoiChan;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Load
        private void frmHoiChan_NhapDuLieu_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.currentHsbaHoiChan.mrd_loaihc_id != 0) //chinh sua
                {
                    LoadThongTinVeHoiChan_ChinhSua();
                }
                else // them moi
                {
                    LoadThongTinVeHoiChan_ThemMoi();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVeHoiChan_ChinhSua()
        {
            try
            {
                if (this.currentHsbaHoiChan != null)
                {
                    txtHopTai.Text = this.currentHsbaHoiChan.diadiemhoichan;
                    txtTomTatTienSuBenh.Text = this.currentHsbaHoiChan.dbb_tomtattiensubenh;
                    txtTinhTrangLucVaoVien.Text = this.currentHsbaHoiChan.dbb_tinhtranglucvaovien;
                    txtChanDoanTuyenDuoi.Text = this.currentHsbaHoiChan.dbb_chandoantuyenduoi;
                    txtTomTatDienBienBenh.Text = this.currentHsbaHoiChan.dbb_tomtatdienbienbenh;
                    txtChanDoanTienLuong.Text = this.currentHsbaHoiChan.yk_chandoantienluong;
                    txtPhuongPhapDieuTri.Text = this.currentHsbaHoiChan.yk_phuongphapdieutri;
                    txtChamSoc.Text = this.currentHsbaHoiChan.yk_chamsoc;
                    txtKetLuan.Text = this.currentHsbaHoiChan.kl_ketluan;

                    //Thanh vien tham gia
                    List<HoiChan_ThanhVienThamGiaDTO> lstThanhVienThamGia = new List<HoiChan_ThanhVienThamGiaDTO>();
                    HoiChan_ThanhVienThamGiaDTO thanhvien_chutich = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_chutich.stt = 1;
                    thanhvien_chutich.loaidoituong_id = 1;
                    thanhvien_chutich.loaidoituong_ten = "Chủ tịch";
                    thanhvien_chutich.hovaten = this.currentHsbaHoiChan.tvtg_chutoa_ten;
                    thanhvien_chutich.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_chutoa_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_chutich);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_thuky = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_thuky.stt = 2;
                    thanhvien_thuky.loaidoituong_id = 2;
                    thanhvien_thuky.loaidoituong_ten = "Thư ký";
                    thanhvien_thuky.hovaten = this.currentHsbaHoiChan.tvtg_thuky_ten;
                    thanhvien_thuky.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thuky_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_thuky);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_1 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_1.stt = 3;
                    thanhvien_1.loaidoituong_id = 3;
                    thanhvien_1.loaidoituong_ten = "Thành viên";
                    thanhvien_1.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien1_ten;
                    thanhvien_1.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien1_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_1);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_2 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_2.stt = 4;
                    thanhvien_2.loaidoituong_id = 3;
                    thanhvien_2.loaidoituong_ten = "Thành viên";
                    thanhvien_2.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien2_ten;
                    thanhvien_2.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien2_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_2);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_3 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_3.stt = 5;
                    thanhvien_3.loaidoituong_id = 3;
                    thanhvien_3.loaidoituong_ten = "Thành viên";
                    thanhvien_3.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien3_ten;
                    thanhvien_3.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien3_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_3);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_4 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_4.stt = 6;
                    thanhvien_4.loaidoituong_id = 3;
                    thanhvien_4.loaidoituong_ten = "Thành viên";
                    thanhvien_4.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien4_ten;
                    thanhvien_4.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien4_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_4);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_5 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_5.stt = 7;
                    thanhvien_5.loaidoituong_id = 3;
                    thanhvien_5.loaidoituong_ten = "Thành viên";
                    thanhvien_5.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien5_ten;
                    thanhvien_5.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien5_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_5);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_6 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_6.stt = 8;
                    thanhvien_6.loaidoituong_id = 3;
                    thanhvien_6.loaidoituong_ten = "Thành viên";
                    thanhvien_6.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien6_ten;
                    thanhvien_6.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien6_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_6);

                    gridControlThanhVienThamGia.DataSource = lstThanhVienThamGia;

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVeHoiChan_ThemMoi()
        {
            try
            {
                LoadThongTinVeThanhVienThamGia_Default();
                switch (this.currentloaihoichan)
                {
                    case 1: //Hoi chan PTTT
                        {
                            txtYCHoiChan.Text = "Hội chẩn phẫu thuật";
                            break;
                        }
                    case 2: //Hoi CHan THuoc
                        {
                            txtYCHoiChan.Text = "Hội chẩn thuốc hoạt chất *";

                            break;
                        }
                    case 3://Hoi chan chuyen vien
                        {
                            txtYCHoiChan.Text = "Hội chẩn chuyển viện";

                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadThongTinVeThanhVienThamGia_Default()
        {
            try
            {
                List<HoiChan_ThanhVienThamGiaDTO> lstThanhVienThamGia = new List<HoiChan_ThanhVienThamGiaDTO>();

                HoiChan_ThanhVienThamGiaDTO thanhvien_chutich = new HoiChan_ThanhVienThamGiaDTO();
                thanhvien_chutich.stt = 1;
                thanhvien_chutich.loaidoituong_id = 1;
                thanhvien_chutich.loaidoituong_ten = "Chủ tịch";
                lstThanhVienThamGia.Add(thanhvien_chutich);

                HoiChan_ThanhVienThamGiaDTO thanhvien_thuky = new HoiChan_ThanhVienThamGiaDTO();
                thanhvien_thuky.stt = 2;
                thanhvien_thuky.loaidoituong_id = 2;
                thanhvien_thuky.loaidoituong_ten = "Thư ký";
                lstThanhVienThamGia.Add(thanhvien_thuky);

                for (int i = 3; i <= 8; i++)
                {
                    HoiChan_ThanhVienThamGiaDTO thanhvien = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien.stt = i;
                    thanhvien.loaidoituong_id = 3;
                    thanhvien.loaidoituong_ten = "Thành viên";
                    lstThanhVienThamGia.Add(thanhvien);
                }

                gridControlThanhVienThamGia.DataSource = lstThanhVienThamGia;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Nut Chuc Nang
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                string sql_saveHoichan = "";
                MrdHsbaHoiChanDTO HsbaHoiChanSave = new MrdHsbaHoiChanDTO();

                //thong tin chung BN

                HsbaHoiChanSave.hosobenhanid = this.currentMedicalrecord.hosobenhanid;
                HsbaHoiChanSave.medicalrecordid = this.currentMedicalrecord.medicalrecordid;
                HsbaHoiChanSave.patientid = this.currentMedicalrecord.patientid;
                HsbaHoiChanSave.vienphiid = this.currentMedicalrecord.vienphiid;
                HsbaHoiChanSave.departmentgroupid = this.currentMedicalrecord.departmentgroupid;
                HsbaHoiChanSave.departmentid = this.currentMedicalrecord.departmentid;

                HsbaHoiChanSave.thoigianhoichan = this.currentHsbaHoiChan.thoigianhoichan;

                //thong tin
                HsbaHoiChanSave.yeucauhoichan = txtYCHoiChan.Text;
                HsbaHoiChanSave.diadiemhoichan = txtHopTai.Text;
                HsbaHoiChanSave.dbb_tomtattiensubenh = txtTomTatTienSuBenh.Text;
                HsbaHoiChanSave.dbb_tinhtranglucvaovien = txtTinhTrangLucVaoVien.Text;
                HsbaHoiChanSave.dbb_chandoantuyenduoi = txtChanDoanTuyenDuoi.Text;
                HsbaHoiChanSave.dbb_tomtatdienbienbenh = txtTomTatDienBienBenh.Text;
                HsbaHoiChanSave.yk_chandoantienluong = txtChanDoanTienLuong.Text;
                HsbaHoiChanSave.yk_phuongphapdieutri = txtPhuongPhapDieuTri.Text;
                HsbaHoiChanSave.yk_chamsoc = txtChamSoc.Text;
                HsbaHoiChanSave.kl_ketluan = txtKetLuan.Text;

                //Thanh vien
                HsbaHoiChanSave.tvtg_chutoa_ten = gridViewThanhVienThamGia.GetRowCellValue(0, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_chutoa_cdcv = gridViewThanhVienThamGia.GetRowCellValue(0, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thuky_ten = gridViewThanhVienThamGia.GetRowCellValue(1, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thuky_cdcv = gridViewThanhVienThamGia.GetRowCellValue(1, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien1_ten = gridViewThanhVienThamGia.GetRowCellValue(2, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien1_cdcv = gridViewThanhVienThamGia.GetRowCellValue(2, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien2_ten = gridViewThanhVienThamGia.GetRowCellValue(3, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien2_cdcv = gridViewThanhVienThamGia.GetRowCellValue(3, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien3_ten = gridViewThanhVienThamGia.GetRowCellValue(4, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien3_cdcv = gridViewThanhVienThamGia.GetRowCellValue(4, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien4_ten = gridViewThanhVienThamGia.GetRowCellValue(5, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien4_cdcv = gridViewThanhVienThamGia.GetRowCellValue(5, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien5_ten = gridViewThanhVienThamGia.GetRowCellValue(6, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien5_cdcv = gridViewThanhVienThamGia.GetRowCellValue(6, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien6_ten = gridViewThanhVienThamGia.GetRowCellValue(7, "hovaten").ToString();
                HsbaHoiChanSave.tvtg_thanhvien6_cdcv = gridViewThanhVienThamGia.GetRowCellValue(7, "chucdanhchucvu").ToString();

                //List<HoiChan_ThanhVienThamGiaDTO> lstThanhVienThamGia_Save = new List<HoiChan_ThanhVienThamGiaDTO>();
                //for (int i = 0; i < gridViewThanhVienThamGia.RowCount; i++)
                //{
                //    HoiChan_ThanhVienThamGiaDTO thanhvien = new HoiChan_ThanhVienThamGiaDTO();
                //    thanhvien.stt = Utilities.Util_TypeConvertParse.ToInt32(gridViewThanhVienThamGia.GetRowCellValue(i, "stt").ToString());
                //    thanhvien.loaidoituong_id = Utilities.Util_TypeConvertParse.ToInt32(gridViewThanhVienThamGia.GetRowCellValue(i, "loaidoituong_id").ToString());
                //    thanhvien.loaidoituong_ten = gridViewThanhVienThamGia.GetRowCellValue(i, "loaidoituong_ten").ToString();
                //    thanhvien.hovaten = gridViewThanhVienThamGia.GetRowCellValue(i, "hovaten").ToString();
                //    thanhvien.chucdanhchucvu = gridViewThanhVienThamGia.GetRowCellValue(i, "chucdanhchucvu").ToString();
                //    lstThanhVienThamGia_Save.Add(thanhvien);
                //}
                switch (this.currentloaihoichan)
                {
                    case 1: //Hoi chan PTTT
                        {
                            HsbaHoiChanSave.maubenhphamid = this.currentHsbaHoiChan.maubenhphamid;
                            HsbaHoiChanSave.servicepriceid = this.currentHsbaHoiChan.servicepriceid;
                            HsbaHoiChanSave.servicepricecode = this.currentHsbaHoiChan.servicepricecode;
                            HsbaHoiChanSave.servicepricename = this.currentHsbaHoiChan.servicepricename;
                            HsbaHoiChanSave.servicepricedate = this.currentHsbaHoiChan.servicepricedate;

                            sql_saveHoichan = "INSERT INTO mrd_hsba_hcpttt(mrd_dmhc_ptttid, maubenhphamid, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichan, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcptttstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.maubenhphamid + "', '" + this.currentHsbaHoiChan.servicepriceid + "', '" + this.currentHsbaHoiChan.servicepricecode + "', '" + this.currentHsbaHoiChan.servicepricename + "', '" + this.currentHsbaHoiChan.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.yeucauhoichan + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            break;
                        }
                    case 2: //Hoi CHan THuoc
                        {
                            HsbaHoiChanSave.maubenhphamid = this.currentHsbaHoiChan.maubenhphamid;
                            HsbaHoiChanSave.servicepriceid = this.currentHsbaHoiChan.servicepriceid;
                            HsbaHoiChanSave.servicepricecode = this.currentHsbaHoiChan.servicepricecode;
                            HsbaHoiChanSave.servicepricename = this.currentHsbaHoiChan.servicepricename;
                            HsbaHoiChanSave.servicepricedate = this.currentHsbaHoiChan.servicepricedate;

                            sql_saveHoichan = "INSERT INTO mrd_hsba_hcthuoc(mrd_dmhc_thuocid, maubenhphamid, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichan, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcptttstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.maubenhphamid + "', '" + this.currentHsbaHoiChan.servicepriceid + "', '" + this.currentHsbaHoiChan.servicepricecode + "', '" + this.currentHsbaHoiChan.servicepricename + "', '" + this.currentHsbaHoiChan.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.yeucauhoichan + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            break;
                        }
                    case 3://Hoi chan chuyen vien
                        {
                            sql_saveHoichan = "INSERT INTO mrd_hsba_hccvien(mrd_dmhc_cvienid, his_chuyenvienid, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichan, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hccvienstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.his_chuyenvienid + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.thoigianhoichan + "', '" + HsbaHoiChanSave.yeucauhoichan + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                            break;
                        }
                    default:
                        break;
                }

                if (condb.ExecuteNonQuery_HSBA(sql_saveHoichan))
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.LUU_THANH_CONG);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void btnInTrichBienBanHC_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void btnInSoBienBanHC_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void btnInTatCa_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion
    }
}
