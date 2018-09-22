using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grizzhacks3Client
{
    public partial class Form1 : Form
    {
        tcpClient client = new tcpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.sendData(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client.start();
            tUpdate.Enabled = true;
        }

        private void tUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                if (txtLog.Text != client.recievedData.First<String>())
                {
                    txtLog.Text = client.recievedData.First<String>();
                }
            }
            catch { }

        }
    }
}
