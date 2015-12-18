using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
	public enum Commands
	{
		[HumanFriendlyName("Flee when attacked")]
		FleeWhenAttacked,
		[HumanFriendlyName("Attack ships from other empires I encounter")]
		AttackShipsNotInMyEmpire
	}
}
