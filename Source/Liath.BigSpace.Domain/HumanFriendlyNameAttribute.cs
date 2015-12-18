using System;

namespace Liath.BigSpace.Domain
{
	public class HumanFriendlyNameAttribute : Attribute
	{
		public string FriendlyValue { get; set; }

		public HumanFriendlyNameAttribute(string friendlyValue)
		{
			FriendlyValue = friendlyValue;
		}
	}
}