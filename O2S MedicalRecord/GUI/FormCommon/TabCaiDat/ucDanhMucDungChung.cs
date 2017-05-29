using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace O2S_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class ucDanhMucDungChung : UserControl
    {
        #region Khai bao
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();
        private DataView loaiDanhMuc { get; set; } 
        private long selectmrd_othertypelistid { get; set; }
        private long selectmrd_otherlistid { get; set; }

        #endregion
        public ucDanhMucDungChung()
        {
            InitializeComponent();
        }

        #region Load
        private void ucDanhMucDungChung_Load(object sender, EventArgs e)
        {
            try
            {
                Load_ControlDefault();
                LoadDS_LoaiDanhMuc();
                LoadDS_DanhMuc(0);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void Load_ControlDefault()
        {
            try
            {
                btnLoaiDM_Them.Enabled = true;
                btnLoaiDM_Luu.Enabled = false;
                txtLoaiDM_Ma.ReadOnly = true;
                txtLoaiDM_Ten.ReadOnly = true;

                btnDM_Them.Enabled = false;
                btnDM_Luu.Enabled = false;
                txtDM_Ma.ReadOnly = true;
                txtDM_Ten.ReadOnly = true;
                txtDM_GiaTri.ReadOnly = true;
                cboDM_LoaiDMTen.ReadOnly = true;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDS_LoaiDanhMuc()
        {
            try
            {
                loaiDanhMuc = new DataView();
                string sqlgetdanhsach = "select ROW_NUMBER() OVER (ORDER BY mrd_othertypelistname) as stt, mrd_othertypelistid, mrd_othertypelistcode, mrd_othertypelistname, mrd_othertypeliststatus from mrd_othertypelist; ";
                DataView dataDanhSach = new DataView(condb.GetDataTable_HSBA(sqlgetdanhsach));
                gridControlLoaiDM.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DisplayMember = "mrd_othertypelistname";
                cboDM_LoaiDMTen.Properties.ValueMember = "mrd_othertypelistid";
                loaiDanhMuc = dataDanhSach;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDS_DanhMuc(long othertypelistid)
        {
            try
            {
                string mrd_othertypelistid = "";
                if (othertypelistid != 0)
                {
                    mrd_othertypelistid = " where oty.mrd_othertypelistid=" + othertypelistid;
                }
                string sqlgetdanhsach = "select ROW_NUMBER() OVER (ORDER BY oty.mrd_othertypelistname, o.mrd_otherlistname) as stt, oty.mrd_othertypelistid, oty.mrd_othertypelistcode, oty.mrd_othertypelistname, o.mrd_otherlistid, o.mrd_otherlistcode, o.mrd_otherlistname, o.mrd_otherliststatus from mrd_othertypelist oty inner join mrd_otherlist o on o.mrd_othertypelistid=oty.mrd_othertypelistid " + mrd_othertypelistid + "; ";
                DataView dataDanhSach = new DataView(condb.GetDataTable_HSBA(sqlgetdanhsach));
                gridControlDM.DataSource = dataDanhSach;
                cboDM_LoaiDMTen.Properties.DataSource = this.loaiDanhMuc;
                cboDM_LoaiDMTen.Properties.DisplayMember = "mrd_othertypelistname";
                cboDM_LoaiDMTen.Properties.ValueMember = "mrd_othertypelistid";
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Loai danh muc
        private void btnLoaiDM_Them_Click(object sender, EventArgs e)
        {
            try
            {
                this.selectmrd_othertypelistid = 0;
                btnLoaiDM_Them.Enabled = true;
                btnLoaiDM_Luu.Enabled = true;
                txtLoaiDM_Ma.ReadOnly = false;
                txtLoaiDM_Ten.ReadOnly = false;
                txtLoaiDM_Ma.Text = "";
                txtLoaiDM_Ten.Text = "";

                btnDM_Them.Enabled = false;
                btnDM_Luu.Enabled = false;
                txtDM_Ma.ReadOnly = true;
                txtDM_Ten.ReadOnly = true;
                txtDM_GiaTri.ReadOnly = true;
                txtDM_Ma.Text = "";
                txtDM_Ten.Text = "";
                txtDM_GiaTri.Text = "";
                cboDM_LoaiDMTen.ReadOnly = true;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void btnLoaiDM_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoaiDM_Ma.Text.Trim() != "" && txtLoaiDM_Ten.Text.Trim() != "")
                {
                    if (this.selectmrd_othertypelistid == 0)
                    {
                        string kiemtratontai = "select mrd_othertypelistid from mrd_othertypelist where mrd_othertypelistcode='" + txtLoaiDM_Ma.Text.Trim().ToUpper() + "';";
                        DataView dataDanhSach = new DataView(condb.GetDataTable_HSBA(kiemtratontai));
                        if (dataDanhSach != null && dataDanhSach.Count > 0)
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.KHONG_THE_SU_DUNG_MA_NAY);
                            frmthongbao.Show();
                        }
                        else //them moi
                        {
                            string insert = "INSERT INTO mrd_othertypelist(mrd_othertypelistcode, mrd_othertypelistname) VALUES ('" + txtLoaiDM_Ma.Text.Trim().ToUpper() + "', '" + txtLoaiDM_Ten.Text.Trim() + "'); ";
                            if (condb.ExecuteNonQuery_HSBA(insert))
                            {
                                O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    else//cap nhat
                    {
                        string insert = "UPDATE mrd_othertypelist SET mrd_othertypelistname='" + txtLoaiDM_Ten.Text.Trim() + "' WHERE mrd_othertypelistid=" + this.selectmrd_othertypelistid + "; ";
                        if (condb.ExecuteNonQuery_HSBA(insert))
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                    LoadDS_LoaiDanhMuc();
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlLoaiDM_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewLoaiDM.RowCount > 0)
                {
                    var rowHandle = gridViewLoaiDM.FocusedRowHandle;

                    this.selectmrd_othertypelistid = Utilities.Util_TypeConvertParse.ToInt64(gridViewLoaiDM.GetRowCellValue(rowHandle, "mrd_othertypelistid").ToString());
                    txtLoaiDM_Ma.Text = gridViewLoaiDM.GetRowCellValue(rowHandle, "mrd_othertypelistcode").ToString();
                    txtLoaiDM_Ten.Text = gridViewLoaiDM.GetRowCellValue(rowHandle, "mrd_othertypelistname").ToString();
                    btnLoaiDM_Them.Enabled = true;
                    btnLoaiDM_Luu.Enabled = true;
                    txtLoaiDM_Ma.ReadOnly = true;
                    txtLoaiDM_Ten.ReadOnly = false;
                    LoadDS_DanhMuc(this.selectmrd_othertypelistid);

                    btnDM_Them.Enabled = true;
                    btnDM_Luu.Enabled = true;
                    txtDM_Ma.ReadOnly = true;
                    txtDM_Ten.ReadOnly = true;
                    txtDM_GiaTri.ReadOnly = true;
                    txtDM_Ma.Text = "";
                    txtDM_Ten.Text = "";
                    txtDM_GiaTri.Text = "";
                    cboDM_LoaiDMTen.ReadOnly = true;
                    cboDM_LoaiDMTen.EditValue = null;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Danh muc
        private void btnDM_Them_Click(object sender, EventArgs e)
        {
            try
            {
                this.selectmrd_otherlistid = 0;
                btnLoaiDM_Them.Enabled = false;
                btnLoaiDM_Luu.Enabled = false;
                txtLoaiDM_Ma.ReadOnly = true;
                txtLoaiDM_Ten.ReadOnly = true;
                txtLoaiDM_Ma.Text = "";
                txtLoaiDM_Ten.Text = "";

                btnDM_Them.Enabled = true;
                btnDM_Luu.Enabled = true;
                txtDM_Ma.ReadOnly = false;
                txtDM_Ten.ReadOnly = false;
                txtDM_GiaTri.ReadOnly = false;
                txtDM_Ma.Text = "";
                txtDM_Ten.Text = "";
                txtDM_GiaTri.Text = "";
                cboDM_LoaiDMTen.ReadOnly = false;
                cboDM_LoaiDMTen.EditValue = null;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void btnDM_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDM_Ma.Text.Trim() != "" && txtDM_Ten.Text.Trim() != "" && cboDM_LoaiDMTen.EditValue != null)
                {
                    if (this.selectmrd_otherlistid == 0)
                    {
                        string kiemtratontai = "select mrd_otherlistid from mrd_otherlist where mrd_otherlistcode='" + txtDM_Ma.Text.Trim().ToUpper() + "';";
                        DataView dataDanhSach = new DataView(condb.GetDataTable_HSBA(kiemtratontai));
                        if (dataDanhSach != null && dataDanhSach.Count > 0)
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.KHONG_THE_SU_DUNG_MA_NAY);
                            frmthongbao.Show();
                        }
                        else //them moi
                        {
                            string insert = "INSERT INTO mrd_otherlist(mrd_otherlistcode, mrd_otherlistname, mrd_othertypelistid, mrd_otherlistvalue) VALUES ('" + txtDM_Ma.Text.Trim().ToUpper() + "', '" + txtDM_Ten.Text.Trim() + "','" + cboDM_LoaiDMTen.EditValue.ToString() + "', '" + txtDM_GiaTri.Text.Trim().ToUpper() + "'); ";
                            if (condb.ExecuteNonQuery_HSBA(insert))
                            {
                                O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                                frmthongbao.Show();
                            }
                        }
                    }
                    else//cap nhat
                    {
                        string insert = "UPDATE mrd_otherlist SET mrd_otherlistname='" + txtDM_Ten.Text.Trim() + "', mrd_otherlistvalue='" + txtDM_GiaTri.Text.Trim() + "' WHERE mrd_otherlistid=" + this.selectmrd_otherlistid + "; ";
                        if (condb.ExecuteNonQuery_HSBA(insert))
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                    LoadDS_DanhMuc(Utilities.Util_TypeConvertParse.ToInt64(cboDM_LoaiDMTen.EditValue.ToString()));
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void gridControlDM_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDM.RowCount > 0)
                {
                    var rowHandle = gridViewDM.FocusedRowHandle;

                    this.selectmrd_otherlistid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDM.GetRowCellValue(rowHandle, "mrd_otherlistid").ToString());
                    txtDM_Ma.Text = gridViewDM.GetRowCellValue(rowHandle, "mrd_otherlistcode").ToString();
                    txtDM_Ten.Text = gridViewDM.GetRowCellValue(rowHandle, "mrd_otherlistname").ToString();
                    cboDM_LoaiDMTen.EditValue = gridViewDM.GetRowCellValue(rowHandle, "mrd_othertypelistid");
                    txtDM_GiaTri.Text = gridViewDM.GetRowCellValue(rowHandle, "mrd_otherlistvalue").ToString();
                    btnDM_Them.Enabled = true;
                    btnDM_Luu.Enabled = true;
                    txtDM_Ma.ReadOnly = true;
                    txtDM_Ten.ReadOnly = false;
                    txtDM_GiaTri.ReadOnly = false;
                    cboDM_LoaiDMTen.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        #region Custom
        private void gridViewLoaiDM_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (e.RowHandle == view.FocusedRowHandle)
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
        #endregion





    }
}
