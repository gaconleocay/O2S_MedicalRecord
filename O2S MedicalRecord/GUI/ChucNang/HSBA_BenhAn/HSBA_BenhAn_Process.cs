using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using O2S_MedicalRecord.DTO;
using System.Linq;
using System.Diagnostics;
//using Microsoft.Office.Interop.Word;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class HSBA_BenhAn_Process
    {
        private static DAL.ConnectDatabase condb = new DAL.ConnectDatabase();
        public static void LayDuLieuVaXuatFileWord(MrdHsbaHosobenhanDTO mrdHsbaHsba)
        {
            try
            {
                if (mrdHsbaHsba.mrd_hsba_hosobenhanid != 0) //chinh sua
                {
                    //richEditControlData.HtmlText = datahsba.Rows[0]["mrd_hsba_hosobenhandata"].ToString();
                }
                else //them moi
                {
                    string templateFullPath = "";
                    string datetimenow = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

                    string templatename = GlobalStore.GlobalLst_MrdHsbaTemplate.Where(o => o.mrd_hsbatemid == mrdHsbaHsba.mrd_hsbatemid).FirstOrDefault().mrd_hsbatemnamepath;
                    if (templatename != null && templatename != "")
                    {
                        templateFullPath = Environment.CurrentDirectory + Base.KeyTrongPhanMem.PhieuMauBenhAn_Path + "\\" + templatename;
                        //ngay 23/5
                        string getthongtinservice = "SELECT (select giamdocname from hospital limit 1) as GIAMDOCBV, '" + Base.SessionLogin.SessionUsername + "' as TENUSER, '" + datetimenow + "' as CURRENT_FULLTIME, (select soytename from hospital limit 1) as SOYTE, (select hospitalname from hospital limit 1) as TENBENHVIEN, '' as TENKHOA, '' as GIUONG, '' as SOLUUTRU, hsba.patientname as PATIENTNAME, substr(to_char(hsba.birthday, 'dd'),1,1) as NS_1, substr(to_char(hsba.birthday, 'dd'),2,1) as NS_2, substr(to_char(hsba.birthday, 'mm'),1,1) as NS_3, substr(to_char(hsba.birthday, 'mm'),2,1) as NS_4, substr(to_char(hsba.birthday, 'yyyy'),1,1) as NS_5, substr(to_char(hsba.birthday, 'yyyy'),2,1) as NS_6, substr(to_char(hsba.birthday, 'yyyy'),3,1) as NS_7, substr(to_char(hsba.birthday, 'yyyy'),4,1) as NS_8, (case when (cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as integer) <10) then '0' else (substr(cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text),1,1)) end) as TUOI_1, (case when (cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as integer) <10) then (cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text)) else (substr(cast((cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) - cast(to_char(hsba.birthday, 'yyyy') as integer)) as text),2,1)) end) as TUOI_2, (case when hsba.gioitinhcode='01' then 'X' end) as GT_NAM, (case when hsba.gioitinhcode='02' then 'X' end) as GT_NU, hsba.nghenghiepname as NNGHIEP, substr(hsba.nghenghiepcode,1,1) as NNGHIEP_1, substr(hsba.nghenghiepcode,2,1) as NNGHIEP_2, hsba.hc_dantocname as DANTOC, substr(hsba.hc_dantoccode,1,1) as DANTOC_1, substr(hsba.hc_dantoccode,2,1) as DANTOC_2, hsba.hc_quocgianame as NKIEU, substr(hsba.hc_quocgiacode,1,1) as NKIEU_1, substr(hsba.hc_quocgiacode,2,1) as NKIEU_2, hsba.hc_sonha as SONHA, hsba.hc_thon as THON, (case when hsba.hc_xacode<>'00' then hsba.hc_xaname end) as XA, (case when hsba.hc_huyencode<>'00' then hsba.hc_huyenname end) as HUYEN, substr(hsba.hc_huyencode,1,1) as HUYEN_1, substr(hsba.hc_huyencode,2,1) as HUYEN_2, (case when hsba.hc_tinhcode<>'00' then hsba.hc_tinhname end) as TINH, substr(hsba.hc_tinhcode,1,1) as TINH_1, substr(hsba.hc_tinhcode,2,1) as TINH_2, hsba.noilamviec as NOILAMVIEC, (case vp.doituongbenhnhanid when 1 then 'X' end) as DT_BHYT, (case when vp.doituongbenhnhanid in (2,3) then 'X' end) as DT_VP, (case vp.doituongbenhnhanid when 5 then 'X' end) as DT_MP, (case when vp.doituongbenhnhanid >6 then 'X' end) as DT_KH, (case when bh.bhytcode<>'' then to_char(bh.bhytutildate, 'ngà\"y\" dd tháng mm năm yyyy') end) as BHDEN_FULLTIME, substr(bh.bhytcode,1,2) as BHYT_1, substr(bh.bhytcode,3,1) as BHYT_2, substr(bh.bhytcode,4,2) as BHYT_3, substr(bh.bhytcode,6,2) as BHYT_4, substr(bh.bhytcode,8,8) as BHYT_5, hsba.nguoithan_name || ' ' || hsba.nguoithan_address as NNHA_TEN, hsba.nguoithan_phone as NNHA_SDT, (to_char(hsba.hosobenhandate, 'hh g\"i\"ờ mi phút ngà\"y\" dd/mm/yyyy')) as VVIEN_FULLTIME, '' as TTIEP_CC, '' as TTIEP_KKB, '' as TTIEP_KDT, '' as NGT_CQYT, 'X' as NGT_TD, '' as NGT_KH, '' as VVLANTHU, '' as VKHOA_MA, '' as VKHOA_FULLTIME, '' as SNDT_1, '' as SNDT_2, '' as CVIEN_TT, '' as CVIEN_TD, '' as CVIEN_CK, '' as CVIEN_CDEN, (case when hsba.hosobenhanstatus=1 then to_char(hsba.hosobenhandate_ravien, 'hh g\"i\"ờ mi phút ngà\"y\" dd/mm/yyyy') else '....giờ.... phút ngày ..../..../.....' end) as RVIEN_FULLTIME, (case hsba.hinhthucravienid when 1 then 'X' end) as RVIEN_RV, (case hsba.hinhthucravienid when 2 then 'X' end) as RVIEN_XV, (case hsba.hinhthucravienid when 3 then 'X' end) as RVIEN_BV, (case hsba.hinhthucravienid when 4 then 'X' end) as RVIEN_DV, (case hsba.hosobenhanstatus when 1 then cast((cast(to_char(hsba.hosobenhandate_ravien, 'yyyy') as integer) - cast(to_char(hsba.hosobenhandate, 'yyyy') as integer) + 1) as text) end) as TSNDT, '' as TSNDT_1, '' as TSNDT_2, '' as TSNDT_3, '' as CD_NCD, '' as CD_NCD_1, '' as CD_NCD_2, '' as CD_NCD_3, '' as CD_NCD_4, '' as CD_KKB, '' as CD_KKB_1, '' as CD_KKB_2, '' as CD_KKB_3, '' as CD_KKB_4, '' as CD_KDT, '' as CD_KDT_1, '' as CD_KDT_2, '' as CD_KDT_3, '' as CD_KDT_4, '' as THUTHUAT, '' as PHAUTHUAT, hsba.chandoanravien as RVIEN_BENHCHINH, substr(hsba.chandoanravien_code,1,1) as RVIEN_BENHCHINH_1, substr(hsba.chandoanravien_code,2,1) as RVIEN_BENHCHINH_2, substr(hsba.chandoanravien_code,3,1) as RVIEN_BENHCHINH_3, substr(hsba.chandoanravien_code,4,1) as RVIEN_BENHCHINH_4, hsba.chandoanravien_kemtheo as RVIEN_BENHPHU, substr(hsba.chandoanravien_kemtheo_code,1,1) as RVIEN_BENHPHU_1, substr(hsba.chandoanravien_kemtheo_code,2,1) as RVIEN_BENHPHU_2, substr(hsba.chandoanravien_kemtheo_code,3,1) as RVIEN_BENHPHU_3, substr(hsba.chandoanravien_kemtheo_code,4,1) as RVIEN_BENHPHU_4, '' as RVIEN_TAIBIEN, '' as RVIEN_BIENCHUNG, (case hsba.ketquadieutriid when 1 then 'X' end) as TTRV_KHOI, (case hsba.ketquadieutriid when 2 then 'X' end) as TTRV_DO, (case hsba.ketquadieutriid when 3 then 'X' end) as TTRV_KTD, (case hsba.ketquadieutriid when 4 then 'X' end) as TTRV_NANG, (case hsba.ketquadieutriid when 5 then 'X' end) as TTRV_TUVONG, '' as TTRV_GPB_LT, '' as TTRV_GPB_NN, '' as TTRV_GPB_AT, '' as THTV_FULLTIME, '' as THTV_DOBENH, '' as THTV_TBDT, '' as THTV_KH, '' as THTV_24H, '' as THTV_SAU24H, '' as THTV_NNTV, '' as THTV_NNTV_1, '' as THTV_NNTV_2, '' as THTV_NNTV_3, '' as THTV_NNTV_4, '' as THTV_KNTT, '' as THTV_CDGPTT, '' as THTV_CDGPTT_1, '' as THTV_CDGPTT_2, '' as THTV_CDGPTT_3, '' as THTV_CDGPTT_4 FROM hosobenhan hsba inner join vienphi vp on vp.hosobenhanid=hsba.hosobenhanid left join bhyt bh on bh.bhytid=vp.bhytid WHERE hsba.hosobenhanid=" + mrdHsbaHsba.hosobenhanid + " ;  ";


                        System.Data.DataTable dataService = condb.GetDataTable_HIS(getthongtinservice);
                        Aspose.Words.Document documentWord = new Aspose.Words.Document();
                        documentWord = Utilities.Common.Word.WordMergeTemplateExport.ExportWordMailMergeFormat(templateFullPath, dataService, "PhieuBenhAn_" + mrdHsbaHsba.hosobenhanid + ".doc", Aspose.Words.SaveFormat.Doc);

                        MoFileWordBangOffice(Environment.CurrentDirectory + Base.KeyTrongPhanMem.ReportTemps_Path + "\\PhieuBenhAn_" + mrdHsbaHsba.hosobenhanid + ".doc");
                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại thiết lập template Hồ sơ bệnh án", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        private static void MoFileWordBangOffice(string filePath)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application ap = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document document = ap.Documents.Open(filePath); ;
                ap.Visible = true;

                //Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                //object wFileName = filePath;
                //object wConfirmConversions = true;
                //object wReadOnly = true;
                //object wAddToRecentFiles = true;
                //object wPasswordDocument = "";
                //object wPasswordTemplate = "";
                //object wRevert = true;
                //object wWritePasswordDocument = "";
                //object wWritePasswordTemplate = "";
                //object wFormat = Microsoft.Office.Interop.Word.WdOpenFormat.wdOpenFormatAuto;
                //object wEncoding = Microsoft.Office.Core.MsoEncoding.msoEncodingAutoDetect;
                //object wVisible = true;
                //object wOpenAndRepair = true;
                //object wDocumentDirection = Microsoft.Office.Interop.Word.WdDocumentDirection.wdRightToLeft;
                //object wNoEncodingDialog = false;
                //object wXMLTransform = Missing.Value;

                //object nullobj = System.Reflection.Missing.Value;

                ////          Document doc = wordApp.Documents.Open( ref wFileName, ref nullobj, ref nullobj, ref nullobj, 
                ////                                                 ref nullobj, ref nullobj, ref nullobj, ref nullobj, 
                ////                                                 ref nullobj, ref nullobj, ref nullobj, ref nullobj, 
                ////                                                 ref nullobj, ref nullobj, ref nullobj, ref nullobj );
                ////   wordApp = true;
                //Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref wFileName, ref wConfirmConversions, ref nullobj,
                //                                       ref wAddToRecentFiles, ref wPasswordDocument, ref wPasswordTemplate,
                //                                       ref wRevert, ref wWritePasswordDocument, ref wWritePasswordTemplate,
                //                                       ref wFormat, ref wEncoding, ref wVisible, ref wOpenAndRepair,
                //                                       ref wDocumentDirection, ref wNoEncodingDialog, ref wXMLTransform);
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }



    }
}
