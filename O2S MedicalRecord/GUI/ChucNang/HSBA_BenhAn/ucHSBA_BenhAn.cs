using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using O2S_MedicalRecord.DTO;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class ucHSBA_BenhAn : UserControl
    {
        #region Declaration
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private MrdHsbaHosobenhanDTO currentMrdHsbaHsba { get; set; }
        #endregion
        public ucHSBA_BenhAn()
        {
            InitializeComponent();
        }

        public ucHSBA_BenhAn(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.mecicalrecordCurrentDTO = filterDTO;
        }

        #region Load
        private void ucHSBA_BenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachLoaiBenhAn();
                LoadDanhSachBenhAn();
                LoadBenhAnCuaBenhNhan();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachLoaiBenhAn()
        {
            try
            {
                cboMauBenhAn.Properties.DataSource = GlobalStore.GlobalLst_MrdHsbaTemplate;
                cboMauBenhAn.Properties.DisplayMember = "mrd_hsbatemname";
                cboMauBenhAn.Properties.ValueMember = "mrd_hsbatemid";
                if (GlobalStore.GlobalLst_MrdHsbaTemplate.Count == 1)
                {
                    cboMauBenhAn.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachBenhAn()
        {
            try
            {

                //todo
                //string sqlPhieuPTT = "select ROW_NUMBER () OVER (ORDER BY mbp.maubenhphamdate) as stt, mbp.maubenhphamid, mbp.sophieu, mbp.maubenhphamdate, mbp.maubenhphamdate_sudung, mbp.maubenhphamdate_thuchien, mbp.maubenhphamfinishdate, degp.departmentgroupname, de.departmentname, nv.username as nguoichidinh, mbp.maubenhphamdatastatus from maubenhpham mbp inner join departmentgroup degp on degp.departmentgroupid=mbp.departmentgroupid left join department de on de.departmentid=mbp.departmentid left join tools_tblnhanvien nv on nv.userhisid=mbp.userid where mbp.hosobenhanid='" + this.mecicalrecordCurrentDTO.hosobenhanid + "' and mbp.maubenhphamgrouptype=4 and mbp.isdeleted=0; ";
                //gridControlHSBADSBenhAn.DataSource = condb.GetDataTable_HIS(sqlPhieuPTT);

            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadBenhAnCuaBenhNhan()
        {
            try
            {
                this.currentMrdHsbaHsba = new MrdHsbaHosobenhanDTO();

                string sqlkiemtrabenhan = "SELECT mrd_hsba_hosobenhanid, hosobenhanid, mrd_hsbatemid, mrd_hsba_hosobenhandata FROM mrd_hsba_hosobenhan WHERE hosobenhanid=" + this.mecicalrecordCurrentDTO.hosobenhanid + ";";
                DataTable databenhan = condb.GetDataTable_HSBA(sqlkiemtrabenhan);
                if (databenhan != null && databenhan.Rows.Count > 0)
                {
                    cboMauBenhAn.EditValue = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_hsbatemid"].ToString());
                    this.currentMrdHsbaHsba.mrd_hsba_hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_hsba_hosobenhanid"].ToString());
                    this.currentMrdHsbaHsba.mrd_hsba_hosobenhandata = databenhan.Rows[0]["mrd_hsba_hosobenhandata"].ToString();

                    btnChonTaoBenhAn.Visible = false;
                    btnXemBenhAn.Visible = true;
                    cboMauBenhAn.ReadOnly = true;
                }
                else
                {
                    btnChonTaoBenhAn.Visible = true;
                    btnXemBenhAn.Visible = false;
                    cboMauBenhAn.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Button
        private void btnChonTaoBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMauBenhAn.EditValue != null)
                {
                    if (this.mecicalrecordCurrentDTO.medicalrecordstatus == 0)
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_NHAN_CHUA_DUOC_TIEP_NHAN_VAO_PHONG);
                        frmthongbao.Show();
                    }
                    else if (this.mecicalrecordCurrentDTO.medicalrecordstatus == 2)
                    {
                        this.currentMrdHsbaHsba.patientid = this.mecicalrecordCurrentDTO.patientid;
                        this.currentMrdHsbaHsba.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                        this.currentMrdHsbaHsba.medicalrecordid = this.mecicalrecordCurrentDTO.medicalrecordid;
                        this.currentMrdHsbaHsba.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                        this.currentMrdHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(cboMauBenhAn.EditValue.ToString());
                        frmNhapBenhAn frmNhap = new frmNhapBenhAn(this.currentMrdHsbaHsba);
                        frmNhap.ShowDialog();

                        //Sua dung Office de mo
                        //  HSBA_BenhAn_Process.LayDuLieuVaXuatFileWord(mrdHsbaHsba);
                        ucHSBA_BenhAn_Load(null,null);
                    }
                    else
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_AN_DA_KET_THUC);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CHUA_CHON_LOAI_BENH_AN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void btnXemBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentMrdHsbaHsba.patientid = this.mecicalrecordCurrentDTO.patientid;
                this.currentMrdHsbaHsba.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                this.currentMrdHsbaHsba.medicalrecordid = this.mecicalrecordCurrentDTO.medicalrecordid;
                this.currentMrdHsbaHsba.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                this.currentMrdHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(cboMauBenhAn.EditValue.ToString());
                frmNhapBenhAn frmNhap = new frmNhapBenhAn(this.currentMrdHsbaHsba);
                frmNhap.ShowDialog();

                //Sua dung Office de mo
                //  HSBA_BenhAn_Process.LayDuLieuVaXuatFileWord(mrdHsbaHsba);
                ucHSBA_BenhAn_Load(null, null);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion


    }
}
