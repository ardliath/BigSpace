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
        private IDbTransaction _transaction;
        private bool _transactionRolledBack;

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
                if(_transaction != null)
                {
                    if (!_transactionRolledBack)
                    {
                        _transaction.Commit();
                    }
                    _transaction.Dispose();
                }
                logger.Trace("Connection is not null disposing");
                _connection.Dispose();
                logger.Trace("Connection was disposed");
            }
        }

	    public IDbCommand CreateCommand(string query)
	    {
		    if (query == null) throw new ArgumentNullException("query");
		    var conn = GetConnection();
		    var cmd = conn.CreateCommand();
            cmd.Transaction = _transaction;
		    cmd.CommandText = query;
		    return cmd;
	    }

	    private IDbConnection GetConnection()
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
		    _connection = new SqlConnection(_connectionStringSettings.ConnectionString);            
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            _transactionRolledBack = false;
            return _connection;
	    }


        public void Rollback()
        {
            if(_transaction != null)
            {
                _transaction.Rollback();
                _transactionRolledBack = true;
            }
        }
    }
}
