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

        public static byte[] GetBytes(this IDataReader dr, string name)
        {
            return (byte[])dr[dr.GetOrdinal(name)];
        }

        public static Int64 GetInt64(this IDataReader dr, string name)
        {
            return dr.GetInt64(dr.GetOrdinal(name));
        }

        public static Int64? GetNullableInt64(this IDataReader dr, string name)
        {
            var ordinal = dr.GetOrdinal(name);
            return dr.IsDBNull(ordinal)
                ? null
                : (Int64?)dr.GetInt64(ordinal);
        }

        public static bool GetBoolean(this IDataReader dr, string name)
        {
            return dr.GetBoolean(dr.GetOrdinal(name));
        }

        public static DateTime GetDateTime(this IDataReader dr, string name)
        {
            return dr.GetDateTime(dr.GetOrdinal(name));
        }

        public static bool IsDBNull(this IDataReader dr, string name)
        {
            return dr.IsDBNull(dr.GetOrdinal(name));
        }
    }
}
