using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using O2S_MedicalRecord.Base;
using System.IO;
using O2S_MedicalRecord.DTO;

namespace O2S_MedicalRecord.GUI.FormCommon
{
    public partial class frmLogin : Form
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();
        NpgsqlConnection conn;
        public frmLogin()
        {
            InitializeComponent();
        }

        #region Load
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraKetNoiDenCoSoDuLieu() == false)
                {
                    MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                KiemTraInsertMayTram();
                LoadDataFromDatabase();
                LoadDefaultValue();
                //  KiemTraVaCopyFileLaucherNew(); chua co Lanucher
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private bool KiemTraKetNoiDenCoSoDuLieu()
        {
            bool result = false;
            try
            {
                string serverhost = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
                string serveruser = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
                string serverpass = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
                string serverdb = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);
                string serverhost_HSBA = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost_HSBA"].ToString().Trim() ?? "", true);
                string serveruser_HSBA = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username_HSBA"].ToString().Trim(), true);
                string serverpass_HSBA = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password_HSBA"].ToString().Trim(), true);
                string serverdb_HSBA = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database_HSBA"].ToString().Trim(), true);

                if (conn == null)
                {
                    conn = new NpgsqlConnection("Server=" + serverhost + ";Port=5432;User Id=" + serveruser + "; " + "Password=" + serverpass + ";Database=" + serverdb + ";CommandTimeout=1800000;");
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                conn.Close();
                result = true;
                //todo them ket noi den CSDL HSBA
                if (conn == null)
                {
                    conn = new NpgsqlConnection("Server=" + serverhost_HSBA + ";Port=5432;User Id=" + serveruser_HSBA + "; " + "Password=" + serverpass_HSBA + ";Database=" + serverdb_HSBA + ";CommandTimeout=1800000;");
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                conn.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
            return result;
        }
        private void KiemTraInsertMayTram()
        {
            try
            {
                SessionLogin.MaDatabase = Base.KiemTraLicense.LayThongTinMaDatabase();
                //string tenmay = O2S_MedicalRecord.GUI.FormCommon.DangKyBanQuyen.HardwareInfo.GetComputerName();
                string license_trang = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt("", true);

                string kiemtra_client = "SELECT * FROM mrd_license WHERE datakey='" + SessionLogin.MaDatabase + "' ;";
                DataView dv = new DataView(condb.GetDataTable_HSBA(kiemtra_client));
                if (dv != null && dv.Count > 0)
                {
                    //Kiem tra license
                    //O2S_MedicalRecord.GUI.FormCommon.DangKyBanQuyen.kiemTraLicenseHopLe.KiemTraLicenseHopLe();
                }
                else
                {
                    string insert_client = "INSERT INTO mrd_license(datakey, licensekey) VALUES ('" + SessionLogin.MaDatabase + "','" + license_trang + "' );";
                    condb.GetDataTable_HSBA(insert_client);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                LoadDataSystems.Load_Serviceprice();
                LoadDataSystems.Load_MrdHsbaTemplate();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void KiemTraVaCopyFileLaucherNew()
        {
            try
            {
                string versionDatabase = "";
                DataView dataVer = new DataView(condb.GetDataTable_HSBA("SELECT appversion from mrd_version where app_type=1 LIMIT 1;"));
                if (dataVer != null && dataVer.Count > 0)
                {
                    versionDatabase = dataVer[0]["appversion"].ToString();
                }
                //lấy thông tin version của phần mềm MSO2 MedicalRecord Launcher.exe
                FileVersionInfo.GetVersionInfo(Path.Combine(Environment.CurrentDirectory, "MSO2 MedicalRecord Launcher.exe"));
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(Environment.CurrentDirectory + "\\MSO2 MedicalRecord Launcher.exe");
                if (myFileVersionInfo.FileVersion.ToString() != versionDatabase)
                {
                    DataView dataurlfile = new DataView(condb.GetDataTable_HSBA("select app_link from mrd_version where app_type=1 limit 1;"));
                    if (dataurlfile != null && dataurlfile.Count > 0)
                    {
                        string tempDirectory = dataurlfile[0]["app_link"].ToString();
                        CopyFolder(tempDirectory, Environment.CurrentDirectory);
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }
        private static void CopyFolder(string SourceFolder, string DestFolder)
        {
            try
            {
                Directory.CreateDirectory(DestFolder); //Tao folder moi
                string[] files = Directory.GetFiles(SourceFolder);
                //Neu co file thi phai copy file
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
            }
            catch (Exception ex)
            {
                Base.Logging.Error(ex);
            }
        }

        private void LoadDefaultValue()
        {
            try
            {
                if (ConfigurationManager.AppSettings["LoginUser"].ToString() != "" && ConfigurationManager.AppSettings["LoginPassword"].ToString() != "")
                {
                    this.txtUsername.Text = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginUser"].ToString(), true);
                    this.txtPassword.Text = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["LoginPassword"].ToString(), true);
                    this.checkEditNhoPass.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["checkEditNhoPass"]);
                }
                else
                {
                    this.txtUsername.Text = "";
                    this.txtPassword.Text = "";
                }
                txtUsername.Focus();

                SessionLogin.SessionMachineName = Environment.MachineName;
                // Địa chỉ Ip
                String strHostName = Dns.GetHostName();
                IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
                //int nIP = 0;
                string listIP = "";
                for (int i = 0; i < iphostentry.AddressList.Count(); i++)
                {
                    listIP += iphostentry.AddressList[i] + ";";
                }
                SessionLogin.SessionMyIP = listIP;
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                SessionLogin.SessionVersion = fvi.FileVersion;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.Focus();
                    return;
                }
                // tạo 1 tài khoản ở trên PM, không chứa trong DB để làm tài khoản admin
                else if (txtUsername.Text.ToLower() == Base.KeyTrongPhanMem.AdminUser_key && txtPassword.Text == Base.KeyTrongPhanMem.AdminPass_key)
                {
                    SessionLogin.SessionUserID = -1;
                    SessionLogin.SessionUsercode = txtUsername.Text.Trim().ToLower();
                    SessionLogin.SessionUsername = "Administrator";

                    LoadDuLieuSauKhiDangNhap();
                }
                else
                {
                    string en_txtUsername = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim().ToLower(), true);
                    string en_txtPassword = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                    try
                    {
                        string command = "SELECT userid, usercode, username, userpassword FROM mrd_tbluser WHERE usercode='" + en_txtUsername + "' and userpassword='" + en_txtPassword + "';";
                        DataView dv = new DataView(condb.GetDataTable_HSBA(command));
                        if (dv != null && dv.Count > 0)
                        {
                            Base.KiemTraLicense.KiemTraLicenseHopLe();
                            SessionLogin.SessionUserID = Utilities.Util_TypeConvertParse.ToInt64(dv[0]["userid"].ToString());
                            SessionLogin.SessionUsercode = txtUsername.Text.Trim().ToLower();
                            SessionLogin.SessionUsername = O2S_MedicalRecord.Base.EncryptAndDecrypt.Decrypt(dv[0]["username"].ToString(), true);

                            LoadDuLieuSauKhiDangNhap();
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        O2S_MedicalRecord.Base.Logging.Error(ex);
                        txtUsername.Focus();
                    }
                }

                // Khi được check vào nút ghi nhớ thì sẽ lưu tên đăng nhập và mật khẩu vào file config
                if (checkEditNhoPass.Checked)
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = Convert.ToString(checkEditNhoPass.Checked);
                    _config.AppSettings.Settings["LoginUser"].Value = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(txtUsername.Text.Trim(), true);
                    _config.AppSettings.Settings["LoginPassword"].Value = O2S_MedicalRecord.Base.EncryptAndDecrypt.Encrypt(txtPassword.Text.Trim(), true);
                    _config.Save(ConfigurationSaveMode.Modified);

                    ConfigurationManager.RefreshSection("appSettings");
                }
                // không thì sẽ xóa giá trị đã lưu trong file congfig đi
                else
                {
                    Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    _config.AppSettings.Settings["checkEditNhoPass"].Value = "false";
                    _config.AppSettings.Settings["LoginUser"].Value = "";
                    _config.AppSettings.Settings["LoginPassword"].Value = "";
                    _config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn("Dang nhap " + ex.ToString());
            }
        }

        private void LoadDuLieuSauKhiDangNhap()
        {
            try
            {
                SessionLogin.SessionLstPhanQuyenNguoiDung = O2S_MedicalRecord.Base.CheckPermission.GetListPhanQuyenNguoiDung();
                SessionLogin.SessionlstPhanQuyen_KhoaPhong = O2S_MedicalRecord.Base.CheckPermission.GetPhanQuyen_KhoaPhong();
                //SessionLogin.SessionLstPhanQuyen_KhoThuoc = O2S_MedicalRecord.Base.CheckPermission.GetPhanQuyen_KhoThuoc();
                //SessionLogin.SessionLstPhanQuyen_PhongLuu = O2S_MedicalRecord.Base.CheckPermission.GetPhanQuyen_PhongLuu();


                frmMain frmm = new frmMain();
                frmm.Show();
                this.Visible = false;
                O2S_MedicalRecord.Base.Logging.Info("Application open successfull. Time=" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"));
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
        }


        #region Custom
        // Khi nhập username và nhấn enter thì forcus vào ô nhập pass
        private void txtUsername_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        // Sau khi nhập password và ấn enter thì đăng nhập luôn
        private void txtPassword_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        // nếu viết vào ô username = "config" thì mở ra bảng để cấu hình DB
        private void txtUsername_EditValueChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.ToUpper() == "CONFIG")
            {
                frmConnectDB frmcon = new frmConnectDB();
                frmcon.Dock = System.Windows.Forms.DockStyle.Bottom;
                frmcon.ShowDialog();
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
                this.Dispose();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
        }

        private void linkTroGiup_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Liên hệ với tác giả để được trợ giúp! \nAuthor: Hồng Minh Nhất \nE-mail: hongminhnhat15@gmail.com \nPhone: 0868-915-456", "Thông tin về tác giả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
