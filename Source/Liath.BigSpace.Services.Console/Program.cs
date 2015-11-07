using Liath.BigSpace.Configuration;
using Liath.BigSpace.DataAccess.Definitions.Jobs;
using Liath.BigSpace.DataAccess.Implementations;
using Liath.BigSpace.Implementations;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Liath.BigSpace.Services.Console
{
    class Program
    {
        static void Main(string[] args)
        {            

            // Uncomment this to run app in the background
            //var timer = new Timer(5000);
            //timer.Elapsed += timer_Elapsed;
            //timer.Start();

            // Run once
            UpdateJobs();

            System.Console.WriteLine("Press any key to end...");
            System.Console.ReadKey();
        }

        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateJobs();
        }

        private static void UpdateJobs()
        {
            var sessionManager = new SessionManager(new ConfigurationManager());
            using (var uow = sessionManager.CreateUnitOfWork())
            {
                var jobManager = new JobManager(new JobRepository(sessionManager, new SolarSystems(sessionManager)));
                jobManager.CompleteJobs();
            }
        }
    }
}
