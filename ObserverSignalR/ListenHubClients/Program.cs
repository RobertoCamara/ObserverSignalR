using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ListenHubClients
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

#if (!DEBUG)
            try
            {
                ServiceBase.Run(new ListenHubClientA());
                ServiceBase.Run(new ListenHubClientB());
            }
            catch (Exception ex)
            {
                Util.Logger.LogController.Instance.GravarLogErro(ex);
                throw;
            }
#else
            var serviceA = new ListenHubClientA();
            var serviceB = new ListenHubClientB();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif

      
        }
    }
}
