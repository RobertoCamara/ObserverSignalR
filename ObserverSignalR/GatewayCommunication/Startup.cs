using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using System;
using Util.Logger;

namespace GatewayCommunication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                app.Map("/signalr", map =>
                {
                    map.UseCors(CorsOptions.AllowAll);
                    var hubConfiguration = new HubConfiguration()
                    {
                        EnableDetailedErrors = true,
                        EnableJSONP = true
                    };

                    map.RunSignalR(hubConfiguration);
                });
            }
            catch (Exception e)
            {
                LogController.Instance.GravarLogErro(e);
                throw;
            }
        }
    }
}
