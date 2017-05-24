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
using O2S_MedicalRecord.DTO;
using O2S_MedicalRecord.Base;

namespace O2S_MedicalRecord.GUI.FormCommon.TabCaiDat
{
    public partial class ucDanhMucBenhAn : UserControl
    {
        O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();
        private long mrd_hsbatemid = 0;
        private string mrd_hsbatemnamepath = "";

        #region Load
        public ucDanhMucBenhAn()
        {
            InitializeComponent();
        }
        private void ucDanhMucBenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl(false);
                LoadDanhSachBenhAn();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void EnableAndDisableControl(bool result)
        {
            try
            {
                txtHSBATempCode.ReadOnly = true;
                txtHSBATempName.ReadOnly = !result;
                txtHSBATempNamePath.ReadOnly = !result;
                btnTimFileTemplate.Enabled = result;
                btnSua.Enabled = result;
                btnLuu.Enabled = result;
                btnHuy.Enabled = result;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachBenhAn()
        {
            try
            {
                string sqldsbenhan = "select ROW_NUMBER() OVER (ORDER BY mrd_hsbatemname) as stt, mrd_hsbatemid, mrd_hsbatemcode, mrd_hsbatemname, mrd_hsbatemtypeid, mrd_hsbatemnamepath from mrd_hsba_template;";
                DataView dataOption = new DataView(condb.GetDataTable_HSBA(sqldsbenhan));
                if (dataOption != null && dataOption.Count > 0)
                {
                    gridControlDSBenhAn.DataSource = dataOption;
                }
                else
                {
                    gridControlDSBenhAn.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void gridViewDSOption_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void btnTimFileTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialogSelect.ShowDialog() == DialogResult.OK)
                {
                    txtHSBATempNamePath.Text = openFileDialogSelect.FileName;
                    this.mrd_hsbatemnamepath = openFileDialogSelect.SafeFileName;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                txtHSBATempCode.ReadOnly = false;
                txtHSBATempName.ReadOnly = false;
                txtHSBATempNamePath.ReadOnly = false;
                btnTimFileTemplate.Enabled = true;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;
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
                txtHSBATempCode.ReadOnly = true;
                txtHSBATempName.ReadOnly = false;
                txtHSBATempNamePath.ReadOnly = false;
                EnableAndDisableControl(true);
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                gridControlDSBenhAn.Enabled = false;
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
                if (this.mrd_hsbatemid != 0) //sua
                {
                    //Update servicepriceref
                    string updateservicepriceref = "update mrd_hsba_template set mrd_hsbatemname='" + txtHSBATempName.Text.Trim() + "', mrd_hsbatemnamepath='" + this.mrd_hsbatemnamepath + "' where mrd_hsbatemid=" + this.mrd_hsbatemid + ";";
                    if (condb.ExecuteNonQuery_HSBA(updateservicepriceref))
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CAP_NHAT_THANH_CONG);
                        frmthongbao.Show();
                    }
                    gridControlDSBenhAn.Enabled = true;
                }
                else //them moi
                {
                    if (txtHSBATempCode.Text != "" && this.mrd_hsbatemnamepath !="")
                    {
                        string insertbenhan = "INSERT INTO mrd_hsba_template(mrd_hsbatemcode, mrd_hsbatemname, mrd_hsbatemtypeid, mrd_hsbatemnamepath) VALUES ('" + txtHSBATempCode.Text.Trim() + "', '" + txtHSBATempName.Text.Trim() + "', '0', '" + this.mrd_hsbatemnamepath + "'); ";
                        if (condb.ExecuteNonQuery_HSBA(insertbenhan))
                        {
                            O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.THEM_MOI_THANH_CONG);
                            frmthongbao.Show();
                        }
                    }
                    else
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.VUI_LONG_NHAP_DAY_DU_THONG_TIN);
                        frmthongbao.Show();
                    }
                }
                LoadDanhSachBenhAn();
                txtHSBATempCode.Text = "";
                txtHSBATempName.Text = "";
                txtHSBATempNamePath.Text = "";
                txtHSBATempCode.ReadOnly = true;
                txtHSBATempName.ReadOnly = true;
                txtHSBATempNamePath.ReadOnly = true;
                btnTimFileTemplate.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                gridControlDSBenhAn.Enabled = true;
                this.mrd_hsbatemid = 0;
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
                txtHSBATempCode.Text = "";
                txtHSBATempName.Text = "";
                txtHSBATempNamePath.Text = "";
                txtHSBATempCode.ReadOnly = true;
                txtHSBATempName.ReadOnly = true;
                txtHSBATempNamePath.ReadOnly = true;
                btnTimFileTemplate.Enabled = false;
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                gridControlDSBenhAn.Enabled = true;
                this.mrd_hsbatemid = 0;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void gridControlDSBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridViewDSBenhAn.RowCount > 0)
                {
                    var rowHandle = gridViewDSBenhAn.FocusedRowHandle;
                    mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(gridViewDSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemid").ToString());
                    txtHSBATempCode.Text = gridViewDSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemcode").ToString();
                    txtHSBATempName.Text = gridViewDSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemname").ToString();
                    txtHSBATempNamePath.Text = gridViewDSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemnamepath").ToString();
                    btnSua.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }




    }
}
