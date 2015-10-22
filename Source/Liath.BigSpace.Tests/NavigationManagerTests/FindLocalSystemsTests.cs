using Create = Liath.BigSpace.Testing.Create;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Tests.NavigationManagerTests
{
    public class FindLocalSystemsTests
    {
        [Test]
        public void Ensure_ArgumentNullException_is_thrown_if_screenSize_is_null()
        {
            var manager = Create.BusinessLogicClass.NavigationManager();

            var ex = Assert.Throws<ArgumentNullException>(() => manager.FindLocalSystems(null));
            Assert.AreEqual("screenSize", ex.ParamName);
        }


    }
}
