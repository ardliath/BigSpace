using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.UserAccountDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Create
{
    public class DomainClasses
    {
        public static ScreenSize ScreenSize(int? height = null, int? width = null)
        {
            return new ScreenSize(height.HasValue ? height.Value : 21,
                width.HasValue ? width.Value : 21);
        }

        public static UserAccount UserAccount(Coordinates focusCoordinates = null)
        {
            return new UserAccount
            {
                FocusCoordinates = focusCoordinates ?? new Coordinates()
            };
        }

        public static SolarSystem SolarSystem(int id = 5, string name = null, Coordinates coordinates = null)
        {
            return new SolarSystem
            {
                SolarSystemID = id,
                Name = name ?? Guid.NewGuid().ToString(),
                Coordinates = coordinates ?? new Coordinates()
            };
        }

        public static Coordinates Coordinates(int x = 12, int y = 150, int z = -3)
        {
            return new Coordinates
            {
                X = x,
                Y = y,
                Z = z
            };
        }
    }
}
