using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

namespace O2S_MedicalRecordLauncher
{
    static class Program
    {
        private const string TEMP_DIR = "O2S MedicalRecordUpdate_File";
        // private const string UPDATE_FILE_NAME = "MedicalLinkUpdate.zip";
        private static ConnectDatabase condb = new ConnectDatabase();
        private static string versionDatabase = "";
        private static string tempDirectory = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (CheckVersionUpdate()) //update
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật lên phiên bản mới? \n Hãy tắt phần mềm đang chạy trước khi cập nhật.", "Thông báo có phiên bản mới.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            CopyFolder(tempDirectory, Environment.CurrentDirectory);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                //sau khi copy đè tất cả các file xong, ta sẽ tiến hành gọi lại chương trình chính (O2S MedicalRecord.exe) để chạy lại chương trình
                System.Diagnostics.Process.Start(@"O2S MedicalRecord.exe");
                Application.Exit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Process.Start(@"O2S MedicalRecord.exe");
                Application.Exit();
            }
        }

        private static bool CheckVersionUpdate()
        {
            bool result = false;
            try
            {
                DataView dataVer = new DataView(condb.GetDataTable_HSBA("SELECT appversion,app_link from mrd_version where app_type=0 LIMIT 1;"));
                if (dataVer != null && dataVer.Count > 0)
                {
                    versionDatabase = dataVer[0]["appversion"].ToString();
                    tempDirectory = dataVer[0]["app_link"].ToString();
                }
                //lấy thông tin version của phần mềm O2S MedicalRecord.exe
                FileVersionInfo.GetVersionInfo(Path.Combine(Environment.CurrentDirectory, "O2S MedicalRecord.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory + "\\O2S MedicalRecord.exe");
                if (myFileVersionInfo.FileVersion.ToString() != versionDatabase)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = true;
            }
            return result;
        }

        private static void CopyFolder(string SourceFolder, string DestFolder)
        {
            Directory.CreateDirectory(DestFolder); //Tao folder moi
            string[] files = Directory.GetFiles(SourceFolder);
            //Neu co file thy phai copy file
            foreach (string file in files)
            {
                try
                {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(DestFolder, name);
                    File.Copy(file, dest, true);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            string[] folders = Directory.GetDirectories(SourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(DestFolder, name);
                CopyFolder(folder, dest);
            }
        }

    }

}
