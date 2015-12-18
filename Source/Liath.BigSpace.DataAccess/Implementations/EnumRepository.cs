using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Session;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class EnumRepository : DataAccessBase, IEnumRepository
	{
		public EnumRepository(ISessionManager sessionManager) : base(sessionManager)
		{
		}

		public bool DoesEnumValueExist(Type enumType, int value)
		{
			var tableName = this.GetTableName(enumType);
			var pkName = this.GetPkName(enumType);
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand($"SELECT COUNT(*) FROM {tableName} WHERE {pkName} = @ID"))
			{
				cmd.AddParameter("ID", DbType.Int32, value);
				return (int)cmd.ExecuteScalar() > 0;
			}
		}

		private string GetPkName(Type enumType)
		{
			return string.Concat(enumType.Name.TrimEnd('s'), "ID");
		}

		private string GetTableName(Type enumType)
		{
			return enumType.Name;
		}

		public void InsertEnumValue(Type enumType, int value, string code, string description)
		{
			var tableName = this.GetTableName(enumType);
			var pkName = this.GetPkName(enumType);
			var query = $"INSERT INTO {tableName} ({pkName}, Code, Description) VALUES (@ID, @Code, @Description)";
			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand(query))
			{
				cmd.AddParameter("ID", DbType.Int32, value);
				cmd.AddParameter("Code", DbType.String, code);
				cmd.AddParameter("Description", DbType.String, description);
				cmd.ExecuteNonQuery();
			}
		}
	}
}
