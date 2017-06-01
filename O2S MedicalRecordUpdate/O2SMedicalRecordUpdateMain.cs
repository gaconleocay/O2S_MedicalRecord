using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace O2S_MedicalRecordUpdate
{
    public partial class O2SMedicalRecordUpdateMain : Form
    {
        private static ConnectDatabase condb = new ConnectDatabase();
        public O2SMedicalRecordUpdateMain()
        {
            InitializeComponent();
        }
        #region Load
        private void Update_Load(object sender, EventArgs e)
        {
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                string version = fvi.FileVersion;
                this.Text = "Update version phần mềm (v" + version + ")";
                KiemTraTonTaiVaInsert();
                LoadDuLieuMacDinhLenForm();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void KiemTraTonTaiVaInsert()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM mrd_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM mrd_version WHERE app_type=1;";
                DataTable dataApp = condb.GetDataTable_HSBA(kiemtraApp);
                DataTable dataLauncher = condb.GetDataTable_HSBA(kiemtraLauncher);
                if (dataApp == null || dataApp.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO mrd_version(app_type) values('0') ;";
                    condb.ExecuteNonQuery_HSBA(insertApp);
                }
                if (dataLauncher == null || dataLauncher.Rows.Count != 1)
                {
                    string insertApp = "INSERT INTO mrd_version(app_type) values('1') ;";
                    condb.ExecuteNonQuery_HSBA(insertApp);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadDuLieuMacDinhLenForm()
        {
            try
            {
                string kiemtraApp = "SELECT * FROM mrd_version WHERE app_type=0;";
                string kiemtraLauncher = "SELECT * FROM mrd_version WHERE app_type=1;";
                DataTable dataApp = condb.GetDataTable_HSBA(kiemtraApp);
                DataTable dataLauncher = condb.GetDataTable_HSBA(kiemtraLauncher);
                if (dataApp != null || dataApp.Rows.Count > 0)
                {
                    txtUpdateLinkFile.Text = dataApp.Rows[0]["app_link"].ToString();
                    txtVersionMecicalRecord.Text = dataApp.Rows[0]["appversion"].ToString();
                }
                if (dataLauncher != null || dataLauncher.Rows.Count > 0)
                {
                    txtVersionLauncher.Text = dataLauncher.Rows[0]["appversion"].ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogShelect.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialogShelect.FileName;
                    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(openFileDialogShelect.FileName);
                    txtVersionMecicalRecord.Text = myFileVersionInfo.FileVersion.ToString();
                    //txtUpdateLinkFile.Text = openFileDialogShelect.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "insert into mrd_version(appversion, updateapp)  values ('" + txtVersionMecicalRecord.Text.Trim() + "', (SELECT bytea_import('" + txtFilePath.Text.Trim() + "')));";
                string deleteversionold = "delete from mrd_version where appversion <>'" + txtVersionMecicalRecord.Text.Trim() + "';";

                condb.ExecuteNonQuery_HSBA(sqlcommit);
                condb.ExecuteNonQuery_HSBA(deleteversionold);
                MessageBox.Show("Commit thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Update duong link va file len server
        private void btnUpdateLink_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO...
                CopyFileLenServer(txtFilePath.Text.Trim(), txtUpdateLinkFile.Text.Trim());
                string sqlcommit = "update mrd_version set app_link= '" + txtUpdateLinkFile.Text.Trim() + "';";
                condb.ExecuteNonQuery_HSBA(sqlcommit);
                MessageBox.Show("Update file lên server thành công.", "Thông báo");
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static void CopyFileLenServer(string SourceFolder, string DestFolder)
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
                CopyFileLenServer(folder, dest);
            }
        }

        private void btnUpdateMedicalLink_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update mrd_version set appversion='" + txtVersionMecicalRecord.Text.Trim() + "' where app_type=0;";
                condb.ExecuteNonQuery_HSBA(sqlcommit);
                MessageBox.Show("Update Version MedicalLink thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnUpdateLauncher_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlcommit = "update mrd_version set appversion='" + txtVersionLauncher.Text.Trim() + "' where app_type=1;";
                condb.ExecuteNonQuery_HSBA(sqlcommit);
                MessageBox.Show("Update Version Launcher thành công.", "Thông báo");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtKeyDangNhap.Text.Trim() == KeyTrongPhanMem.AdminPass_key)
                {
                    btnUpdateLink.Enabled = true;
                    btnUpdateMedicalLink.Enabled = true;
                    btnUpdateLauncher.Enabled = true;
                }
                else
                {
                    btnUpdateLink.Enabled = false;
                    btnUpdateMedicalLink.Enabled = false;
                    btnUpdateLauncher.Enabled = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtKeyDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null,null);
            }
        }
       
    }
}
