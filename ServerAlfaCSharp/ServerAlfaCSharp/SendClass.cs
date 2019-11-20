using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerAlfaCSharp
{
    class SendClass
    {
        static public void SendMessageToTheClient(byte[] sendByte)
        {
            string IP = "192.168.43.32";
            int port = 8081;
            IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(IP), port);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            System.Threading.Thread.Sleep(100);
            server.SendTo(sendByte, clientAddress);
        }

    }
}
