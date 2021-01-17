using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using BankIntegration.SoapClasses;
using System.Xml.Linq;
using System.Configuration;

namespace BankIntegration.Services
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


        /// <summary>
        /// create a Umarket Login Session
        /// </summary>
        /// <returns></returns>
        public static string CreatAndLoginSession()
        {
            #region Config
            string init = ConfigurationManager.AppSettings["init"].ToString();
            string salt = ConfigurationManager.AppSettings["salt"].ToString();
            string pin = ConfigurationManager.AppSettings["pin"].ToString();

            #endregion
            // Get 
            string response = SoapServices.SendXMLRequest(GetXMLCommands.GetSessionXML());
            var t = XElement.Parse(response);

            string session = t.Value.Remove(t.Value.Length - 8);

            pin = GeneralService.GetPin(session, salt, pin);
            response = GetXMLCommands.GetLoginXML(init, pin, session);
            response = SoapServices.SendXMLRequest(response);


            t = XElement.Parse(response);

            string transactionId = t.Value.Remove(t.Value.Length - 8);

            return session;
        }


        /// <summary>
        /// send an HTTP get request
        /// </summary>
        /// <returns></returns>
        public static string HTTPURLRequest()
        {
            return string.Empty;
        }
    }
}
