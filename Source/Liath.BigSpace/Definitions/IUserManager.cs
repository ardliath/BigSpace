using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface IUserManager
    {
        void UpdateFocusedCoordinates(int diffX, int diffY, int diffZ);
    }
}
