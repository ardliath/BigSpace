using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Session;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liath.BigSpace.DataAccess.Extensions;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using System.Data;

namespace Liath.BigSpace.DataAccess.Implementations
{
	public class Users : DataAccessBase, IUsers
	{
		private static ILogger logger = LogManager.GetCurrentClassLogger();

		public Users(ISessionManager sessionManager)
			: base(sessionManager)
		{
		}

		public UserAccount GetUserByUsername(string username)
		{
			if (username == null) throw new ArgumentNullException("username");

			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT UserAccountID, Username, X, Y, Z FROM UserAccounts WHERE Username = @Username"))
			{
				cmd.AddParameter("Username", System.Data.DbType.String, username);
				using (var dr = cmd.ExecuteReader())
				{
					if (dr.Read())
					{
						return new UserAccount
						{
							UserAccountID = dr.GetInt32("UserAccountID"),
							Username = dr.GetString("Username"),
							FocusCoordinates = new Coordinates
							{
								X = dr.GetInt64("X"),
								Y = dr.GetInt64("Y"),
								Z = dr.GetInt64("Z")
							}
						};
					}

					return null;
				}
			}
		}


        public UserAccount Update(UserAccount userAccount)
        {
            if (userAccount == null) throw new ArgumentNullException("userAccount");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("UPDATE UserAccounts SET X = @X, Y = @Y, Z = @Z WHERE UserAccountID = @ID"))
            {
                cmd.AddParameter("ID", DbType.Int32, userAccount.UserAccountID);
                cmd.AddParameter("X", DbType.Int32, userAccount.FocusCoordinates.X);
                cmd.AddParameter("Y", DbType.Int32, userAccount.FocusCoordinates.Y);
                cmd.AddParameter("Z", DbType.Int32, userAccount.FocusCoordinates.Z);

                cmd.ExecuteNonQuery();

                return userAccount;
            }
        }
    }
}
