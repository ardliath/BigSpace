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


        public Empire GetEmpire(int id)
        {
            using(var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT EmpireID, Name From Empires WHERE EmpireID = @ID"))
            {
                cmd.AddParameter("ID", System.Data.DbType.Int32, id);
                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        return new Empire
                        {
                            EmpireID = dr.GetInt32("EmpireID"),
                            Name = dr.GetString("Name")
                        };
                    }

                    return null;
                }
            }
        }
    }
}
