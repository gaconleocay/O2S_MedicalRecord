using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using MSO2_MedicalRecord.DTO;

namespace MSO2_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class ucUpdateTemplateDVPTTT : UserControl
    {
        MSO2_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<mrd_serviceref> lstmrd_serviceref { get; set; }
        long mrd_servicerefidCurrent { get; set; }
        public ucUpdateTemplateDVPTTT()
        {
            InitializeComponent();
        }

        private void ucSuaDanhMucDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                txtmrd_templatename.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Error(ex);
            }
        }

        #region Radio Changed
        private void radioKhamBenh_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioKhamBenh.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioCDHA.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    GetDataDanhMucDichVu();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void radioXetNghiem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioXetNghiem.Checked)
                {
                    radioKhamBenh.Checked = false;
                    radioCDHA.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    GetDataDanhMucDichVu();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void radioCDHA_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioCDHA.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioKhamBenh.Checked = false;
                    radioChuyenKhoa.Checked = false;
                    GetDataDanhMucDichVu();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void radioChuyenKhoa_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioChuyenKhoa.Checked)
                {
                    radioXetNghiem.Checked = false;
                    radioKhamBenh.Checked = false;
                    radioCDHA.Checked = false;
                    GetDataDanhMucDichVu();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion


        private void GetDataDanhMucDichVu()
        {
            SplashScreenManager.ShowForm(typeof(MSO2_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                string servicegrouptype = "";
                int servicelock = 0;
                if (radioKhamBenh.Checked)
                {
                    servicegrouptype = "1";
                }
                if (radioXetNghiem.Checked)
                {
                    servicegrouptype = "2";
                }
                if (radioCDHA.Checked)
                {
                    servicegrouptype = "3";
                }
                if (radioChuyenKhoa.Checked)
                {
                    servicegrouptype = "4";
                }
                if (chkDaKhoa.Checked)
                {
                    servicelock = 1;
                }

                string sqlLayDanhMuc = "select mrd_servicerefid, his_servicepricerefid, servicegrouptype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid, mrd_templatename from mrd_serviceref where servicegrouptype=" + servicegrouptype + " and servicelock=" + servicelock + "; ";
                DataView dv_DanhMucDichVu = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                if (dv_DanhMucDichVu.Count > 0)
                {
                    lstmrd_serviceref = new List<mrd_serviceref>();
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        mrd_serviceref dmDichVu = new mrd_serviceref();
                        dmDichVu.mrd_servicerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["mrd_servicerefid"].ToString());
                        dmDichVu.his_servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["his_servicepricerefid"].ToString());
                        dmDichVu.servicegrouptype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicegrouptype"].ToString());
                        dmDichVu.bhyt_groupcode = dv_DanhMucDichVu[i]["bhyt_groupcode"].ToString();
                        dmDichVu.servicepricegroupcode = dv_DanhMucDichVu[i]["servicepricegroupcode"].ToString();
                        dmDichVu.servicepricecode = dv_DanhMucDichVu[i]["servicepricecode"].ToString();
                        dmDichVu.servicepricename = dv_DanhMucDichVu[i]["servicepricename"].ToString();
                        dmDichVu.servicepricenamenhandan = dv_DanhMucDichVu[i]["servicepricenamenhandan"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucDichVu[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepricenamenuocngoai = dv_DanhMucDichVu[i]["servicepricenamenuocngoai"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucDichVu[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee"].ToString());
                        dmDichVu.servicepricefeenhandan = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString());
                        dmDichVu.servicepricefeebhyt = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString());
                        dmDichVu.servicepricefeenuocngoai = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString());
                        dmDichVu.servicelock = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["servicelock"].ToString());
                        dmDichVu.servicepricecodeuser = dv_DanhMucDichVu[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricesttuser = dv_DanhMucDichVu[i]["servicepricesttuser"].ToString();
                        dmDichVu.pttt_hangid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_hangid"].ToString());
                        dmDichVu.pttt_loaiid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_loaiid"].ToString());
                        dmDichVu.mrd_templatename = dv_DanhMucDichVu[i]["mrd_templatename"].ToString();

                        lstmrd_serviceref.Add(dmDichVu);
                    }
                    gridControlDMDichVu.DataSource = dv_DanhMucDichVu;
                }
                else
                {
                    gridControlDMDichVu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioKhamBenh.Checked == false && radioXetNghiem.Checked == false && radioCDHA.Checked == false && radioChuyenKhoa.Checked == false)
                {
                    MSO2_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new MSO2_MedicalRecord.Utilities.ThongBao.frmThongBao(MSO2_MedicalRecord.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                GetDataDanhMucDichVu();
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void gridControlDMDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDMDichVu.FocusedRowHandle;
                mrd_servicerefidCurrent = Convert.ToInt64(gridViewDMDichVu.GetRowCellValue(rowHandle, "mrd_servicerefid").ToString());
                var serviceprice = lstmrd_serviceref.Where(o => o.mrd_servicerefid == mrd_servicerefidCurrent).FirstOrDefault();
                if (serviceprice != null)
                {
                    txtservicepricecode.Text = serviceprice.servicepricecode;
                    txtservicepricegroupcode.Text = serviceprice.servicepricegroupcode;
                    txtservicepricecodeuser.Text = serviceprice.servicepricecodeuser;
                    txtservicepricenamebhyt.Text = serviceprice.servicepricenamebhyt;
                    txtservicepricefeebhyt.Text = serviceprice.servicepricefeebhyt.ToString();
                    txtservicepricefeenhandan.Text = serviceprice.servicepricefeenhandan.ToString();
                    txtservicepricefee.Text = serviceprice.servicepricefee.ToString();
                    txtservicepricefeenuocngoai.Text = serviceprice.servicepricefeenuocngoai.ToString();
                    txtmrd_templatename.Text = serviceprice.mrd_templatename;
                    switch (serviceprice.bhyt_groupcode)
                    {
                        case "01KB":
                            txtbhyt_groupcode.Text = "Khám bệnh";
                            break;
                        case "03XN":
                            txtbhyt_groupcode.Text = "Xét nghiệm";
                            break;
                        case "04CDHA":
                            txtbhyt_groupcode.Text = "Chẩn đoán hình ảnh";
                            break;
                        case "05TDCN":
                            txtbhyt_groupcode.Text = "Thăm dò chức năng";
                            break;
                        case "06PTTT":
                            txtbhyt_groupcode.Text = "Phẫu thuật thủ thuật";
                            break;
                        case "07KTC":
                            txtbhyt_groupcode.Text = "Dịch vụ kỹ thuật cao";
                            break;
                        case "11VC":
                            txtbhyt_groupcode.Text = "Vận chuyển";
                            break;
                        case "12NG":
                            txtbhyt_groupcode.Text = "Ngày giường";
                            break;
                        case "999DVKHAC":
                            txtbhyt_groupcode.Text = "Dịch vụ khác";
                            break;
                        case "1000PhuThu":
                            txtbhyt_groupcode.Text = "Phụ thu";
                            break;
                        default:
                            break;
                    }

                    btnSua.Enabled = true;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                }
                else
                {
                    txtservicepricecode.Clear();
                    txtservicepricegroupcode.Clear();
                    txtservicepricecodeuser.Clear();
                    txtservicepricenamebhyt.Clear();
                    txtservicepricefeebhyt.Clear();
                    txtservicepricefeenhandan.Clear();
                    txtservicepricefee.Clear();
                    txtservicepricefeenuocngoai.Clear();
                    txtmrd_templatename.Clear();
                    txtbhyt_groupcode.Clear();
                    btnSua.Enabled = false;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                txtmrd_templatename.ReadOnly = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string mrd_templatename = "";

                if (mrd_templatename != "")
                {
                    //Update servicepriceref
                    string updateservicepriceref = "";
                    if (condb.ExecuteNonQuery_HIS(updateservicepriceref))
                    {
                        MessageBox.Show("Cập nhật template thành công dịch vụ mã [" + txtservicepricecode.Text.Trim() + "] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                        txtmrd_templatename.ReadOnly = true;
                    }
                }
                else
                {
                    MSO2_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new MSO2_MedicalRecord.Utilities.ThongBao.frmThongBao(MSO2_MedicalRecord.Base.ThongBaoLable.CHUA_CHON_TEMPLATE);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Error(ex);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                mrd_servicerefidCurrent = 0;
                txtservicepricecode.Clear();
                txtservicepricegroupcode.Clear();
                txtservicepricecodeuser.Clear();
                txtservicepricenamebhyt.Clear();
                txtservicepricefeebhyt.Clear();
                txtservicepricefeenhandan.Clear();
                txtservicepricefee.Clear();
                txtservicepricefeenuocngoai.Clear();
                txtmrd_templatename.Clear();
                txtbhyt_groupcode.Clear();
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                txtmrd_templatename.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnTimFileKetQua_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtmrd_templatename.Text = openFileDialogSelect.FileName;
                }
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }







    }
}
