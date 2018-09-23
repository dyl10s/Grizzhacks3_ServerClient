using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Grizzhacks3Client
{
    public partial class ucImage : UserControl
    {
        clsImage loadedImage;
        public static Image lastImage;

        public ucImage(clsImage img)
        {
            InitializeComponent();
            loadedImage = img;
            pbImage.Image = img.img;
            pbCopy.Width = panel1.Width / 2;
            pbImage.Width = panel1.Width / 2;
        }

        public void playAnimation()
        {
            
        }

        public void pbCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pbImage.Image);
        }

        public void pbSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Image.png";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
        }

        //128, 128, 255 Original Color
        private void pbCopy_MouseEnter(object sender, EventArgs e)
        {
            pbCopy.BackColor = Color.Blue;
        }

        private void pbCopy_MouseLeave(object sender, EventArgs e)
        {
            pbCopy.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void pbSave_MouseEnter(object sender, EventArgs e)
        {
            pbSave.BackColor = Color.Blue;
        }

        private void pbSave_MouseLeave(object sender, EventArgs e)
        {
            pbSave.BackColor = Color.FromArgb(128, 128, 255);
        }
    }
}
