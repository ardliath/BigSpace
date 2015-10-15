using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Liath.BigSpace.Session
{
    public class UnitOfWork : IUnitOfWork
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private IDbConnection _connection;
        private ConnectionStringSettings _connectionStringSettings;

        public UnitOfWork(ConnectionStringSettings connectionStringSettings)
        {
            if (connectionStringSettings == null) throw new ArgumentNullException("connectionStringSettings");
            _connectionStringSettings = connectionStringSettings;
        }

        public void Dispose()
        {
            logger.Trace("Disposing of UnitOfWork");
            if(_connection != null)
            {
                logger.Trace("Connection is not null disposing");
                _connection.Dispose();
                logger.Trace("Connection was disposed");
            }
        }

        public IDbConnection GetConnection()
        {
            logger.Trace("Getting Connection");
            if(_connection == null)
            {
                logger.Trace("Requesting a new connection");
                _connection = this.CreateConnection();
            }
            return _connection;
        }

        private IDbConnection CreateConnection()
        {
            logger.Trace("Creating a new connection");
            return new SqlConnection(_connectionStringSettings.ConnectionString);
        }
    }
}
