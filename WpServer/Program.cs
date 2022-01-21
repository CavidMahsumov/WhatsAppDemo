using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WpServer.Helper;

namespace WpServer
{
    class Program
    {
        static void Main(string[] args)
        {

            int count = 1;


            TcpListener ServerSocket = new TcpListener(IPAddress.Parse(ConnectHelper.IpAdress), ConnectHelper.Port);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();
                ConnectHelper.list_clients.Add(count, client);
      
                Console.WriteLine("Someone connected!!");
                count++;
                Box box = new Box(client, ConnectHelper.list_clients);

                Thread t = new Thread(handle_clients);
                t.Start(box);
            }

        }

        public static void handle_clients(object o)
        {
            Box box = (Box)o;
            Dictionary<int, TcpClient> list_connections = box.list;

            while (true)
            {
                NetworkStream stream = box.c.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);
                byte[] formated = new Byte[byte_count];
                //handle  the null characteres in the byte array
                Array.Copy(buffer, formated, byte_count);
                string data = Encoding.ASCII.GetString(formated);
                broadcast(list_connections, data);

            }
        }

        public static void broadcast(Dictionary<int, TcpClient> conexoes, string data)
        {
            ConnectHelper.list_clients = conexoes;

            foreach (TcpClient c in conexoes.Values)
            {
                NetworkStream stream = c.GetStream();

                byte[] buffer = Encoding.ASCII.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
    class Box
    {
        public TcpClient c;
        public Dictionary<int, TcpClient> list;

        public Box(TcpClient c, Dictionary<int, TcpClient> list)
        {
            this.c = c;
            this.list = list;
        }

    }
}
