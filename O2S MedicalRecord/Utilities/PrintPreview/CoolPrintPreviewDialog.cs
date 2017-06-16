using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace O2S_MedicalRecord.Utilities.PrintPreview
{
    /// <summary>
    /// Represents a dialog containing a <see cref="CoolPrintPreviewControl"/> control
    /// used to preview and print <see cref="PrintDocument"/> objects.
    /// </summary>
    /// <remarks>
    /// This dialog is similar to the standard <see cref="PrintPreviewDialog"/>
    /// but provides additional options such printer and page setup buttons,
    /// a better UI based on the <see cref="ToolStrip"/> control, and built-in
    /// PDF export.
    /// </remarks>
    internal partial class CoolPrintPreviewDialog : Form
    {
        private double zoomsize = 1;
        //--------------------------------------------------------------------
        #region ** fields

        PrintDocument _doc;

        #endregion

        //--------------------------------------------------------------------
        #region ** ctor

        /// <summary>
        /// Initializes a new instance of a <see cref="CoolPrintPreviewDialog"/>.
        /// </summary>
        public CoolPrintPreviewDialog()
            : this(null)
        {
        }
        /// <summary>
        /// Initializes a new instance of a <see cref="CoolPrintPreviewDialog"/>.
        /// </summary>
        /// <param name="parentForm">Parent form that defines the initial size for this dialog.</param>
        public CoolPrintPreviewDialog(Control parentForm)
        {
            InitializeComponent();
            if (parentForm != null)
            {
                Size = parentForm.Size;
            }
        }
        #endregion

        //--------------------------------------------------------------------
        #region ** object model

        /// <summary>
        /// Gets or sets the <see cref="PrintDocument"/> to preview.
        /// </summary>
        public PrintDocument Document
        {
            get { return _doc; }
            set
            {
                // unhook event handlers
                if (_doc != null)
                {
                    _doc.BeginPrint -= _doc_BeginPrint;
                    _doc.EndPrint -= _doc_EndPrint;
                }

                // save the value
                _doc = value;

                // hook up event handlers
                if (_doc != null)
                {
                    _doc.BeginPrint += _doc_BeginPrint;
                    _doc.EndPrint += _doc_EndPrint;
                }


                // don't assign document to preview until this form becomes visible
                if (Visible)
                {
                    _preview.Document = Document;
                }
            }
        }

        #endregion

        //--------------------------------------------------------------------
        #region ** overloads

        /// <summary>
        /// Overridden to assign document to preview control only after the 
        /// initial activation.
        /// </summary>
        /// <param name="e"><see cref="EventArgs"/> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _preview.Document = Document;
        }
        /// <summary>
        /// Overridden to cancel any ongoing previews when closing form.
        /// </summary>
        /// <param name="e"><see cref="FormClosingEventArgs"/> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (_preview.IsRendering && !e.Cancel)
            {
                _preview.Cancel();
            }
        }

        #endregion

        //--------------------------------------------------------------------
        #region ** main commands

        private void _btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var dlg = new PrintDialog())
            {
                // configure dialog
                dlg.AllowSomePages = true;
                dlg.AllowSelection = true;
                dlg.UseEXDialog = true;
                dlg.Document = Document;

                // show allowed page range
                var ps = dlg.PrinterSettings;
                ps.MinimumPage = ps.FromPage = 1;
                ps.MaximumPage = ps.ToPage = _preview.PageCount;

                // show dialog
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // print selected page range
                    _preview.Print();
                }
            }
        }

        private void _btnPageSetup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var dlg = new PageSetupDialog())
            {
                dlg.Document = Document;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // to show new page layout
                    _preview.RefreshPreview();
                }
            }
        }
        #endregion

        //--------------------------------------------------------------------
        #region ** zoom

        void _btnZoom_ButtonClick(object sender, EventArgs e)
        {
            _preview.ZoomMode = _preview.ZoomMode == ZoomMode.ActualSize
                ? ZoomMode.FullPage
                : ZoomMode.ActualSize;
        }

        //void _btnZoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    if (e.ClickedItem == _itemActualSize)
        //    {
        //        _preview.ZoomMode = ZoomMode.ActualSize;
        //    }
        //    else if (e.ClickedItem == _itemFullPage)
        //    {
        //        _preview.ZoomMode = ZoomMode.FullPage;
        //    }
        //    else if (e.ClickedItem == _itemPageWidth)
        //    {
        //        _preview.ZoomMode = ZoomMode.PageWidth;
        //    }
        //    else if (e.ClickedItem == _itemTwoPages)
        //    {
        //        _preview.ZoomMode = ZoomMode.TwoPages;
        //    }
        //    if (e.ClickedItem == _item10)
        //    {
        //        _preview.Zoom = .1;
        //    }
        //    else if (e.ClickedItem == _item100)
        //    {
        //        _preview.Zoom = 1;
        //    }
        //    else if (e.ClickedItem == _item150)
        //    {
        //        _preview.Zoom = 1.5;
        //    }
        //    else if (e.ClickedItem == _item200)
        //    {
        //        _preview.Zoom = 2;
        //    }
        //    else if (e.ClickedItem == _item25)
        //    {
        //        _preview.Zoom = .25;
        //    }
        //    else if (e.ClickedItem == _item50)
        //    {
        //        _preview.Zoom = .5;
        //    }
        //    else if (e.ClickedItem == _item500)
        //    {
        //        _preview.Zoom = 5;
        //    }
        //    else if (e.ClickedItem == _item75)
        //    {
        //        _preview.Zoom = .75;
        //    }
        //}
        #endregion

        //--------------------------------------------------------------------
        #region ** page navigation
        private void _btnFirst_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.StartPage = 0;
        }

        private void _btnPrev_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.StartPage--;
        }

        private void _btnNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.StartPage++;
        }

        private void _btnLast_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.StartPage = _preview.PageCount - 1;
        }
        private void _txtStartPage_EditValueChanged(object sender, EventArgs e)
        {
            CommitPageNumber();
        }
        void _txtStartPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            var c = e.KeyChar;
            if (c == (char)13)
            {
                CommitPageNumber();
                e.Handled = true;
            }
            else if (c > ' ' && !char.IsDigit(c))
            {
                e.Handled = true;
            }
        }
        void CommitPageNumber()
        {
            int page;
            if (int.TryParse(_txtStartPage.EditValue.ToString(), out page))
            {
                _preview.StartPage = page - 1;
            }
        }
        void _preview_StartPageChanged(object sender, EventArgs e)
        {
            var page = _preview.StartPage + 1;
            _txtStartPage.EditValue = page.ToString();
        }
        private void _preview_PageCountChanged(object sender, EventArgs e)
        {
            this.Update();
            Application.DoEvents();
            _lblPageCount.EditValue = string.Format("{0}", _preview.PageCount);
        }

        #endregion

        //--------------------------------------------------------------------
        #region ** job control

        private void _btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_preview.IsRendering)
            {
                _preview.Cancel();
            }
            else
            {
                Close();
            }
        }
        void _doc_BeginPrint(object sender, PrintEventArgs e)
        {
            _btnCancel.Caption = "&Cancel";
            _btnPrint.Enabled = _btnPageSetup.Enabled = false;
        }
        void _doc_EndPrint(object sender, PrintEventArgs e)
        {
            _btnCancel.Caption = "&Close";
            _btnPrint.Enabled = _btnPageSetup.Enabled = true;
        }

        #endregion

        private void ribbonStatusBar1_Click(object sender, EventArgs e)
        {

        }

        private void _itemActualSize_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.ZoomMode = ZoomMode.ActualSize;
            this.zoomsize = 1;
        }

        private void _itemFullPage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.ZoomMode = ZoomMode.FullPage;
        }

        private void _itemPageWidth_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.ZoomMode = ZoomMode.PageWidth;
        }

        private void _itemTwoPages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _preview.ZoomMode = ZoomMode.TwoPages;
        }

        private void _btnZoomOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.zoomsize > 0.1)
            {
                _preview.Zoom = this.zoomsize - 0.1;
                this.zoomsize -= 0.1;
            }
        }

        //phong to
        private void _btnZoomIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.zoomsize < 5)
            {
                _preview.Zoom = this.zoomsize + 0.1;
                this.zoomsize += 0.1;
            }
        }

        private void _Zoom_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double str =Utilities.Util_TypeConvertParse.ToDouble( _Zoom.EditValue.ToString());
                _preview.Zoom = str;
                _ZoomLable.Caption = (str * 100).ToString() + "%";
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void CoolPrintPreviewDialog_Load(object sender, EventArgs e)
        {
            try
            {
                _Zoom.EditValue = 1;
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }








    }
}
