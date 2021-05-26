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
            Print("New client accepted", ConsoleColor.DarkYellow);
            bool status = true;
            while (status)
            {
                Print($"wait for client answer", ConsoleColor.Green);
                InfoExchange();
                string continues = ReceiveData();
                if(continues == "0")
                {
                    Print("goodbye!", ConsoleColor.DarkBlue);
                    status = false;
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        private void InfoExchange()
        {
            string[] answer = JsonConvert.DeserializeObject<string[]>(ReceiveData());
            IEnumerable<string> files;
            if (answer.Length > 1)
            {
                files = indexerService.GetFilesByWords(answer);
            }
            else
            {
                files = indexerService.GetFilesByWord(answer.First());
            }
            SendData(JsonConvert.SerializeObject(files));
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

        private void Print(string toPrint, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(toPrint);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
