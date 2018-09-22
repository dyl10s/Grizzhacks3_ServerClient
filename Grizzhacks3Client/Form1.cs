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
        int pcID;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client.start();
            tUpdate.Enabled = true;
            Random r = new Random();
            pcID = r.Next(10000, 99999);
            label1.Text = "ID: " + pcID;
            client.sendData("pc;" + pcID);
        }

        private void tUpdate_Tick(object sender, EventArgs e)
        {
            if (client.recievedData.Count > 0)
            {
                if (client.recievedData.First<String>().Contains("image"))
                {
                    clsImage img = new clsImage(client.recievedData.First<String>().Split(';')[2]);
                    ucImage newImageControl = new ucImage(img);
                    flpMain.Controls.Add(newImageControl);
                }
                client.recievedData.Clear();
            }
        }
    }
}
