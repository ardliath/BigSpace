using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Extensions
{
    public static class IDbCommandExtensions
    {
        public static void AddParameter(this IDbCommand cmd, string name, DbType dbType, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.DbType = dbType;
            param.Value = value;
            cmd.Parameters.Add(param);
        }
    }
}
