using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grizzhacks3
{
    public partial class Form1 : Form
    {
        tcpServer server = new tcpServer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server.start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = server.log;
        }
    }
}
