using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdHsbaHoiChan_ServiceDTO
    {
        public long medicinerefid_org { get; set; }
        public long servicepriceid { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricename { get; set; }
        public long maubenhphamid { get; set; }
        public DateTime servicepricedate { get; set; }

    }
}
