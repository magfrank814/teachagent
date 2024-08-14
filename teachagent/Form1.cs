using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace teachagent
{

    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form warning = new warning();
            warning.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (activemode == "1")
            {
                button2.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                button1.Text = "正在执行";
                progressBar1.Value = 0;
                for (int i = 0; i < 100; i++)
                {
                    progressBar1.Value += 1;
                    Thread.Sleep(10);
                }

                for (int i = 0; i < 3; i++)
                {
                    Process process = function.Newprocess("cmd.exe");
                    process.StandardInput.WriteLine("taskkill /f /t /im studentmain.exe");
                    process.StandardInput.WriteLine("exit");
                }

                Thread.Sleep(1000);

                bool flag = !function.IsRuning("studentmain");
                if (flag)
                {
                    Form suc = new suc();
                    suc.Show();
                }
                else
                {
                    Form fail = new fail();
                    fail.Show();
                }
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button1.Text = "结束极域进程";
                progressBar1.Value = 100;
                Thread.Sleep(100);
                progressBar1.Value = 0;
            }
            else
            {
                Form activeerror = new activeerror();
                activeerror.Show();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Form help = new help();
            help.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (activemode == "1")
            {
                button1.Visible = false;
                
                button3.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
                string r4 = "";
                button2.Text = "正在执行";
                progressBar2.Value = 0;
                for (int i = 0; i < 100; i++)
                {
                    progressBar2.Value += 1;
                    Thread.Sleep(10);

                }

                for (int a = 0; a < 4; a++)
                {
                    Process process = function.Newprocess("cmd.exe");
                    process.StandardInput.WriteLine("taskkill /f /t /im stuagent.exe");
                    process.StandardInput.WriteLine("exit");


                    Thread.Sleep(10);
                    Process process3 = function.Newprocess("cmd.exe");
                    process3.StandardInput.WriteLine("taskkill /f /t /im server.exe");
                    process3.StandardInput.WriteLine("exit");


                    Thread.Sleep(10);

                    Process process1 = function.Newprocess("cmd.exe");
                    process1.StandardInput.WriteLine("taskkill /f /t /im stuserver.exe");
                    process1.StandardInput.WriteLine("exit");


                    Thread.Sleep(10);
                    Process process2 = function.Newprocess("cmd.exe");
                    process2.StandardInput.WriteLine("taskkill /f /t /im wmi.exe");
                    process2.StandardInput.WriteLine("exit");


                    Process[] pcs = Process.GetProcesses();
                    foreach (Process p in pcs)
                    {
                        try
                        {
                            if (p.MainModule.FileName == "C:\\Windows\\svchost.exe")
                            {
                                p.Kill();
                            }
                        }
                        catch
                        {

                        }

                    }
                }




                Thread.Sleep(1000);
                File.Delete("C:\\Windows\\svchost.exe");
                File.Delete("C:\\Windows\\svchost.exe");
                File.Delete("C:\\Windows\\svchost.exe");

                File.Delete("C:\\Windows\\stuserver.exe");
                File.Delete("C:\\Windows\\stuagent.exe");
                Thread.Sleep(500);
                if (!File.Exists("C:\\Windows\\svchost.exe"))
                {
                    r4 = "yes";
                }
                else
                {
                    r4 = "no";
                }
                Thread.Sleep(1000);


                if (!function.IsRuning("stuagent") && !function.IsRuning("stuserver") && !function.IsRuning("wmi") && !function.IsRuning("server") && r4 == "yes")
                {
                    Form suc = new suc();
                    suc.Show();
                }
                else
                {
                    Form fail = new fail();
                    fail.Show();
                }
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button5.Visible = true;
                button6.Visible = true;


                button2.Text = "结束stuagent等进程";
                progressBar2.Value = 100;
                Thread.Sleep(100);
                progressBar2.Value = 0;
            }
            else
            {
                Form activeerror = new activeerror();
                activeerror.Show();
            }
            



            
            


        }
        public static string ipv = "";
        public void button3_Click(object sender, EventArgs e)
        {
            if (activemode == "1")
            {
                if (userid == "")
                {
                    Form locationnull = new locationnull();
                    locationnull.Show();
                }
                else
                {
                    button1.Visible = false;
                    button2.Visible = false;
                    
                    button5.Visible = false;
                    button6.Visible = false;
                    button3.Text = "正在执行";
                    progressBar3.Value = 0;
                    for (int i = 0; i < 100; i++)
                    {
                        progressBar3.Value += 1;
                        Thread.Sleep(10);

                    }



                    


                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (var ip in host.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipv = ip.ToString();
                            Console.WriteLine(ip.ToString());
                        }
                    }
                    Thread.Sleep(1000);
                    Process process = function.Newprocess("cmd.exe");
                    string input = "netsh interface ipv4 set address name=\"本地连接\" static " + ipv + " 255.255.255.0 " + userid;
                    Console.WriteLine(input);
                    process.StandardInput.WriteLine(input);
                    process.StandardInput.WriteLine("exit");
                    Thread.Sleep(2000);
                    bool flag = function.Web("https://www.baidu.com");
                    if (flag)
                    {
                        Form suc = new suc();
                        suc.Show();
                    }
                    else
                    {
                        Form fail = new fail();
                        fail.Show();
                    }

                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;


                    button3.Text = "尝试开网";
                    progressBar3.Value = 100;
                    Thread.Sleep(100);
                    progressBar3.Value = 0;
                }
            }
            else
            {
                Form activeerror = new activeerror();
                activeerror.Show();
            }

            
            

        }


        public static string userid = "";

        private void button5_Click(object sender, EventArgs e)
        {
            string location = textBox1.Text;
            if(location == "411")
            {
                userid = "192.168.10.1";
                Form suc = new suc();
                suc.Show();
            }
            else if(location == "debug")
            {
                userid = "192.168.20.2";
                Form suc = new suc();
                suc.Show();
            }
            else
            {
                userid = "";
                Form locationfail = new locationfail();
                locationfail.Show();
            }
            textBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(activemode == "1")
            {
                
            }
            else
            {
                Form activeerror = new activeerror();
                activeerror.Show();
            }
            
        }
        public static string activemode = "0";
        
        public void button7_Click(object sender, EventArgs e)
        {
            if(activemode == "1")
            {
                Form active_already = new active_already();
                active_already.Show();
            }
            else
            {
                Form login = new login();
                login.Show();
            }
            
            
            
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            System.Diagnostics.Process.Start("https://github.com/magfrank814/teachagent");
        }
    }


    
}
