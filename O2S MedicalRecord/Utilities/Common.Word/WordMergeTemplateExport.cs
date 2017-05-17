using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.Utilities.Common.Word
{
    public class WordMergeTemplateExport
    {
        public static void ExportWordMailMerge(string filePath, string saveFile, DataTable dt)
        {
            try
            {
                string strRoot = Environment.CurrentDirectory;

                Aspose.Words.License l = new Aspose.Words.License();
                string strLicense = strRoot + "Library\\Aspose.Words.lic";
                l.SetLicense(strLicense);

                string path = strRoot + filePath;

                Aspose.Words.Document doc = new Aspose.Words.Document(path);
                doc.MailMerge.Execute(dt);
                doc.Save(saveFile, SaveFormat.Docx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Aspose.Words.Document ExportWordMailMerge(string fileFullPath, DataTable dt)
        {
            Aspose.Words.Document doc = new Document();
            try
            {
                string strRoot = Environment.CurrentDirectory;
                // Aspose.Words.License l = new Aspose.Words.License();
                //string strLicense = strRoot + "\\Library\\Aspose.Words.lic";
                // l.SetLicense(strLicense);
                Aspose.Words.Document docccc = new Aspose.Words.Document(fileFullPath);
                doc = docccc.Clone();
                doc.MailMerge.Execute(dt);
                doc.Save(strRoot + "\\Templates\\PhieuPhauThuatThuThuat_Tmp\\PTTT.docx", SaveFormat.Docx);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            return doc;
        }
    }
}
