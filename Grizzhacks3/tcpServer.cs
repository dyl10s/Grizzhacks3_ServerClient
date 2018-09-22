using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grizzhacks3
{
    class tcpServer
    {
        List<TcpClient> connectedClients = new List<TcpClient>();
        TcpListener server;
        Boolean running = false;
        int port = 7865;

        public String log = "";

        public void start()
        {
            running = true;
            server = new TcpListener(IPAddress.Any, port);

            server.Start();

            Thread acceptThread = new Thread(acceptClients);
            acceptThread.Start();

        }

        private void acceptClients()
        {
            while (running)
            {
                TcpClient newClient = server.AcceptTcpClient();
                connectedClients.Add(newClient);

                Thread recieveThread = new Thread(recieveData);
                recieveThread.Start(newClient);
            }
        }

        public void sendDataToAll()
        {
            foreach(TcpClient c in connectedClients)
            {
                Stream s = c.GetStream();
                StreamWriter sw = new StreamWriter(s);
                sw.WriteLine("TestData");
                sw.Flush();
            }
        }

        public void sendData(TcpClient c)
        {
                Stream s = c.GetStream();
                StreamWriter sw = new StreamWriter(s);
                sw.WriteLine("TestData");
                sw.Flush();
        }

        private void recieveData(object c)
        {
            TcpClient client = c as TcpClient;

            using(StreamReader sr = new StreamReader(client.GetStream()))
            {
                while (running)
                {

                    if (client.ReceiveBufferSize > 0)
                    {
                        try
                        {
                            Byte[] data = new byte[client.ReceiveBufferSize];

                            string StringData = sr.ReadLine();

                            if (StringData != "")
                            {
                                log += StringData + Environment.NewLine;
                                sendData(client);
                            }

                        }
                        catch
                        {

                            Console.WriteLine("ERROR");

                        }
                    }

                }
            }
            
        }
    }
}
