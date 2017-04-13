using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;
using Util.Logger;

[assembly: OwinStartup(typeof(GatewayCommunication.Startup))]
namespace GatewayCommunication
{

    partial class CommunicationService : ServiceBase
    {
        private Timer _timer;

        public CommunicationService()
        {
            try
            {
                LogController.Instance.GravarLogInformacao("Starting...");
                InitializeComponent();
                AutoLog = false;
#if (DEBUG)
                InitializeSelfHosting();
#endif
            }
            catch (Exception e)
            {
                LogController.Instance.GravarLogErro(e);
                throw;
            }

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _timer = new Timer();
                InitializeSelfHosting();
                LogController.Instance.GravarLogInformacao("Gateway Communication Start");
            }
            catch (Exception e)
            {
                LogController.Instance.GravarLogErro(e);
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
            LogController.Instance.GravarLogInformacao("Gateway Communication Stop");
        }

        private void InitializeSelfHosting()
        {
            try
            {
                string url = Convert.ToString(ConfigurationManager.AppSettings["uriService"]);
                WebApp.Start(url);
            }
            catch (Exception e)
            {
                LogController.Instance.GravarLogErro(e);
                throw;
            }
        }
    }
}
