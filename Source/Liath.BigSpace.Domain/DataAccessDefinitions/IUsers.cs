using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
    public interface IUsers
    {
        UserAccount GetUserByUsername(string username);
        UserAccount Update(UserAccount userAccount);

        SecurityUserAccount GetUserAccount(string emailAddress);

        void CreateUserAccount(SecurityUserAccount securityUser, int empireID, Coordinates coordinates);
    }
}
