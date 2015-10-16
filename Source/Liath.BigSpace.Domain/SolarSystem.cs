using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class SolarSystem
    {
        public Int64 SolarSystemID { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Name { get; set; }
    }
}
