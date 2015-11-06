using Liath.BigSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class Journey : Job
    {
        public SolarSystem From { get; set; }
        public SolarSystem To { get; set; }
    }
}
