using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.SolarSystems
{
    public class SolarSystemPlanetDetails : SolarSystem
    {
        public IEnumerable<Planet> Planets { get; set; }
    }
}
