namespace O2S_MedicalRecord.Utilities.PrintPreview
{
    partial class CoolPrintPreviewDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoolPrintPreviewDialog));
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this._ZoomLable = new DevExpress.XtraBars.BarStaticItem();
            this._Zoom = new DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem();
            this._preview = new O2S_MedicalRecord.Utilities.PrintPreview.CoolPrintPreviewControl();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this._btnPrint = new DevExpress.XtraBars.BarButtonItem();
            this._btnPageSetup = new DevExpress.XtraBars.BarButtonItem();
            this._btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this._btnFirst = new DevExpress.XtraBars.BarButtonItem();
            this._btnPrev = new DevExpress.XtraBars.BarButtonItem();
            this._btnNext = new DevExpress.XtraBars.BarButtonItem();
            this._btnLast = new DevExpress.XtraBars.BarButtonItem();
            this._txtStartPage = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._itemActualSize = new DevExpress.XtraBars.BarButtonItem();
            this._itemFullPage = new DevExpress.XtraBars.BarButtonItem();
            this._itemPageWidth = new DevExpress.XtraBars.BarButtonItem();
            this._itemTwoPages = new DevExpress.XtraBars.BarButtonItem();
            this._btnZoomOut = new DevExpress.XtraBars.BarButtonItem();
            this._btnZoomIn = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this._lblPageCount = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemRangeTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar();
            this.repositoryItemRangeTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar();
            this.repositoryItemZoomTrackBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.repositoryItemRangeTrackBar3 = new DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar();
            this.printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this._ZoomLable);
            this.ribbonStatusBar1.ItemLinks.Add(this._Zoom);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 635);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1008, 27);
            this.ribbonStatusBar1.Click += new System.EventHandler(this.ribbonStatusBar1_Click);
            // 
            // _ZoomLable
            // 
            this._ZoomLable.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this._ZoomLable.Caption = "100%";
            this._ZoomLable.Id = 19;
            this._ZoomLable.Name = "_ZoomLable";
            this._ZoomLable.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // _Zoom
            // 
            this._Zoom.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this._Zoom.ContextSpecifier = this._preview;
            this._Zoom.Edit = this.repositoryItemZoomTrackBar1;
            this._Zoom.EditValue = 90;
            this._Zoom.EditWidth = 200;
            this._Zoom.Id = 55;
            this._Zoom.Name = "_Zoom";
            this._Zoom.Range = new int[] {
        10,
        500};
            this._Zoom.EditValueChanged += new System.EventHandler(this._Zoom_EditValueChanged);
            // 
            // _preview
            // 
            this._preview.AutoScroll = true;
            this._preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._preview.Document = null;
            this._preview.Location = new System.Drawing.Point(0, 141);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(1008, 494);
            this._preview.TabIndex = 2;
            this._preview.StartPageChanged += new System.EventHandler(this._preview_StartPageChanged);
            this._preview.PageCountChanged += new System.EventHandler(this._preview_PageCountChanged);
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemZoomTrackBar1.AllowFocused = false;
            this.repositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemZoomTrackBar1.Middle = 5;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this._btnPrint,
            this._btnPageSetup,
            this._btnCancel,
            this._btnFirst,
            this._btnPrev,
            this._btnNext,
            this._btnLast,
            this._txtStartPage,
            this._itemActualSize,
            this._itemFullPage,
            this._itemPageWidth,
            this._itemTwoPages,
            this._btnZoomOut,
            this._btnZoomIn,
            this._Zoom,
            this._ZoomLable,
            this.barStaticItem1,
            this._lblPageCount});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemZoomTrackBar1,
            this.repositoryItemRangeTrackBar1,
            this.repositoryItemRangeTrackBar2,
            this.repositoryItemZoomTrackBar2,
            this.repositoryItemRangeTrackBar3,
            this.repositoryItemTextEdit2});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl1.Size = new System.Drawing.Size(1008, 141);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // _btnPrint
            // 
            this._btnPrint.Caption = "In";
            this._btnPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnPrint.Glyph")));
            this._btnPrint.Id = 1;
            this._btnPrint.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnPrint.LargeGlyph")));
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnPrint_ItemClick);
            // 
            // _btnPageSetup
            // 
            this._btnPageSetup.Caption = "Thiết lập trang";
            this._btnPageSetup.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnPageSetup.Glyph")));
            this._btnPageSetup.Id = 2;
            this._btnPageSetup.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnPageSetup.LargeGlyph")));
            this._btnPageSetup.Name = "_btnPageSetup";
            this._btnPageSetup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnPageSetup_ItemClick);
            // 
            // _btnCancel
            // 
            this._btnCancel.Caption = "Đóng lại";
            this._btnCancel.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnCancel.Glyph")));
            this._btnCancel.Id = 3;
            this._btnCancel.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnCancel.LargeGlyph")));
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnCancel_ItemClick);
            // 
            // _btnFirst
            // 
            this._btnFirst.Caption = "Trang đầu tiên";
            this._btnFirst.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnFirst.Glyph")));
            this._btnFirst.Id = 4;
            this._btnFirst.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnFirst.LargeGlyph")));
            this._btnFirst.Name = "_btnFirst";
            this._btnFirst.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnFirst_ItemClick);
            // 
            // _btnPrev
            // 
            this._btnPrev.Caption = "Trang trước";
            this._btnPrev.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnPrev.Glyph")));
            this._btnPrev.Id = 5;
            this._btnPrev.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnPrev.LargeGlyph")));
            this._btnPrev.Name = "_btnPrev";
            this._btnPrev.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnPrev_ItemClick);
            // 
            // _btnNext
            // 
            this._btnNext.Caption = "Trang sau";
            this._btnNext.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnNext.Glyph")));
            this._btnNext.Id = 6;
            this._btnNext.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnNext.LargeGlyph")));
            this._btnNext.Name = "_btnNext";
            this._btnNext.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnNext_ItemClick);
            // 
            // _btnLast
            // 
            this._btnLast.Caption = "Trang cuối cùng";
            this._btnLast.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnLast.Glyph")));
            this._btnLast.Id = 7;
            this._btnLast.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_btnLast.LargeGlyph")));
            this._btnLast.Name = "_btnLast";
            this._btnLast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnLast_ItemClick);
            // 
            // _txtStartPage
            // 
            this._txtStartPage.Caption = "Trang";
            this._txtStartPage.Edit = this.repositoryItemTextEdit1;
            this._txtStartPage.EditHeight = 35;
            this._txtStartPage.EditValue = 1;
            this._txtStartPage.EditWidth = 65;
            this._txtStartPage.Id = 8;
            this._txtStartPage.Name = "_txtStartPage";
            this._txtStartPage.EditValueChanged += new System.EventHandler(this._txtStartPage_EditValueChanged);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit1.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit1.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit1.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemTextEdit1.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit1.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTextEdit1.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemTextEdit1.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // _itemActualSize
            // 
            this._itemActualSize.Caption = "Kích thước chuẩn";
            this._itemActualSize.Glyph = ((System.Drawing.Image)(resources.GetObject("_itemActualSize.Glyph")));
            this._itemActualSize.Id = 9;
            this._itemActualSize.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_itemActualSize.LargeGlyph")));
            this._itemActualSize.Name = "_itemActualSize";
            this._itemActualSize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._itemActualSize_ItemClick);
            // 
            // _itemFullPage
            // 
            this._itemFullPage.Caption = "Toàn trang";
            this._itemFullPage.Glyph = ((System.Drawing.Image)(resources.GetObject("_itemFullPage.Glyph")));
            this._itemFullPage.Id = 10;
            this._itemFullPage.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_itemFullPage.LargeGlyph")));
            this._itemFullPage.Name = "_itemFullPage";
            this._itemFullPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._itemFullPage_ItemClick);
            // 
            // _itemPageWidth
            // 
            this._itemPageWidth.Caption = "Toàn màn hình";
            this._itemPageWidth.Id = 11;
            this._itemPageWidth.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_itemPageWidth.LargeGlyph")));
            this._itemPageWidth.Name = "_itemPageWidth";
            this._itemPageWidth.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._itemPageWidth_ItemClick);
            // 
            // _itemTwoPages
            // 
            this._itemTwoPages.Caption = "Hai trang";
            this._itemTwoPages.Glyph = ((System.Drawing.Image)(resources.GetObject("_itemTwoPages.Glyph")));
            this._itemTwoPages.Id = 12;
            this._itemTwoPages.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("_itemTwoPages.LargeGlyph")));
            this._itemTwoPages.Name = "_itemTwoPages";
            this._itemTwoPages.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._itemTwoPages_ItemClick);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.Caption = "Thu nhỏ";
            this._btnZoomOut.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnZoomOut.Glyph")));
            this._btnZoomOut.Id = 13;
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnZoomOut_ItemClick);
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.Caption = "Phóng to";
            this._btnZoomIn.Glyph = ((System.Drawing.Image)(resources.GetObject("_btnZoomIn.Glyph")));
            this._btnZoomIn.Id = 14;
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this._btnZoomIn_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 22;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // _lblPageCount
            // 
            this._lblPageCount.Caption = "/";
            this._lblPageCount.Edit = this.repositoryItemTextEdit2;
            this._lblPageCount.EditHeight = 35;
            this._lblPageCount.EditWidth = 60;
            this._lblPageCount.Id = 23;
            this._lblPageCount.ItemAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this._lblPageCount.ItemAppearance.Disabled.Options.UseFont = true;
            this._lblPageCount.ItemInMenuAppearance.Disabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this._lblPageCount.ItemInMenuAppearance.Disabled.Options.UseFont = true;
            this._lblPageCount.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblPageCount.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this._lblPageCount.Name = "_lblPageCount";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit2.Appearance.Options.UseFont = true;
            this.repositoryItemTextEdit2.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit2.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemTextEdit2.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit2.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTextEdit2.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 12F);
            this.repositoryItemTextEdit2.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            this.repositoryItemTextEdit2.ReadOnly = true;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3,
            this.ribbonPageGroup4,
            this.ribbonPageGroup5});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Print Preview";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this._btnPrint);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "In ấn";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this._btnPageSetup);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Thiết lập trang";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this._btnFirst);
            this.ribbonPageGroup3.ItemLinks.Add(this._btnPrev);
            this.ribbonPageGroup3.ItemLinks.Add(this._txtStartPage);
            this.ribbonPageGroup3.ItemLinks.Add(this._lblPageCount);
            this.ribbonPageGroup3.ItemLinks.Add(this._btnNext);
            this.ribbonPageGroup3.ItemLinks.Add(this._btnLast);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Điều hướng";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this._itemActualSize);
            this.ribbonPageGroup4.ItemLinks.Add(this._btnZoomOut);
            this.ribbonPageGroup4.ItemLinks.Add(this._btnZoomIn);
            this.ribbonPageGroup4.ItemLinks.Add(this._itemFullPage);
            this.ribbonPageGroup4.ItemLinks.Add(this._itemPageWidth);
            this.ribbonPageGroup4.ItemLinks.Add(this._itemTwoPages);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Xem";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this._btnCancel);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "Đóng";
            // 
            // repositoryItemRangeTrackBar1
            // 
            this.repositoryItemRangeTrackBar1.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemRangeTrackBar1.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemRangeTrackBar1.Name = "repositoryItemRangeTrackBar1";
            // 
            // repositoryItemRangeTrackBar2
            // 
            this.repositoryItemRangeTrackBar2.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemRangeTrackBar2.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemRangeTrackBar2.Name = "repositoryItemRangeTrackBar2";
            // 
            // repositoryItemZoomTrackBar2
            // 
            this.repositoryItemZoomTrackBar2.Name = "repositoryItemZoomTrackBar2";
            this.repositoryItemZoomTrackBar2.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            // 
            // repositoryItemRangeTrackBar3
            // 
            this.repositoryItemRangeTrackBar3.LabelAppearance.Options.UseTextOptions = true;
            this.repositoryItemRangeTrackBar3.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemRangeTrackBar3.Name = "repositoryItemRangeTrackBar3";
            // 
            // printPreviewBarItem6
            // 
            this.printPreviewBarItem6.Caption = "Print";
            this.printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
            this.printPreviewBarItem6.Enabled = false;
            this.printPreviewBarItem6.Glyph = global::O2S_MedicalRecord.PrintRibbonControllerResources.RibbonPrintPreview_Print;
            this.printPreviewBarItem6.Id = 6;
            this.printPreviewBarItem6.LargeGlyph = global::O2S_MedicalRecord.PrintRibbonControllerResources.RibbonPrintPreview_PrintLarge;
            this.printPreviewBarItem6.Name = "printPreviewBarItem6";
            superToolTip2.FixedTooltipWidth = true;
            toolTipTitleItem2.Text = "Print (Ctrl+P)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Select a printer, number of copies and other printing options before printing.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            superToolTip2.MaxWidth = 210;
            this.printPreviewBarItem6.SuperTip = superToolTip2;
            // 
            // CoolPrintPreviewDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this._preview);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Name = "CoolPrintPreviewDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CoolPrintPreviewDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRangeTrackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem _btnPrint;
        private DevExpress.XtraPrinting.Preview.PrintPreviewBarItem printPreviewBarItem6;
        private DevExpress.XtraBars.BarButtonItem _btnPageSetup;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.BarButtonItem _btnCancel;
        private DevExpress.XtraBars.BarButtonItem _btnFirst;
        private DevExpress.XtraBars.BarButtonItem _btnPrev;
        private DevExpress.XtraBars.BarButtonItem _btnNext;
        private DevExpress.XtraBars.BarButtonItem _btnLast;
        private DevExpress.XtraBars.BarEditItem _txtStartPage;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem _itemActualSize;
        private DevExpress.XtraBars.BarButtonItem _itemFullPage;
        private DevExpress.XtraBars.BarButtonItem _itemPageWidth;
        private DevExpress.XtraBars.BarButtonItem _itemTwoPages;
        private DevExpress.XtraBars.BarButtonItem _btnZoomOut;
        private DevExpress.XtraBars.BarButtonItem _btnZoomIn;
        private DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem _Zoom;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        private DevExpress.XtraBars.BarStaticItem _ZoomLable;
        private DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar repositoryItemRangeTrackBar1;
        private DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar repositoryItemRangeTrackBar2;
        private DevExpress.XtraEditors.Repository.RepositoryItemRangeTrackBar repositoryItemRangeTrackBar3;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem _lblPageCount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private CoolPrintPreviewControl _preview;
    }
}