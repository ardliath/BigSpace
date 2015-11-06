using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class JobRepository : IJobRepository
    {
        IEnumerable<IJobChildRepository> _childJobRepositories;

        public JobRepository(ISolarSystems solarSystems)
        {
            _childJobRepositories = new IJobChildRepository[] {
                //new BuildShipRepository(),
                new JourneyRepository(solarSystems)
            };
        }

        public IEnumerable<Job> LoadJobs()
        {
            throw new NotImplementedException();
        }
    }    
}
