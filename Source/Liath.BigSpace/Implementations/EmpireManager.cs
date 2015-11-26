using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class EmpireManager : IEmpireManager
    {
        private IEmpires _empireRepository;
        public EmpireManager(IEmpires empireRepository)
        {
            if (empireRepository == null) throw new ArgumentNullException("empireRepository");
            _empireRepository = empireRepository;
        }

        public Empire CreateEmpire(string name)
        {
            return _empireRepository.CreateEmpire(name);
        }
    }
}
