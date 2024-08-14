using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teachagent
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passwd = textBox1.Text;
            if(passwd == "87741326")
            {
                teachagent.Form1.activemode = "1";
                Form active = new active();
                active.Show();
                Close();
            }
            else
            {
                teachagent.Form1.activemode = "0";
                Form inactive = new inactive();
                inactive.Show();
            }
            textBox1.Text = "";
        }
    }
}
