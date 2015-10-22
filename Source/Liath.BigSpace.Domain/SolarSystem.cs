using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class SolarSystem
    {
        /// <summary>
        /// The unique ID of the solar system
        /// </summary>
        public Int64 SolarSystemID { get; set; }

        /// <summary>
        /// The coordinates of the solar system in the galaxy
        /// </summary>
        public Coordinates Coordinates { get; set; }

        /// <summary>
        /// The name of the solar system
        /// </summary>
        public string Name { get; set; }
    }
}
