using Liath.BigSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.Jobs
{
    public class BuildShip : Job
    {
        public SolarSystem SolarSystem { get; set; }

        public override void Complete()
        {
            throw new NotImplementedException();
        }
    }    
}
