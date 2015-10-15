using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Extensions
{
    public static class IDataReaderExtensions
    {
        public static int GetInt32(this IDataReader dr, string name)
        {
            return dr.GetInt32(dr.GetOrdinal(name));
        }

        public static string GetString(this IDataReader dr, string name)
        {
            return dr.GetString(dr.GetOrdinal(name));
        }

        public static Int64 GetInt64(this IDataReader dr, string name)
        {
            return dr.GetInt64(dr.GetOrdinal(name));
        }
    }
}
