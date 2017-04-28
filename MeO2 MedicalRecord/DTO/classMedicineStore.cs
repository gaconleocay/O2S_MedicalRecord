using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO2_MedicalRecord.DTO
{
    public class classMedicineStore
    {
        public long? medicinestoreid { get; set; }
        public long? departmentgroupid { get; set; }
        public int? medicinestoretype { get; set; }
        public string medicinestorecode { get; set; }
        public string medicinestorename { get; set; }

    }
}
