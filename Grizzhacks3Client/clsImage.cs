using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grizzhacks3Client
{
    public class clsImage
    {
        public Image img;

        public clsImage(String base64Code)
        {
            try
            {
                img = (Bitmap)((new ImageConverter()).ConvertFrom(Convert.FromBase64String(base64Code)));
            }
            catch {
                img = Properties.Resources.eoor;
            }
                
        }

    }
}
