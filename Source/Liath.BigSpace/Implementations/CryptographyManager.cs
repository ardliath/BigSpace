using Liath.BigSpace.Configuration;
using Liath.BigSpace.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Implementations
{
    public class CryptographyManager : ICryptographyManager
    {
        private IConfigurationManager _configurationManager;

        public CryptographyManager(IConfigurationManager configurationManager)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            _configurationManager = configurationManager;
        }

       

        /// <summary>
        /// Creates a password hash from the details provided
        /// </summary>
        /// <param name="created">The time when the person was created</param>
        /// <param name="password">The desired password</param>
        /// <param name="salt">The login's salt</param>
        /// <returns>A Byte[] with the password hash</returns>
        public Byte[] CreateHash(DateTime created, string password, string salt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (salt == null) throw new ArgumentNullException("salt");

            SHA1 sha = new SHA1CryptoServiceProvider();
            UTF32Encoding enc = new UTF32Encoding();
            var stringValue = string.Concat(_configurationManager.HashConstant, created, password, salt);
            return sha.ComputeHash(enc.GetBytes(stringValue));
        }

        /// <summary>
        /// Creates a random salt
        /// </summary>
        public string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[16];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }
    }
}
