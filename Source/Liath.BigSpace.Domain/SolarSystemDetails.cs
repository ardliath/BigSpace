using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class SolarSystemDetails : SolarSystem
    {
        public IEnumerable<Ship> Ships { get; set; }
    }
}
