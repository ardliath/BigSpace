using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Configuration
{
    public interface IConfigurationManager
    {
        ConnectionStringSettings ConnectionString { get; }
    }
}
