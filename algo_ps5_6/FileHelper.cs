using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_ps5_6
{
    public class FileHelper
    {
        public void LoadLine()
        {

        }
        public static void WriteLineToTxt(string sn, string fileName)
        {
            StreamWriter sw = new($"..\\..\\..\\{fileName}.txt", append: true);
            sw.WriteLine(sn);
            sw.Close();
        }
    }
}
