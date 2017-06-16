using Aspose.Words.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.Utilities.PrintPreview
{
    public partial class frmDevPrintPreviewDialog : Form
    {
        public frmDevPrintPreviewDialog()
        {
            InitializeComponent();
        }

        private void frmDevPrintPreviewDialog_Load(object sender, EventArgs e)
        {
            try
            {
                string fullPath = Environment.CurrentDirectory + "\\Templates\\BienBanHoiChan\\";

                Aspose.Words.Document doc = new Aspose.Words.Document(fullPath + "40. Trich bien ban hoi chuan.DOC");

                PrintDialog printDlg = new PrintDialog();
                // Initialize the print dialog with the number of pages in the document.
                printDlg.AllowSomePages = true;
                printDlg.PrinterSettings.MinimumPage = 1;
                printDlg.PrinterSettings.MaximumPage = doc.PageCount;
                printDlg.PrinterSettings.FromPage = 1;
                printDlg.PrinterSettings.ToPage = doc.PageCount;

                AsposeWordsPrintDocument awPrintDoc = new AsposeWordsPrintDocument(doc);
                awPrintDoc.PrinterSettings = printDlg.PrinterSettings;

                documentViewerShow.DocumentSource = awPrintDoc;
                printingSystem1.LoadDocument(fullPath + "40. Trich bien ban hoi chuan.DOC");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
