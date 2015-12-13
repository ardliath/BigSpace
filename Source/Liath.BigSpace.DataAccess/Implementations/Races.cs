using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class Races : DataAccessBase, IRaces
	{
		public Races(ISessionManager sessionManager) : base(sessionManager)
		{
		}

		public Race GetRace(int id)
		{
			var query = this.CreateQuery("RaceID = @ID");

			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("ID", DbType.Int32, id);

				using (var dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						return this.InflateRace(dr);
					}
					return null;
				}
			}
		}

		private string CreateQuery(string filter = null, int? top = null)
		{
			return string.Concat("SELECT ",
				top.HasValue ? string.Concat("TOP ", top.Value, " ") : null,
				"RaceID, Name, Guid FROM Races",
				filter == null ? null : " WHERE ",
				filter);
		}

		private Race InflateRace(IDataReader dr)
		{
			var race = new Race();
			this.PopulateProperties(dr, race);
			return race;
		}

		private void PopulateProperties(IDataReader dr, Race race)
		{
			race.RaceID = dr.GetInt32("RaceID");
			race.Name = dr.GetString("Name");
			race.Guid = dr.GetGuid("Guid");
		}
	}
}