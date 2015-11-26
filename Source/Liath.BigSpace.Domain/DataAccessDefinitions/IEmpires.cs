using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
    public interface IEmpires
    {
        Empire CreateEmpire(string name);
    }
}
