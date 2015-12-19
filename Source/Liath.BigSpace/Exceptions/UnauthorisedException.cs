using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.Domain.UserAccountDomain;

namespace Liath.BigSpace.Exceptions
{
	public class UnauthorisedException<T> : SecurityException
	{
		public UserAccount UserAccount { get; set; }
		public T Entity { get; set; }

		public UnauthorisedException(UserAccount userAccount, T entity)
			:base("The user was not authorised to access the object")
		{
			UserAccount = userAccount;
			Entity = entity;
		}
	}
}
