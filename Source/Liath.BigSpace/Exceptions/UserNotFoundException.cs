using System;

namespace Liath.BigSpace.Exceptions
{
	public class UserNotFoundException : SecurityException
	{
		public string Username { get; set; }

		protected UserNotFoundException()
		{
		}

		public UserNotFoundException(string username)
		{
			if (username == null) throw new ArgumentNullException(nameof(username));

			Username = username;
		}
	}
}