using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grizzhacks3Client
{
    public partial class Form1 : Form
    {
        bool autoSave = false;
        bool autoCopy = false;
        tcpClient client = new tcpClient();
        int pcID;
        Stopwatch refreshTime = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            refreshTime.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String curID = Properties.Settings.Default.UniqueID;
            if (curID == "none")
            {
                Properties.Settings.Default.UniqueID = Guid.NewGuid().ToString();
                Properties.Settings.Default.Save();
            }

            client.start();
            Random r = new Random();
            int oldID = pcID;
            pcID = r.Next(10000, 99999);
            label1.Text = "ID: " + pcID;
            client.sendData("pc;" + pcID + ";" + curID);
            refreshTime.Restart();

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData("https://api.qrserver.com/v1/create-qr-code/?size=150x150&bgcolor=25-25-112&color=255-255-255&data=http://grizzyhack.gear.host/?id=" + pcID);
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            pbQR.Image = img;
        }

        private void tUpdate_Tick(object sender, EventArgs e)
        {
            if (client.recievedData.Count > 0)
            {
                if (client.recievedData.First<String>().Contains("image"))
                {
                    clsImage img = new clsImage(client.recievedData.First<String>().Split(';')[2]);
                    ucImage newImageControl = new ucImage(img);
                    flowLayoutPanel1.Controls.Add(newImageControl);

                    frmNotify notification = new frmNotify(img.img);
                    notification.TopMost = true;
                    notification.Show();

                    if(autoCopy == true)
                    {
                        newImageControl.pbCopy_Click(null, null);
                    }

                    if (autoSave == true)
                    {
                        img.img.Save(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + ".png", ImageFormat.Png);
                    }

                }
                client.recievedData.Clear();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int oldID = pcID;
            pcID = r.Next(10000, 99999);
            label1.Text = "ID: " + pcID;
            client.sendData("pcupdate;" + oldID + ";" + pcID);
        }

        int rTime = 30000;

        private void tDraw_Tick(object sender, EventArgs e)
        {
            pAutoClipboard.Width = this.Width / 3;
            pAutoSave.Width = this.Width / 3;

            int maxSize = this.Width / 2;
            int curWidth = (int)(((double)refreshTime.ElapsedMilliseconds) / rTime * maxSize);

            pLoading.Width = this.Width - (curWidth * 2);
            pLoading.Location = new Point(this.Width / 2 - pLoading.Width / 2, pLoading.Location.Y);

            if(refreshTime.ElapsedMilliseconds > rTime)
            {
                Random r = new Random();
                int oldID = pcID;
                pcID = r.Next(10000, 99999);
                label1.Text = "ID: " + pcID;
                client.sendData("pcupdate;" + oldID + ";" + pcID);
                refreshTime.Restart();

                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData("https://api.qrserver.com/v1/create-qr-code/?size=150x150&bgcolor=25-25-112&color=255-255-255&data=http://grizzyhack.gear.host/?id=" + pcID);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                pbQR.Image = img;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(autoSave == false)
            {
                pictureBox2.Image = Properties.Resources.switchOn;
                autoSave = true;
            }
            else
            {
                pictureBox2.Image = Properties.Resources.switchOff;
                autoSave = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (autoCopy == false)
            {
                pictureBox1.Image = Properties.Resources.switchOn;
                autoCopy = true;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.switchOff;
                autoCopy = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.sendData("disconnect");
        }
    }
}
