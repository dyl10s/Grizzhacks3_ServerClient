using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Grizzhacks3
{
    class clsComputer
    {
        public TcpClient c;
        public int curID;
        public bool linkedID = false;
        public List<int> pastIDs = new List<int>();

        public clsComputer(TcpClient client, int id)
        {
            c = client;
            curID = id;
        }

        public void changeID(int id)
        {
            if(linkedID == true)
            {
                pastIDs.Add(curID);
            }
            
            curID = id;
        }

    }
}
