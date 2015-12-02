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
using Liath.BigSpace.Domain.SolarSystems;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class SolarSystems : DataAccessBase, ISolarSystems
	{
		private IPlanets _planets;

		public SolarSystems(ISessionManager sessionManager, IPlanets planets) : base(sessionManager)
		{
			if (planets == null) throw new ArgumentNullException("planets");
			_planets = planets;
		}

		public IEnumerable<SolarSystem> FindSystemsInLocalArea(LocalAreaView localAreaView)
		{
			if (localAreaView == null) throw new ArgumentNullException("localAreaView");

			var solarSystems = new List<SolarSystem>();
            var xDiff = (localAreaView.ScreenSize.Width - 1) / 2; // either side of the focal point we show this many squares
            var yDiff = (localAreaView.ScreenSize.Height - 1) / 2;
            var minX = localAreaView.FocusCoordinates.X - xDiff;
			var maxX = localAreaView.FocusCoordinates.X + xDiff;
			var minY = localAreaView.FocusCoordinates.Y - yDiff;
            var maxY = localAreaView.FocusCoordinates.Y + yDiff;

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
			var solarSystem = new SolarSystem();
			this.CopyCoreProperties(solarSystem, dr);
			return solarSystem;
        }

		private void CopyCoreProperties(SolarSystem solarSystem, IDataReader dr)
		{
			solarSystem.SolarSystemID = dr.GetInt64("SolarSystemID");
			solarSystem.Name = dr.GetString("Name");
			solarSystem.Coordinates = new Coordinates
			{
				X = dr.GetInt64("X"),
				Y = dr.GetInt64("Y"),
				Z = dr.GetInt64("Z")
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
            var query = this.CreateQuery("1=1 ORDER BY newid()", 1);
            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
            {
                using(var dr = cmd.ExecuteReader())
                {
                    if(dr.Read())
                    {
                        return this.InflateSolarSystem(dr);
                    }
                    return null;
                }
            }
        }

		public SolarSystemPlanetDetails GetSolarSystemDetails(long id)
		{
			var solarSystem = new SolarSystemPlanetDetails();
			var query = this.CreateQuery("SolarSystemID = @ID", 1);
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("ID", DbType.Int64, id);
				using (var dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						this.CopyCoreProperties(solarSystem, dr);
					}
					else
					{
						return null;
					}
				}
			}

			solarSystem.Planets = _planets.ListPlanetsInSolarSystem(id);
			return solarSystem;
		}
	}
}
