using Liath.BigSpace.Domain.Jobs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions.Jobs
{
    public interface IJobChildRepository
    {
        IEnumerable<string> ColumnNames { get; }
        string TableName { get; }
        string PrimaryKeyColumnName { get; }
        Job Create(IDataReader dr, string alias);
    }    
}
