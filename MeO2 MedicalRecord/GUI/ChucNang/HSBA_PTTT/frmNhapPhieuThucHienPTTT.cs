using MSO2_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSO2_MedicalRecord.GUI.ChucNang.HSBA_PTTT
{
    public partial class frmNhapPhieuThucHienPTTT : Form
    {
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        private serviceprice_ptttDTO serviceCurrentDTO { get; set; }
        public frmNhapPhieuThucHienPTTT()
        {
            InitializeComponent();
        }
        public frmNhapPhieuThucHienPTTT(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.mecicalrecordCurrentDTO = filterDTO;
        }

        private void frmNhapPhieuThucHienPTTT_Load(object sender, EventArgs e)
        {
            try
            {
                string getthongtinservice = "";



                this.serviceCurrentDTO = new serviceprice_ptttDTO();
                //this.serviceCurrentDT
                //Load thong tin benh an
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }


    }
}
