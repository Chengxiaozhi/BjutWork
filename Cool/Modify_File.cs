using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Cool
{
    class Modify_File
    {
        public static void EditFile(int curLine, string newLineValue, string patch)
        {
            FileStream fs = new FileStream(patch, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312"));
            string line = sr.ReadLine();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; line != null; i++)
            {
                sb.Append(line + "\r\n");
                if (i != curLine - 1)
                    line = sr.ReadLine();
                else
                {
                    sr.ReadLine();
                    line = newLineValue;
                }
            }
            sr.Close();
            fs.Close();
            FileStream fs1 = new FileStream(patch, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1,Encoding.GetEncoding("GB2312"));
            sw.Write(sb.ToString());
            sw.Close();
            fs.Close();
        }  
    }
}
