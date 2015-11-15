using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Definitions
{
    public interface ICryptographyManager
    {
        Byte[] CreateHash(DateTime created, string password, string salt);
        string CreateSalt();
    }
}
