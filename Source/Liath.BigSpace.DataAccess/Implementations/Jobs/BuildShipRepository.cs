using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions.Jobs;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.Jobs;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class BuildShipRepository : IJobChildRepository
    {
        private ISolarSystems _solarSystems;
        public BuildShipRepository(ISolarSystems solarSystems)
        {
            if (solarSystems == null) throw new ArgumentNullException("solarSystems");
            _solarSystems = solarSystems;
        }

        public IEnumerable<string> ColumnNames
        {
            get { return new string[] { "SolarSystemID" }; }
        }

        public string TableName
        {
            get { return "ShipBuilds"; }
        }

        public string PrimaryKeyColumnName
        {
            get { return "JobID"; }
        }

        public Job Create(IDataReader dr, string alias)
        {
            var job = new BuildShip();

            var solarSystemID = dr.GetInt64(string.Concat(alias, ".SolarSystemID"));
            job.SolarSystem = _solarSystems.GetSolarSystem(solarSystemID);

            return job;
        }
    }
}
