﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using O2S_MedicalRecord.DTO;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.Utils.Menu;
using Aspose.Cells;

namespace O2S_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class ucUpdateTemplateDVPTTT : UserControl
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<MrdServicerefDTO> lstmrd_serviceref { get; set; }
        long mrd_servicerefidCurrent { get; set; }
        string mrd_templatenameSelect = "";
        private string worksheetName = "DanhMucDichVu";
        public ucUpdateTemplateDVPTTT()
        {
            InitializeComponent();
        }

        #region Load
        private void ucSuaDanhMucDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnTimFileKetQua.Enabled = false;
                txtmrd_templatename.ReadOnly = true;
                LoadDanhSachImportExport();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachImportExport()
        {
            try
            {
                DXPopupMenu menu = new DXPopupMenu();
                menu.Items.Add(new DXMenuItem("Xuất danh sách dịch vụ"));
                menu.Items.Add(new DXMenuItem("Cập nhật template kết quả"));
                // ... add more items
                dropDownImportExport.DropDownControl = menu;
                // subscribe item.Click event
                foreach (DXMenuItem item in menu.Items)
                    item.Click += Item_ImportExport_Click;
                // setup initial selection
                //...
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion
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
                O2S_MedicalRecord.Base.Logging.Warn(ex);
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
                O2S_MedicalRecord.Base.Logging.Warn(ex);
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
                O2S_MedicalRecord.Base.Logging.Warn(ex);
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
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion


        private void GetDataDanhMucDichVu()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                long servicegrouptype = 0;
                int servicelock = 0;
                if (radioKhamBenh.Checked)
                {
                    servicegrouptype = 1;
                }
                if (radioXetNghiem.Checked)
                {
                    servicegrouptype = 2;
                }
                if (radioCDHA.Checked)
                {
                    servicegrouptype = 3;
                }
                if (radioChuyenKhoa.Checked)
                {
                    servicegrouptype = 4;
                }
                if (chkDaKhoa.Checked)
                {
                    servicelock = 1;
                }

                if (GlobalStore.GlobalLstMrdServiceref != null && GlobalStore.GlobalLstMrdServiceref.Count > 0)
                {
                    this.lstmrd_serviceref = new List<MrdServicerefDTO>();
                    this.lstmrd_serviceref = GlobalStore.GlobalLstMrdServiceref.Where(o => o.servicegrouptype == servicegrouptype && o.servicelock == servicelock).ToList();
                }
                else
                {
                    string sqlLayDanhMuc = "select ROW_NUMBER () OVER (ORDER BY bhyt_groupcode, servicepricename) as stt, mrd_servicerefid, his_servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid, mrd_templatename from mrd_serviceref where servicegrouptype=" + servicegrouptype + " and servicelock=" + servicelock + "; ";
                    DataView dv_DanhMucDichVu = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                    if (dv_DanhMucDichVu.Count > 0)
                    {
                        this.lstmrd_serviceref = new List<MrdServicerefDTO>();
                        for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                        {
                            MrdServicerefDTO dmDichVu = new MrdServicerefDTO();
                            dmDichVu.mrd_servicerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["mrd_servicerefid"].ToString());
                            dmDichVu.his_servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["his_servicepricerefid"].ToString());
                            dmDichVu.servicepricetype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicepricetype"].ToString());
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

                            this.lstmrd_serviceref.Add(dmDichVu);
                        }
                    }
                }
                //hien thi
                if (this.lstmrd_serviceref != null && this.lstmrd_serviceref.Count > 0)
                {
                    HienThiLenTreeList();
                }
                else
                {
                    treeListDSDichVu.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void HienThiLenTreeList()
        {
            try
            {
                if (this.lstmrd_serviceref != null)
                {
                    treeListDSDichVu.ClearNodes();
                    TreeListNode parentForRootNodes = null;

                    string servicegrouptype_code = "";
                    string servicegrouptype_name = "";
                    if (radioKhamBenh.Checked)
                    {
                        servicegrouptype_code = "G0";
                        servicegrouptype_name = "KHÁM BỆNH";
                    }
                    if (radioXetNghiem.Checked)
                    {
                        servicegrouptype_code = "G1";
                        servicegrouptype_name = "XÉT NGHIỆM";
                    }
                    if (radioCDHA.Checked)
                    {
                        servicegrouptype_code = "G2";
                        servicegrouptype_name = "CHẨN ĐOÁN HÌNH ẢNH";
                    }
                    if (radioChuyenKhoa.Checked)
                    {
                        servicegrouptype_code = "G3";
                        servicegrouptype_name = "CÁC DỊCH VỤ CHUYÊN KHOA, KHÁC";
                    }
                    TreeListNode rootNode_0 = treeListDSDichVu.AppendNode(
               new object[] { "0", servicegrouptype_code, servicegrouptype_name, null, null, null, null },
               parentForRootNodes, null);
                    CreateChildNodeServiceType(parentForRootNodes, servicegrouptype_code);
                    treeListDSDichVu.ExpandAll();
                }
                else
                {
                    treeListDSDichVu.ClearNodes();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #region Create Tree
        private void CreateChildNodeServiceType(TreeListNode rootNode, string servicegrouptype_code)
        {
            try
            {
                var listServiceTypeId = this.lstmrd_serviceref.FindAll(o => o.servicepricegroupcode == servicegrouptype_code).ToList().OrderBy(o => o.servicepricename);
                if (listServiceTypeId != null && listServiceTypeId.Count() > 0)
                {
                    foreach (var serviceTypeId in listServiceTypeId)
                    {
                        var serviceTypeObj = this.lstmrd_serviceref.Where(o => o.servicepricegroupcode == serviceTypeId.servicepricecode);
                        if (serviceTypeObj != null && serviceTypeObj.Count() > 0)
                        {
                            TreeListNode childNode = treeListDSDichVu.AppendNode(
                    new object[] { serviceTypeId.mrd_servicerefid, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, null, null, null, null },
                    rootNode, null);
                            CreateChildNodeServiceType(childNode, serviceTypeId.servicepricecode);
                        }
                        else //là lá
                        {
                            TreeListNode childChildNode = treeListDSDichVu.AppendNode(
                                new object[] { serviceTypeId.mrd_servicerefid, serviceTypeId.servicepricecode, serviceTypeId.servicepricename, serviceTypeId.servicepriceunit, serviceTypeId.servicepricefeebhyt, serviceTypeId.servicepricefeenhandan, serviceTypeId.mrd_templatename },
                                rootNode, serviceTypeId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioKhamBenh.Checked == false && radioXetNghiem.Checked == false && radioCDHA.Checked == false && radioChuyenKhoa.Checked == false)
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                    return;
                }
                GetDataDanhMucDichVu();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                btnTimFileKetQua.Enabled = true;
                txtmrd_templatename.ReadOnly = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.mrd_templatenameSelect != "" && this.mrd_servicerefidCurrent != 0)
                {
                    //Update servicepriceref
                    string updateservicepriceref = "update mrd_serviceref set mrd_templatename='" + this.mrd_templatenameSelect + "' where mrd_servicerefid=" + this.mrd_servicerefidCurrent + " ;";
                    if (condb.ExecuteNonQuery_HSBA(updateservicepriceref))
                    {
                        MessageBox.Show("Cập nhật template thành công dịch vụ mã [" + txtservicepricecode.Text.Trim() + "] !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                        txtmrd_templatename.ReadOnly = true;
                        btnTimFileKetQua.Enabled = false;
                        GetDataDanhMucDichVu();
                    }
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CHUA_CHON_TEMPLATE);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
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
                txtservicepricenamenhandan.Clear();
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
                btnTimFileKetQua.Enabled = false;
                txtmrd_templatename.ReadOnly = true;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnTimFileKetQua_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtmrd_templatename.Text = openFileDialogSelect.FileName;
                    this.mrd_templatenameSelect = openFileDialogSelect.SafeFileName;

                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void treeListDSDichVu_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            try
            {
                if (e.Node == (sender as TreeList).FocusedNode)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void treeListDSDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                var nodes = treeListDSDichVu.FocusedNode;
                mrd_servicerefidCurrent = Convert.ToInt64(nodes.GetValue(mrd_servicerefid).ToString());
                var serviceprice = lstmrd_serviceref.Where(o => o.mrd_servicerefid == mrd_servicerefidCurrent).FirstOrDefault();
                if (serviceprice != null)
                {
                    txtservicepricecode.Text = serviceprice.servicepricecode;
                    txtservicepricegroupcode.Text = serviceprice.servicepricegroupcode;
                    txtservicepricecodeuser.Text = serviceprice.servicepricecodeuser;
                    txtservicepricenamenhandan.Text = serviceprice.servicepricenamenhandan;
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
                    btnTimFileKetQua.Enabled = false;
                }
                else
                {
                    txtservicepricecode.Clear();
                    txtservicepricegroupcode.Clear();
                    txtservicepricecodeuser.Clear();
                    txtservicepricenamenhandan.Clear();
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
                    btnTimFileKetQua.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        void Item_ImportExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Xuất danh sách dịch vụ")
                {
                    long servicegrouptype = 0;
                    int servicelock = 0;
                    if (radioKhamBenh.Checked)
                    {
                        servicegrouptype = 1;
                    }
                    if (radioXetNghiem.Checked)
                    {
                        servicegrouptype = 2;
                    }
                    if (radioCDHA.Checked)
                    {
                        servicegrouptype = 3;
                    }
                    if (radioChuyenKhoa.Checked)
                    {
                        servicegrouptype = 4;
                    }
                    if (chkDaKhoa.Checked)
                    {
                        servicelock = 1;
                    }

                    string sql_laydanhsach = "select ROW_NUMBER () OVER (ORDER BY bhyt_groupcode, servicepricename) as stt, mrd_servicerefid, his_servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid, mrd_templatename from mrd_serviceref where servicegrouptype=" + servicegrouptype + " and servicelock=" + servicelock + ";";
                    DataTable dv_dataserviceref = condb.GetDataTable_HSBA(sql_laydanhsach);
                    if (dv_dataserviceref.Rows.Count > 0)
                    {
                        List<DTO.reportExcelDTO> thongTinThem = new List<DTO.reportExcelDTO>();
                        DTO.reportExcelDTO reportitem = new DTO.reportExcelDTO();
                        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                        reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        thongTinThem.Add(reportitem);
                        string fileTemplatePath = "0_Mrd_DMDV__ExportTemplateKetQua.xlsx";
                        Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                        export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dv_dataserviceref);
                    }
                }
                else if (tenbaocao == "Cập nhật template kết quả")
                {
                    if (openFileDialogImport.ShowDialog() == DialogResult.OK)
                    {
                        List<MrdServicerefDTO> lstServiceTemplateRef = new List<MrdServicerefDTO>();
                        Workbook workbook = new Workbook(openFileDialogImport.FileName);
                        Worksheet worksheet = workbook.Worksheets[worksheetName];
                        DataTable data_Excel = worksheet.Cells.ExportDataTable(6, 0, worksheet.Cells.MaxDataRow - 5, worksheet.Cells.MaxDataColumn + 1, true);
                        data_Excel.TableName = "DATA";
                        if (data_Excel != null && data_Excel.Rows.Count > 0)
                        {
                            int count_row = 0;
                            foreach (DataRow row in data_Excel.Rows)
                            {
                                try
                                {
                                    if (row["STT"].ToString() != "")
                                    {
                                        string mrd_templatename = row["MRD_TEMPLATENAME"].ToString();
                                        if (mrd_templatename != null && mrd_templatename != "")
                                        {
                                            string sqlupdateTemplate = "Update mrd_serviceref set mrd_templatename='" + mrd_templatename + "' where mrd_servicerefid='" + row["MRD_SERVICEREFID"].ToString() + "';";
                                            if (condb.ExecuteNonQuery_HSBA(sqlupdateTemplate))
                                            {
                                                count_row += 1;
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            MessageBox.Show("Cập nhật phiếu template thành công SL=" + count_row, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                            frmthongbao.Show();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }








    }
}
