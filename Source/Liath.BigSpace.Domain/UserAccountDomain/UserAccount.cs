using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.UserAccountDomain
{
	public class UserAccount
	{
		public int UserAccountID { get; set; }

		public string Username { get; set; }
		public Coordinates FocusCoordinates { get; set; }
	}
}
