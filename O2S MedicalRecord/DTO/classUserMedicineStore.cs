using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class classUserMedicineStore
    {
        public bool MedicineStoreCheck { get; set; }
        public Int32 MedicineStoreId { get; set; }
        public string MedicineStoreCode { get; set; }
        public string MedicineStoreName { get; set; }
        public Int32 MedicineStoreType { get; set; }
        public string MedicineStoreTypeName { get; set; }
    }
}
