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
    public partial class frmNotify : Form
    {
        public frmNotify(Image img)
        {
            InitializeComponent();
            pictureBox1.Image = img;
            this.Opacity = 50;
        }

        private void frmNotify_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tAnimate_Tick(object sender, EventArgs e)
        {
            //if(Opacity < 100)
            //{
            //    Opacity += 1;
            //}
            //else
            //{
            //    tAnimate.Enabled = false;
            //}
        }
    }
}
