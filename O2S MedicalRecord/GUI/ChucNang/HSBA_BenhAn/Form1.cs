using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richEditControlData.Unit = DevExpress.Office.DocumentUnit.Inch;
            richEditControlData.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A5;
            // richEditControlData.Document.Sections[0].Page.Landscape = true;
            //richEditControlData.Document.Sections[0].Margins.Left = 0.2f;
            //richEditControlData.Document.Sections[0].Margins.Right = 0.2f;
            //richEditControlData.Document.Sections[0].Margins.Top = 0.2f;
            //richEditControlData.Document.Sections[0].Margins.Bottom = 0.2f;
           // richEditControlData.ActiveViewType = RichEditViewType.PrintLayout;

        }



    }
}
