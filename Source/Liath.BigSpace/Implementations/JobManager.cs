using Liath.BigSpace.DataAccess.Definitions.Jobs;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain.DataAccessDefinitions.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class JobManager : IJobManager
    {
        private IJobRepository _jobRepositories;        

        public JobManager(IJobRepository jobRepository)
        {
            if (jobRepository == null) throw new ArgumentNullException("jobRepository");
            _jobRepositories = jobRepository;
        }

        public void CompleteJobs()
        {
            var completedJobs = _jobRepositories.LoadExpiredButIncompleteJobs();
            foreach(var job in completedJobs)
            {
                job.Complete();
                _jobRepositories.MarkJobComplete(job);
            }
        }
    }
}
