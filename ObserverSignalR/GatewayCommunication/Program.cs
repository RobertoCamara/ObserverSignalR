namespace GatewayCommunication
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
                ServiceBase.Run(new CommunicationService());
            }
	        catch (Exception ex)
	        {
                Util.Logger.LogController.Instance.GravarLogErro(ex);
                throw;
            }
#else
            var service = new CommunicationService();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }
    }
}
