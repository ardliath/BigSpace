using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.Session
{
    public interface IUnitOfWork : IDisposable
    {
        IDbCommand CreateCommand(string query);
    }
}
