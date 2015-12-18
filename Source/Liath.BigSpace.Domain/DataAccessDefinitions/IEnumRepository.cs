using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain.DataAccessDefinitions
{
	public interface IEnumRepository
	{
		bool DoesEnumValueExist(Type enumType, int value);
		void InsertEnumValue(Type enumType, int value, string code, string description);
	}
}
