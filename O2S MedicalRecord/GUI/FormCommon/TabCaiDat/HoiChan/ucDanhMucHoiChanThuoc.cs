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
    public partial class ucDanhMucHoiChanThuoc : UserControl
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        List<MrdDMHoiChanThuocDTO> mrd_dmhc_thuoc { get; set; }
        long mrd_dmhc_thuocidCurrent { get; set; }
        string mrd_templatenameSelect = "";
        private string worksheetName = "DanhMucHoiChanThuoc";
        public ucDanhMucHoiChanThuoc()
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
                // LoadDanhSachImportExport();
                GetDataDanhMucThuoc();
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

        private void GetDataDanhMucThuoc()
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                this.mrd_dmhc_thuoc = new List<MrdDMHoiChanThuocDTO>();
                string sqlLayDanhMuc = "select ROW_NUMBER () OVER (ORDER BY medicinename) as stt, mrd_dmhc_thuocid, medicinerefid, medicinecode, medicinegroupcode, medicinereftype, medicinetype, medicinename, tenkhoahoc, donvitinh, nongdo, lieuluong, hoatchat, gianhap, giaban, vatnhap, vatban, bhyt_groupcode, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, mahoatchat, medicinecodeuser, stt_dauthau, medicinename_byt, stt_thuoc_tt40, stt_thuoc_tt40text, mrd_dmhc_thuocnamepath from mrd_dmhc_thuoc; ";
                DataView dv_DanhMucThuocHC = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                if (dv_DanhMucThuocHC.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucThuocHC.Count; i++)
                    {
                        MrdDMHoiChanThuocDTO dmDichVu = new MrdDMHoiChanThuocDTO();
                        dmDichVu.stt = i + 1;
                        dmDichVu.mrd_dmhc_thuocid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucThuocHC[i]["mrd_dmhc_thuocid"].ToString());
                        dmDichVu.medicinerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucThuocHC[i]["medicinerefid"].ToString());
                        dmDichVu.medicinecode = dv_DanhMucThuocHC[i]["medicinecode"].ToString();
                        dmDichVu.medicinegroupcode = dv_DanhMucThuocHC[i]["medicinegroupcode"].ToString();
                        dmDichVu.medicinereftype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucThuocHC[i]["medicinereftype"].ToString());
                        dmDichVu.medicinetype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucThuocHC[i]["medicinetype"].ToString());
                        dmDichVu.medicinename = dv_DanhMucThuocHC[i]["medicinename"].ToString();
                        dmDichVu.medicinename_byt = dv_DanhMucThuocHC[i]["medicinename_byt"].ToString();
                        dmDichVu.donvitinh = dv_DanhMucThuocHC[i]["donvitinh"].ToString();
                        dmDichVu.hoatchat = dv_DanhMucThuocHC[i]["hoatchat"].ToString();
                        dmDichVu.mahoatchat = dv_DanhMucThuocHC[i]["mahoatchat"].ToString();
                        dmDichVu.bhyt_groupcode = dv_DanhMucThuocHC[i]["bhyt_groupcode"].ToString();
                        dmDichVu.medicinecodeuser = dv_DanhMucThuocHC[i]["medicinecodeuser"].ToString();
                        dmDichVu.stt_dauthau = dv_DanhMucThuocHC[i]["stt_dauthau"].ToString();
                        dmDichVu.stt_thuoc_tt40text = dv_DanhMucThuocHC[i]["stt_thuoc_tt40text"].ToString();
                        dmDichVu.mrd_dmhc_thuocnamepath = dv_DanhMucThuocHC[i]["mrd_dmhc_thuocnamepath"].ToString();
                        dmDichVu.medicinecode_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.medicinecode).ToLower();
                        dmDichVu.medicinename_khongdau = Utilities.Common.String.Convert.UnSignVNese(dmDichVu.medicinename).ToLower();
                        this.mrd_dmhc_thuoc.Add(dmDichVu);
                    }
                }
                //hien thi
                if (this.mrd_dmhc_thuoc != null && this.mrd_dmhc_thuoc.Count > 0)
                {
                    if (txtTuKhoaTimKiem.Text.Trim() != "")
                    {
                        string tukhoa = Utilities.Common.String.Convert.UnSignVNese(txtTuKhoaTimKiem.Text.Trim().ToLower());
                        List<MrdDMHoiChanThuocDTO> lstmrd_serviceref_timkiem = this.mrd_dmhc_thuoc.Where(o => o.medicinecode_khongdau.Contains(tukhoa) || o.medicinename_khongdau.Contains(tukhoa)).ToList();
                        gridControlDMThuocHC.DataSource = lstmrd_serviceref_timkiem;
                    }
                    else
                    {
                        gridControlDMThuocHC.DataSource = this.mrd_dmhc_thuoc;
                    }
                }
                else
                {
                    gridControlDMThuocHC.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
            SplashScreenManager.CloseForm();
        }
        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataDanhMucThuoc();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #region Button Clink row
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
                if (this.mrd_templatenameSelect != "" && this.mrd_dmhc_thuocidCurrent != 0)
                {
                    //Update servicepriceref
                    string updateservicepriceref = "update mrd_dmhc_thuoc set mrd_dmhc_thuocnamepath='" + this.mrd_templatenameSelect + "' ;";
                    if (condb.ExecuteNonQuery_HSBA(updateservicepriceref))
                    {
                        MessageBox.Show("Cập nhật template thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSua.Enabled = true;
                        btnLuu.Enabled = false;
                        btnHuy.Enabled = false;
                        txtmrd_templatename.ReadOnly = true;
                        btnTimFileKetQua.Enabled = false;
                        GetDataDanhMucThuoc();
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
                mrd_dmhc_thuocidCurrent = 0;
                txtmedicinecode.Clear();
                txtstt_dauthau.Clear();
                txtmedicinecodeuser.Clear();
                txtstt_thuoc_tt40text.Clear();
                txtmedicinename.Clear();
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
        #endregion

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
        void Item_ImportExport_Click(object sender, EventArgs e)
        {
            try
            {
                string tenbaocao = ((DXMenuItem)sender).Caption;
                if (tenbaocao == "Xuất danh sách danh mục")
                {
                    string sql_laydanhsach = "select ROW_NUMBER () OVER (ORDER BY medicinename) as stt, mrd_dmhc_thuocid, medicinerefid, medicinecode, medicinegroupcode, medicinereftype, medicinetype, medicinename, tenkhoahoc, donvitinh, nongdo, lieuluong, hoatchat, gianhap, giaban, vatnhap, vatban, bhyt_groupcode, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, mahoatchat, medicinecodeuser, stt_dauthau, medicinename_byt, stt_thuoc_tt40, stt_thuoc_tt40text from mrd_dmhc_thuoc;";
                    DataTable dv_dataserviceref = condb.GetDataTable_HSBA(sql_laydanhsach);
                    if (dv_dataserviceref.Rows.Count > 0)
                    {
                        List<DTO.reportExcelDTO> thongTinThem = new List<DTO.reportExcelDTO>();
                        DTO.reportExcelDTO reportitem = new DTO.reportExcelDTO();
                        reportitem.name = Base.bienTrongBaoCao.THOIGIANBAOCAO;
                        reportitem.value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        thongTinThem.Add(reportitem);
                        string fileTemplatePath = "1_Mrd_DMHCThuoc__ExportTemplateKetQua.xlsx";
                        Utilities.Common.Excel.ExcelExport export = new Utilities.Common.Excel.ExcelExport();
                        export.ExportExcelTemplate("", fileTemplatePath, thongTinThem, dv_dataserviceref);
                    }
                }
                else if (tenbaocao == "Cập nhật template kết quả")
                {
                    if (openFileDialogImport.ShowDialog() == DialogResult.OK)
                    {
                        List<MrdDMHoiChanThuocDTO> lstServiceTemplateRef = new List<MrdDMHoiChanThuocDTO>();
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
                                        string mrd_templatename = row["MRD_DMHC_THUOCNAMEPATH"].ToString();
                                        if (mrd_templatename != null && mrd_templatename != "")
                                        {
                                            string sqlupdateTemplate = "Update mrd_dmhc_thuoc set mrd_dmhc_thuocnamepath='" + mrd_templatename + "' where mrd_dmhc_thuocid='" + row["MRD_DMHC_THUOCID"].ToString() + "';";
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

        private void gridViewDMThuocHC_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridControlDMThuocHC_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDMThuocHC.RowCount > 0)
                {
                    var rowHandle = gridViewDMThuocHC.FocusedRowHandle;
                    this.mrd_dmhc_thuocidCurrent = Utilities.Util_TypeConvertParse.ToInt64(gridViewDMThuocHC.GetRowCellValue(rowHandle, "mrd_dmhc_thuocid").ToString());
                    txtmedicinecode.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "medicinecode").ToString();
                    txtmedicinecodeuser.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "medicinecodeuser").ToString();
                    txtstt_dauthau.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "stt_dauthau").ToString();
                    txtbhyt_groupcode.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "bhyt_groupcode").ToString();
                    txtstt_thuoc_tt40text.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "stt_thuoc_tt40text").ToString();
                    txtmedicinename.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "medicinename").ToString();
                    txtservicepricenamebhyt.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "medicinename_byt").ToString();
                    txtservicepricefeebhyt.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "servicepricefeebhyt").ToString();
                    txtservicepricefeenhandan.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "servicepricefeenhandan").ToString();
                    txtservicepricefee.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "servicepricefee").ToString();
                    txtservicepricefeenuocngoai.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "servicepricefeenuocngoai").ToString();
                    txtmrd_templatename.Text = gridViewDMThuocHC.GetRowCellValue(rowHandle, "mrd_dmhc_thuocnamepath").ToString();
                    btnSua.Enabled = true;
                    btnLuu.Enabled = false;
                    btnHuy.Enabled = false;
                    btnTimFileKetQua.Enabled = false;
                    txtmrd_templatename.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        //Them nhieu thuoc
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                frmChonThuocDauSao frmChon = new frmChonThuocDauSao();
                frmChon.ShowDialog();
                GetDataDanhMucThuoc();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        //Tu dong tim kiem va lay nhưng thuoc co hoat chat co dau * vào danh sách
        private void btnRefreshDS_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(O2S_MedicalRecord.Utilities.ThongBao.WaitForm1));
            try
            {
                int dem_insert = 0;
                int dem_update = 0;
                //kiemtra so sanh tron danh sach DM thuoc * co hay chua, neu co roi thi update thong tin thuoc, chưa có thì thêm mới.
                string sql_kiemtra = "select medicinerefid, medicinecode, medicinegroupcode, medicinereftype, medicinetype, medicinename, tenkhoahoc, donvitinh, nongdo, lieuluong, hoatchat, gianhap, giaban, vatnhap, vatban, bhyt_groupcode, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, mahoatchat, medicinecodeuser, stt_dauthau, medicinename_byt, stt_thuoc_tt40, stt_thuoc_tt40text from medicine_ref where medicinerefid in (select medicinerefid_org from medicine_ref where hoatchat like '%*%' and datatype=0 and medicinecode<>'' and isremove=0 group by medicinerefid_org);";
                DataView dv_DanhMucThuocHIS = new DataView(condb.GetDataTable_HIS(sql_kiemtra));
                if (dv_DanhMucThuocHIS != null && dv_DanhMucThuocHIS.Count > 0)
                {
                    if (this.mrd_dmhc_thuoc != null && this.mrd_dmhc_thuoc.Count > 0) //kiemtra va update
                    {
                        for (int i = 0; i < dv_DanhMucThuocHIS.Count; i++)
                        {
                            List<MrdDMHoiChanThuocDTO> lstthuoc = this.mrd_dmhc_thuoc.Where(o => o.medicinecode == dv_DanhMucThuocHIS[i]["medicinecode"].ToString()).ToList();
                            if (lstthuoc != null && lstthuoc.Count > 0) //update
                            {
                                string sql_update = "update mrd_dmhc_thuoc set medicinegroupcode='" + dv_DanhMucThuocHIS[i]["medicinegroupcode"].ToString() + "', medicinereftype='" + dv_DanhMucThuocHIS[i]["medicinereftype"].ToString() + "', medicinetype='" + dv_DanhMucThuocHIS[i]["medicinetype"].ToString() + "', medicinename='" + dv_DanhMucThuocHIS[i]["medicinename"].ToString() + "', donvitinh='" + dv_DanhMucThuocHIS[i]["donvitinh"].ToString() + "', mahoatchat='" + dv_DanhMucThuocHIS[i]["mahoatchat"].ToString() + "', hoatchat='" + dv_DanhMucThuocHIS[i]["hoatchat"].ToString() + "', bhyt_groupcode='" + dv_DanhMucThuocHIS[i]["bhyt_groupcode"].ToString() + "', medicinename_byt='" + dv_DanhMucThuocHIS[i]["medicinename_byt"].ToString() + "', medicinecodeuser='" + dv_DanhMucThuocHIS[i]["medicinecodeuser"].ToString() + "', stt_dauthau='" + dv_DanhMucThuocHIS[i]["stt_dauthau"].ToString() + "', stt_thuoc_tt40text='" + dv_DanhMucThuocHIS[i]["stt_thuoc_tt40text"].ToString() + "' where medicinecode='" + dv_DanhMucThuocHIS[i]["medicinecode"].ToString() + "'; ";
                                if (condb.ExecuteNonQuery_HSBA(sql_update))
                                {
                                    dem_update += 1;
                                }
                            }
                            else //insert
                            {
                                string sql_insert = "insert into mrd_dmhc_thuoc(medicinerefid, medicinegroupcode, medicinereftype, medicinetype, medicinecode, medicinename, donvitinh, mahoatchat, hoatchat, bhyt_groupcode, medicinename_byt, medicinecodeuser, stt_dauthau, stt_thuoc_tt40text) values ('" + dv_DanhMucThuocHIS[i]["medicinerefid"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinegroupcode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinereftype"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinetype"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinecode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinename"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["donvitinh"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["mahoatchat"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["hoatchat"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["bhyt_groupcode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinename_byt"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinecodeuser"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["stt_dauthau"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["stt_thuoc_tt40text"].ToString() + "');";
                                if (condb.ExecuteNonQuery_HSBA(sql_insert))
                                {
                                    dem_insert += 1;
                                }
                            }
                        }
                    }
                    else //insert tat ca
                    {
                        for (int i = 0; i < dv_DanhMucThuocHIS.Count; i++)
                        {
                            string sql_insert = "insert into mrd_dmhc_thuoc(medicinerefid, medicinegroupcode, medicinereftype, medicinetype, medicinecode, medicinename, donvitinh, mahoatchat, hoatchat, bhyt_groupcode, medicinename_byt, medicinecodeuser, stt_dauthau, stt_thuoc_tt40text) values ('" + dv_DanhMucThuocHIS[i]["medicinerefid"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinegroupcode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinereftype"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinetype"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinecode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinename"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["donvitinh"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["mahoatchat"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["hoatchat"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["bhyt_groupcode"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinename_byt"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["medicinecodeuser"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["stt_dauthau"].ToString() + "', '" + dv_DanhMucThuocHIS[i]["stt_thuoc_tt40text"].ToString() + "');";
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
            GetDataDanhMucThuoc();
        }

        private void txtTuKhoaTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetDataDanhMucThuoc();
            }
        }
        private void txtTuKhoaTimKiem_TextChanged(object sender, EventArgs e)
        {
            GetDataDanhMucThuoc();
        }

        #region Menu Xoa thuoc
        private void gridViewDMThuocHC_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Xóa thuốc khỏi danh sách");
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
                var rowHandle = gridViewDMThuocHC.FocusedRowHandle;
                long mrd_dmhc_thuocid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDMThuocHC.GetRowCellValue(rowHandle, "mrd_dmhc_thuocid").ToString());
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Thông báo !!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    string sql_delete = "delete from mrd_dmhc_thuoc where mrd_dmhc_thuocid=" + mrd_dmhc_thuocid + ";";
                    if (condb.ExecuteNonQuery_HSBA(sql_delete))
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(Base.ThongBaoLable.XOA_THANH_CONG);
                        frmthongbao.Show();
                        GetDataDanhMucThuoc();
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
