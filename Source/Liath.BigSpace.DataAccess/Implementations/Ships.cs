using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.UserAccountDomain;

namespace Liath.BigSpace.DataAccess.Implementations
{
    public class Ships : DataAccessBase, IShips
    {
        public Ships(ISessionManager sessionManager)
            : base(sessionManager)
		{
		}

        public IEnumerable<Ship> ListShipsAtSolarSystem(long solarSystemID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("SolarSystemID = @ID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("ID", DbType.Int64, solarSystemID);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public Ship GetShip(int shipID)
        {
            var query = this.CreateSelectQuery("ShipID = @ID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("ID", DbType.Int32, shipID);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return this.InflateShip(dr);
                    }

                    return null;
                }
            }
        }

        private Ship InflateShip(IDataReader dr)
        {
            return new Ship
            {
                ShipID = dr.GetInt32("ShipID"),
                UserAccountID = dr.GetInt32("UserAccountID"),
                Name = dr.GetString("Name"),
                IsSelected = dr.GetBoolean("IsSelected"),
                SolarSystemID = dr.GetNullableInt64("SolarSystemID"),
                JobID = dr.GetNullableInt64("JobID")
            };
        }

        public static string[] RequiredFields = new string[] { "Name", "SolarSystemID", "UserAccountID", "IsSelected", "JobID", "EmpireID" };

        private string CreateSelectQuery(string filter = null)
        {
            var sb = new StringBuilder("SELECT ShipID, ");
            sb.Append(string.Join(", ", RequiredFields));
            sb.Append(" FROM Ships");
            if(filter != null)
            {
                sb.AppendFormat(" WHERE {0}", filter);
            }

            return sb.ToString();
        }

        public void Save(Ship ship)
        {
            if (ship == null) throw new ArgumentNullException("ship");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET Name = @Name, IsSelected = @IsSelected WHERE ShipID = @ID"))
            {
                cmd.AddParameter("ID", DbType.Int32, ship.ShipID);
                cmd.AddParameter("Name", DbType.String, ship.Name);
                cmd.AddParameter("IsSelected", DbType.Boolean, ship.IsSelected);

                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Ship> ListShipsDoingJob(long jobID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("JobID = @JobID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("JobID", DbType.Int64, jobID);

                using(var dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public void SetShipLocation(int shipID, Int64? solarSystemID)
        {
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET SolarSystemID = @SolarSystemID WHERE ShipID = @ShipID"))
            {
                cmd.AddParameter("ShipID", DbType.Int32, shipID);
                cmd.AddParameter("SolarSystemID", DbType.Int64, (object)solarSystemID ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public void SetShipJob(int shipID, Int64? jobID)
        {
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET JobID = @JobID WHERE ShipID = @ShipID"))
            {
                cmd.AddParameter("ShipID", DbType.Int32, shipID);
                cmd.AddParameter("JobID", DbType.Int64, (object)jobID ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Ship> ListSelectedShips(int userAccountID)
        {
            var ships = new List<Ship>();
            var query = this.CreateSelectQuery("IsSelected = 1 and UserAccountID = @UserAccountID");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("UserAccountID", DbType.Int32, userAccountID);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ships.Add(this.InflateShip(dr));
                    }
                }
            }

            return ships;
        }


        public Ship CreateShip(SolarSystem located, UserAccount owner, string name)
        {
            int id;
            var query = string.Concat("INSERT INTO Ships (", string.Join(", ", RequiredFields), ") VALUES (", string.Join(", ", RequiredFields.Select(f => string.Concat("@", f))), ") SELECT Scope_Identity()");
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("UserAccountID", DbType.Int32, owner.UserAccountID);
                cmd.AddParameter("Name", DbType.String, name);
                cmd.AddParameter("SolarSystemID", DbType.Int64, located.SolarSystemID);
                cmd.AddParameter("IsSelected", DbType.Boolean, 0);
                cmd.AddParameter("JobID", DbType.Int64, DBNull.Value);
                cmd.AddParameter("EmpireID", DbType.Int32, owner.EmpireID);

                id = (int)(decimal)cmd.ExecuteScalar();                
            }

            return this.GetShip(id);
        }
    }
}
