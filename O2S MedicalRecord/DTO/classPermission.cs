using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecord.DTO
{
    public class classPermission
    {
        //public mrd_tbluser_permission();
        public bool permissioncheck { get; set; }
        public Int32 permissionid { get; set; }
        public string permissioncode { get; set; }
		public string en_permissioncode { get; set; }
        public string permissionname { get; set; }
		public string en_permissionname { get; set; }
        public Int32 permissiontype { get; set; } // 1: he thong; 2: chuc nang; 3: bao cao; 4: thao tac
        public string permissionnote { get; set; }
    }
}
