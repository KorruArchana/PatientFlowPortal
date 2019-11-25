using System.ServiceProcess;
namespace EMIS.PatientFlow.SyncService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            SyncService srv = new SyncService();
            srv.OnDebug();

            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else
            var servicesToRun = new ServiceBase[] 
            { 
                new SyncService() 
            };
            ServiceBase.Run(servicesToRun);
#endif
        }
    }
}
