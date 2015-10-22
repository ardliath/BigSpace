using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Extensions.DataAccessExtensions
{
    public static class SolarSystemExtensions
    {
        public static Mock<ISolarSystems> SetupFindSystemsInLocalArea(this Mock<ISolarSystems> solarSystems, int numberToCreate = 1)
        {
            var responses = new List<SolarSystem>();
            for(int i = 0; i < numberToCreate; i++)
            {
                responses.Add(Create.DomainClasses.SolarSystem());
            }
            solarSystems.Setup(x => x.FindSystemsInLocalArea(It.IsAny<LocalAreaView>())).Returns(responses);
            return solarSystems;
        }
    }
}
