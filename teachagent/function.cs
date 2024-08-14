using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace teachagent
{
    internal class function
    {



        public static Process Newprocess(string exe)
        {
           Process process = new Process();
            process .StartInfo.FileName = exe;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            return process;
        }



        public static bool IsRuning(string exeName)
        {
            bool result = false;
            try
            {
                bool flag = Process.GetProcessesByName(exeName).ToList<Process>().Count > 0;
                if (flag)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }




        public static bool Web(string url)
        {
            try
            {
                System.Net.WebRequest myrequest = System.Net.WebRequest.Create(url);
                System.Net.WebResponse myresponse = myrequest.GetResponse();
            }
            catch (System.Net.WebException)
            {
                return false;
            }
            return true;
        }


        




    }
}
