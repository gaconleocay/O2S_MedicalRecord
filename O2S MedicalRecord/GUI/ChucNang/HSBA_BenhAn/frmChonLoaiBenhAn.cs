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

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class frmChonLoaiBenhAn : Form
    {
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        public frmChonLoaiBenhAn()
        {
            InitializeComponent();
        }

        public frmChonLoaiBenhAn(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.mecicalrecordCurrentDTO = filterDTO;
        }

        private void frmChonLoaiBenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachLoaiBenhAn();
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

        private void btnChonTaoBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMauBenhAn.EditValue != null)
                {
                    MrdHsbaHosobenhanDTO mrdHsbaHsba = new MrdHsbaHosobenhanDTO();
                    mrdHsbaHsba.patientid = this.mecicalrecordCurrentDTO.patientid;
                    mrdHsbaHsba.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                    mrdHsbaHsba.medicalrecordid = this.mecicalrecordCurrentDTO.medicalrecordid;
                    mrdHsbaHsba.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                    mrdHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(cboMauBenhAn.EditValue.ToString());

                    HSBA_BenhAn_Process.LayDuLieuVaXuatFileWord(mrdHsbaHsba);
                    this.Close();
                    this.Dispose();
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
    }
}
