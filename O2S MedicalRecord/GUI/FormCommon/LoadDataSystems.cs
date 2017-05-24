using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.GUI.FormCommon
{
    public class LoadDataSystems
    {
        static O2S_MedicalRecord.DAL.ConnectDatabase condb = new O2S_MedicalRecord.DAL.ConnectDatabase();

        #region LoadData Phan Mem
        public static void Load_Serviceprice()
        {
            try
            {
                GlobalStore.GlobalLst_MrdServiceref = new List<DTO.MrdServicerefDTO>();
                string sqlLayDanhMuc = "select ROW_NUMBER () OVER (ORDER BY bhyt_groupcode, servicepricename) as stt, mrd_servicerefid, his_servicepricerefid, servicegrouptype, servicepricetype, bhyt_groupcode, servicepricegroupcode, servicepricecode, servicepricename, servicepricenamenhandan, servicepricenamebhyt, servicepricenamenuocngoai, servicepriceunit, servicepricefee, servicepricefeenhandan, servicepricefeebhyt, servicepricefeenuocngoai, servicelock, servicepricecodeuser, servicepricesttuser, pttt_hangid, pttt_loaiid, mrd_templatename from mrd_serviceref; ";
                DataView dv_DanhMucDichVu = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                if (dv_DanhMucDichVu.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        MrdServicerefDTO dmDichVu = new MrdServicerefDTO();
                        dmDichVu.mrd_servicerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["mrd_servicerefid"].ToString());
                        dmDichVu.his_servicepricerefid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["his_servicepricerefid"].ToString());
                        dmDichVu.servicepricetype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicepricetype"].ToString());
                        dmDichVu.servicegrouptype = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["servicegrouptype"].ToString());
                        dmDichVu.bhyt_groupcode = dv_DanhMucDichVu[i]["bhyt_groupcode"].ToString();
                        dmDichVu.servicepricegroupcode = dv_DanhMucDichVu[i]["servicepricegroupcode"].ToString();
                        dmDichVu.servicepricecode = dv_DanhMucDichVu[i]["servicepricecode"].ToString();
                        dmDichVu.servicepricename = dv_DanhMucDichVu[i]["servicepricename"].ToString();
                        dmDichVu.servicepricenamenhandan = dv_DanhMucDichVu[i]["servicepricenamenhandan"].ToString();
                        dmDichVu.servicepricenamebhyt = dv_DanhMucDichVu[i]["servicepricenamebhyt"].ToString();
                        dmDichVu.servicepricenamenuocngoai = dv_DanhMucDichVu[i]["servicepricenamenuocngoai"].ToString();
                        dmDichVu.servicepriceunit = dv_DanhMucDichVu[i]["servicepriceunit"].ToString();
                        dmDichVu.servicepricefee = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefee"].ToString());
                        dmDichVu.servicepricefeenhandan = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenhandan"].ToString());
                        dmDichVu.servicepricefeebhyt = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeebhyt"].ToString());
                        dmDichVu.servicepricefeenuocngoai = Utilities.Util_TypeConvertParse.ToDecimal(dv_DanhMucDichVu[i]["servicepricefeenuocngoai"].ToString());
                        dmDichVu.servicelock = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["servicelock"].ToString());
                        dmDichVu.servicepricecodeuser = dv_DanhMucDichVu[i]["servicepricecodeuser"].ToString();
                        dmDichVu.servicepricesttuser = dv_DanhMucDichVu[i]["servicepricesttuser"].ToString();
                        dmDichVu.pttt_hangid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_hangid"].ToString());
                        dmDichVu.pttt_loaiid = Utilities.Util_TypeConvertParse.ToInt16(dv_DanhMucDichVu[i]["pttt_loaiid"].ToString());
                        dmDichVu.mrd_templatename = dv_DanhMucDichVu[i]["mrd_templatename"].ToString();

                        GlobalStore.GlobalLst_MrdServiceref.Add(dmDichVu);
                    }
                }
            }
            catch (Exception ex)
            {
                O2S_MedicalRecord.Base.Logging.Warn(ex);
            }
        }

        public static void Load_MrdHsbaTemplate()
        {
            try
            {
                GlobalStore.GlobalLst_MrdHsbaTemplate = new List<DTO.MrdHsbaTemplateDTO>();
                string sqlLayDanhMuc = "select ROW_NUMBER() OVER (ORDER BY mrd_hsbatemname) as stt, mrd_hsbatemid, mrd_hsbatemcode, mrd_hsbatemname, mrd_hsbatemtypeid, mrd_hsbatemnamepath from mrd_hsba_template; ";
                DataView dv_DanhMucDichVu = new DataView(condb.GetDataTable_HSBA(sqlLayDanhMuc));
                if (dv_DanhMucDichVu.Count > 0)
                {
                    for (int i = 0; i < dv_DanhMucDichVu.Count; i++)
                    {
                        MrdHsbaTemplateDTO dmDichVu = new MrdHsbaTemplateDTO();
                        dmDichVu.mrd_hsbatemid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["mrd_hsbatemid"].ToString());
                        dmDichVu.mrd_hsbatemcode = dv_DanhMucDichVu[i]["mrd_hsbatemcode"].ToString();
                        dmDichVu.mrd_hsbatemname = dv_DanhMucDichVu[i]["mrd_hsbatemname"].ToString();
                        dmDichVu.mrd_hsbatemtypeid = Utilities.Util_TypeConvertParse.ToInt64(dv_DanhMucDichVu[i]["mrd_hsbatemtypeid"].ToString());
                        dmDichVu.mrd_hsbatemnamepath = dv_DanhMucDichVu[i]["mrd_hsbatemnamepath"].ToString();

                        GlobalStore.GlobalLst_MrdHsbaTemplate.Add(dmDichVu);
                    }
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
