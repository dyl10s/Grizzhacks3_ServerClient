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

        public ucImage(clsImage img)
        {
            InitializeComponent();
            loadedImage = img;
            pbImage.Image = img.img;
        }

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(pbImage.Image);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "Image.png";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbImage.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
            }
        }
    }
}
