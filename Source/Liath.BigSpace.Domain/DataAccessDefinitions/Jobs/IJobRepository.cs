using Liath.BigSpace.Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions.Jobs
{
    public interface IJobRepository
    {
        IEnumerable<Job> LoadCompletedJobs();
    }
}
