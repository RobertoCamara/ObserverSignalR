using Microsoft.AspNet.SignalR.Client;
using System;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using Util;
using Util.ExtensionMethods;
using Util.Logger;
using Util.Singleton;

namespace ListenUDP
{
    partial class ListenUDPService : ServiceBase
    {
        private const string HUB_NAME = "GatewayCommunicationHub";
        private static SingletonSignalrConnection _instanceSignalR;
        private static UdpClient clientUdp;

        static ListenUDPService()
        {
            _instanceSignalR = SingletonSignalrConnection.GetInstance;
            clientUdp = new UdpClient(4096);
        }

        public ListenUDPService()
        {
            InitializeComponent();
#if DEBUG
            OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            _instanceSignalR.StartHubConnection(HUB_NAME);
            LogController.Instance.GravarLogInformacao("****OUVINTE UDP E COMUNICAÇÃO COM GATEWAY*****");
            ReceiveMessage(HubInvokeSendMessage);
        }

        protected override void OnStop()
        {
            _instanceSignalR.DestroyHubConnection();
            LogController.Instance.GravarLogInformacao("****PARADO*****");
        }

        private static void HubInvokeSendMessage(string mensagem)
        {
            _instanceSignalR.Invoke<string>(HUB_NAME, "SendMessage", "udp", mensagem);
            _instanceSignalR.Invoke<string>(HUB_NAME, "SendMessageOther", "other", $"{DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss fff")} - ONLY OTHER GROUP");
        }

        private static async void ReceiveMessage(Action<string> onInvoke)
        {
            UdpReceiveResult result = await clientUdp.ReceiveAsync();

            string mensagemRecebida = result.Buffer.BytesReceivedToString();
            string mensagemTransmitida = $"{mensagemRecebida} + {System.Diagnostics.Stopwatch.GetTimestamp()}";

            onInvoke(mensagemTransmitida);
#if DEBUG
            LogController.Instance.GravarLogInformacao(DateTime.Now.ToString("dd-mm-yyyy HH:mm:ss fff") + $"\r\nMENSAGEM RECEBIDA: {mensagemRecebida} - bytes recebidos: {result.Buffer.Length}\r\nMENSAGEM TRANSMITIDA HUB: {mensagemTransmitida}\r\n");
#endif
            ReceiveMessage(onInvoke);
        }
    }
}
