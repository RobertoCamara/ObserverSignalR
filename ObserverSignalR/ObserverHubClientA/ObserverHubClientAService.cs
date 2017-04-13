using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Util.Logger;
using Util.Singleton;

namespace ObserverHubClientA
{
    partial class ObserverHubClientAService : ServiceBase
    {
        private static SingletonSignalrConnection _instanceSignalR;
        private const string HUB_NAME = "GatewayCommunicationHub";

        static ObserverHubClientAService()
        {
            _instanceSignalR = SingletonSignalrConnection.GetInstance;
        }

        public ObserverHubClientAService()
        {
            InitializeComponent();
#if (DEBUG)
            OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            _instanceSignalR.StartHubConnection(HUB_NAME);
            _instanceSignalR.Invoke<string>(HUB_NAME, "JoinGroup", "udp");
            LogController.Instance.GravarLogInformacao("****OBSERVER HUB GATEWAY - CLIENTE A*****");
            _instanceSignalR.On<string>(HUB_NAME, "SendMessage", m => LogController.Instance.GravarLogInformacao($"METHOD: SendMessage - OBSERVER SIGNALR CLIENTE A - MSG:{m}"));
        }

        protected override void OnStop()
        {
            _instanceSignalR.DestroyHubConnection();
        }
    }
}
