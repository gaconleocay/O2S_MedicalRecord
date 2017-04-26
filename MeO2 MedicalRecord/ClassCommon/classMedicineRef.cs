using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO2_MedicalRecord.ClassCommon
{
    public class classMedicineRef
    {
        public long medicinerefid { get; set; }
        public long medicinerefid_org { get; set; }
        public string medicinecode { get; set; }
        public string medicinename { get; set; }
        public decimal giaban { get; set; }
    }
}
