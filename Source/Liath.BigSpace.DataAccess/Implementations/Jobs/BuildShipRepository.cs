using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.DataAccess.Definitions.Jobs
{
    public class BuildShipRepository : IJobChildRepository
    {
        public IEnumerable<string> ColumnNames
        {
            get { throw new NotImplementedException(); }
        }

        public string TableName
        {
            get { throw new NotImplementedException(); }
        }

        public string PrimaryKeyColumnName
        {
            get { throw new NotImplementedException(); }
        }

        public Job Create(System.Data.IDataReader dr, string alias)
        {
            throw new NotImplementedException();
        }
    }
}
