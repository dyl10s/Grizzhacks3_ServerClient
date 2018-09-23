using System;
using System.Collections.Generic;
using System.Data;
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
        clsSqlConnector sql = new clsSqlConnector();

        TcpListener server;
        Boolean running = false;
        int port = 7865;

        public String log = "";

        public void start()
        {
            running = true;

            sql.Setup("den1.mssql3.gear.host", "grizzhacks", "grizzhacks", "Vx1iq98X~4~m");
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
            try
            {
                Stream s = c.GetStream();
                StreamWriter sw = new StreamWriter(s);
                sw.WriteLine(data);
                sw.Flush();
            }
            catch { }
        }

        private void recieveData(object c)
        {
            TcpClient client = c as TcpClient;

            using(StreamReader sr = new StreamReader(client.GetStream()))
            {
                while (running)
                {
                    if(client.Connected == false)
                    {
                        connectedComputers.RemoveAll(x => x.c == client);
                        break;
                    }


                    if (client.ReceiveBufferSize > 0)
                    {
                        try
                        {

                            string StringData = "";
                            StringData = sr.ReadToEnd();

                            if (StringData != "")
                            {
                                
                                if(StringData == "disconnect")
                                {
                                    links.RemoveAll(x => x.pcGUID == connectedComputers.Where<clsComputer>(y => y.c == client).First<clsComputer>().guid);
                                    connectedComputers.RemoveAll(x => x.c == client);
                                    client.Close();
                                    client.Dispose();
                                    break;
                                }

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
                                                        log += "SentData" + Environment.NewLine;
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
                                    connectedComputers.RemoveAll(x => x.guid == StringData.Split(';')[2]);
                                    clsComputer newPc = new clsComputer(client, Int32.Parse(StringData.Split(';')[1]), StringData.Split(';')[2]);
                                    foreach(DataRow r in sql.GetDataTable("select * from links where pcID='" + StringData.Split(';')[2] + "'").Rows)
                                    {
                                        newPc.pastIDs.Add(Int32.Parse(r[2].ToString()));
                                        links.Add(new clsLink(Int32.Parse(r[2].ToString()), Int32.Parse(r[3].ToString()), r[1].ToString()));
                                    }
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
                                    clsComputer connectedPC = null;

                                    foreach (clsComputer pc in tempList)
                                    {
                                        if(pc.curID == Int32.Parse(StringData.Split(';')[1]))
                                        {
                                            foundConnection = true;
                                            pc.linkedID = true;
                                            connectedComputers.Add(pc);
                                            connectedPC = pc;
                                            break;
                                        }
                                    }
                                    
                                    if(foundConnection == false)
                                    {
                                        sendData(client, "FAIL");
                                    }
                                    else
                                    {
                                        sql.Execute(String.Format("Insert into links(pcID, pcNum, phoneNum) VALUES ('{0}','{1}','{2}')", connectedPC.guid, Int32.Parse(StringData.Split(';')[1]), Int32.Parse(StringData.Split(';')[2])));
                                        log += String.Format("Insert into links(pcID, pcNum, phoneNum) VALUES ('{0}','{1}','{2}')", connectedPC.guid, Int32.Parse(StringData.Split(';')[1]), Int32.Parse(StringData.Split(';')[2]));
                                        links.Add(new clsLink(Int32.Parse(StringData.Split(';')[1]), Int32.Parse(StringData.Split(';')[2]), connectedPC.guid));
                                        sendData(client, "GOOD");
                                    }

                                }

                            }

                            sr.DiscardBufferedData();

                        }
                        catch(Exception e)
                        {
                            //if(client.Connected == false)
                            //{

                            //    foreach (clsComputer pc in connectedComputers)
                            //    {
                            //        if (pc.c == client)
                            //        {
                            //            pc.curID = -1;
                            //        }
                            //    }
                            //log += "ERROR: " + e.Message + " INNER " + e.InnerException.ToString();
                            //    connectedClients.Remove(client);
                            //    client.Dispose();

                            //}

                        }
                    }

                }
            }
            
        }
    }
}
