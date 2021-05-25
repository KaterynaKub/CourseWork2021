using CourseWork2021;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerManager
    {
        private IndexerService indexerService;
        private readonly Socket socket;

        public ServerManager(IndexerService service, Socket newSocket)
        {
            indexerService = service;
            socket = newSocket;
        }
        public Task HandleAsync()
        {
            return Task.Run(TalkToUser);
        }
        public void TalkToUser()
        {
            bool status = true;
            while (status)
            {
                Console.WriteLine("wait");
                InfoExchange();
                string continues = ReceiveData();
                if(continues == "0")
                {
                    Console.WriteLine("goodbye!");
                    status = false;
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void InfoExchange()
        {
            string answer = ReceiveData();
            List<string> files = indexerService.GetFilesByWord(answer);
            if (files.Count == 0)
            {
                SendData($"There is no files including {answer} word");
            }
            else
            {
                SendData(JsonConvert.SerializeObject(files));
            }
        }

        private void SendData(string strData)
        {
            byte[] data = Encoding.Unicode.GetBytes(strData);  
            socket.Send(BitConverter.GetBytes(data.Length) 
                                            .Concat(data)
                                            .ToArray());
        }

        string ReceiveData() 
        {
            byte[] size = new byte[sizeof(int)]; 
            socket.Receive(size);
            byte[] data = new byte[BitConverter.ToInt32(size)]; 
            socket.Receive(data);
            string result = Encoding.Unicode.GetString(data); 
            return result;
        }
    }
}
