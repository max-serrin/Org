using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace OrgNetwork
{
    class OrgSocket
    {
        delegate void AddMessage(string message);
        const int port = 80;
        const string broadcastAddress = "127.0.0.1";
        UdpClient receivingClient;
        UdpClient sendingClient;
        Thread receivingThread;
        


    }
}
