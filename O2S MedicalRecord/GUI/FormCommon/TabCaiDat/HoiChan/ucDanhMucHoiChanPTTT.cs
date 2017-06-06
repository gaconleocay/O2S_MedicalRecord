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
using O2S_MedicalRecord.DTO;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.Utils.Menu;
using Aspose.Cells;
using DevExpress.XtraGrid.Views.Grid;

namespace O2S_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class ucDanhMucHoiChanPTTT : UserControl
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<MrdDMHoiChanPTTTDTO> mrd_dmhc_pttt { get; set; }
        long mrd_dmhc_ptttidCurrent { get; set; }
        string mrd_templatenameSelect = "";
        private string worksheetName = "DanhMucHoiChanPTTT";
        public ucDanhMucHoiChanPTTT()
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
                txtmrd_dmhc_ptttnamepath.ReadOnly = true;
                // LoadDanhSachImportExport();
                GetDataDanhMucPTTT();
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
                menu.Items.Add(new DXMenuItem("Xuất danh sách danh mục"));
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
        private void GetDataDanhMucPTTT()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                this.mrd_dmhc_pttt = new List<MrdDMHoiChanPTTTDTO>();
                string sqlLayDanhMuc = "select mrd_dmhc_ptttid, servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, (case pttt_loaiid when 1 then 'Phẫu thuật đặc biệt' when 2 then 'Phẫu thuật loại 1' when 3 then 'Phẫu thuật loại 2' when 4 then 'Phẫu thuật loại 3' when 5 then 'Thủ thuật đặc biệt' when 6 then 'Thủ thuật loại 1' when 7 then 'Thủ thuật loại 2' when 8 then 'Thủ thuật loại 3' else '' end) as pttt_loai, servicepricecodeuser, servicepricesttuser, mrd_dmhc_ptttnamepath from mrd_dmhc_pttt order by servicepricegroupcode, servicepricename; ";
                DataView dv_DanhMucPTTTHC = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                if (dv_DanhMucPTTTHC.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucPTTTHC.Count; i++)
                    {
                        MrdDMHoiChanPTTTDTO dmDichVu = new MrdDMHoiChanPTTTDTO();
                        dmDichVu.stt = i + 1;
                        dmDichVu.mrd_dmhc_ptttid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["mrd_dmhc_ptttid"].ToString());
                        dmDichVu.servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucPTTTHC[i]["servicepricerefid"].ToString());
                        dmDichVu.servicepricecode = dv_DanhMucPTTTHC[i]["servicepricecode"].ToString();
                        dmDichVu.servicepricename = dv_DanhMucPTTTHC[i]["servicepricename"].ToString();
                        dmDichVu.servicepricenamenuocngoai = dv_DanhMucPTTTHC[i]["servicepricenamenuocngoai"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucPTTTHC[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = dv_DanhMucPTTTHC[i]["servicepricefee"].ToString();
                        dmDichVu.servicepricefeenhandan = dv_DanhMucPTTTHC[i]["servicepricefeenhandan"].ToString();
                        dmDichVu.servicepricefeebhyt = dv_DanhMucPTTTHC[i]["servicepricefeebhyt"].ToString();
                        dmDichVu.servicepricefeenuocngoai = dv_DanhMucPTTTHC[i]["servicepricefeenuocngoai"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucPTTTHC[i]["bhyt_groupcode"].ToString();
                        dmDichVu.servicepricecodeuser = dv_DanhMucPTTTHC[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricesttuser = dv_DanhMucPTTTHC[i]["servicepricesttuser"].ToString();
                        dmDichVu.mrd_dmhc_ptttnamepath = dv_DanhMucPTTTHC[i]["mrd_dmhc_ptttnamepath"].ToString();
                        dmDichVu.pttt_loai = dv_DanhMucPTTTHC[i]["pttt_loai"].ToString();
                        dmDichVu.servicepricecode_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricecode).ToLower();
                        dmDichVu.servicepricename_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.servicepricename).ToLower();
                        this.mrd_dmhc_pttt.Add(dmDichVu);
                    }
                }
                //hien thi
                if (this.mrd_dmhc_pttt != null && this.mrd_dmhc_pttt.Count > 0)
                {
                    if (txtTuKhoaTimKiem.Text.Trim() != "")
                    {
                        string tukhoa = Utilities.Common.String.Convert.UnSignVNese(txtTuKhoaTimKiem.Text.Trim().ToLower());
                        List<MrdDMHoiChanPTTTDTO> lstmrd_serviceref_timkiem = this.mrd_dmhc_pttt.Where(o => o.servicepricecode_khongdau.Contains(tukhoa) || o.servicepricename_khongdau.Contains(tukhoa)).ToList();
                        gridControlDMPTTTHC.DataSource = lstmrd_serviceref_timkiem;
                    }
                    else
                    {
                        gridControlDMPTTTHC.DataSource = this.mrd_dmhc_pttt;
                    }
                }
                else
                {
                    gridControlDMPTTTHC.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucPTTT();
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
                txtmrd_dmhc_ptttnamepath.ReadOnly = false;
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
                if (this.mrd_templatenameSelect != "" && this.mrd_dmhc_ptttidCurrent != 0)
                {
                    //Update servicepriceref
                    string updateservicepriceref = "update mrd_dmhc_pttt set mrd_dmhc_ptttnamepath='" + this.mrd_templatenameSelect + "' ;";
                    if (condb.ExecuteNonQuery_HSBA(updateservicepriceref))
                    {
                        MessageBox.Show("Cập nhật template thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                        txtmrd_dmhc_ptttnamepath.ReadOnly = true;
                        btnTimFileKetQua.Enabled = false;
                        GetDataDanhMucPTTT();
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
                mrd_dmhc_ptttidCurrent = 0;
                txtservicepricecode.Clear();
                txtservicepricesttuser.Clear();
                txtservicepricecodeuser.Clear();
                txtpttt_loai.Clear();
                txtservicepricename.Clear();
                txtservicepricenamenuocngoai.Clear();
                txtservicepricefeebhyt.Clear();
                txtservicepricefeenhandan.Clear();
                txtservicepricefee.Clear();
                txtservicepricefeenuocngoai.Clear();
                txtmrd_dmhc_ptttnamepath.Clear();
                txtbhyt_groupcode.Clear();

                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnTimFileKetQua.Enabled = false;
                txtmrd_dmhc_ptttnamepath.ReadOnly = true;
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
                    txtmrd_dmhc_ptttnamepath.Text = openFileDialogSelect.FileName;
                    this.mrd_templatenameSelect = openFileDialogSelect.SafeFileName;
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
                //string tenbaocao = ((DXMenuItem)sender).Caption;
                //if (tenbaocao == "Xuất danh sách danh mục")
                //{
                //    string sql_laydanhsach = "select ROW_NUMBER () OVER (ORDER BY medicinename) as stt, mrd_dmhc_PTTTid, medicinerefid, medicinecode, medicinegroupcode, medicinereftype, medicinetype, medicinename, tenkhoahoc, donvitinh, nongdo, lieuluong, hoatchat, gianhap, giaban, vatnhap, vatban, bhyt_groupcode, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, mahoatchat, medicinecodeuser, stt_dauthau, medicinename_byt, stt_PTTT_tt40, stt_PTTT_tt40text from mrd_dmhc_pttt;";
                //    DataTable dv_dataserviceref = condb.GetDataTable_HSBA(sql_laydanhsach);
                //    if (dv_dataserviceref.Rows.Count > 0)
                //    {
                //        List<DTO.reportExcelDTO> thongTinThem = new List<DTO.reportExcelDTO>();
                //        DTO.reportExcelDTO reportitem = new DTO.reportExcelDTO();
                //        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                //        reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //        thongTinThem.Add(reportitem);
                //        string fileTemplatePath = "1_Mrd_DMHCPTTT__ExportTemplateKetQua.xlsx";
                //        Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                //        export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dv_dataserviceref);
                //    }
                //}
                //else if (tenbaocao == "Cập nhật template kết quả")
                //{
                //    if (openFileDialogImport.ShowDialog() == DialogResult.OK)
                //    {
                //        List<MrdDMHoiChanPTTTDTO> lstServiceTemplateRef = new List<MrdDMHoiChanPTTTDTO>();
                //        Workbook workbook = new Workbook(openFileDialogImport.FileName);
                //        Worksheet worksheet = workbook.Worksheets[worksheetName];
                //        DataTable data_Excel = worksheet.Cells.ExportDataTable(6, 0, worksheet.Cells.MaxDataRow - 5, worksheet.Cells.MaxDataColumn + 1, true);
                //        data_Excel.TableName = "DATA";
                //        if (data_Excel != null && data_Excel.Rows.Count > 0)
                //        {
                //            int count_row = 0;
                //            foreach (DataRow row in data_Excel.Rows)
                //            {
                //                try
                //                {
                //                    if (row["STT"].ToString() != "")
                //                    {
                //                        string mrd_templatename = row["MRD_DMHC_THUOCNAMEPATH"].ToString();
                //                        if (mrd_templatename != null && mrd_templatename != "")
                //                        {
                //                            string sqlupdateTemplate = "Update mrd_dmhc_pttt set mrd_dmhc_ptttnamepath='" + mrd_templatename + "' where mrd_dmhc_PTTTid='" + row["MRD_DMHC_THUOCID"].ToString() + "';";
                //                            if (condb.ExecuteNonQuery_HSBA(sqlupdateTemplate))
                //                            {
                //                                count_row += 1;
                //                            }
                //                        }
                //                    }
                //                }
                //                catch (Exception)
                //                {
                //                    continue;
                //                }
                //            }
                //            MessageBox.Show("Cập nhật phiếu template thành công SL=" + count_row, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        }
                //        else
                //        {
                //            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.KHONG_TIM_THAY_BAN_GHI_NAO);
                //            frmthongbao.Show();
                //            return;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }


        private void gridViewDMPTTTHC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridControlDMPTTTHC_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDMPTTTHC.RowCount > 0)
                {
                    var rowHandle = gridViewDMPTTTHC.FocusedRowHandle;
                    this.mrd_dmhc_ptttidCurrent = Utilities.Util_TypeConvertParse.ToInt64(gridViewDMPTTTHC.GetRowCellValue(rowHandle, "mrd_dmhc_ptttid").ToString());
                    MrdDMHoiChanPTTTDTO hoichanPTTT = this.mrd_dmhc_pttt.Where(o => o.mrd_dmhc_ptttid == this.mrd_dmhc_ptttidCurrent).FirstOrDefault();
                    if (hoichanPTTT != null)
                    {
                        txtservicepricecode.Text = hoichanPTTT.servicepricecode;
                        txtservicepricecodeuser.Text = hoichanPTTT.servicepricecodeuser;
                        txtservicepricesttuser.Text = hoichanPTTT.servicepricesttuser;
                        txtbhyt_groupcode.Text = hoichanPTTT.bhyt_groupcode;
                        txtpttt_loai.Text = hoichanPTTT.pttt_loai;
                        txtservicepricename.Text = hoichanPTTT.servicepricename;
                        txtservicepricenamenuocngoai.Text = hoichanPTTT.servicepricenamenuocngoai;
                        txtservicepricefeebhyt.Text = hoichanPTTT.servicepricefeebhyt;
                        txtservicepricefeenhandan.Text = hoichanPTTT.servicepricefeenhandan;
                        txtservicepricefee.Text = hoichanPTTT.servicepricefee;
                        txtservicepricefeenuocngoai.Text = hoichanPTTT.servicepricefeenuocngoai;
                        txtmrd_dmhc_ptttnamepath.Text = hoichanPTTT.mrd_dmhc_ptttnamepath;
                    }
                    btnSua.Enabled = true;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                    btnTimFileKetQua.Enabled = false;
                    txtmrd_dmhc_ptttnamepath.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        //Them nhieu PTTT
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                frmChonDichVuPTTTHoiChan frmChon = new frmChonDichVuPTTTHoiChan();
                frmChon.ShowDialog();
                GetDataDanhMucPTTT();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        //Tu dong tim kiem va lay nhưng PTTT co hoat chat co dau * vào danh sách
        private void btnRefreshDS_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                int dem_insert = 0;
                int dem_update = 0;
                string mrd_dmhc_ptttnamepath = "";
                //kiemtra so sanh trong danh sach DM PTTT .
                string sql_kiemtra = "select servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, servicepricecodeuser, servicepricesttuser from servicepriceref where servicegrouptype = 4 and bhyt_groupcode in ('06PTTT','07KTC') and pttt_loaiid<>0;   ";
                DataView dv_DanhMucPTTTHIS = new DataView(condb.GetDataTable_HIS(sql_kiemtra));
                if (dv_DanhMucPTTTHIS != null && dv_DanhMucPTTTHIS.Count > 0)
                {
                    if (this.mrd_dmhc_pttt != null && this.mrd_dmhc_pttt.Count > 0) //kiemtra va update
                    {
                        MrdDMHoiChanPTTTDTO dm_hc_ptttt = this.mrd_dmhc_pttt.Where(o => o.mrd_dmhc_ptttnamepath != "").FirstOrDefault();
                        if (dm_hc_ptttt != null)
                        {
                            mrd_dmhc_ptttnamepath = dm_hc_ptttt.mrd_dmhc_ptttnamepath;
                        }
                        for (int i = 0; i < dv_DanhMucPTTTHIS.Count; i++)
                        {
                            List<MrdDMHoiChanPTTTDTO> lsttpttt = this.mrd_dmhc_pttt.Where(o => o.servicepricecode == dv_DanhMucPTTTHIS[i]["servicepricecode"].ToString()).ToList();
                            if (lsttpttt != null && lsttpttt.Count > 0) //update
                            {
                                string sql_update = "update mrd_dmhc_pttt set servicepricerefid='" + dv_DanhMucPTTTHIS[i]["servicepricerefid"].ToString() + "', servicepricegroupcode='" + dv_DanhMucPTTTHIS[i]["servicepricegroupcode"].ToString() + "', servicegrouptype='" + dv_DanhMucPTTTHIS[i]["servicegrouptype"].ToString() + "', servicepricetype='" + dv_DanhMucPTTTHIS[i]["servicepricetype"].ToString() + "', servicepricename='" + dv_DanhMucPTTTHIS[i]["servicepricename"].ToString() + "', servicepricenamebhyt='" + dv_DanhMucPTTTHIS[i]["servicepricenamebhyt"].ToString() + "', servicepricenamenuocngoai='" + dv_DanhMucPTTTHIS[i]["servicepricenamenuocngoai"].ToString() + "', servicepriceunit='" + dv_DanhMucPTTTHIS[i]["servicepriceunit"].ToString() + "', servicepricefee='" + dv_DanhMucPTTTHIS[i]["servicepricefee"].ToString() + "', servicepricefeenhandan='" + dv_DanhMucPTTTHIS[i]["servicepricefeenhandan"].ToString() + "', servicepricefeebhyt='" + dv_DanhMucPTTTHIS[i]["servicepricefeebhyt"].ToString() + "', servicepricefeenuocngoai='" + dv_DanhMucPTTTHIS[i]["servicepricefeenuocngoai"].ToString() + "', bhyt_groupcode='" + dv_DanhMucPTTTHIS[i]["bhyt_groupcode"].ToString() + "', pttt_hangid='" + dv_DanhMucPTTTHIS[i]["pttt_hangid"].ToString() + "', pttt_loaiid='" + dv_DanhMucPTTTHIS[i]["pttt_loaiid"].ToString() + "', servicepricecodeuser='" + dv_DanhMucPTTTHIS[i]["servicepricecodeuser"].ToString() + "', servicepricesttuser='" + dv_DanhMucPTTTHIS[i]["servicepricesttuser"].ToString() + "'  where servicepricecode='" + dv_DanhMucPTTTHIS[i]["servicepricecode"].ToString() + "'; ";
                                if (condb.ExecuteNonQuery_HSBA(sql_update))
                                {
                                    dem_update += 1;
                                }
                            }
                            else //insert
                            {
                                string sql_insert = "insert into mrd_dmhc_pttt(servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, servicepricecodeuser, servicepricesttuser, mrd_dmhc_ptttnamepath) values('" + dv_DanhMucPTTTHIS[i]["servicepricerefid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricecode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricegroupcode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicegrouptype"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricetype"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricename"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricenamebhyt"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricenamenuocngoai"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepriceunit"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefee"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeenhandan"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeebhyt"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeenuocngoai"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["bhyt_groupcode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["pttt_hangid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["pttt_loaiid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricecodeuser"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricesttuser"].ToString() + "', '" + mrd_dmhc_ptttnamepath + "'); ";
                                if (condb.ExecuteNonQuery_HSBA(sql_insert))
                                {
                                    dem_insert += 1;
                                }
                            }
                        }
                    }
                    else //insert tat ca
                    {
                        for (int i = 0; i < dv_DanhMucPTTTHIS.Count; i++)
                        {
                            string sql_insert = "insert into mrd_dmhc_pttt(servicepricerefid, servicepricecode, servicepricegroupcode, servicegrouptype, servicepricetype, servicepricename, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, bhyt_groupcode, pttt_hangid, pttt_loaiid, servicepricecodeuser, servicepricesttuser, mrd_dmhc_ptttnamepath) values('" + dv_DanhMucPTTTHIS[i]["servicepricerefid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricecode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricegroupcode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicegrouptype"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricetype"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricename"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricenamebhyt"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricenamenuocngoai"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepriceunit"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefee"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeenhandan"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeebhyt"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricefeenuocngoai"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["bhyt_groupcode"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["pttt_hangid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["pttt_loaiid"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricecodeuser"].ToString() + "', '" + dv_DanhMucPTTTHIS[i]["servicepricesttuser"].ToString() + "', ''); ";
                            if (condb.ExecuteNonQuery_HSBA(sql_insert))
                            {
                                dem_insert += 1;
                            }
                        }
                    }
                }
                MessageBox.Show("Làm mới danh mục thành công (thêm mới SL=[" + dem_insert + "]; cập nhật SL=[" + dem_update + "]) !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
            GetDataDanhMucPTTT();
        }

        private void txtTuKhoaTimKiem_TextChanged(object sender, EventArgs e)
        {
            GetDataDanhMucPTTT();
        }
        private void txtTuKhoaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataDanhMucPTTT();
            }
        }

        #region xoa danh muc
        private void gridViewDMPTTTHC_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xóa dịch vụ khỏi danh sách");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(ItemXoaThuocKhoiDS_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        void ItemXoaThuocKhoiDS_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDMPTTTHC.FocusedRowHandle;
                long mrd_dmhc_thuocid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDMPTTTHC.GetRowCellValue(rowHandle, "mrd_dmhc_ptttid").ToString());
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string sql_delete = "delete from mrd_dmhc_pttt where mrd_dmhc_ptttid=" + mrd_dmhc_thuocid + ";";
                    if (condb.ExecuteNonQuery_HSBA(sql_delete))
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                        frmthongbao.Show();
                        GetDataDanhMucPTTT();
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion





    }
}
