using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdHsbaHoiChanDTO
    {
        public long mrd_loaihc_id { get; set; }
        public string mrd_loaihc_name { get; set; }
        public long mrd_hsba_hcid { get; set; }
        public long mrd_dmhc_id { get; set; }
        public long his_chuyenvienid { get; set; }
        public long maubenhphamid { get; set; }
        public long medicinerefid_org { get; set; }
        public long servicepriceid { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public DateTime servicepricedate { get; set; }
        public long hosobenhanid { get; set; }
        public long medicalrecordid { get; set; }
        public long patientid { get; set; }
        public long vienphiid { get; set; }
        public long departmentgroupid { get; set; }
        public long departmentid { get; set; }
        public DateTime thoigianhoichan { get; set; }
        public string yeucauhoichan { get; set; }
        public string diadiemhoichan { get; set; }
        public string dbb_tomtattiensubenh { get; set; }
        public string dbb_tinhtranglucvaovien { get; set; }
        public string dbb_chandoantuyenduoi { get; set; }
        public string dbb_tomtatdienbienbenh { get; set; }
        public string yk_chandoantienluong { get; set; }
        public string yk_phuongphapdieutri { get; set; }
        public string yk_chamsoc { get; set; }
        public string kl_ketluan { get; set; }
        public string tvtg_chutoa_ten { get; set; }
        public string tvtg_chutoa_cdcv { get; set; }
        public string tvtg_thuky_ten { get; set; }
        public string tvtg_thuky_cdcv { get; set; }
        public string tvtg_thanhvien1_ten { get; set; }
        public string tvtg_thanhvien1_cdcv { get; set; }
        public string tvtg_thanhvien2_ten { get; set; }
        public string tvtg_thanhvien2_cdcv { get; set; }
        public string tvtg_thanhvien3_ten { get; set; }
        public string tvtg_thanhvien3_cdcv { get; set; }
        public string tvtg_thanhvien4_ten { get; set; }
        public string tvtg_thanhvien4_cdcv { get; set; }
        public string tvtg_thanhvien5_ten { get; set; }
        public string tvtg_thanhvien5_cdcv { get; set; }
        public string tvtg_thanhvien6_ten { get; set; }
        public string tvtg_thanhvien6_cdcv { get; set; }
        public long mrd_hsba_hcstatus { get; set; }
        public long create_mrduserid { get; set; }
        public string create_mrdusercode { get; set; }
        public DateTime create_date { get; set; }
    }
}
