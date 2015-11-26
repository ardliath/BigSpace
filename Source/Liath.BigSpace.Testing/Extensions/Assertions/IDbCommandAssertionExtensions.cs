using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Testing.Extensions.Assertions
{
    public static class IDbCommandAssertionExtensions
    {
        public static IDbCommand AssertParameter(this IDbCommand command, string name, object expectedValue)
        {
            IDbDataParameter parameter = command.Parameters[name] as IDbDataParameter;
            if (parameter == null) Assert.Fail(string.Format("Parameter '{0}' does not exist", name));
            Assert.AreEqual(expectedValue, parameter.Value, string.Format("Parameter '{0}' had the value '{1}' but '{2}' was expected", name, parameter.Value, expectedValue));
            return command;
        }
    }
}
