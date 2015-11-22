using Liath.BigSpace.DataAccess.Definitions;
using Liath.BigSpace.Definitions;
using Liath.BigSpace.Domain;
using Liath.BigSpace.Domain.DataAccessDefinitions;
using Liath.BigSpace.Domain.UserAccountDomain;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class SecurityManager : ISecurityManager
    {
        private IUsers _dataAccess;
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private ICryptographyManager _cryptographyManager;
        private IUsers _users;

        public SecurityManager(IUsers userDataAccess, ICryptographyManager cryptographyManager, IUsers users)
        {
            if (userDataAccess == null) throw new ArgumentNullException("userDataAccess");
            if (cryptographyManager == null) throw new ArgumentNullException("cryptographyManager");
            if (users == null) throw new ArgumentNullException("users");

            _dataAccess = userDataAccess;
            _cryptographyManager = cryptographyManager;
            _users = users;
        }

        public UserAccount GetCurrentUserAccount()
        {
            var currentUsername = this.CurrentUsername;
            return _dataAccess.GetUserByUsername(currentUsername);
        }

        public string CurrentUsername
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }


        public bool Login(string emailAddress, string password, out SecurityUserAccount currentUserAccount)
        {
            if (emailAddress == null) throw new ArgumentNullException("emailAddress");
            if (password == null) throw new ArgumentNullException("password");

            var userAccountFromRepository = _users.GetUserAccount(emailAddress);
            if (userAccountFromRepository == null)
            {
                logger.Info("UserAcount with email address '{0}' not found", emailAddress);
                currentUserAccount = null;
                return false;
            }

            var hashForEnteredUser = _cryptographyManager.CreateHash(userAccountFromRepository.CreateTS, password, userAccountFromRepository.PasswordSalt);

            var trimmedHash = userAccountFromRepository.PasswordHash.Take(hashForEnteredUser.Count()).ToArray();
            if (!Enumerable.SequenceEqual(trimmedHash, hashForEnteredUser))
            {
                logger.Info("Password was incorrect for user '{0}'", emailAddress);
                currentUserAccount = null;
                return false;
            }

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userAccountFromRepository.Username), new string[] { });
            currentUserAccount = userAccountFromRepository;
            return true;
        }


        public bool CreateUserAccount(string username, string emailAddress, string password, string confirmPassword, out UserAccount user, out string errors)
        {
            string validatedErrors;
            if(this.Validate(username, emailAddress, password, confirmPassword, out validatedErrors))
            {
                var created = DateTime.UtcNow;
                var salt = _cryptographyManager.CreateSalt();
                var hash = _cryptographyManager.CreateHash(created, password, salt);

                var securityUser = new SecurityUserAccount
                {
                    CreateTS = created,
                    EmailAddress = emailAddress,
                    PasswordSalt = salt,
                    PasswordHash = hash,
                    Username = username
                };

                _users.CreateUserAccount(securityUser);
                user = _users.GetUserByUsername(username);
                errors = null;
                return true;
            }
            else
            {
                errors = validatedErrors;
                user = null;
                return false;
            }
        }

        private bool Validate(string username, string emailAddress, string password, string confirmPassword, out string errors)
        {
            throw new NotImplementedException();
        }
    }
}
