using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackingClient
{
    class Program
    {
        private const String Server = "172.28.172.2"; //"localhost";
        private const Int32 Port = 13000;
        private static string passwords = "Empty";
        static void Main(string[] args)
        {
            SingleRequest();
        }

        private static void SingleRequest()
        {
            Console.WriteLine("PasswordRequest started...");
            // https://msdn.microsoft.com/en-us/library/system.net.sockets.tcpclient.aspx
            using (TcpClient client = new TcpClient(Server, Port))
            {
                Console.WriteLine("Found the connection!!!");
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                string request = "GET";
                writer.WriteLine(request);
                writer.Flush();
                StreamReader reader = new StreamReader(stream);
                String response = reader.ReadLine();
                
                Cracking cracker = new Cracking();
                passwords = cracker.RunCracking(response);
                if (passwords != "Empty")
                {
                    ReturnResult();
                }

            }

            
        }

        private static void ReturnResult()
        {
            using (TcpClient client = new TcpClient(Server, Port))
            {
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(passwords);
                writer.Flush();
                
            }
        }
    }
}
