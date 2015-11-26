using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Testing.Create;
using System.Data;
using Moq;
using Liath.BigSpace.Session;
using Liath.BigSpace.Testing.Extensions.SessionManagerExtensions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Testing.Extensions.Assertions;
using System.Data.SqlClient;

namespace Liath.BigSpace.DataAccess.Tests.SolarSystemRepositoryTests
{
    public class FindSystemsInLocalAreaTests
    {
        [Test]
        public void Throws_exception_if_parameter_is_null()
        {
            var mgr = DataAccessClasses.SolarSystems();
            Assert.Throws<ArgumentNullException>(() => mgr.FindSystemsInLocalArea(null));
        }

        [Test]
        public void Verify_parameters_at_focus()
        {
            var parameters = new MockedParameters();
            var cmd = new Mock<IDbCommand>();            
            cmd.Setup(x => x.CreateParameter()).Returns(() => new MockedParameter());
            cmd.Setup(x => x.ExecuteReader()).Returns(Mock.Of<IDataReader>());
            cmd.Setup(x => x.Parameters).Returns(parameters);
            var sessionManager = new Mock<ISessionManager>();
            sessionManager.SetupUoWCreateCommand(cmd.Object);
            var mgr = DataAccessClasses.SolarSystems(sessionManager.Object);

            var systems = mgr.FindSystemsInLocalArea(new LocalAreaView(new Coordinates
            {
                X = 0,
                Y = 0,
                Z = 0
            }, new ScreenSize(5, 5)));

            cmd.Object.AssertParameter("MinX", -2)
                .AssertParameter("MaxX", 2)
                .AssertParameter("MinY", -2)
                .AssertParameter("MaxY", 2)
                .AssertParameter("Z", 0);
        }

        [Test]
        public void MoreComplex()
        {
            var parameters = new MockedParameters();
            var cmd = new Mock<IDbCommand>();
            cmd.Setup(x => x.CreateParameter()).Returns(() => new MockedParameter());
            cmd.Setup(x => x.ExecuteReader()).Returns(Mock.Of<IDataReader>());
            cmd.Setup(x => x.Parameters).Returns(parameters);
            var sessionManager = new Mock<ISessionManager>();
            sessionManager.SetupUoWCreateCommand(cmd.Object);
            var mgr = DataAccessClasses.SolarSystems(sessionManager.Object);

            var systems = mgr.FindSystemsInLocalArea(new LocalAreaView(new Coordinates
            {
                X = 1000,
                Y = -170,
                Z = 0
            }, new ScreenSize(5, 5)));

            cmd.Object.AssertParameter("MinX", 998)
                .AssertParameter("MaxX", 1002)
                .AssertParameter("MinY", -172)
                .AssertParameter("MaxY", -168)
                .AssertParameter("Z", 0);
        }
    }



    public class MockedParameters : IDataParameterCollection
    {
        public Dictionary<string, object> _internalDictionay;

        public MockedParameters()
        {
            _internalDictionay = new Dictionary<string, object>();
        }

        public bool Contains(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }

        public object this[string parameterName]
        {
            get
            {
                return _internalDictionay[parameterName];
            }
            set
            {
                _internalDictionay[parameterName] = value;
            }
        }

        public int Add(object value)
        {
            var parameter = (IDbDataParameter)value;
            _internalDictionay.Add(parameter.ParameterName, parameter);
            return _internalDictionay.Count() - 1; // I assume we're returning some sort of index?
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get
            {
                return _internalDictionay[_internalDictionay.Keys.ElementAt(index)];
            }
            set
            {
                _internalDictionay[_internalDictionay.Keys.ElementAt(index)] = value;
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _internalDictionay.Count; }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _internalDictionay.Select(id => id.Value).GetEnumerator();
        }
    }

    public class MockedParameter : IDbDataParameter
    {
        public byte Precision { get; set; }

        public byte Scale { get; set; }

        public int Size { get; set; }

        public DbType DbType { get; set; }

        public ParameterDirection Direction { get; set; }

        public bool IsNullable { get; set; }

        public string ParameterName { get; set; }

        public string SourceColumn { get; set; }

        public DataRowVersion SourceVersion { get; set; }

        public object Value { get; set; }
    }


}
