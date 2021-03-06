﻿using Liath.BigSpace.DataAccess.Implementations;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Create
{
    public class DataAccessClasses
    {
        public static SolarSystems SolarSystems(ISessionManager sessionManager = null, IPlanets planets = null)
        {
            return new SolarSystems(sessionManager ?? Mock.Of<ISessionManager>(),
							planets ?? Mock.Of<IPlanets>());
        }
    }
}
