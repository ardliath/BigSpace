using Liath.BigSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface IEmpireManager
    {
        Empire CreateEmpire(string name);

        Empire GetMyEmpire();
    }
}
