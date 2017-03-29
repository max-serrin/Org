using System;
using Open.P2P;
using Open.P2P.Listeners;
using Open.P2P.IO;
using Open.P2P.Streams;
using System.IO;

namespace OrgNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] buf = new char[1024];
            Console.WriteLine("Hello World!");
            TcpListener listener = new TcpListener(80);
            //UdpListener listener = new UdpListener(80);
            CommunicationManager com = new CommunicationManager(listener);
            com.PeerConnected += (s, e) =>
            {
                using (var sr = new StreamReader(e.Peer.Stream))
                {
                    using (var sw = new StreamWriter(e.Peer.Stream))
                    {
                        var read = sr.Read(buf, 0, buf.Length);
                        while (read > 0)
                        {
                            sw.Write(buf, 0, read);
                            read = sr.Read(buf, 0, buf.Length);
                        }
                    }
                }
            }
        }
    }
}