using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
   public  class HoiChan_ThanhVienThamGiaDTO
    {
       public int stt { get; set; }
       public int loaidoituong_id { get; set; } //chu tich=1; thu ky=2; thanh vien=3
       public string loaidoituong_ten { get; set; }
       public string hovaten { get; set; }
       public string chucdanhchucvu { get; set; } 

    }
}
