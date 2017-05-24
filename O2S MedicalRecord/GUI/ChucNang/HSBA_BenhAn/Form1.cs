using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O2S_MedicalRecord.GUI.ChucNang.HSBA_BenhAn
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string GetTextFile(string textFile)
        {
            string textBytes = null;
            Console.WriteLine("Loading File: " + textFile);

            FileStream fs = new FileStream(textFile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            textBytes = sr.ReadToEnd();

            Console.WriteLine("TextBytes has length {0} bytes.", textBytes.Length);
            return textBytes;
        }


    }
}
