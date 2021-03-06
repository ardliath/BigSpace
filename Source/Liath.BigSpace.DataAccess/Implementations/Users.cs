﻿using Liath.BigSpace.DataAccess.Definitions;
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
using Liath.BigSpace.Domain.UserAccountDomain;

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

			using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT UserAccountID, Username, X, Y, Z, EmpireID FROM UserAccounts WHERE Username = @Username"))
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
                            EmpireID = dr.GetInt32("EmpireID"),
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


        public SecurityUserAccount GetUserAccount(string emailAddress)
        {
            if (emailAddress == null) throw new ArgumentNullException("emailAddress");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("SELECT UserAccountID, Username, EmailAddress, PasswordHash, PasswordSalt, CreateTS from UserAccounts WHERE EmailAddress = @EmailAddress"))
            {
                cmd.AddParameter("EmailAddress", DbType.String, emailAddress);

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        return new SecurityUserAccount
                        {
                            UserAccountID = dr.GetInt32("UserAccountID"),
                            Username = dr.GetString("Username"),
                            EmailAddress = dr.GetString("EmailAddress"),
                            PasswordHash = dr.GetBytes("PasswordHash"),
                            PasswordSalt = dr.GetString("PasswordSalt"),
                            CreateTS = dr.GetDateTime("CreateTS")
                        };
                    }

                    return null;
                }
            }
        }


        public void CreateUserAccount(SecurityUserAccount securityUser, int empireID, Coordinates coordinates)
        {
            if (securityUser == null) throw new ArgumentNullException("securityUser");

            using (var cmd = this.SessionManager.GetCurrentUnitOfWork().CreateCommand("INSERT INTO UserAccounts (Username, EmailAddress, PasswordHash, PasswordSalt, CreateTS, EmpireID, X, Y, Z) VALUES (@Username, @EmailAddress, @PasswordHash, @PasswordSalt, @CreateTS, @Empire, @X, @Y, @Z) SELECT Scope_Identity()"))
            {
                cmd.AddParameter("Username", DbType.String, securityUser.Username);
                cmd.AddParameter("EmailAddress", DbType.String, securityUser.EmailAddress);
                cmd.AddParameter("PasswordHash", DbType.Binary, securityUser.PasswordHash);
                cmd.AddParameter("PasswordSalt", DbType.String, securityUser.PasswordSalt);
                cmd.AddParameter("CreateTS", DbType.DateTime, securityUser.CreateTS);

                cmd.AddParameter("Empire", DbType.Int32, empireID);
                cmd.AddParameter("X", DbType.Int64, coordinates.X);
                cmd.AddParameter("Y", DbType.Int64, coordinates.Y);
                cmd.AddParameter("Z", DbType.Int64, coordinates.Z);

                securityUser.UserAccountID = (int)(decimal)cmd.ExecuteScalar();
            }
        }
    }
}
