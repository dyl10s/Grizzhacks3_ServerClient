using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grizzhacks3
{
    class clsLink
    {
        public string pcGUID;
        public int computerNumber;
        public int phoneNumber;

        public clsLink(int pcNum, int phoneNum, string guid)
        {
            computerNumber = pcNum;
            phoneNumber = phoneNum;
            pcGUID = guid;
        }

    }
}
