using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using O2S_MedicalRecord.Utilities.ThongBao;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using System.Collections;
using DevExpress.XtraGrid.Views.Base;
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Utilities.GUIGridView;
using DevExpress.Utils.Menu;
using O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn;

namespace O2S_MedicalRecord.GUI.ChucNang
{
    public partial class ucDanhSachHoSoBenhAn : UserControl
    {
        #region Khai bao
        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        //lay thong tin ve benh nhan
        public delegate void GetStringThongTinBenhNhan(long medicalrecordid, long hosobenhanid);
        public GetStringThongTinBenhNhan MyGetDataThongTinBenhNhan;

        private DAL.ConnectDatabase conn = new DAL.ConnectDatabase();

        #endregion
        public ucDanhSachHoSoBenhAn()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDanhSachHoSoBenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataDefault();
                LoadTabChucNangVaPhanQuyen();
                LoadDanhSachKhoa();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void LoadDataDefault()
        {
            try
            {
                dateTuNgay.DateTime = Convert.ToDateTime(DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd") + " 00:00:00");
                dateDenNgay.DateTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");

                xtraTabThongTinChung.PageVisible = false;
                xtraTabHSBA.PageVisible = false;
                xtraTabHoiChan.PageVisible = false;
                xtraTabPTTT.PageVisible = false;

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadTabChucNangVaPhanQuyen()
        {
            try
            {
                if (O2S_MedicalRecord.Base.CheckPermission.ChkPerModule("TOOL_01")) //Thong tin chung
                {
                    xtraTabThongTinChung.PageVisible = true;
                    xtraTabThongTinChung.Controls.Clear();
                    O2S_MedicalRecord.GUI.ChucNang.HSBA_ThongTinChung.ucHSBA_ThongTinChung ucHSBA_TTChung = new HSBA_ThongTinChung.ucHSBA_ThongTinChung();
                    //ucHSBA_TTChung.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                    ucHSBA_TTChung.Dock = System.Windows.Forms.DockStyle.Fill;
                    xtraTabThongTinChung.Controls.Add(ucHSBA_TTChung);
                }

                if (O2S_MedicalRecord.Base.CheckPermission.ChkPerModule("TOOL_01")) //Ho so benh an
                {
                    xtraTabHSBA.PageVisible = true;
                    xtraTabHSBA.Controls.Clear();
                    O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn.ucHSBA_BenhAn ucHSBA_BenhAn = new HSBA_BenhAn.ucHSBA_BenhAn();
                    //ucHSBA_BenhAn.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                    ucHSBA_BenhAn.Dock = System.Windows.Forms.DockStyle.Fill;
                    xtraTabHSBA.Controls.Add(ucHSBA_BenhAn);
                }

                if (O2S_MedicalRecord.Base.CheckPermission.ChkPerModule("TOOL_03")) //Hoi chan
                {
                    xtraTabHoiChan.PageVisible = true;
                    //xtraTabHSBA.Controls.Clear();
                    //O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn.ucHSBA_BenhAn ucHSBA_BenhAn = new HSBA_BenhAn.ucHSBA_BenhAn();
                    ////ucHSBA_BenhAn.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                    //ucHSBA_BenhAn.Dock = System.Windows.Forms.DockStyle.Fill;
                    //xtraTabHSBA.Controls.Add(ucHSBA_BenhAn);
                }

                if (O2S_MedicalRecord.Base.CheckPermission.ChkPerModule("TOOL_04")) //PTTT
                {
                    xtraTabPTTT.PageVisible = true;
                    xtraTabPTTT.Controls.Clear();
                    O2S_MedicalRecord.GUI.ChucNang.HSBA_PTTT.ucHSBA_PTTT ucHSBA_PTTT = new HSBA_PTTT.ucHSBA_PTTT();
                    //ucHSBA_PTTT.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                    ucHSBA_PTTT.Dock = System.Windows.Forms.DockStyle.Fill;
                    xtraTabPTTT.Controls.Add(ucHSBA_PTTT);
                }

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachKhoa()
        {
            try
            {
                //linq groupby
                var lstDSKhoa = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgrouptype == 1 || o.departmentgrouptype == 4 || o.departmentgrouptype == 11).ToList().GroupBy(o => o.departmentgroupid).Select(n => n.First()).ToList();
                if (lstDSKhoa != null && lstDSKhoa.Count > 0)
                {
                    cboKhoa.Properties.DataSource = lstDSKhoa;
                    cboKhoa.Properties.DisplayMember = "departmentgroupname";
                    cboKhoa.Properties.ValueMember = "departmentgroupid";
                }
                if (lstDSKhoa.Count == 1)
                {
                    cboKhoa.ItemIndex = 0;
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }


        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue == null || cboPhong.EditValue == null)
                {
                    frmThongBao frmthongbao = new frmThongBao(Base.ThongBaoLable.CHUA_CHON_KHOA_PHONG);
                    frmthongbao.Show();
                }
                else
                {
                    gridControlDSHSBA.DataSource = null;

                    string thoiGianTu = DateTime.ParseExact(dateTuNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string thoiGianDen = DateTime.ParseExact(dateDenNgay.Text, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
                    string medicalrecordstatus = "";
                    if (cboTrangThai.Text == "Chưa tiếp nhận")
                    {
                        medicalrecordstatus = " and mrd.medicalrecordstatus=0";
                    }
                    else if (cboTrangThai.Text == "Đang điều trị")
                    {
                        medicalrecordstatus = " and mrd.medicalrecordstatus=2";
                    }
                    else if (cboTrangThai.Text == "Đã đóng bệnh án")
                    {
                        medicalrecordstatus = " and mrd.medicalrecordstatus=99";
                    }

                    string getdsbenhnhan = "SELECT ROW_NUMBER () OVER (ORDER BY medicalrecordstatus, mrd.thoigianvaovien desc) as stt, hsba.patientid, hsba.patientcode, hsba.patientname, mrd.medicalrecordid, mrd.medicalrecordcode, hsba.bhytcode, mrd.thoigianvaovien, mrd.medicalrecordstatus, hsba.hosobenhanid FROM medicalrecord mrd inner join hosobenhan hsba on mrd.hosobenhanid=hsba.hosobenhanid WHERE mrd.thoigianvaovien>='" + thoiGianTu + "' and mrd.thoigianvaovien<='" + thoiGianDen + "'" + medicalrecordstatus + " and mrd.departmentid='" + cboPhong.EditValue.ToString() + "';";
                    gridControlDSHSBA.DataSource = conn.GetDataTable_HIS(getdsbenhnhan);
                    if (gridViewDSHSBA.RowCount > 0)
                    {
                        gridViewDSHSBA.FocusedRowHandle = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridControlDSHSBA_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewDSHSBA.FocusedRowHandle;
                MedicalrecordDTO filterDTO = new MedicalrecordDTO();
                filterDTO.medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                filterDTO.hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "hosobenhanid").ToString());
                filterDTO.medicalrecordstatus = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "medicalrecordstatus").ToString());
                filterDTO.departmentid = Utilities.Util_TypeConvertParse.ToInt64(cboPhong.EditValue.ToString());
                LoadDataTabChucNangClickRow(filterDTO);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        //Xem lai - sua lai toi uu hon
        private void LoadDataTabChucNangClickRow(MedicalrecordDTO filterDTO)
        {
            try
            {
                xtraTabThongTinChung.Controls.Clear();
                O2S_MedicalRecord.GUI.ChucNang.HSBA_ThongTinChung.ucHSBA_ThongTinChung ucHSBA_TTChung = new HSBA_ThongTinChung.ucHSBA_ThongTinChung(filterDTO);
                //ucHSBA_TTChung.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                ucHSBA_TTChung.Dock = System.Windows.Forms.DockStyle.Fill;
                xtraTabThongTinChung.Controls.Add(ucHSBA_TTChung);

                xtraTabHSBA.Controls.Clear();
                O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn.ucHSBA_BenhAn ucHSBA_BenhAn = new HSBA_BenhAn.ucHSBA_BenhAn(filterDTO);
                //ucHSBA_BenhAn.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                ucHSBA_BenhAn.Dock = System.Windows.Forms.DockStyle.Fill;
                xtraTabHSBA.Controls.Add(ucHSBA_BenhAn);

                xtraTabPTTT.Controls.Clear();
                O2S_MedicalRecord.GUI.ChucNang.HSBA_PTTT.ucHSBA_PTTT ucHSBA_PTTT = new HSBA_PTTT.ucHSBA_PTTT(filterDTO);
                //ucHSBA_PTTT.MyGetData = new FormCommon.ucTrangChu.GetString(HienThiTenChucNang);
                ucHSBA_PTTT.Dock = System.Windows.Forms.DockStyle.Fill;
                xtraTabPTTT.Controls.Add(ucHSBA_PTTT);

            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }



        private void xtraTabTTHSBA_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (e.Page.Name == xtraTabThongTinChung.Name)
                {

                }
                else if (e.Page.Name == xtraTabPTTT.Name)
                {

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSHSBA_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Nhập hồ sơ bệnh án");
                    itemXoaPhieuChiDinh.Image = imMenu.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(ItemNhapHoSoBenhAn_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        void ItemNhapHoSoBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị tại dòng click chuột
                var rowHandle = gridViewDSHSBA.FocusedRowHandle;
                MedicalrecordDTO filterDTO = new MedicalrecordDTO();
                filterDTO.medicalrecordid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "medicalrecordid").ToString());
                filterDTO.hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "hosobenhanid").ToString());
                filterDTO.medicalrecordstatus = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSHSBA.GetRowCellValue(rowHandle, "medicalrecordstatus").ToString());
                filterDTO.departmentid = Utilities.Util_TypeConvertParse.ToInt64(cboPhong.EditValue.ToString());

                if (filterDTO.medicalrecordstatus == 0)
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_NHAN_CHUA_DUOC_TIEP_NHAN_VAO_PHONG);
                    frmthongbao.Show();
                }
                else if (filterDTO.medicalrecordstatus == 2)
                {
                    frmChonLoaiBenhAn frmChon = new frmChonLoaiBenhAn(filterDTO);
                    frmChon.ShowDialog();
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_AN_DA_KET_THUC);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Custom
        private void cboPhong_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                btnRefresh_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void dateTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue != null || cboPhong.EditValue != null)
                {
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void dateDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue != null || cboPhong.EditValue != null)
                {
                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void gridViewDSHSBA_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }
        private void gridViewDSHSBA_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "status_img")
                {
                    string val = Convert.ToString(gridViewDSHSBA.GetRowCellValue(e.RowHandle, "medicalrecordstatus"));
                    if (val == "2")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[0]);
                        e.Graphics.DrawImage(imageListstatus.Images[0], pos);
                    }
                    else if (val == "99")
                    {
                        e.Handled = true;
                        Point pos = Util_GUIGridView.CalcPosition(e, imageListstatus.Images[1]);
                        e.Graphics.DrawImage(imageListstatus.Images[1], pos);
                    }

                }
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void cboKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhoa.EditValue != null)
                {
                    //Load danh muc phong thuoc khoa
                    var lstDSPhong = Base.SessionLogin.SessionlstPhanQuyen_KhoaPhong.Where(o => o.departmentgroupid == Utilities.Util_TypeConvertParse.ToInt64(cboKhoa.EditValue.ToString())).OrderBy(o => o.departmentname).ToList();
                    if (lstDSPhong != null && lstDSPhong.Count > 0)
                    {
                        cboPhong.Properties.DataSource = lstDSPhong;
                        cboPhong.Properties.DisplayMember = "departmentname";
                        cboPhong.Properties.ValueMember = "departmentid";
                    }
                    if (lstDSPhong.Count == 1)
                    {
                        cboPhong.ItemIndex = 0;
                        btnRefresh_Click(null, null);
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
