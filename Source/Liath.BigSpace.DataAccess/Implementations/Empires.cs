using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;

namespace Liath.BigSpace.DataAccess.Implementations
{
    public class Empires : DataAccessBase, IEmpires
    {
        public Empires(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public Empire CreateEmpire(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            using(var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("INSERT INTO Empires (Name) VALUES (@Name) SELECT Scope_Identity()"))
            {
                cmd.AddParameter("Name", System.Data.DbType.String, name);

                return new Empire
                {
                    EmpireID = (int)(decimal)cmd.ExecuteScalar(),
                    Name = name
                };
            }
        }
    }
}
