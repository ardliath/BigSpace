using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions.Jobs
{
    public interface IJourneyRepository
    {
        Int64 CreateJourney(SolarSystem from, SolarSystem to, DateTime start, TimeSpan duration);
    }
}
