using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.UserAccountDomain
{
    public class SecurityUserAccount
    {
        public int UserAccountID { get; set; }
        public string Username { get; set; }        
        public virtual string EmailAddress { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual byte[] PasswordHash { get; set; }
        public virtual DateTime CreateTS { get; set; }
    }
}
