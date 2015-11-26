using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface IRegistrationManager
    {
        bool RegisterUser(string username, string emailAddress, string password, string confirmPassword, out UserAccount user, out string error);
    }
}
