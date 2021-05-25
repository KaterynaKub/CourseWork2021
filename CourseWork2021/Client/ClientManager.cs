using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class ClientManager
    {
        private readonly int port = 8005; 
        private readonly string address = "127.0.0.1"; 
        private readonly IPEndPoint ipPoint;
        private Socket socket;

        public ClientManager()
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        }

        public void ClientRun()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint); 
                bool answer = true;
                while (answer)
                {
                    InfoEcxchange();
                    Console.WriteLine("\nЖелаешь еще почпокать? (Да - 1 /Нет - все остальное)");
                    string a = Console.ReadLine().Trim();
                    SendInfo(a);
                    answer = a != "0";
                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InfoEcxchange()
        {
            Console.WriteLine("Enter a word you want to find");
            string word = Console.ReadLine();
            SendInfo(word);
            List<string> fileList = JsonConvert.DeserializeObject<List<string>>(ReceiveData());
            foreach (var item in fileList)
            {
                Console.WriteLine(item);
            }
        }
        private string ReceiveData() 
        {
            byte[] size = new byte[sizeof(int)]; 
            socket.Receive(size);
            byte[] data = new byte[BitConverter.ToInt32(size)]; 
            socket.Receive(data);
            string result = Encoding.Unicode.GetString(data);
            return result;
        }
        private void SendInfo(string info)
        {
            byte[] data = Encoding.Unicode.GetBytes(info);
            socket.Send(BitConverter.GetBytes(data.Length).Concat(data).ToArray());
        }
    }
}
