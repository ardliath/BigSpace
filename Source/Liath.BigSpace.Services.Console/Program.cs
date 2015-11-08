using Liath.BigSpace.Configuration;
using Liath.BigSpace.DataAccess.Definitions.Jobs;
using Liath.BigSpace.DataAccess.Implementations;
using Liath.BigSpace.Implementations;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (Debugger.IsAttached)
            {
                UpdateJobs();
            }
            else
            {
                var timer = new Timer(5000);
                timer.Elapsed += timer_Elapsed;
                timer.Start();
            }

            System.Console.WriteLine("Press any key to end...");
            System.Console.ReadKey();
        }

        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Console.WriteLine("Running Jobs...");
            UpdateJobs();
        }

        private static void UpdateJobs()
        {
            var sessionManager = new SessionManager(new ConfigurationManager());
            using (var uow = sessionManager.CreateUnitOfWork())
            {
                var jobManager = new JobManager(new JobRepository(sessionManager,
                    new Ships(sessionManager)));
                jobManager.CompleteJobs();
            }
        }
    }
}
