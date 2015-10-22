using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class RelativeSolarSystem : SolarSystem
    {
        /// <summary>
        /// The zero based coordinates from where the point should be drawn. 0,0 is top left and 7, 10 would be the 8th square
        /// in from the left and 11 squares down from the top
        /// </summary>
        public ScreenOffSet ScreenOffset { get; set; }
    }
}
