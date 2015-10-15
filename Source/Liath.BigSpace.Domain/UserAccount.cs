using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
    public class UserAccount
    {
        public int UserAccountID { get; set; }

        public string Username { get; set; }

        public Int64 X { get; set; }

        public Int64 Z { get; set; }

        public Int64 Y { get; set; }
    }
}
