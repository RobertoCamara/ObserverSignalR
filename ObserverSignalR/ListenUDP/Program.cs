namespace ListenUDP
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
                ServiceBase.Run(new ListenUDPService());
            }
            catch (Exception ex)
            {
                Util.Logger.LogController.Instance.GravarLogErro(ex);
                throw;
            }
#else
            var service = new ListenUDPService();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }
    }
}
