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

namespace ObserverHubClientB
{
    partial class ObserverHubClientBService : ServiceBase
    {
        private static SingletonSignalrConnection _instanceSignalR;
        private const string HUB_NAME = "GatewayCommunicationHub";

        static ObserverHubClientBService()
        {
            _instanceSignalR = SingletonSignalrConnection.GetInstance;
        }

        public ObserverHubClientBService()
        {
            InitializeComponent();
#if (DEBUG)
            OnStart(null);
#endif
        }

        protected override void OnStart(string[] args)
        {
            _instanceSignalR.StartHubConnection(HUB_NAME);
            _instanceSignalR.Invoke<string>(HUB_NAME, "JoinGroup", "other");
            LogController.Instance.GravarLogInformacao("****OBSERVER HUB GATEWAY - CLIENTE B*****");
            _instanceSignalR.On<string>(HUB_NAME, "SendMessageOther", m => LogController.Instance.GravarLogInformacao($"METHOD: SendMessageOther - OBSERVER SIGNALR CLIENTE B - MSG:{m}"));
        }

        protected override void OnStop()
        {
            _instanceSignalR.DestroyHubConnection();
        }
    }
}
