using System.ServiceProcess;
using Util.Logger;
using Util.Singleton;

namespace ListenHubClients
{
    partial class ListenHubClientA : ServiceBase
    {
        private static SingletonSignalrConnection _instanceSignalR;
        private const string HUB_NAME = "GatewayCommunicationHub";

        static ListenHubClientA()
        {
            _instanceSignalR = SingletonSignalrConnection.GetInstance;
        }

        public ListenHubClientA()
        {
            InitializeComponent();
#if(DEBUG)
            OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            _instanceSignalR.StartHubConnection(HUB_NAME);
            _instanceSignalR.Invoke<string>(HUB_NAME, "JoinGroup", "udp");
            LogController.Instance.GravarLogInformacao("****OUVINTE HUB GATEWAY - CLIENTE A*****");
            _instanceSignalR.On<string>(HUB_NAME, "SendMessage", m => LogController.Instance.GravarLogInformacao($"OUVINTE SIGNALR CLIENTE A - MSG:{m}"));
        }

        protected override void OnStop()
        {
            _instanceSignalR.DestroyHubConnection();
        }
    }
}
