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
        List<clsComputer> connectedComputers = new List<clsComputer>();

        public List<clsLink> links = new List<clsLink>();

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

        public void sendData(TcpClient c, String data)
        {
                Stream s = c.GetStream();
                StreamWriter sw = new StreamWriter(s);
                sw.WriteLine(data);
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
                                
                                if(StringData.Split(';')[0] == "pcupdate")
                                {
                                    foreach(clsComputer pc in connectedComputers)
                                    {
                                        if(pc.curID == Int32.Parse(StringData.Split(';')[1]))
                                        {
                                            pc.changeID(Int32.Parse(StringData.Split(';')[2]));
                                        }
                                    }
                                }

                                if(StringData.Split(';')[0] == "image")
                                {
                                    log += "New Image from:" + StringData.Split(';')[2] + " to:" + StringData.Split(';')[1] + Environment.NewLine;
                                    foreach(clsLink link in links)
                                    {
                                        if(link.computerNumber == Int32.Parse(StringData.Split(';')[1]) && link.phoneNumber == Int32.Parse(StringData.Split(';')[2]))
                                        {
                                            foreach(clsComputer pc in connectedComputers)
                                            {
                                                if (pc.curID == link.computerNumber)
                                                {
                                                    log += "SentData" + Environment.NewLine;
                                                    sendData(pc.c, "image;" + link.phoneNumber + ";" + StringData.Split(';')[3]);
                                                    break;
                                                }

                                                foreach(int id in pc.pastIDs)
                                                {
                                                    if (id == link.computerNumber)
                                                    {
                                                        //log += "SentData" + Environment.NewLine;
                                                        sendData(pc.c, "image;" + link.phoneNumber + ";" + StringData.Split(';')[3]);
                                                        break;
                                                    }
                                                }
                                                
                                            }
                                        }
                                    }
                                }

                                if (StringData.Split(';')[0] == "pc")
                                {
                                    clsComputer newPc = new clsComputer(client, Int32.Parse(StringData.Split(';')[1]));
                                    connectedComputers.Add(newPc);
                                }

                                if(StringData.Split(';')[0] == "oldphone")
                                {

                                    bool foundDevice = false;

                                    foreach (clsLink l in links)
                                    {
                                        if(l.computerNumber == Int32.Parse(StringData.Split(';')[1]))
                                        {
                                            if (l.phoneNumber == Int32.Parse(StringData.Split(';')[2]))
                                            {
                                                foundDevice = true;
                                                break;
                                            }

                                            if (foundDevice == true)
                                            {
                                                break;
                                            }
                                        }
                                    }

                                    if(foundDevice == true)
                                    {
                                        sendData(client, "GOOD");
                                    }
                                    else
                                    {
                                        sendData(client, "FAIL");
                                    }

                                }

                                if (StringData.Split(';')[0] == "newphone")
                                {
                                    clsComputer[] tempList = new clsComputer[connectedComputers.Count];
                                    connectedComputers.CopyTo(tempList);
                                    Boolean foundConnection = false;

                                    foreach (clsComputer pc in tempList)
                                    {
                                        if(pc.curID == Int32.Parse(StringData.Split(';')[1]))
                                        {
                                            foundConnection = true;
                                            pc.linkedID = true;
                                            //connectedComputers.Add(pc);
                                        }
                                    }
                                    
                                    if(foundConnection == false)
                                    {
                                        sendData(client, "FAIL");
                                    }
                                    else
                                    {
                                        links.Add(new clsLink(Int32.Parse(StringData.Split(';')[1]), Int32.Parse(StringData.Split(';')[2])));
                                        sendData(client, "GOOD");
                                    }

                                }

                            }

                            sr.DiscardBufferedData();

                        }
                        catch
                        {
                            foreach(clsComputer pc in connectedComputers)
                            {
                                if (pc.c == client)
                                {
                                    pc.curID = -1;
                                }
                            }

                            connectedClients.Remove(client);
                            client.Dispose();

                            break;

                        }
                    }

                }
            }
            
        }
    }
}
