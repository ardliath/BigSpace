using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        public System.Configuration.ConnectionStringSettings ConnectionString
        {
            get { return System.Configuration.ConfigurationManager.ConnectionStrings["Liath.BigSpace"]; }
        }


        public int GameSpeed
        {
            get { return 1; }
        }
    }
}
