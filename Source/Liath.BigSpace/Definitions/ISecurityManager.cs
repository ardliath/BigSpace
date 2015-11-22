using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface ISecurityManager
    {
        UserAccount GetCurrentUserAccount();

        bool Login(string emailAddress, string password, out SecurityUserAccount currentUser);

        bool CreateUserAccount(string username, string emailAddress, string password, string confirmPassword, out UserAccount user, out string errors);
    }
}
