using DevExpress.XtraRichEdit.API.Native;
using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_CDHA
{
    public partial class frmNhapPhieuThucHienCDHA : Form
    {
        private MrdCDHAServiceDTO mrdcdhaserviceDTO { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        public frmNhapPhieuThucHienCDHA()
        {
            InitializeComponent();
        }
        public frmNhapPhieuThucHienCDHA(MrdCDHAServiceDTO mrdcdhaserviceDTO)
        {
            InitializeComponent();
            this.mrdcdhaserviceDTO = mrdcdhaserviceDTO;
        }

        #region Load
        private void frmNhapPhieuThucHienCDHA_Load(object sender, EventArgs e)
        {
            try
            {
                EnableAndDisableControl();
                LoadDuLieuTemplateTheoDichVu();
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void EnableAndDisableControl()
        {
            try
            {
                //fileSaveItem.Enabled = false;
                //LuuNhapItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //LuuMauItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //fileNewItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //fileSaveAsItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                quickPrintItem1.Enabled = false;
                printItem1.Enabled = false;
                printPreviewItem1.Enabled = false;
                //quickPrintItem1.Control.Enabled = false;
                richEditControlData.ReadOnly = this.mrdcdhaserviceDTO.file_readonly;

            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LoadDuLieuTemplateTheoDichVu()
        {
            try
            {
                //Kiem tra chinh sua hay them moi phieu CDHA
                if (this.mrdcdhaserviceDTO.mrd_cdha_serviceid > 0)//chinh sua
                {
                    string getthongtinservice = "select mrd_cdha_servicedata from mrd_cdha_service where mrd_cdha_serviceid=" + this.mrdcdhaserviceDTO.mrd_cdha_serviceid + "; ";
                    DataTable dataService = condb.GetDataTable_HSBA(getthongtinservice);
                    if (dataService != null && dataService.Rows.Count > 0)
                    {
                        richEditControlData.Document.RtfText = dataService.Rows[0]["mrd_cdha_servicedata"].ToString();
                    }
                }
                else
                {
                    string templateFullPath = "";
                    string templatename = "Phieu chuan doan hinh anh chung.doc";
                    //lay ten phieu cdha
                    string gettentemplate = "select COALESCE(mrd_templatename,'') as mrd_templatename from mrd_serviceref where servicepricecode='" + this.mrdcdhaserviceDTO.servicepricecode + "';";
                    DataTable datatemplate = condb.GetDataTable_HSBA(gettentemplate);
                    if (datatemplate != null && datatemplate.Rows.Count > 0)
                    {
                        if (datatemplate.Rows[0]["mrd_templatename"].ToString() != "")
                        {
                            templatename = datatemplate.Rows[0]["mrd_templatename"].ToString();
                        }
                    }
                    templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.PhieuCDHA_Path + "\\" + templatename;

                    string getthongtinservice = "SELECT hsba.sovaovien as SOVAOVIEN, hsba.PATIENTCODE, hsba.PATIENTNAME, cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) as PATIENT_AGE, gioitinhname as PATIENT_GENDERNAME, degp.departmentgroupname AS DEPARTMENTGROUPNAME, de.departmentname as DEPARTMENTNAME, '' as GIUONG, to_char(hsba.hosobenhandate,'HH24') as VIENPHIDATE_NT_GIO, to_char(hsba.hosobenhandate,'MI') as VIENPHIDATE_NT_PHUT, to_char(hsba.hosobenhandate,'dd') as VIENPHIDATE_NT_NGAY, to_char(hsba.hosobenhandate,'MM') as VIENPHIDATE_NT_THANG, to_char(hsba.hosobenhandate,'yyyy') as VIENPHIDATE_NT_NAM, to_char(cdha.thuchienclsdate,'HH24') as TG_PTTT_GIO, to_char(cdha.thuchienclsdate,'MI') as TG_PTTT_PHUT, to_char(cdha.thuchienclsdate,'dd') as TG_PTTT_NGAY, to_char(cdha.thuchienclsdate,'MM') as TG_PTTT_THANG, to_char(cdha.thuchienclsdate,'yyyy') as TG_PTTT_NAM, mbp.chandoan as CD_TRUOC_PTTT, mbp.chandoan as CD_SAU_PTTT, cdha.phuongphappttt as PHUONGPHAP_PTTT, '' as LOAIPHAP_PTTT, '' as PHUONGPHAP_VOCAM, ptv.username as BACSI_PTTT, bsgm.username as BACSI_GAYME, to_char(now(),'dd') as CURRENT_NGAY, to_char(now(),'MM') as CURRENT_THANG, to_char(now(),'yyyy') as CURRENT_NAM, upper(ser.servicepricename_nuocngoai) as servicepricename, mbp.chandoan, nglap.username as NGUOI_LAP FROM (select * from hosobenhan where hosobenhanid=" + this.mrdcdhaserviceDTO.hosobenhanid + ") hsba inner join (select * from serviceprice where servicepriceid=" + this.mrdcdhaserviceDTO.servicepriceid + ") ser on ser.hosobenhanid=hsba.hosobenhanid inner join (select * from maubenhpham where hosobenhanid=" + this.mrdcdhaserviceDTO.hosobenhanid + ") mbp on mbp.maubenhphamid=ser.maubenhphamid left join (select * from thuchiencls where servicepriceid=" + this.mrdcdhaserviceDTO.servicepriceid + ") cdha on cdha.servicepriceid=ser.servicepriceid left join nhompersonnel ptv on ptv.userhisid=cdha.phauthuatvien left join nhompersonnel bsgm on bsgm.userhisid=cdha.bacsigayme left join nhompersonnel nglap on nglap.userhisid=mbp.userid left join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid left join department de on de.departmentid=ser.departmentid and de.departmenttype in (2,3,9); ";
                    DataTable dataService = condb.GetDataTable_HIS(getthongtinservice);
                    Aspose.Words.Document documentWord = new Aspose.Words.Document();
                    documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMergeFormat(templateFullPath, dataService, "CDHA.doc", Aspose.Words.SaveFormat.Docx);

                    richEditControlData.LoadDocument(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\CDHA.doc", DevExpress.XtraRichEdit.DocumentFormat.OpenXml); //load Doc thì bị hiển thị thiếu nội dung; load Docx =ok

                    //File.Delete(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\CDHA.docx");

                    //MemoryStream streammemory = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMerge_ToStream(templateFullPath, dataService);
                    //richEditControlData.LoadDocumentTemplate(streammemory, DevExpress.XtraRichEdit.DocumentFormat.Doc);

                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        #endregion

        #region Event Button
        private void fileSaveItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LuuVaoCoSoDuLieu(1);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LuuNhapItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                LuuVaoCoSoDuLieu(0);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LuuMauItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
        private void LuuVaoCoSoDuLieu(int mrd_cdha_servicestatus)
        {
            try
            {
                string sqlCDHA = "";
                string mrd_cdha_servicedata = richEditControlData.Document.RtfText.Replace("'", "''");

                if (this.mrdcdhaserviceDTO.mrd_cdha_serviceid == 0) //them moi
                {
                    //Stream InputStream = null;
                    //richEditControlData.SaveDocument(InputStream, DevExpress.XtraRichEdit.DocumentFormat.Rtf);

                    //byte[] result;
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    richEditControlData.SaveDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                    //    result = ms.ToArray();
                    //}

                    //richEditControlData.Document.OpenXmlBytes = result;



                    sqlCDHA = "INSERT INTO mrd_cdha_service(servicepriceid, servicepricecode, maubenhphamid, patientid, vienphiid, hosobenhanid, medicalrecordid, mrd_cdhatemid, departmentgroupid, departmentid, mrd_cdha_servicedata, mrd_cdha_servicedata_nd, mrd_cdha_servicestatus, create_mrduserid, create_mrdusercode, create_date, note) VALUES (" + this.mrdcdhaserviceDTO.servicepriceid + ", '" + this.mrdcdhaserviceDTO.servicepricecode + "', " + this.mrdcdhaserviceDTO.maubenhphamid + ", " + this.mrdcdhaserviceDTO.patientid + ", " + this.mrdcdhaserviceDTO.vienphiid + ", " + this.mrdcdhaserviceDTO.hosobenhanid + ", " + this.mrdcdhaserviceDTO.medicalrecordid + ", " + mrd_cdha_servicestatus + ", " + this.mrdcdhaserviceDTO.departmentgroupid + ", " + this.mrdcdhaserviceDTO.departmentid + ", '" + mrd_cdha_servicedata + "', '', '0', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '');";
                }
                else
                {
                    sqlCDHA = "Update mrd_cdha_service set mrd_cdha_servicedata='" + mrd_cdha_servicedata + "', modify_mrduserid='" + Base.SessionLogin.SessionUserID + "', modify_mrdusercode='" + Base.SessionLogin.SessionUsercode + "', modify_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mrd_cdha_serviceid=" + this.mrdcdhaserviceDTO.mrd_cdha_serviceid + ";";
                }
                if (condb.ExecuteNonQuery_HSBA(sqlCDHA))
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.LUU_THANH_CONG);
                    frmthongbao.Show();

                    string sqlkiemtrabenhan = "SELECT mrd_cdha_serviceid FROM mrd_cdha_service WHERE servicepriceid=" + this.mrdcdhaserviceDTO.servicepriceid + ";";
                    DataTable databenhan = condb.GetDataTable_HSBA(sqlkiemtrabenhan);
                    if (databenhan != null && databenhan.Rows.Count > 0 && this.mrdcdhaserviceDTO.mrd_cdha_serviceid == 0)
                    {
                        this.mrdcdhaserviceDTO.mrd_cdha_serviceid = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_cdha_serviceid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Error(ex);
            }
        }



        #endregion

        private void quickPrintItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // Invoke the Print Preview dialog
                using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                {
                    using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                    {
                        link.Component = richEditControlData;
                        link.CreateDocument();
                        link.Print();
                    }


                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void printPreviewItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // Invoke the Print Preview dialog
                using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                {
                    using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                    {
                        link.Component = richEditControlData;
                        link.CreateDocument();
                        link.ShowPreviewDialog();
                    }


                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private void printItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                // Invoke the Print Preview dialog
                using (DevExpress.XtraPrinting.PrintingSystem printingSystem = new DevExpress.XtraPrinting.PrintingSystem())
                {
                    using (DevExpress.XtraPrinting.PrintableComponentLink link = new DevExpress.XtraPrinting.PrintableComponentLink(printingSystem))
                    {
                        link.Component = richEditControlData;
                        link.CreateDocument();
                        link.PrintDlg();
                    }


                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }
    }
}
