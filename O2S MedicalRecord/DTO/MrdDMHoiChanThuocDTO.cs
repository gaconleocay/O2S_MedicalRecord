using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdDMHoiChanThuocDTO
    {
        public long stt { get; set; }
        public long mrd_dmhc_thuocid { get; set; }
        public long medicinerefid { get; set; }
        public string medicinecode { get; set; }
        public string medicinegroupcode { get; set; }
        public long medicinereftype { get; set; }
        public long medicinetype { get; set; }
        public string medicinename { get; set; }
        public string tenkhoahoc { get; set; }
        public string donvitinh { get; set; }
        public string nongdo { get; set; }
        public string lieuluong { get; set; }
        public string hoatchat { get; set; }
        public decimal gianhap { get; set; }
        public decimal giaban { get; set; }
        public decimal vatnhap { get; set; }
        public decimal vatban { get; set; }
        public string bhyt_groupcode { get; set; }
        public decimal servicepricefee { get; set; }
        public decimal servicepricefeenhandan { get; set; }
        public decimal servicepricefeebhyt { get; set; }
        public decimal servicepricefeenuocngoai { get; set; }
        public string mahoatchat { get; set; }
        public string medicinecodeuser { get; set; }
        public string stt_dauthau { get; set; }
        public string medicinename_byt { get; set; }
        public long stt_thuoc_tt40 { get; set; }
        public string stt_thuoc_tt40text { get; set; }
        public string mrd_dmhc_thuocnamepath { get; set; }
        public string medicinecode_khongdau { get; set; }
        public string medicinename_khongdau { get; set; }
    }
}
