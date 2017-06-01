using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;


namespace O2S_MedicalRecordLauncher
{
    public class ConnectDatabase
    {
        #region Khai bao
        private string serverhost = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost"].ToString().Trim() ?? "", true);
        private string serveruser = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username"].ToString().Trim(), true);
        private string serverpass = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password"].ToString().Trim(), true);
        private string serverdb = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database"].ToString().Trim(), true);
        private string serverhost_HSBA = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["ServerHost_HSBA"].ToString().Trim() ?? "", true);
        private string serveruser_HSBA = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Username_HSBA"].ToString().Trim(), true);
        private string serverpass_HSBA = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Password_HSBA"].ToString().Trim(), true);
        private string serverdb_HSBA = EncryptAndDecrypt.Decrypt(ConfigurationManager.AppSettings["Database_HSBA"].ToString().Trim(), true);

        NpgsqlConnection conn;
        NpgsqlConnection conn_HSBA;
        private bool kiemtraketnoi = false;

        #endregion
        #region Database HIS
        public void connect()
        {
            try
            {
                if (conn == null)
                    conn = new NpgsqlConnection("Server=" + serverhost + ";Port=5432;User Id=" + serveruser + "; " + "Password=" + serverpass + ";Database=" + serverdb + ";CommandTimeout=1800000;");
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                kiemtraketnoi = true;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                // MessageBox.Show("Không kết nối được cơ sở dữ liệu", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Logging.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }
        public void disconnect()
        {
            try
            {
                if ((conn != null) && (conn.State == ConnectionState.Open))
                    conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                MessageBox.Show("Có lỗi khi đóng kết nối đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Logging.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }
        public DataTable GetDataTable_HIS(string sql)
        {
            connect();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Logging.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }
        public bool ExecuteNonQuery_HIS(string sql)
        {
            bool result = false;
            try
            {
                connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Logging.Error("Loi ExecuteNonQuery: " + ex.ToString());
            }
            return result;
        }
        public bool ExecuteNonQuery_Error_HIS(string sql)
        {
            bool result = false;
            try
            {
                connect();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    disconnect();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                //Logging.Error("Loi ExecuteNonQuery_Error: " + ex.ToString());
            }
            return result;
        }
        public NpgsqlDataReader getDataReader(string sql)
        {
            try
            {
                connect();
                NpgsqlCommand com = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = com.ExecuteReader();
                //dr.Read();
                disconnect();
                return dr;
            }
            catch (Exception ex)
            {
                //Logging.Error("Loi get dataReader ve: " + ex.ToString());
                return null;
            }

        }

        #endregion

        #region Database Ho So Benh An
        public void Connect_HSBA()
        {
            try
            {
                if (conn_HSBA == null)
                    conn_HSBA = new NpgsqlConnection("Server=" + serverhost_HSBA + ";Port=5432;User Id=" + serveruser_HSBA + "; " + "Password=" + serverpass_HSBA + ";Database=" + serverdb_HSBA + ";CommandTimeout=1800000;");
                if (conn_HSBA.State == ConnectionState.Closed)
                    conn_HSBA.Open();
                kiemtraketnoi = true;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                // Logging.Error("Loi ket noi den CSDL: " + ex.ToString());
            }
        }
        public void Disconnect_HSBA()
        {
            try
            {
                if ((conn_HSBA != null) && (conn_HSBA.State == ConnectionState.Open))
                    conn_HSBA.Close();
                conn_HSBA.Dispose();
                conn_HSBA = null;
            }
            catch (Exception ex)
            {
                kiemtraketnoi = false;
                // Logging.Error("Loi dong ket noi den CSDL: " + ex.ToString());
            }
        }
        public DataTable GetDataTable_HSBA(string sql)
        {
            Connect_HSBA();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn_HSBA);
            DataTable dt = new DataTable();
            try
            {
                if (kiemtraketnoi == true)
                {
                    da.Fill(dt);
                    Disconnect_HSBA();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi dữ liệu đầu vào" + ex.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Logging.Error("Loi getDataTable: " + ex.ToString());
            }
            return dt;
        }
        public bool ExecuteNonQuery_HSBA(string sql)
        {
            bool result = false;
            try
            {
                Connect_HSBA();
                if (kiemtraketnoi == true)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, conn_HSBA);
                    cmd.ExecuteNonQuery();
                    Disconnect_HSBA();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thực thi đến CSDL", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Logging.Error("Loi ExecuteNonQuery: " + ex.ToString());
            }
            return result;
        }

        #endregion

        #region Sử dụng DB_LINK để kết nối đến một CSDL khác
        public DataTable GetDataTable_Dblink(string sql)
        {
            DataTable result = new DataTable();
            try
            {
                //dblink_connect
                Execute_Dblink_Connect_HIS();
                //Chay SQL thuc thi
                result = GetDataTable_HSBA(sql);
                //Disconnect
                Execute_Dblink_Disconnect_HIS();
            }
            catch (Exception ex)
            {
                Execute_Dblink_Disconnect_HIS();
                Execute_Dblink_Connect_HIS();
                result = GetDataTable_HSBA(sql);
                Execute_Dblink_Disconnect_HIS();
            }
            return result;
        }
        public bool ExecuteNonQuery_Dblink(string sql)
        {
            bool result = false;
            try
            {
                //dblink_connect
                Execute_Dblink_Connect_HIS();
                //Chay SQL thuc thi
                result = ExecuteNonQuery_HSBA(sql);
                //Disconnect
                Execute_Dblink_Disconnect_HIS();
            }
            catch (Exception ex)
            {
                //Logging.Error("Loi getDataTable Dblink: " + ex.ToString());
                Execute_Dblink_Disconnect_HIS();
                Execute_Dblink_Connect_HIS();
                result = ExecuteNonQuery_HSBA(sql);
                Execute_Dblink_Disconnect_HIS();
            }
            return result;
        }

        public void Execute_Dblink_Connect_HIS()
        {
            try
            {
                string dblink_connect = "SELECT dblink_connect('myconn', 'dbname=" + serverdb + " port=5432 host=" + serverhost + " user=" + serveruser + " password=" + serverpass + "');";
                GetDataTable_HSBA(dblink_connect);
            }
            catch (Exception)
            {
            }
        }
        public void Execute_Dblink_Disconnect_HIS()
        {
            try
            {
                string dblink_dis = "SELECT dblink_disconnect('myconn');";
                GetDataTable_HSBA(dblink_dis);
            }
            catch (Exception)
            {
            }
        }
        #endregion




    }
}

// daolekwan.wordpress.com/2013/06/10/cclass-ket-noi-trong-c/