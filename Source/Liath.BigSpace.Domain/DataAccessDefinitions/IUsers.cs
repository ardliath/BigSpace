using Liath.BigSpace.Domain;
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
    }
}
