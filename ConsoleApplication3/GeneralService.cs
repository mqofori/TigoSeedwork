using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace ConsoleApplication3
{
    public static class GeneralService
    {
        /// <summary>
        /// Get login pin 
        /// </summary>
        /// <param name="session">umarket session</param>
        /// <param name="salt">Salt- Msisdn </param>
        /// <param name="initator">Requestor Msisdn</param>
        /// <returns></returns>
        public static string GetPin(string session, string salt,  string pin)
        {
           return GetGetUMarketHashKey(session + GetGetUMarketHashKey(salt + pin).ToLower()).ToUpper();            
        }

        /// <summary>
        /// Hash the selected string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string GetGetUMarketHashKey(string data)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(data);
            SHA1 sha1 = SHA1.Create();

            byte[] hash = sha1.ComputeHash(key);

            StringBuilder result = new StringBuilder();
            foreach (byte b in hash)
                result.Append(Convert.ToInt32(b).ToString("x2"));

            return result.ToString().ToUpper();
        }
    }
}
