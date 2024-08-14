using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
 

namespace teachagent
{
    public partial class teachplus : Form
    {


        private System.Timers.Timer routeMonitorTimer;
        private string lastRouteTable = ""; // 存储上一次查询的路由表信息以便比较
        public teachplus()
        {
            InitializeComponent();
            InitializeRouteMonitor();
        }
        private void InitializeRouteMonitor()
        {
            routeMonitorTimer = new System.Timers.Timer(1500); // 设置定时器间隔为10秒  
            routeMonitorTimer.Elapsed += TimerElapsed;
            // 不在构造函数中启动定时器，而是在按钮点击事件中启动  
        }




        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //将光标位置设置到当前内容的末尾
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //滚动到光标位置
            richTextBox1.ScrollToCaret();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if(Form1.userid == "")
            {
                Form iderror = new iderror();
                iderror.Show();
            }
            else
            {
                richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 开始进行深度清除...\r\n");
                
                richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 正在获取当前路由表...\r\n");
                
                if (!routeMonitorTimer.Enabled)
                {
                    routeMonitorTimer.Start();
                    MessageBox.Show("开始监控路由表...");
                    richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 开始持续监视路由表...\r\n");
                    
                    richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 监视进程已启动\r\n");
                    
                    richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 提示:请保持此页面不要退出（可以最小化）\r\n");
                    richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 否则进程将失效，无法上网\r\n");

                }
            }
            
            









        }



        private void TimerElapsed(Object source, ElapsedEventArgs e)
        {
            string currentRouteTable = GetRouteTable();
            if (currentRouteTable != lastRouteTable)
            {
                // 路由表发生变化  
                ProcessRouteChanges(currentRouteTable, lastRouteTable);
                lastRouteTable = currentRouteTable; // 更新上一次查询的路由表信息  
            }
        }

        private string GetRouteTable()
        {
            using (var process = new Process())
            {
                var startInfo = new ProcessStartInfo("cmd.exe", "/c route print")
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // 这里可以添加额外的逻辑来解析你需要的路由信息  
                // 例如，你可以只保留IPv4路由信息，或者提取特定的字段  
                // 由于route print的输出格式复杂，你可能需要使用字符串分割、正则表达式等方法来解析  

                // 这里为了简单起见，我们直接返回整个输出（仅用于示例）  
                return output;
            }
        }


        private void ProcessRouteChanges(string currentRouteTable, string lastRouteTable)
        {
            // 在这里处理路由表的变化  
            // 例如，你可以比较两个字符串来找出差异，并记录或显示这些差异  
            // 由于字符串可能很长且包含很多不相关的信息，你可能需要实现更复杂的比较逻辑  

            // 为了简单起见，我们只是将当前路由表输出到控制台（或可以是一个日志文件、UI元素等）  
            Console.WriteLine("路由表已更改:");
            Console.WriteLine(currentRouteTable);

            richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 检测到路由表被篡改\r\n");
            
            richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 路由表被改为"+ currentRouteTable+"\r\n");
            
            richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 正在尝试恢复路由表...\r\n");
            

            string ipv = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipv = ip.ToString();
                    Console.WriteLine(ip.ToString());
                }
            }
            Process process = function.Newprocess("cmd.exe");
            string input = "netsh interface ipv4 set address name=\"本地连接\" static " + ipv + " 255.255.255.0 " + Form1.userid;
            Console.WriteLine(input);
            process.StandardInput.WriteLine(input);
            process.StandardInput.WriteLine("exit");
            richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 恢复成功\r\n");
            
            richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 正在检测网络连接状态...\r\n");
            
            bool flag = function.Web("https://www.baidu.com");
            if (flag)
            {
                richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 网络连接成功\r\n");
            }
            else
            {
                richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 网络连接失败\r\n");
            }

            // 注意：由于这是在非UI线程上执行的，如果需要更新UI元素，请使用Invoke或BeginInvoke方法  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (routeMonitorTimer.Enabled)
            {
                routeMonitorTimer.Stop();
                MessageBox.Show("停止监控路由表...");
                richTextBox1.AppendText("[" + System.DateTime.Now.ToString("T") + "] 已停止监控，可能无法继续上网\r\n");
            }
        }
    }
}
