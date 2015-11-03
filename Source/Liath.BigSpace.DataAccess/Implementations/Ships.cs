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

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT ShipID, Name, SolarSystemID, UserAccountID, IsSelected FROM Ships WHERE ShipID = @ID"))
            {
                cmd.AddParameter("ID", DbType.Int64, solarSystemID);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ships.Add(new Ship
                        {
                            ShipID = dr.GetInt32("ShipID"),
                            Name = dr.GetString("Name"),
                            IsSelected = dr.GetBoolean("IsSelected")
                        });
                    }
                }
            }

            return ships;
        }


        public Ship GetShip(int shipID)
        {
            throw new NotImplementedException();
        }


        public void Save(Ship ship)
        {
            if (ship == null) throw new ArgumentNullException("ship");

            throw new NotImplementedException();
        }
    }
}
