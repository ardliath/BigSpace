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
                Name = dr.GetString("Name"),
                IsSelected = dr.GetBoolean("IsSelected")
            };
        }

        private string CreateSelectQuery(string filter = null)
        {
            var sb = new StringBuilder("SELECT ShipID, Name, SolarSystemID, UserAccountID, IsSelected FROM Ships ");
            if(filter != null)
            {
                sb.AppendFormat("WHERE {0}", filter);
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
            }
        }

        public void SetShipJob(int shipID, Int64? jobID)
        {
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE Ships SET JobID = @JobID WHERE ShipID = @ShipID"))
            {
                cmd.AddParameter("ShipID", DbType.Int32, shipID);
                cmd.AddParameter("JobID", DbType.Int64, (object)jobID ?? DBNull.Value);
            }
        }
    }
}
