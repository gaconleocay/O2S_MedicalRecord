using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace O2S_MedicalRecordLauncher
{
    public static class Util_FileCheckSum
    {
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                    }
                }
            }
            catch (Exception)
            {
                return "32323";
            }
        }





    }
}
