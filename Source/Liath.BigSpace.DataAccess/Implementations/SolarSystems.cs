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
			var query = "SELECT SolarSystemID, Name, X, Y, Z FROM SolarSystems WHERE X BETWEEN @MinX AND @MaxX and Y BETWEEN @MinY AND @MaxY AND Z = @Z";

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
						solarSystems.Add(new SolarSystem
						{
							SolarSystemID = dr.GetInt64("SolarSystemID"),
							Name = dr.GetString("Name"),
							Coordinates = new Coordinates
							{
								X = dr.GetInt64("X"),
								Y = dr.GetInt64("Y"),
								Z = dr.GetInt64("Z")
							}
						});
					}
				}
			}

			return solarSystems;
		}
	}
}
