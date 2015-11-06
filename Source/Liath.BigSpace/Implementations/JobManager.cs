using Liath.BigSpace.DataAccess.Definitions.Jobs;
using Liath.BigSpace.Definitions;
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
    }
}
