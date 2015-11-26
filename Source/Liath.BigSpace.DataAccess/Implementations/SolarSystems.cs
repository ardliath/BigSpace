using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Session;
using Liath.BigSpace.Domain.DataAccessDefinitions;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class SolarSystems : DataAccessBase, ISolarSystems
	{
		public SolarSystems(ISessionManager sessionManager) : base(sessionManager)
		{
		}

		public IEnumerable<SolarSystem> FindSystemsInLocalArea(LocalAreaView localAreaView)
		{
			if (localAreaView == null) throw new ArgumentNullException("localAreaView");

			var solarSystems = new List<SolarSystem>();
			var minX = (localAreaView.FocusCoordinates.X - localAreaView.ScreenSize.Width - 1)/2;
			var maxX = (localAreaView.FocusCoordinates.X + localAreaView.ScreenSize.Width + 1)/2;
			var minY = (localAreaView.FocusCoordinates.Y - localAreaView.ScreenSize.Height - 1) / 2;
			var maxY = (localAreaView.FocusCoordinates.Y + localAreaView.ScreenSize.Height + 1) / 2;

			// This will need to change once we consider three dimensional space
			var query = this.CreateQuery("X BETWEEN @MinX AND @MaxX and Y BETWEEN @MinY AND @MaxY AND Z = @Z");

			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{				
				cmd.AddParameter("MinX", DbType.Int64, minX);
				cmd.AddParameter("MaxX", DbType.Int64, maxX);
				cmd.AddParameter("MinY", DbType.Int64, minY);
				cmd.AddParameter("MaxY", DbType.Int64, maxY);
                cmd.AddParameter("Z", DbType.Int64, 0);

				using (var dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						solarSystems.Add(this.InflateSolarSystem(dr));
					}
				}
			}

			return solarSystems;
		}

        private SolarSystem InflateSolarSystem(IDataReader dr)
        {
            return new SolarSystem
            {
                SolarSystemID = dr.GetInt64("SolarSystemID"),
                Name = dr.GetString("Name"),
                Coordinates = new Coordinates
                {
                    X = dr.GetInt64("X"),
                    Y = dr.GetInt64("Y"),
                    Z = dr.GetInt64("Z")
                }
            };
        }

        private string CreateQuery(string filter = null, int? top = null)
        {
            return string.Concat("SELECT ",
                top.HasValue ? string.Concat("TOP ", top.Value, " ") : null,
               "SolarSystemID, Name, X, Y, Z FROM SolarSystems",
                filter == null ? null : " WHERE ",
                filter);
        }


        public SolarSystem GetSolarSystem(Int64 id)
        {
            var query = this.CreateQuery("SolarSystemID = @ID");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("ID", DbType.Int64, id);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return this.InflateSolarSystem(dr);
                    }
                    return null;
                }
            }
        }


        public SolarSystem CreateSolarSystem(SolarSystem solarSystem)
        {
            if(solarSystem == null) throw new ArgumentNullException("solarSystem");

            var query = "INSERT INTO SolarSystems (Name, X, Y, Z) VALUES (@Name, @X, @Y, @Z) select scope_identity()";

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                cmd.AddParameter("Name", DbType.String, solarSystem.Name);
                cmd.AddParameter("X", DbType.Int64, solarSystem.Coordinates.X);
                cmd.AddParameter("Y", DbType.Int64, solarSystem.Coordinates.Y);
                cmd.AddParameter("Z", DbType.Int64, solarSystem.Coordinates.Z);

                solarSystem.SolarSystemID = (long)(decimal)cmd.ExecuteScalar();

                return solarSystem;
            }
        }


        public SolarSystem GetRandomUnoccupiedSolarSystem()
        {
            throw new NotImplementedException();
        }
    }
}
