using Aspose.Words.Rendering;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.PrintPreview;
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
        private MrdHsbaHoiChanDTO HsbaHoiChanSave { get; set; }
        private List<HoiChan_ThanhVienThamGiaDTO> lstdataDSNVBenhVien { get; set; }
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
                LoadControlDefault();
                LoadDanhMucYeuCauHoiChan();
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
        private void LoadControlDefault()
        {
            try
            {
                btnInTrichBienBanHC.Enabled = false;
                btnInSoBienBanHC.Enabled = false;
                btnInTatCa.Enabled = false;

                if (this.currentMedicalrecord.medicalrecordstatus != 2) //BN da ket thuc dieu tri
                {
                    if (this.currentloaihoichan == 3) //chuyen vien
                    {
                        btnLuuLai.Enabled = true;
                    }
                    else
                    {
                        btnLuuLai.Enabled = false;
                    }
                }
                else
                {
                    btnLuuLai.Enabled = true;
                }
               // repositoryItemGridLookUp_HoTen.ImmediatePopup = true;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhMucYeuCauHoiChan()
        {
            try
            {
                string getdanhmuc = "select o.mrd_otherlistid, o.mrd_otherlistcode, o.mrd_otherlistname from mrd_otherlist o inner join mrd_othertypelist ot on ot.mrd_othertypelistid=o.mrd_othertypelistid where ot.mrd_othertypelistcode='HC_YCHC' order by o.mrd_otherlistname; ";
                DataTable dataYCHC = condb.GetDataTable_HSBA(getdanhmuc);
                if (dataYCHC != null && dataYCHC.Rows.Count > 0)
                {
                    //for (int i = 0; i < dataYCHC.Rows.Count; i++)
                    //{
                    //    cboYCHoiChan.Properties.Items.Add(dataYCHC.Rows[i]["mrd_otherlistname"].ToString());
                    //}
                    cboYCHoiChan.Properties.DataSource = dataYCHC;
                    cboYCHoiChan.Properties.DisplayMember = "mrd_otherlistname";
                    cboYCHoiChan.Properties.ValueMember = "mrd_otherlistid";
                    //cboYCHoiChan.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup; //dòng này để gridcontrol trong GridlookupEdit tự động resize các column để không thừa không thiếu nội dung 
                    //cboYCHoiChan.Properties.ImmediatePopup = true; // dòng này tự động mở popup khi search có kết quả
                    //cboYCHoiChan.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; //Setup dòng này để có thể nhập vào gridlookup
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
                    cboYCHoiChan.EditValue = this.currentHsbaHoiChan.yeucauhoichanid;
                   // cboYCHoiChan.Text = this.currentHsbaHoiChan.yeucauhoichanname;
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
                    thanhvien_chutich.hovaten_code = this.currentHsbaHoiChan.tvtg_chutoa_code;
                    thanhvien_chutich.hovaten = this.currentHsbaHoiChan.tvtg_chutoa_code;
                    thanhvien_chutich.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_chutoa_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_chutich);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_thuky = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_thuky.stt = 2;
                    thanhvien_thuky.loaidoituong_id = 2;
                    thanhvien_thuky.loaidoituong_ten = "Thư ký";
                    thanhvien_thuky.hovaten_code = this.currentHsbaHoiChan.tvtg_thuky_code;
                    thanhvien_thuky.hovaten = this.currentHsbaHoiChan.tvtg_thuky_code;
                    thanhvien_thuky.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thuky_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_thuky);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_1 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_1.stt = 3;
                    thanhvien_1.loaidoituong_id = 3;
                    thanhvien_1.loaidoituong_ten = "Thành viên";
                    thanhvien_1.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien1_code;
                    thanhvien_1.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien1_code;
                    thanhvien_1.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien1_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_1);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_2 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_2.stt = 4;
                    thanhvien_2.loaidoituong_id = 3;
                    thanhvien_2.loaidoituong_ten = "Thành viên";
                    thanhvien_2.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien2_code;
                    thanhvien_2.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien2_code;
                    thanhvien_2.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien2_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_2);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_3 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_3.stt = 5;
                    thanhvien_3.loaidoituong_id = 3;
                    thanhvien_3.loaidoituong_ten = "Thành viên";
                    thanhvien_3.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien3_code;
                    thanhvien_3.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien3_code;
                    thanhvien_3.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien3_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_3);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_4 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_4.stt = 6;
                    thanhvien_4.loaidoituong_id = 3;
                    thanhvien_4.loaidoituong_ten = "Thành viên";
                    thanhvien_4.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien4_code;
                    thanhvien_4.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien4_code;
                    thanhvien_4.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien4_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_4);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_5 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_5.stt = 7;
                    thanhvien_5.loaidoituong_id = 3;
                    thanhvien_5.loaidoituong_ten = "Thành viên";
                    thanhvien_5.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien5_code;
                    thanhvien_5.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien5_code;
                    thanhvien_5.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien5_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_5);

                    HoiChan_ThanhVienThamGiaDTO thanhvien_6 = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien_6.stt = 8;
                    thanhvien_6.loaidoituong_id = 3;
                    thanhvien_6.loaidoituong_ten = "Thành viên";
                    thanhvien_6.hovaten_code = this.currentHsbaHoiChan.tvtg_thanhvien6_code;
                    thanhvien_6.hovaten = this.currentHsbaHoiChan.tvtg_thanhvien6_code;
                    thanhvien_6.chucdanhchucvu = this.currentHsbaHoiChan.tvtg_thanhvien6_cdcv;
                    lstThanhVienThamGia.Add(thanhvien_6);

                    LoadDanhSachVeThanhVienThamGia();
                    gridControlThanhVienThamGia.DataSource = lstThanhVienThamGia;
                    //LoadDanhSachVeThanhVienThamGia();
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
                LoadDanhSachVeThanhVienThamGia();
                if (this.currentMedicalrecord.departmentgroupname == null || this.currentMedicalrecord.departmentgroupname == "")
                {
                    string gettenkhoa = "select mrd.giuong, de.departmentname, degp.departmentgroupname from medicalrecord mrd inner join department de on de.departmentid=mrd.departmentid inner join departmentgroup degp on degp.departmentgroupid=mrd.departmentgroupid where mrd.medicalrecordid=" + this.currentMedicalrecord.medicalrecordid + ";";

                    DataTable datakhoa = condb.GetDataTable_HIS(gettenkhoa);
                    if (datakhoa != null && datakhoa.Rows.Count > 0)
                    {
                        txtHopTai.Text = datakhoa.Rows[0]["departmentgroupname"].ToString();
                        this.currentMedicalrecord.giuong = datakhoa.Rows[0]["giuong"].ToString();
                        this.currentMedicalrecord.departmentname = datakhoa.Rows[0]["departmentname"].ToString();
                        this.currentMedicalrecord.departmentgroupname = datakhoa.Rows[0]["departmentgroupname"].ToString();
                    }
                }
                else
                {
                    txtHopTai.Text = this.currentMedicalrecord.departmentgroupname;
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
                thanhvien_chutich.hovaten_code = "";
                thanhvien_chutich.hovaten = "";
                thanhvien_chutich.chucdanhchucvu = "";
                lstThanhVienThamGia.Add(thanhvien_chutich);

                HoiChan_ThanhVienThamGiaDTO thanhvien_thuky = new HoiChan_ThanhVienThamGiaDTO();
                thanhvien_thuky.stt = 2;
                thanhvien_thuky.loaidoituong_id = 2;
                thanhvien_thuky.loaidoituong_ten = "Thư ký";
                thanhvien_thuky.hovaten_code = "";
                thanhvien_thuky.hovaten = "";
                thanhvien_thuky.chucdanhchucvu = "";
                lstThanhVienThamGia.Add(thanhvien_thuky);

                for (int i = 3; i <= 8; i++)
                {
                    HoiChan_ThanhVienThamGiaDTO thanhvien = new HoiChan_ThanhVienThamGiaDTO();
                    thanhvien.stt = i;
                    thanhvien.loaidoituong_id = 3;
                    thanhvien.loaidoituong_ten = "Thành viên";
                    thanhvien.hovaten_code = "";
                    thanhvien.hovaten = "";
                    thanhvien.chucdanhchucvu = "";
                    lstThanhVienThamGia.Add(thanhvien);
                }

                gridControlThanhVienThamGia.DataSource = lstThanhVienThamGia;
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachVeThanhVienThamGia()
        {
            try
            {
                string layDSNhanVienBenhVien = "SELECT usercode, username FROM tools_tblnhanvien WHERE username<>'' GROUP BY usercode, username ORDER BY username";
                DataTable dataDSNVBenhVien = condb.GetDataTable_HIS(layDSNhanVienBenhVien);
                if (dataDSNVBenhVien != null && dataDSNVBenhVien.Rows.Count > 0)
                {
                    this.lstdataDSNVBenhVien = new List<HoiChan_ThanhVienThamGiaDTO>();
                    repositoryItemGridLookUp_HoTen.DataSource = dataDSNVBenhVien;
                    repositoryItemGridLookUp_HoTen.DisplayMember = "username";
                    repositoryItemGridLookUp_HoTen.ValueMember = "usercode";
                    for (int i = 0; i < dataDSNVBenhVien.Rows.Count; i++)
                    {
                        HoiChan_ThanhVienThamGiaDTO dataThanhVien = new HoiChan_ThanhVienThamGiaDTO();
                        dataThanhVien.hovaten_code = dataDSNVBenhVien.Rows[i]["usercode"].ToString();
                        dataThanhVien.hovaten = dataDSNVBenhVien.Rows[i]["username"].ToString();
                        this.lstdataDSNVBenhVien.Add(dataThanhVien);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion

        #region Nut Chuc Nang
        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                string sql_saveHoichan = "";
                HsbaHoiChanSave = new MrdHsbaHoiChanDTO();

                //thong tin chung BN

                HsbaHoiChanSave.hosobenhanid = this.currentMedicalrecord.hosobenhanid;
                HsbaHoiChanSave.medicalrecordid = this.currentMedicalrecord.medicalrecordid;
                HsbaHoiChanSave.patientid = this.currentMedicalrecord.patientid;
                HsbaHoiChanSave.vienphiid = this.currentMedicalrecord.vienphiid;
                HsbaHoiChanSave.departmentgroupid = this.currentMedicalrecord.departmentgroupid;
                HsbaHoiChanSave.departmentid = this.currentMedicalrecord.departmentid;

                HsbaHoiChanSave.thoigianhoichan = this.currentHsbaHoiChan.thoigianhoichan;

                //thong tin
                if (cboYCHoiChan.EditValue != null)
                {
                    HsbaHoiChanSave.yeucauhoichanid = Utilities.Util_TypeConvertParse.ToInt64(cboYCHoiChan.EditValue.ToString());
                }
                HsbaHoiChanSave.yeucauhoichanname = cboYCHoiChan.Text;
                HsbaHoiChanSave.diadiemhoichan = txtHopTai.Text.Replace("'", "''");
                HsbaHoiChanSave.dbb_tomtattiensubenh = txtTomTatTienSuBenh.Text.Replace("'", "''");
                HsbaHoiChanSave.dbb_tinhtranglucvaovien = txtTinhTrangLucVaoVien.Text.Replace("'", "''");
                HsbaHoiChanSave.dbb_chandoantuyenduoi = txtChanDoanTuyenDuoi.Text.Replace("'", "''");
                HsbaHoiChanSave.dbb_tomtatdienbienbenh = txtTomTatDienBienBenh.Text.Replace("'", "''");
                HsbaHoiChanSave.yk_chandoantienluong = txtChanDoanTienLuong.Text.Replace("'", "''");
                HsbaHoiChanSave.yk_phuongphapdieutri = txtPhuongPhapDieuTri.Text.Replace("'", "''");
                HsbaHoiChanSave.yk_chamsoc = txtChamSoc.Text.Replace("'", "''");
                HsbaHoiChanSave.kl_ketluan = txtKetLuan.Text.Replace("'", "''");

                //Thanh vien
                HsbaHoiChanSave.tvtg_chutoa_code = gridViewThanhVienThamGia.GetRowCellValue(0, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_chutoa_code != "")
                {
                    HsbaHoiChanSave.tvtg_chutoa_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_chutoa_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_chutoa_cdcv = gridViewThanhVienThamGia.GetRowCellValue(0, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thuky_code = gridViewThanhVienThamGia.GetRowCellValue(1, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thuky_code != "")
                {
                    HsbaHoiChanSave.tvtg_thuky_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thuky_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thuky_cdcv = gridViewThanhVienThamGia.GetRowCellValue(1, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien1_code = gridViewThanhVienThamGia.GetRowCellValue(2, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien1_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien1_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien1_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien1_cdcv = gridViewThanhVienThamGia.GetRowCellValue(2, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien2_code = gridViewThanhVienThamGia.GetRowCellValue(3, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien2_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien2_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien2_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien2_cdcv = gridViewThanhVienThamGia.GetRowCellValue(3, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien3_code = gridViewThanhVienThamGia.GetRowCellValue(4, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien3_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien3_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien3_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien3_cdcv = gridViewThanhVienThamGia.GetRowCellValue(4, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien4_code = gridViewThanhVienThamGia.GetRowCellValue(5, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien4_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien4_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien4_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien4_cdcv = gridViewThanhVienThamGia.GetRowCellValue(5, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien5_code = gridViewThanhVienThamGia.GetRowCellValue(6, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien5_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien5_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien5_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien5_cdcv = gridViewThanhVienThamGia.GetRowCellValue(6, "chucdanhchucvu").ToString();
                HsbaHoiChanSave.tvtg_thanhvien6_code = gridViewThanhVienThamGia.GetRowCellValue(7, "hovaten").ToString();
                if (HsbaHoiChanSave.tvtg_thanhvien6_code != "")
                {
                    HsbaHoiChanSave.tvtg_thanhvien6_ten = this.lstdataDSNVBenhVien.Where(o => o.hovaten_code == HsbaHoiChanSave.tvtg_thanhvien6_code).Single().hovaten;
                }
                HsbaHoiChanSave.tvtg_thanhvien6_cdcv = gridViewThanhVienThamGia.GetRowCellValue(7, "chucdanhchucvu").ToString();

                if (this.currentHsbaHoiChan.mrd_hsba_hcid == 0)//them moi
                {
                    switch (this.currentloaihoichan)
                    {
                        case 1: //Hoi chan PTTT
                            {
                                HsbaHoiChanSave.maubenhphamid = this.currentHsbaHoiChan.maubenhphamid;
                                HsbaHoiChanSave.servicepriceid = this.currentHsbaHoiChan.servicepriceid;
                                HsbaHoiChanSave.servicepricecode = this.currentHsbaHoiChan.servicepricecode;
                                HsbaHoiChanSave.servicepricename = this.currentHsbaHoiChan.servicepricename;
                                HsbaHoiChanSave.servicepricedate = this.currentHsbaHoiChan.servicepricedate;

                                sql_saveHoichan = "INSERT INTO mrd_hsba_hcpttt(mrd_dmhc_ptttid, maubenhphamid, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcptttstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.maubenhphamid + "', '" + this.currentHsbaHoiChan.servicepriceid + "', '" + this.currentHsbaHoiChan.servicepricecode + "', '" + this.currentHsbaHoiChan.servicepricename + "', '" + this.currentHsbaHoiChan.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.yeucauhoichanid + "', '" + HsbaHoiChanSave.yeucauhoichanname + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_code + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_code + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                break;
                            }
                        case 2: //Hoi CHan THuoc
                            {
                                HsbaHoiChanSave.medicinerefid_org = this.currentHsbaHoiChan.medicinerefid_org;
                                HsbaHoiChanSave.maubenhphamid = this.currentHsbaHoiChan.maubenhphamid;
                                HsbaHoiChanSave.servicepriceid = this.currentHsbaHoiChan.servicepriceid;
                                HsbaHoiChanSave.servicepricecode = this.currentHsbaHoiChan.servicepricecode;
                                HsbaHoiChanSave.servicepricename = this.currentHsbaHoiChan.servicepricename;
                                HsbaHoiChanSave.servicepricedate = this.currentHsbaHoiChan.servicepricedate;

                                sql_saveHoichan = "INSERT INTO mrd_hsba_hcthuoc(mrd_dmhc_thuocid, maubenhphamid, medicinerefid_org, servicepriceid, servicepricecode, servicepricename, servicepricedate, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hcthuocstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.maubenhphamid + "', '" + HsbaHoiChanSave.medicinerefid_org + "', '" + this.currentHsbaHoiChan.servicepriceid + "', '" + this.currentHsbaHoiChan.servicepricecode + "', '" + this.currentHsbaHoiChan.servicepricename + "', '" + this.currentHsbaHoiChan.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.servicepricedate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.yeucauhoichanid + "', '" + HsbaHoiChanSave.yeucauhoichanname + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_code + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_code + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                break;
                            }
                        case 3://Hoi chan chuyen vien
                            {
                                sql_saveHoichan = "INSERT INTO mrd_hsba_hccvien(mrd_dmhc_cvienid, his_chuyenvienid, hosobenhanid, medicalrecordid, patientid, vienphiid, departmentgroupid, departmentid, thoigianhoichan, yeucauhoichanid, yeucauhoichanname, diadiemhoichan, dbb_tomtattiensubenh, dbb_tinhtranglucvaovien, dbb_chandoantuyenduoi, dbb_tomtatdienbienbenh, yk_chandoantienluong, yk_phuongphapdieutri, yk_chamsoc, kl_ketluan, tvtg_chutoa_code, tvtg_chutoa_ten, tvtg_chutoa_cdcv, tvtg_thuky_code, tvtg_thuky_ten, tvtg_thuky_cdcv, tvtg_thanhvien1_code, tvtg_thanhvien1_ten, tvtg_thanhvien1_cdcv, tvtg_thanhvien2_code, tvtg_thanhvien2_ten, tvtg_thanhvien2_cdcv, tvtg_thanhvien3_code, tvtg_thanhvien3_ten, tvtg_thanhvien3_cdcv, tvtg_thanhvien4_code, tvtg_thanhvien4_ten, tvtg_thanhvien4_cdcv, tvtg_thanhvien5_code, tvtg_thanhvien5_ten, tvtg_thanhvien5_cdcv, tvtg_thanhvien6_code, tvtg_thanhvien6_ten, tvtg_thanhvien6_cdcv, mrd_hsba_hccvienstatus, create_mrduserid, create_mrdusercode, create_date) VALUES (0, '" + this.currentHsbaHoiChan.his_chuyenvienid + "', '" + HsbaHoiChanSave.hosobenhanid + "', '" + HsbaHoiChanSave.medicalrecordid + "', '" + HsbaHoiChanSave.patientid + "', '" + HsbaHoiChanSave.vienphiid + "', '" + HsbaHoiChanSave.departmentgroupid + "', '" + HsbaHoiChanSave.departmentid + "', '" + HsbaHoiChanSave.thoigianhoichan.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + HsbaHoiChanSave.yeucauhoichanid + "', '" + HsbaHoiChanSave.yeucauhoichanname + "', '" + HsbaHoiChanSave.diadiemhoichan + "', '" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', '" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', '" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', '" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', '" + HsbaHoiChanSave.yk_chandoantienluong + "', '" + HsbaHoiChanSave.yk_phuongphapdieutri + "', '" + HsbaHoiChanSave.yk_chamsoc + "', '" + HsbaHoiChanSave.kl_ketluan + "', '" + HsbaHoiChanSave.tvtg_chutoa_code + "', '" + HsbaHoiChanSave.tvtg_chutoa_ten + "', '" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', '" + HsbaHoiChanSave.tvtg_thuky_code + "', '" + HsbaHoiChanSave.tvtg_thuky_ten + "', '" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', '" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', '" + 1 + "', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'); ";
                                break;
                            }
                        default:
                            break;
                    }
                }
                else //chinh sua
                {
                    switch (this.currentloaihoichan)
                    {
                        case 1: //Hoi chan PTTT
                            {
                                sql_saveHoichan = "UPDATE mrd_hsba_hcpttt SET yeucauhoichanid='" + HsbaHoiChanSave.yeucauhoichanid + "', yeucauhoichanname='" + HsbaHoiChanSave.yeucauhoichanname + "', diadiemhoichan='" + HsbaHoiChanSave.diadiemhoichan + "', dbb_tomtattiensubenh='" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', dbb_tinhtranglucvaovien='" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', dbb_chandoantuyenduoi='" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', dbb_tomtatdienbienbenh='" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', yk_chandoantienluong='" + HsbaHoiChanSave.yk_chandoantienluong + "', yk_phuongphapdieutri='" + HsbaHoiChanSave.yk_phuongphapdieutri + "', yk_chamsoc='" + HsbaHoiChanSave.yk_chamsoc + "', kl_ketluan='" + HsbaHoiChanSave.kl_ketluan + "', tvtg_chutoa_code='" + HsbaHoiChanSave.tvtg_chutoa_code + "', tvtg_chutoa_ten='" + HsbaHoiChanSave.tvtg_chutoa_ten + "', tvtg_chutoa_cdcv='" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', tvtg_thuky_code='" + HsbaHoiChanSave.tvtg_thuky_code + "', tvtg_thuky_ten='" + HsbaHoiChanSave.tvtg_thuky_ten + "', tvtg_thuky_cdcv='" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', tvtg_thanhvien1_code='" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', tvtg_thanhvien1_ten='" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', tvtg_thanhvien1_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', tvtg_thanhvien2_code='" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', tvtg_thanhvien2_ten='" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', tvtg_thanhvien2_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', tvtg_thanhvien3_code='" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', tvtg_thanhvien3_ten='" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', tvtg_thanhvien3_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', tvtg_thanhvien4_code='" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', tvtg_thanhvien4_ten='" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', tvtg_thanhvien4_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', tvtg_thanhvien5_code='" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', tvtg_thanhvien5_ten='" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', tvtg_thanhvien5_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', tvtg_thanhvien6_code='" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', tvtg_thanhvien6_ten='" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', tvtg_thanhvien6_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', modify_mrduserid='" + Base.SessionLogin.SessionUserID + "', modify_mrdusercode='" + Base.SessionLogin.SessionUsercode + "', modify_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE mrd_hsba_hcptttid=" + this.currentHsbaHoiChan.mrd_hsba_hcid + ";  ";
                                break;
                            }
                        case 2: //Hoi CHan THuoc
                            {
                                sql_saveHoichan = "UPDATE mrd_hsba_hcthuoc SET yeucauhoichanid='" + HsbaHoiChanSave.yeucauhoichanid + "', yeucauhoichanname='" + HsbaHoiChanSave.yeucauhoichanname + "', diadiemhoichan='" + HsbaHoiChanSave.diadiemhoichan + "', dbb_tomtattiensubenh='" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', dbb_tinhtranglucvaovien='" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', dbb_chandoantuyenduoi='" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', dbb_tomtatdienbienbenh='" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', yk_chandoantienluong='" + HsbaHoiChanSave.yk_chandoantienluong + "', yk_phuongphapdieutri='" + HsbaHoiChanSave.yk_phuongphapdieutri + "', yk_chamsoc='" + HsbaHoiChanSave.yk_chamsoc + "', kl_ketluan='" + HsbaHoiChanSave.kl_ketluan + "', tvtg_chutoa_code='" + HsbaHoiChanSave.tvtg_chutoa_code + "', tvtg_chutoa_ten='" + HsbaHoiChanSave.tvtg_chutoa_ten + "', tvtg_chutoa_cdcv='" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', tvtg_thuky_code='" + HsbaHoiChanSave.tvtg_thuky_code + "', tvtg_thuky_ten='" + HsbaHoiChanSave.tvtg_thuky_ten + "', tvtg_thuky_cdcv='" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', tvtg_thanhvien1_code='" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', tvtg_thanhvien1_ten='" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', tvtg_thanhvien1_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', tvtg_thanhvien2_code='" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', tvtg_thanhvien2_ten='" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', tvtg_thanhvien2_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', tvtg_thanhvien3_code='" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', tvtg_thanhvien3_ten='" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', tvtg_thanhvien3_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', tvtg_thanhvien4_code='" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', tvtg_thanhvien4_ten='" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', tvtg_thanhvien4_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', tvtg_thanhvien5_code='" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', tvtg_thanhvien5_ten='" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', tvtg_thanhvien5_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', tvtg_thanhvien6_code='" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', tvtg_thanhvien6_ten='" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', tvtg_thanhvien6_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', modify_mrduserid='" + Base.SessionLogin.SessionUserID + "', modify_mrdusercode='" + Base.SessionLogin.SessionUsercode + "', modify_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE mrd_hsba_hcthuocid=" + this.currentHsbaHoiChan.mrd_hsba_hcid + "; ";
                                break;
                            }
                        case 3://Hoi chan chuyen vien
                            {
                                sql_saveHoichan = "UPDATE mrd_hsba_hccvien SET yeucauhoichanid='" + HsbaHoiChanSave.yeucauhoichanid + "', yeucauhoichanname='" + HsbaHoiChanSave.yeucauhoichanname + "', diadiemhoichan='" + HsbaHoiChanSave.diadiemhoichan + "', dbb_tomtattiensubenh='" + HsbaHoiChanSave.dbb_tomtattiensubenh + "', dbb_tinhtranglucvaovien='" + HsbaHoiChanSave.dbb_tinhtranglucvaovien + "', dbb_chandoantuyenduoi='" + HsbaHoiChanSave.dbb_chandoantuyenduoi + "', dbb_tomtatdienbienbenh='" + HsbaHoiChanSave.dbb_tomtatdienbienbenh + "', yk_chandoantienluong='" + HsbaHoiChanSave.yk_chandoantienluong + "', yk_phuongphapdieutri='" + HsbaHoiChanSave.yk_phuongphapdieutri + "', yk_chamsoc='" + HsbaHoiChanSave.yk_chamsoc + "', kl_ketluan='" + HsbaHoiChanSave.kl_ketluan + "', tvtg_chutoa_code='" + HsbaHoiChanSave.tvtg_chutoa_code + "', tvtg_chutoa_ten='" + HsbaHoiChanSave.tvtg_chutoa_ten + "', tvtg_chutoa_cdcv='" + HsbaHoiChanSave.tvtg_chutoa_cdcv + "', tvtg_thuky_code='" + HsbaHoiChanSave.tvtg_thuky_code + "', tvtg_thuky_ten='" + HsbaHoiChanSave.tvtg_thuky_ten + "', tvtg_thuky_cdcv='" + HsbaHoiChanSave.tvtg_thuky_cdcv + "', tvtg_thanhvien1_code='" + HsbaHoiChanSave.tvtg_thanhvien1_code + "', tvtg_thanhvien1_ten='" + HsbaHoiChanSave.tvtg_thanhvien1_ten + "', tvtg_thanhvien1_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "', tvtg_thanhvien2_code='" + HsbaHoiChanSave.tvtg_thanhvien2_code + "', tvtg_thanhvien2_ten='" + HsbaHoiChanSave.tvtg_thanhvien2_ten + "', tvtg_thanhvien2_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "', tvtg_thanhvien3_code='" + HsbaHoiChanSave.tvtg_thanhvien3_code + "', tvtg_thanhvien3_ten='" + HsbaHoiChanSave.tvtg_thanhvien3_ten + "', tvtg_thanhvien3_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "', tvtg_thanhvien4_code='" + HsbaHoiChanSave.tvtg_thanhvien4_code + "', tvtg_thanhvien4_ten='" + HsbaHoiChanSave.tvtg_thanhvien4_ten + "', tvtg_thanhvien4_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "', tvtg_thanhvien5_code='" + HsbaHoiChanSave.tvtg_thanhvien5_code + "', tvtg_thanhvien5_ten='" + HsbaHoiChanSave.tvtg_thanhvien5_ten + "', tvtg_thanhvien5_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "', tvtg_thanhvien6_code='" + HsbaHoiChanSave.tvtg_thanhvien6_code + "', tvtg_thanhvien6_ten='" + HsbaHoiChanSave.tvtg_thanhvien6_ten + "', tvtg_thanhvien6_cdcv='" + HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "', modify_mrduserid='" + Base.SessionLogin.SessionUserID + "', modify_mrdusercode='" + Base.SessionLogin.SessionUsercode + "', modify_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE mrd_hsba_hccvienid=" + this.currentHsbaHoiChan.mrd_hsba_hcid + "; ";
                                break;
                            }
                        default:
                            break;
                    }
                }

                if (condb.ExecuteNonQuery_HSBA(sql_saveHoichan))
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.LUU_THANH_CONG);
                    frmthongbao.Show();
                    btnInTrichBienBanHC.Enabled = true;
                    btnInSoBienBanHC.Enabled = true;
                    btnInTatCa.Enabled = true;

                    string layIDhoichan = "";
                    switch (this.currentloaihoichan)
                    {
                        case 1: //Hoi chan PTTT
                            {
                                layIDhoichan = "select mrd_hsba_hcptttid as mrd_hsba_hcid from mrd_hsba_hcpttt where servicepriceid=" + this.currentHsbaHoiChan.servicepriceid + "; ";
                                break;
                            }
                        case 2: //Hoi chan thuoc
                            {
                                layIDhoichan = "select mrd_hsba_hcthuocid as mrd_hsba_hcid from mrd_hsba_hcthuoc where servicepriceid=" + this.currentHsbaHoiChan.servicepriceid + "; ";
                                break;
                            }
                        case 3: //Hoi chan chuyen vien
                            {
                                layIDhoichan = "select mrd_hsba_hccvienid as mrd_hsba_hcid from mrd_hsba_hccvien where medicalrecordid=" + this.HsbaHoiChanSave.medicalrecordid + "; ";
                                break;
                            }
                        default:
                            break;
                    }
                    DataTable dataLayIDhoichan = condb.GetDataTable_HSBA(layIDhoichan);
                    if (dataLayIDhoichan != null && dataLayIDhoichan.Rows.Count > 0)
                    {
                        this.currentHsbaHoiChan.mrd_hsba_hcid = Utilities.Util_TypeConvertParse.ToInt64(dataLayIDhoichan.Rows[0]["mrd_hsba_hcid"].ToString());
                    }
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
                string templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.BienBanHoiChan_Path + "\\40. Trich bien ban hoi chuan.DOC";

                string hoichan_fulltime2 = ".......giờ .......phút, ngày....../..../.....";
                if (this.currentloaihoichan == 1 || this.currentloaihoichan == 2)//hoi chan PTTT
                {
                    hoichan_fulltime2 = this.currentHsbaHoiChan.servicepricedate.Hour + " giờ " + this.currentHsbaHoiChan.servicepricedate.Minute + " phút, ngày " + this.currentHsbaHoiChan.servicepricedate.Day + "/" + this.currentHsbaHoiChan.servicepricedate.Month + "/" + this.currentHsbaHoiChan.servicepricedate.Year;
                }
                else //hoi chan chuyen vien
                {
                    hoichan_fulltime2 = this.HsbaHoiChanSave.thoigianhoichan.Hour + " giờ " + this.HsbaHoiChanSave.thoigianhoichan.Minute + " phút, ngày " + this.HsbaHoiChanSave.thoigianhoichan.Day + "/" + this.HsbaHoiChanSave.thoigianhoichan.Month + "/" + this.HsbaHoiChanSave.thoigianhoichan.Year;
                }

                string thongtinbn = "select '" + this.currentHsbaHoiChan.servicepricename.ToString() + "' as servicepricename, hsba.sovaovien as sovaovien, hsba.patientname as patientname, cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi, hsba.gioitinhname as gioitinh, (select to_char(thoigianvaovien, 'dd/mm/yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime2, (select (case when thoigianravien <> '0001-01-01 00:00:00' then to_char(thoigianravien, 'dd/mm/yyyy') end) from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_ravien_fulltime1, '" + this.currentMedicalrecord.giuong + "' as giuong, '" + this.currentMedicalrecord.departmentname + "' as buong, '" + this.currentMedicalrecord.departmentgroupname + "' as khoa, '" + this.currentMedicalrecord.chandoanbandau + "' as chandoan, '" + hoichan_fulltime2 + "' as hoichan_fulltime2, '" + this.HsbaHoiChanSave.tvtg_chutoa_ten + "' as tvtg_chutoa_ten, '" + this.HsbaHoiChanSave.tvtg_chutoa_cdcv + "' as tvtg_chutoa_cdcv, '" + this.HsbaHoiChanSave.tvtg_thuky_ten + "' as tvtg_thuky_ten, '" + this.HsbaHoiChanSave.tvtg_thuky_cdcv + "' as tvtg_thuky_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien1_ten + "' as tvtg_thanhvien1_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "' as tvtg_thanhvien1_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien2_ten + "' as tvtg_thanhvien2_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "' as tvtg_thanhvien2_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien3_ten + "' as tvtg_thanhvien3_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "' as tvtg_thanhvien3_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien4_ten + "' as tvtg_thanhvien4_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "' as tvtg_thanhvien4_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien5_ten + "' as tvtg_thanhvien5_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "' as tvtg_thanhvien5_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien6_ten + "' as tvtg_thanhvien6_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "' as tvtg_thanhvien6_cdcv, '" + this.HsbaHoiChanSave.dbb_tomtatdienbienbenh + "' as dbb_tomtatdienbienbenh, '" + this.HsbaHoiChanSave.kl_ketluan + "' as kl_ketluan, '" + this.HsbaHoiChanSave.yk_phuongphapdieutri + "' as yk_phuongphapdieutri from hosobenhan hsba where hsba.hosobenhanid=" + this.HsbaHoiChanSave.hosobenhanid + "; ";

                DataTable dataTTBenhNhan = condb.GetDataTable_HIS(thongtinbn);
                Aspose.Words.Document documentWord = new Aspose.Words.Document();
                documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMerge(templateFullPath, dataTTBenhNhan, "40. Trich bien ban hoi chuan.doc");

                Aspose.Words.Document doc = new Aspose.Words.Document(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\40. Trich bien ban hoi chuan.doc");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;
                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                using (var dlg = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewDialog())
                {
                    dlg.Document = awPrintDoc;
                    dlg.ShowDialog(this);
                }
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
                string templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.BienBanHoiChan_Path + "\\So BB hoi chuan.DOC";

                string hoichan_fulltime1 = "ngày ...... tháng ...... năm ..........; lúc ........ giờ ........ phút";
                if (this.currentloaihoichan == 1 || this.currentloaihoichan == 2)//hoi chan PTTT
                {
                    hoichan_fulltime1 = "ngày " + this.currentHsbaHoiChan.servicepricedate.Day + " tháng " + this.currentHsbaHoiChan.servicepricedate.Month + " năm " + this.currentHsbaHoiChan.servicepricedate.Year + "; lúc " + this.currentHsbaHoiChan.servicepricedate.Hour + " giờ " + this.currentHsbaHoiChan.servicepricedate.Minute + " phút";
                }
                else //hoi chan chuyen vien
                {
                    hoichan_fulltime1 = "ngày " + this.HsbaHoiChanSave.thoigianhoichan.Day + " tháng " + this.HsbaHoiChanSave.thoigianhoichan.Month + " năm " + this.HsbaHoiChanSave.thoigianhoichan.Year + "; lúc " + this.HsbaHoiChanSave.thoigianhoichan.Hour + " giờ " + this.HsbaHoiChanSave.thoigianhoichan.Minute + " phút";
                }

                string thongtinbn = "select '" + hoichan_fulltime1 + "' as hoichan_fulltime1, '" + this.HsbaHoiChanSave.tvtg_chutoa_ten + "' as tvtg_chutoa_ten, '" + this.HsbaHoiChanSave.tvtg_chutoa_cdcv + "' as tvtg_chutoa_cdcv, '" + this.HsbaHoiChanSave.tvtg_thuky_ten + "' as tvtg_thuky_ten, '" + this.HsbaHoiChanSave.tvtg_thuky_cdcv + "' as tvtg_thuky_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien1_ten + "' as tvtg_thanhvien1_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien1_cdcv + "' as tvtg_thanhvien1_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien2_ten + "' as tvtg_thanhvien2_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien2_cdcv + "' as tvtg_thanhvien2_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien3_ten + "' as tvtg_thanhvien3_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien3_cdcv + "' as tvtg_thanhvien3_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien4_ten + "' as tvtg_thanhvien4_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien4_cdcv + "' as tvtg_thanhvien4_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien5_ten + "' as tvtg_thanhvien5_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien5_cdcv + "' as tvtg_thanhvien5_cdcv, '" + this.HsbaHoiChanSave.tvtg_thanhvien6_ten + "' as tvtg_thanhvien6_ten, '" + this.HsbaHoiChanSave.tvtg_thanhvien6_cdcv + "' as tvtg_thanhvien6_cdcv, '" + this.HsbaHoiChanSave.diadiemhoichan + "' as diadiemhoichan, hsba.patientname as patientname, cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text) as tuoi, hsba.gioitinhname as gioitinh, hsba.hc_dantocname as dantoc, hsba.hc_quocgianame as ngoaikieu, '' as sohochieu, '' as ngay_noicap, hsba.nghenghiepname as nghenghiep, hsba.noilamviec as noilamviec, ((case when hsba.hc_sonha<>'' then hsba.hc_sonha || ', ' else '' end) || (case when hsba.hc_thon<>'' then hsba.hc_thon || ' - ' else '' end) || (case when hsba.hc_xacode<>'00' then hsba.hc_xaname || ' - ' else '' end) || (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname || ' - ' else '' end) || (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname || ' - ' else '' end) || hc_quocgianame) as diachi, hsba.sovaovien as sovaovien, substr(hsba.bhytcode,1,2) as bhyt_1, substr(hsba.bhytcode,3,1) as bhyt_2, substr(hsba.bhytcode,4,2) as bhyt_3, substr(hsba.bhytcode,6,2) as bhyt_4, substr(hsba.bhytcode,8,8) as bhyt_5, (select to_char(thoigianvaovien, 'hh g\"i\"ờ mi phút, ngà\"y\" dd tháng mm năm yyyy') from medicalrecord where loaibenhanid=1 and hosobenhanid=hsba.hosobenhanid order by medicalrecordid limit 1) as tg_vaovien_fulltime1, '" + this.currentMedicalrecord.departmentgroupname + "' as khoa, '" + this.HsbaHoiChanSave.yeucauhoichanname + "' as yeucauhoichanname, '" + this.HsbaHoiChanSave.dbb_tomtattiensubenh + "' as dbb_tomtattiensubenh, '" + this.HsbaHoiChanSave.dbb_tinhtranglucvaovien + "' as dbb_tinhtranglucvaovien, '" + this.HsbaHoiChanSave.dbb_chandoantuyenduoi + "' as dbb_chandoantuyenduoi, '" + this.HsbaHoiChanSave.dbb_tomtatdienbienbenh + "' as dbb_tomtatdienbienbenh, '" + this.HsbaHoiChanSave.yk_chandoantienluong + "' as yk_chandoantienluong, '" + this.HsbaHoiChanSave.yk_phuongphapdieutri + "' as yk_phuongphapdieutri, '" + this.HsbaHoiChanSave.yk_chamsoc + "' as yk_chamsoc, '" + this.HsbaHoiChanSave.kl_ketluan + "' as kl_ketluan from hosobenhan hsba where hsba.hosobenhanid=" + this.HsbaHoiChanSave.hosobenhanid + ";";

                DataTable dataTTBenhNhan = condb.GetDataTable_HIS(thongtinbn);
                Aspose.Words.Document documentWord = new Aspose.Words.Document();
                documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMerge(templateFullPath, dataTTBenhNhan, "So BB hoi chuan.doc");

                Aspose.Words.Document doc = new Aspose.Words.Document(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\So BB hoi chuan.doc");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;
                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                using (var dlg = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewDialog())
                {
                    dlg.Document = awPrintDoc;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        //Chua su dung
        private void btnInTatCa_Click(object sender, EventArgs e)
        {
            try
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\SoBBHoiChan_TrichBBHoiChan.doc");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;
                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                using (var dlg = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewDialog())
                {
                    dlg.Document = awPrintDoc;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        #endregion



        //Print su dung print mac dinh
        //private void btnInTatCa_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string fullPath = Environment.CurrentDirectory + "\\Templates\\BienBanHoiChan\\";

        //        Aspose.Words.Document doc = new Aspose.Words.Document(fullPath + "So BB hoi chuan.DOC");

        //        PrintDialog printDlg = new PrintDialog();
        //        // Initialize the print dialog with the number of pages in the document.
        //        printDlg.AllowSomePages = true;
        //        printDlg.PrinterSettings.MinimumPage = 1;
        //        printDlg.PrinterSettings.MaximumPage = doc.PageCount;
        //        printDlg.PrinterSettings.FromPage = 1;
        //        printDlg.PrinterSettings.ToPage = doc.PageCount;

        //        AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
        //        awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

        //        ActivePrintPreviewDialog previewDlg = new ActivePrintPreviewDialog();

        //        // Pass the Aspose.Words print document to the Print Preview dialog.
        //        previewDlg.Document = awPrintDoc;
        //        // Specify additional parameters of the Print Preview dialog.
        //        previewDlg.ShowInTaskbar = true;
        //        previewDlg.MinimizeBox = true;
        //        previewDlg.PrintPreviewControl.Zoom = 1;
        //        previewDlg.Document.DocumentName = "So BB hoi chuan.doc";
        //        previewDlg.WindowState = FormWindowState.Maximized;
        //        // Show the appropriately configured Print Preview dialog.
        //        previewDlg.ShowDialog();

        //    }
        //    catch (Exception ex)
        //    {
        //        Base.Logging.Error(ex);
        //    }
        //}

        //class ActivePrintPreviewDialog : PrintPreviewDialog
        //{
        //    /// <summary>
        //    /// Brings the Print Preview dialog on top when it is initially displayed. (Đưa hộp thoại Xem trước In trước lên trên khi nó được hiển thị.)
        //    /// </summary>
        //    protected override void OnShown(EventArgs e)
        //    {
        //        Activate();
        //        base.OnShown(e);
        //    }

        //}
    }
}
