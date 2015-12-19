using System;

namespace Liath.BigSpace.Exceptions
{
	public class SecurityException : Exception
	{

		public SecurityException()
			: base()
		{
			
		}

		public SecurityException(string message) : base(message)
		{
		}
	}
}