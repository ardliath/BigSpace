using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class GalaxyManager : IGalaxyManager
    {
        private ISolarSystems _solarSystems;
        public GalaxyManager(ISolarSystems solarSystems)
        {
            if (solarSystems == null) throw new ArgumentNullException("solarSystems");
            _solarSystems = solarSystems;            
        }

        public void CreateGalaxy()
        {
            var random = new Random();
            int numberOfSystems = 50000 + random.Next(10000); // 50,000 give or take
            for(int i = 0; i < numberOfSystems; i++)
            {
                this.RandomlyCreateSolarSystem(random, i);
            }
        }

        private void RandomlyCreateSolarSystem(Random random, int i)
        {
            var solarSystem = new SolarSystem();
            solarSystem.Name = this.RandomlyCreateSolarSystemName(random, i);
            solarSystem.Coordinates = new Coordinates
            {
                X = random.Next(2000) - 1000,
                Y = random.Next(2000) - 1000,
                Z = 0
            };

            _solarSystems.CreateSolarSystem(solarSystem);
        }

        private string RandomlyCreateSolarSystemName(Random random, int i)
        {
            var firstLetter = GetRandomGreekLetter(random);
            var secondLetter = GetRandomGreekLetter(random);
            return string.Concat(firstLetter, " ", secondLetter, " ", random.Next(999));
        }

        private string GetRandomGreekLetter(Random random)
        {
            return GreekLetters.ElementAt(random.Next(GreekLetters.Count() - 1));
        }

        private IEnumerable<string> GreekLetters = new string[]
        {
        "Alpha",
        "Beta",
        "Gamma",
        "Delta",
        "Epsilon",
        "Zeta",
        "Eta",
        "Theta",
        "Iota",
        "Kappa",
        "Lambda",
        "Mu",
        "Or",
        "Nu",
        "Xi",
        "Omicron",
        "Pi",
        "Rho",
        "Sigma",
        "Tau",
        "Upsilon",
        "Phi",
        "Chi",
        "Psi",
        "Omega"
        };
    }
}
