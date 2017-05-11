using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using MSO2_MedicalRecord.Base;
using MSO2_MedicalRecord.BUS;

namespace MSO2_MedicalRecord.GUI.FormCommon
{
    public partial class ucHoSoBenhAn : UserControl
    {
        #region Declaration
        //MSO2_MedicalRecord.DAL.ConnectDatabase condb = new MSO2_MedicalRecord.DAL.ConnectDatabase();
        public string CurrentTabPage { get; set; }
        public int SelectedTabPageIndex { get; set; }
        internal frmMain frmMain;

        // khai báo 1 hàm delegate
        public delegate void GetString(string thoigian);
        // khai báo 1 kiểu hàm delegate
        public GetString MyGetData;

        #endregion
        public ucHoSoBenhAn()
        {
            InitializeComponent();
        }

        #region Load
        private void ucHoSoBenhAn_Load(object sender, EventArgs e)
        {
            try
            {
                //  KiemTraLicense_ChucNang();
                LoadDanhSachHoSoBenhAn();
            }
            catch (Exception ex)
            {
                MSO2_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void LoadDanhSachHoSoBenhAn()
        {
            try
            {
                GUI.ChucNang.ucDanhSachHoSoBenhAn frmHSBA = new ChucNang.ucDanhSachHoSoBenhAn();
                panelMain.Controls.Add(frmHSBA);
            }
            catch (Exception ex)
            {
                Base.Logging.Warn(ex);
            }
        }

        #endregion

        //#region Tabcontrol function
        ////Dong tab
        //private void xtraTabControlChucNang_CloseButtonClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        XtraTabControl xtab = (XtraTabControl)sender;
        //        int i = xtab.SelectedTabPageIndex;
        //        DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs arg = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
        //        xtab.TabPages.Remove((arg.Page as XtraTabPage));
        //        xtab.SelectedTabPageIndex = i - 1;
        //        //(arg.Page as XtraTabPage).PageVisible = false;
        //        System.GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MSO2_MedicalRecord.Base.Logging.Warn(ex);
        //    }
        //}
        //private void xtraTabControlChucNang_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        frmMain = new frmMain();
        //        this.CurrentTabPage = e.Page.Name;
        //        XtraTabControl xtab = new XtraTabControl();
        //        xtab = (XtraTabControl)sender;
        //        if (xtab != null)
        //        {
        //            this.SelectedTabPageIndex = xtab.SelectedTabPageIndex;
        //            //delegate - thong tin chuc nang
        //            if (MyGetData != null)
        //            {// tại đây gọi nó
        //                MyGetData(xtab.TabPages[xtab.SelectedTabPageIndex].Tooltip);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MSO2_MedicalRecord.Base.Logging.Warn(ex);
        //    }
        //}
        //#endregion





    }
}
