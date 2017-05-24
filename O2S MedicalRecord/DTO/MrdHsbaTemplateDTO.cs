using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class MrdHsbaTemplateDTO
    {
        public long mrd_hsbatemid { get; set; }
        public string mrd_hsbatemcode { get; set; }
        public string mrd_hsbatemname { get; set; }
        public long mrd_hsbatemtypeid { get; set; }
        public string mrd_hsbatemnamepath { get; set; }
    }
}
