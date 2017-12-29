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

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_PTTT
{
    public partial class frmNhapPhieuThucHienPTTT : Form
    {
        private MrdPtttServiceDTO mrdptttserviceDTO { get; set; }
        private DAL.ConnectDatabase condb = new DAL.ConnectDatabase();

        public frmNhapPhieuThucHienPTTT()
        {
            InitializeComponent();
        }
        public frmNhapPhieuThucHienPTTT(MrdPtttServiceDTO mrdptttserviceDTO)
        {
            InitializeComponent();
            this.mrdptttserviceDTO = mrdptttserviceDTO;
        }

        #region Load
        private void frmNhapPhieuThucHienPTTT_Load(object sender, EventArgs e)
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
                richEditControlData.ReadOnly = this.mrdptttserviceDTO.file_readonly;

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
                //Kiem tra chinh sua hay them moi phieu PTTT
                if (this.mrdptttserviceDTO.mrd_pttt_serviceid > 0)//chinh sua
                {
                    string getthongtinservice = "select mrd_pttt_servicedata from mrd_pttt_service where mrd_pttt_serviceid=" + this.mrdptttserviceDTO.mrd_pttt_serviceid + "; ";
                    DataTable dataService = condb.GetDataTable_HSBA(getthongtinservice);
                    if (dataService != null && dataService.Rows.Count > 0)
                    {
                        richEditControlData.Document.RtfText = dataService.Rows[0]["mrd_pttt_servicedata"].ToString();
                    }
                }
                else
                {
                    string templateFullPath = "";
                    string templatename = "Phieu phau thuat thu thuat chung.doc";
                    //lay ten phieu phau thuat thu thuat
                    string gettentemplate = "select COALESCE(mrd_templatename,'') as mrd_templatename from mrd_serviceref where servicepricecode='" + this.mrdptttserviceDTO.servicepricecode + "';";
                    DataTable datatemplate = condb.GetDataTable_HSBA(gettentemplate);
                    if (datatemplate != null && datatemplate.Rows.Count > 0)
                    {
                        if (datatemplate.Rows[0]["mrd_templatename"].ToString() != "")
                        {
                            templatename = datatemplate.Rows[0]["mrd_templatename"].ToString();
                        }
                    }
                    templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.PhieuPhauThuatThuThuat_Path + "\\" + templatename;

                    string getthongtinservice = " SELECT hsba.sovaovien as SOVAOVIEN, hsba.PATIENTCODE, hsba.PATIENTNAME, cast((cast(to_char(hsba.hosobenhandate,'yyyy') as integer) - cast(to_char(hsba.birthday,'yyyy') as integer)) as text) as PATIENT_AGE, gioitinhname as PATIENT_GENDERNAME, degp.departmentgroupname AS DEPARTMENTGROUPNAME, de.departmentname as DEPARTMENTNAME, '' as GIUONG, to_char(hsba.hosobenhandate,'HH24') as VIENPHIDATE_NT_GIO, to_char(hsba.hosobenhandate,'MI') as VIENPHIDATE_NT_PHUT, to_char(hsba.hosobenhandate,'dd') as VIENPHIDATE_NT_NGAY, to_char(hsba.hosobenhandate,'MM') as VIENPHIDATE_NT_THANG, to_char(hsba.hosobenhandate,'yyyy') as VIENPHIDATE_NT_NAM, to_char(pttt.phauthuatthuthuatdate,'HH24') as TG_PTTT_GIO, to_char(pttt.phauthuatthuthuatdate,'MI') as TG_PTTT_PHUT, to_char(pttt.phauthuatthuthuatdate,'dd') as TG_PTTT_NGAY, to_char(pttt.phauthuatthuthuatdate,'MM') as TG_PTTT_THANG, to_char(pttt.phauthuatthuthuatdate,'yyyy') as TG_PTTT_NAM, pttt.chandoanvaokhoa as CD_TRUOC_PTTT, pttt.chandoanvaokhoa as CD_SAU_PTTT, pttt.phuongphappttt as PHUONGPHAP_PTTT, '' as LOAIPHAP_PTTT, (case pttt.pttt_phuongphapvocamid when 1 then 'Gây mê tĩnh mạch' when 2 then 'Gây mê nội khí quản' when 3 then 'Gây tê tại chỗ' when 4 then 'Tiền mê + gây tê tại chỗ' when 5 then 'Gây tê tủy sống' when 6 then 'Gây tê' when 7 then 'Gây tê ngoài màng cứng' when 8 then 'Gây tê đám rối thần kinh' when 9 then 'Gây tê Codan' when 10 then 'Gây tê nhãn cầu' when 11 then 'Gây tê cạnh sống' when 99 then 'Khác' end) as PHUONGPHAP_VOCAM, ptv.username as BACSI_PTTT, bsgm.username as BACSI_GAYME, to_char(now(),'dd') as CURRENT_NGAY, to_char(now(),'MM') as CURRENT_THANG, to_char(now(),'yyyy') as CURRENT_NAM, upper(ser.servicepricename_nuocngoai) as servicepricename, mbp.chandoan, nglap.username as NGUOI_LAP FROM (select * from hosobenhan where hosobenhanid=" + this.mrdptttserviceDTO.hosobenhanid + ") hsba inner join (select * from serviceprice where servicepriceid=" + this.mrdptttserviceDTO.servicepriceid + ") ser on ser.hosobenhanid=hsba.hosobenhanid inner join (select * from maubenhpham where hosobenhanid=" + this.mrdptttserviceDTO.hosobenhanid + ") mbp on mbp.maubenhphamid=ser.maubenhphamid left join (select * from phauthuatthuthuat where servicepriceid=" + this.mrdptttserviceDTO.servicepriceid + ") pttt on pttt.servicepriceid=ser.servicepriceid left join nhompersonnel ptv on ptv.userhisid=pttt.phauthuatvien left join nhompersonnel bsgm on bsgm.userhisid=pttt.bacsigayme left join nhompersonnel nglap on nglap.userhisid=mbp.userid left join departmentgroup degp on degp.departmentgroupid=ser.departmentgroupid left join department de on de.departmentid=ser.departmentid and de.departmenttype in (2,3,9); ";
                    DataTable dataService = condb.GetDataTable_HIS(getthongtinservice);
                    Aspose.Words.Document documentWord = new Aspose.Words.Document();
                    documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMergeFormat(templateFullPath, dataService, "PTTT.doc", Aspose.Words.SaveFormat.Docx);

                    richEditControlData.LoadDocument(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\PTTT.doc", DevExpress.XtraRichEdit.DocumentFormat.OpenXml); //load Doc thì bị hiển thị thiếu nội dung; load Docx =ok

                    //File.Delete(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\PTTT.docx");

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
        private void LuuVaoCoSoDuLieu(int mrd_pttt_servicestatus)
        {
            try
            {
                string sqlPTTT = "";
                string mrd_pttt_servicedata = richEditControlData.Document.RtfText.Replace("'", "''");

                if (this.mrdptttserviceDTO.mrd_pttt_serviceid == 0) //them moi
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



                    sqlPTTT = "INSERT INTO mrd_pttt_service(servicepriceid, servicepricecode, maubenhphamid, patientid, vienphiid, hosobenhanid, medicalrecordid, mrd_pttttemid, departmentgroupid, departmentid, mrd_pttt_servicedata, mrd_pttt_servicedata_nd, mrd_pttt_servicestatus, create_mrduserid, create_mrdusercode, create_date, note) VALUES (" + this.mrdptttserviceDTO.servicepriceid + ", '" + this.mrdptttserviceDTO.servicepricecode + "', " + this.mrdptttserviceDTO.maubenhphamid + ", " + this.mrdptttserviceDTO.patientid + ", " + this.mrdptttserviceDTO.vienphiid + ", " + this.mrdptttserviceDTO.hosobenhanid + ", " + this.mrdptttserviceDTO.medicalrecordid + ", " + mrd_pttt_servicestatus + ", " + this.mrdptttserviceDTO.departmentgroupid + ", " + this.mrdptttserviceDTO.departmentid + ", '" + mrd_pttt_servicedata + "', '', '0', '" + Base.SessionLogin.SessionUserID + "', '" + Base.SessionLogin.SessionUsercode + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '');";
                }
                else
                {
                    sqlPTTT = "Update mrd_pttt_service set mrd_pttt_servicedata='" + mrd_pttt_servicedata + "', modify_mrduserid='" + Base.SessionLogin.SessionUserID + "', modify_mrdusercode='" + Base.SessionLogin.SessionUsercode + "', modify_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mrd_pttt_serviceid=" + this.mrdptttserviceDTO.mrd_pttt_serviceid + ";";
                }
                if (condb.ExecuteNonQuery_HSBA(sqlPTTT))
                {
                    O2S_MedicalRecord.Utilities.ThongBao.frmThongBao frmthongbao = new O2S_MedicalRecord.Utilities.ThongBao.frmThongBao(O2S_MedicalRecord.Base.ThongBaoLable.LUU_THANH_CONG);
                    frmthongbao.Show();

                    string sqlkiemtrabenhan = "SELECT mrd_pttt_serviceid FROM mrd_pttt_service WHERE servicepriceid=" + this.mrdptttserviceDTO.servicepriceid + ";";
                    DataTable databenhan = condb.GetDataTable_HSBA(sqlkiemtrabenhan);
                    if (databenhan != null && databenhan.Rows.Count > 0 && this.mrdptttserviceDTO.mrd_pttt_serviceid == 0)
                    {
                        this.mrdptttserviceDTO.mrd_pttt_serviceid = Utilities.Util_TypeConvertParse.ToInt64(databenhan.Rows[0]["mrd_pttt_serviceid"].ToString());
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
