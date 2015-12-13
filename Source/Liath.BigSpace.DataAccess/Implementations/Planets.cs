using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain.SolarSystems;
using System.Data;
using Liath.BigSpace.DataAccess.Extensions;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class Planets : DataAccessBase, IPlanets
	{
		private readonly IRaces _races;

		public Planets(ISessionManager sessionManager) : base(sessionManager)
		{
		}

		public static IEnumerable<string> Fields = new string[] { "PlanetID", "SolarSystemID", "PositionIndex", "Name", "Image", "Population", "MaxPopulation", "RaceID" };

		private string CreateQuery(string filter = null, int? top = null, string orderBy = null)
		{
			var sb = new StringBuilder("SELECT ");
			if(top.HasValue)
			{
				sb.AppendFormat("TOP {0} ", top.Value);
			}

			sb.Append(string.Join(", ", Fields));
			sb.Append(" FROM Planets");
			if(!string.IsNullOrWhiteSpace(filter))
			{
				sb.AppendLine($" WHERE {filter}");
			}

			if (orderBy != null)
			{
				sb.AppendLine($"ORDER BY {orderBy}");
			}

			return sb.ToString();
		}

		public IEnumerable<Planet> ListPlanetsInSolarSystem(long solarSystemID)
		{
			var planets = new List<Planet>();
			var query = this.CreateQuery("SolarSystemID = @SolarSystemID");
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("SolarSystemID", DbType.Int64, solarSystemID);
				using (var dr = cmd.ExecuteReader())
				{
					while(dr.Read())
					{
						planets.Add(this.InflatePlanet(dr));
					}
				}
			}

			return planets;
		}

		private Planet InflatePlanet(IDataReader dr)
		{
			var planet = new Planet();
			this.PopulateCoreProperties(planet, dr);
			return planet;
		}

		private void PopulateCoreProperties(Planet planet, IDataReader dr)
		{
			planet.PlanetID = dr.GetInt64("PlanetID");
			planet.SolarSystemID = dr.GetInt64("SolarSystemID");
			planet.PositionIndex = dr.GetInt16("PositionIndex");
			planet.Name = dr.GetString("Name");
			planet.Image = dr.GetString("Image");
			planet.Population = dr.GetInt64("Population");
			planet.MaxPopulation = dr.GetInt64("MaxPopulation");
			planet.RaceID = dr.GetNullableInt32("RaceID");
		}

		public Planet GetPlanet(long id)
		{
			var query = this.CreateQuery("PlanetID = @ID");
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("ID", DbType.Int64, id);
				using (var dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						return this.InflatePlanet(dr);
					}

					return null;
				}
			}
		}


		public Planet GetMostPopulatedPlanetInSolarSystem(long solarSystemId)
		{
			var query = this.CreateQuery("SolarSystemID = @SolarSystemID", 1, "Population DESC");
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("SolarSystemID", DbType.Int64, solarSystemId);
				using (var dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						return this.InflatePlanet(dr);
					}

					return null;
				}
			}
		}
	}
}
