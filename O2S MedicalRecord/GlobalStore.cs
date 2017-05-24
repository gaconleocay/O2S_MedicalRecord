using O2S_MedicalRecord.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord
{
    public class GlobalStore
    {
       public static List<MrdServicerefDTO> GlobalLst_MrdServiceref { get; set; }
       public static List<MrdHsbaTemplateDTO> GlobalLst_MrdHsbaTemplate { get; set; }



    }
}
