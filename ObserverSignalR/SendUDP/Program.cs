using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Util.Logger;
using Util.ExtensionMethods;

namespace SendUDP
{
    class Program
    {
        private static UdpClient clienteUdp;

        static Program()
        {
            clienteUdp = new UdpClient("192.168.1.44", 4096);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("****INICIANDO ENVIO UDP*****");
            for (int i = 0; i < 100000; i++)
            {
                SendMessage(Guid.NewGuid().ToString());
                Thread.Sleep(2000);
            }
        }


        static void SendMessage(string message)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            clienteUdp.BeginSend(sendBytes, sendBytes.Length, SendCallback, clienteUdp);
        }

        public static void SendCallback(IAsyncResult ar)
        {
            UdpClient u = (UdpClient)ar.AsyncState;
            Console.WriteLine(DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss") + $" - MENSAGEM ENVIADA - Bytes enviados: {u?.EndSend(ar)}");
        }
    }
}
