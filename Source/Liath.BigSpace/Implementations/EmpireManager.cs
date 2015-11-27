using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Exceptions;
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
        private ISecurityManager _securityManager;
        public EmpireManager(IEmpires empireRepository, ISecurityManager securityManager)
        {
            if (empireRepository == null) throw new ArgumentNullException("empireRepository");
            if (securityManager == null) throw new ArgumentNullException("securityManager");

            _empireRepository = empireRepository;
            _securityManager = securityManager;
        }

        public Empire CreateEmpire(string name)
        {
            return _empireRepository.CreateEmpire(name);
        }


        public Empire GetMyEmpire()
        {
            var me = _securityManager.GetCurrentUserAccount();
            if (me == null) throw new CurrentUserNotFoundException();

            var empire = _empireRepository.GetEmpire(me.EmpireID);
            if (empire == null) throw new EntityNotFoundException<Empire>(me.EmpireID);

            return empire;
        }
    }
}
