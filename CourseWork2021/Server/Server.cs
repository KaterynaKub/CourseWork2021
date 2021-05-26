using CourseWork2021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private readonly int port = 8005; 
        private readonly IPEndPoint ipPoint;
        private readonly Socket socket = new (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Server()
        {
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        }
        public async Task RunServer()
        {
            IndexerService indexer = new();
            await indexer.RunThreadsAsync();
            Console.WriteLine("Index created");
            try
            {
                socket.Bind(ipPoint);

                socket.Listen(10); 

                Console.WriteLine("Server is running... Wait for clients");

                while (true)
                {
                    Socket handler = socket.Accept(); 
                    ServerManager manager = new(indexer,handler);
                    _ = manager.HandleAsync();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
