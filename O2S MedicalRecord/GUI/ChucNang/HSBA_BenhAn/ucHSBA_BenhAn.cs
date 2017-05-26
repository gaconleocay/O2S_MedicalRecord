using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using O2S_MedicalRecord.DTO;
using DevExpress.Utils.Menu;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class ucHSBA_BenhAn : UserControl
    {
        #region Declaration
        private MedicalrecordDTO mecicalrecordCurrentDTO { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        private MrdHsbaHosobenhanDTO currentMrdHsbaHsba { get; set; }
        #endregion
        public ucHSBA_BenhAn()
        {
            InitializeComponent();
        }

        public ucHSBA_BenhAn(MedicalrecordDTO filterDTO)
        {
            InitializeComponent();
            this.mecicalrecordCurrentDTO = filterDTO;
        }

        #region Load
        private void ucHSBA_BenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDanhSachLoaiBenhAn();
                LoadDanhSachLichSuBenhAn();
                LoadBenhAnCuaBenhNhan();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachLoaiBenhAn()
        {
            try
            {
                cboMauBenhAn.Properties.DataSource = GlobalStore.GlobalLst_MrdHsbaTemplate;
                cboMauBenhAn.Properties.DisplayMember = "mrd_hsbatemname";
                cboMauBenhAn.Properties.ValueMember = "mrd_hsbatemid";
                if (GlobalStore.GlobalLst_MrdHsbaTemplate.Count == 1)
                {
                    cboMauBenhAn.ItemIndex = 0;
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDanhSachLichSuBenhAn()
        {
            try
            {
                string sqlLichSuBA = "Select ROW_NUMBER() OVER (ORDER BY mba.hosobenhanid desc) as stt, mba.mrd_hsba_hosobenhanid, mba.mrd_hsbatemid, mht.mrd_hsbatemname, mba.patientid, mba.hosobenhanid, mba.vienphiid, hosobenhan.hosobenhandate, (case when hosobenhan.hosobenhandate_ravien<>'0001-01-01 00:00:00' then hosobenhan.hosobenhandate_ravien end) as hosobenhandate_ravien, mba.create_date, mus.username as create_mrdusername from mrd_hsba_hosobenhan mba inner join dblink('myconn','select hosobenhanid, hosobenhandate, hosobenhandate_ravien from hosobenhan where patientid=" + this.mecicalrecordCurrentDTO.patientid + "') as hosobenhan(hosobenhanid integer, hosobenhandate timestamp without time zone, hosobenhandate_ravien timestamp without time zone) on hosobenhan.hosobenhanid=mba.hosobenhanid left join mrd_tbluser mus on mus.userid=mba.create_mrduserid inner join mrd_hsba_template mht on mht.mrd_hsbatemid=mba.mrd_hsbatemid where mba.patientid=" + this.mecicalrecordCurrentDTO.patientid + " ; ";
                gridControlHSBADSBenhAn.DataSource = condb.GetDataTable_Dblink(sqlLichSuBA);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadBenhAnCuaBenhNhan()
        {
            try
            {
                this.currentMrdHsbaHsba = new MrdHsbaHosobenhanDTO();
                if (this.mecicalrecordCurrentDTO.medicalrecordstatus !=2)
                {
                    this.currentMrdHsbaHsba.file_readonly = true;
                    btnChonTaoBenhAn.Visible = false;
                    btnXemBenhAn.Visible = true;
                    cboMauBenhAn.ReadOnly = true;
                }
                else
                {
                    this.currentMrdHsbaHsba.file_readonly = false;
                    string sqlkiemtrabenhan = "SELECT mrd_hsba_hosobenhanid, hosobenhanid, mrd_hsbatemid, mrd_hsba_hosobenhandata FROM mrd_hsba_hosobenhan WHERE hosobenhanid=" + this.mecicalrecordCurrentDTO.hosobenhanid + ";";
                    DataTable databenhan = condb.GetDataTable_HSBA(sqlkiemtrabenhan);
                    if (databenhan != null && databenhan.Rows.Count > 0)
                    {
                        cboMauBenhAn.EditValue = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_hsbatemid"].ToString());
                        this.currentMrdHsbaHsba.mrd_hsba_hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_hsba_hosobenhanid"].ToString());
                        this.currentMrdHsbaHsba.mrd_hsba_hosobenhandata = databenhan.Rows[0]["mrd_hsba_hosobenhandata"].ToString();

                        btnChonTaoBenhAn.Visible = false;
                        btnXemBenhAn.Visible = true;
                        cboMauBenhAn.ReadOnly = true;
                    }
                    else
                    {
                        btnChonTaoBenhAn.Visible = true;
                        btnXemBenhAn.Visible = false;
                        cboMauBenhAn.ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        internal void LoadDuLieuTheoBenhNhan(MedicalrecordDTO filterDTO)
        {
            try
            {
                this.mecicalrecordCurrentDTO = filterDTO;
                ucHSBA_BenhAn_Load(null,null);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Button
        private void btnChonTaoBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMauBenhAn.EditValue != null)
                {
                    if (this.mecicalrecordCurrentDTO.medicalrecordstatus == 0)
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_NHAN_CHUA_DUOC_TIEP_NHAN_VAO_PHONG);
                        frmthongbao.Show();
                    }
                    else if (this.mecicalrecordCurrentDTO.medicalrecordstatus == 2)
                    {
                        this.currentMrdHsbaHsba.patientid = this.mecicalrecordCurrentDTO.patientid;
                        this.currentMrdHsbaHsba.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                        this.currentMrdHsbaHsba.medicalrecordid = this.mecicalrecordCurrentDTO.medicalrecordid;
                        this.currentMrdHsbaHsba.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                        this.currentMrdHsbaHsba.departmentgroupid = this.mecicalrecordCurrentDTO.departmentgroupid;
                        this.currentMrdHsbaHsba.departmentid = this.mecicalrecordCurrentDTO.departmentid;
                        this.currentMrdHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(cboMauBenhAn.EditValue.ToString());
                        frmNhapBenhAn frmNhap = new frmNhapBenhAn(this.currentMrdHsbaHsba);
                        frmNhap.ShowDialog();

                        //Sua dung Office de mo
                        //  HSBA_BenhAn_Process.LayDuLieuVaXuatFileWord(mrdHsbaHsba);
                        ucHSBA_BenhAn_Load(null, null);
                    }
                    else
                    {
                        O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.BENH_AN_DA_KET_THUC);
                        frmthongbao.Show();
                    }
                }
                else
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.CHUA_CHON_LOAI_BENH_AN);
                    frmthongbao.Show();
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void btnXemBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentMrdHsbaHsba.patientid = this.mecicalrecordCurrentDTO.patientid;
                this.currentMrdHsbaHsba.vienphiid = this.mecicalrecordCurrentDTO.vienphiid;
                this.currentMrdHsbaHsba.medicalrecordid = this.mecicalrecordCurrentDTO.medicalrecordid;
                this.currentMrdHsbaHsba.hosobenhanid = this.mecicalrecordCurrentDTO.hosobenhanid;
                this.currentMrdHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(cboMauBenhAn.EditValue.ToString());
                frmNhapBenhAn frmNhap = new frmNhapBenhAn(this.currentMrdHsbaHsba);
                frmNhap.ShowDialog();

                //Sua dung Office de mo
                //  HSBA_BenhAn_Process.LayDuLieuVaXuatFileWord(mrdHsbaHsba);
                ucHSBA_BenhAn_Load(null, null);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        #endregion

        private void gridViewHSBADSBenhAn_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
                {
                    e.Menu.Items.Clear();
                    DXMenuItem itemXoaPhieuChiDinh = new DXMenuItem("Copy mẫu bệnh án này");
                    itemXoaPhieuChiDinh.Image = imMenu_HSBA.Images[0];
                    //itemXoaToanBA.Shortcut = Shortcut.F6;
                    itemXoaPhieuChiDinh.Click += new EventHandler(ItemSuDungMauBenhAn_Click);
                    e.Menu.Items.Add(itemXoaPhieuChiDinh);
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        void ItemSuDungMauBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
                //lấy giá trị tại dòng click chuột - chi can lay ID template
                var rowHandle = gridViewHSBADSBenhAn.FocusedRowHandle;
                MrdHsbaHosobenhanDTO rowHsbaHsba = new MrdHsbaHosobenhanDTO(); rowHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHSBADSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemid").ToString());

                //todo
                //frmNhapBenhAn frmNhap = new frmNhapBenhAn(rowHsbaHsba);
                //frmNhap.ShowDialog();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        private void repositoryItemButtonSuDungMau_Click(object sender, EventArgs e)
        {
            try
            {
                ItemSuDungMauBenhAn_Click(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #region Xem lai benh an cua BN
        private void repositoryItemButtonView_Click(object sender, EventArgs e)
        {
            try
            {
                gridControlHSBADSBenhAn_DoubleClick(null, null);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        private void gridControlHSBADSBenhAn_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridViewHSBADSBenhAn.FocusedRowHandle;
                MrdHsbaHosobenhanDTO rowHsbaHsba = new MrdHsbaHosobenhanDTO();
                rowHsbaHsba.mrd_hsba_hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHSBADSBenhAn.GetRowCellValue(rowHandle, "mrd_hsba_hosobenhanid").ToString());
                rowHsbaHsba.hosobenhanid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHSBADSBenhAn.GetRowCellValue(rowHandle, "hosobenhanid").ToString());
                rowHsbaHsba.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(gridViewHSBADSBenhAn.GetRowCellValue(rowHandle, "mrd_hsbatemid").ToString());
                rowHsbaHsba.file_readonly = true;
                frmNhapBenhAn frmNhap = new frmNhapBenhAn(rowHsbaHsba);
                frmNhap.ShowDialog();
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }
        #endregion





    }
}
