using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdDMHoiChanPTTTDTO
    {
        public long stt { get; set; }
        public long mrd_dmhc_ptttid { get; set; }
        public long servicepricerefid { get; set; }
        public string servicepricecode { get; set; }
        public string servicepricegroupcode { get; set; }
        public long servicegrouptype { get; set; }
        public long servicepricetype { get; set; }
        public string servicepricename { get; set; }
        public string servicepricenamebhyt { get; set; }
        public string servicepricenamenuocngoai { get; set; }
        public string servicepriceunit { get; set; }
        public string servicepricefee { get; set; }
        public string servicepricefeenhandan { get; set; }
        public string servicepricefeebhyt { get; set; }
        public string servicepricefeenuocngoai { get; set; }
        public string bhyt_groupcode { get; set; }
        public long pttt_hangid { get; set; }
        public string pttt_hang { get; set; }
        public long pttt_loaiid { get; set; }
        public string pttt_loai { get; set; }
        public string servicepricecodeuser { get; set; }
        public string servicepricesttuser { get; set; }
        public string mrd_dmhc_ptttnamepath { get; set; }
        public string servicepricecode_khongdau { get; set; }
        public string servicepricename_khongdau { get; set; }
    }
}
