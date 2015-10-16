using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain;

namespace Liath.BigSpace.Definitions
{
    public interface INavigationManager
    {
        void FindLocalSystems(ScreenSize screenSize);
    }
}
