using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
   public class MedicalrecordDTO
    {
       public long stt { get; set; }
       public long patientid { get; set; }
       public string patientcode { get; set; }
       public string patientname { get; set; }
       public long medicalrecordid { get; set; }
       public string medicalrecordcode { get; set; }
       public string bhytcode { get; set; }
       public DateTime thoigianvaovien { get; set; }
       public long medicalrecordstatus { get; set; }
       public long hosobenhanid { get; set; }
       public long departmentid { get; set; }
       public long departmentgroupid { get; set; }
       public long servicepriceid { get; set; }
    }
}
