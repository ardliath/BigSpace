using Liath.BigSpace.Domain;
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
    }
}
