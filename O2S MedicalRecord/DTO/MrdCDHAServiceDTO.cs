using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdCDHAServiceDTO
    {
        public long mrd_cdha_serviceid { get; set; }
        public long servicepriceid { get; set; }
        public string servicepricecode { get; set; }
        public long maubenhphamid { get; set; }
        public long patientid { get; set; }
        public long vienphiid { get; set; }
        public long hosobenhanid { get; set; }
        public long medicalrecordid { get; set; }
        public long mrd_cdhatemid { get; set; }
        public long departmentgroupid { get; set; }
        public long departmentid { get; set; }
        public string mrd_cdha_servicedata { get; set; }
        public string mrd_cdha_servicedata_nd { get; set; }
        public long mrd_cdha_servicestatus { get; set; }
        public long create_userid { get; set; }
        public long create_mrduserid { get; set; }
        public long create_hisuserid { get; set; }
        public string create_hisusercode { get; set; }
        public DateTime create_date { get; set; }
        public long modify_userid { get; set; }
        public DateTime modify_date { get; set; }
        public string note { get; set; }
        public bool file_readonly { get; set; }
    }
}
