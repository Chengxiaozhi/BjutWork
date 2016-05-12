using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Cool
{
    class CheckNet
    {
        public string CmdPing(string strIp)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string pingrst;
            p.Start();
            p.StandardInput.WriteLine("ping -n 1 " + strIp);
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();
            if (strRst.IndexOf("(0% 丢失)") != -1 || strRst.IndexOf("(0% loss)") != -1)

                pingrst = "连接";

            else if (strRst.IndexOf("Destination host unreachable.") != -1)

                pingrst = "无法到达目的主机";

            else if (strRst.IndexOf("Request timed out.") != -1)

                pingrst = "超时";

            else if (strRst.IndexOf("Unknown host") != -1)

                pingrst = "无法解析主机";

            else

                pingrst = strRst;

            p.Close();

            return pingrst;

        }
    }
}
