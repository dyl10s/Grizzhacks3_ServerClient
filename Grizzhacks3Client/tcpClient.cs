using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grizzhacks3Client
{
    class tcpClient
    {
        TcpClient client = new TcpClient();
        //35.237.232.103
        String ip = "35.237.232.103";
        public List<string> recievedData = new List<string>();
        Boolean running = true;

        int port = 7865;

        public void start()
        {
            client.Connect(ip, port);

            Thread recieveThread = new Thread(recievePackets);
            recieveThread.Start();
        }

        public void sendData(String data)
        {

            StreamWriter sw = new StreamWriter(client.GetStream());
            sw.WriteLine(data);
            sw.FlushAsync();

        }

        public void recievePackets()
        {
            using (StreamReader sr = new StreamReader(client.GetStream()))
            {
                while (running)
                {
                    String data = sr.ReadLine();
                    if(data != "" && data != null)
                    {
                        if (!(recievedData.Contains(data)))
                        {
                            recievedData.Add(data);
                            sr.DiscardBufferedData();
                        }
                    }              
                }

            }
        }

    }
}
