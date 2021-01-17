using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

using SubscriberManagement.Domain.SMS;

namespace ConsoleApplication3.SoapClasses
{
    public static class SoapServices
    {
        #region Properties
        private static ICredentials CurrentCredentials
        {
            get
            {
                ICredentials oCredential;
                //if ()
                //{
                oCredential = CredentialCache.DefaultCredentials;
                //}
                //else
                //{
                //    oCredential = new NetworkCredential(username, password);

                //     (oCredential as NetworkCredential).Domain = domain.Text;

                //}
                return oCredential;
            }
        }
        #endregion


        #region Operations        
        /// <summary>
        /// Send XML command to UMarkert Platform
        /// </summary>
        /// <param name="soapXML"></param>
        /// <returns></returns>
        public static string  SendXMLRequest(string soapXML)
        {
            ClassLogger log = null;
           // string umarkert_Server = "http://10.1.5.64:8280/services/umarketsc";    test server

           // string umarkert_Server = "http://10.1.5.60:8280/services/umarketsc";  old live .

            string umarkert_Server = "http://10.1.5.82:8280/services/umarketsc"; 

           // string umarkert_Server = "http://10.11.14.4:7004/osb/services/RegisterPayment_1_1"; middleware test bed
            ClassSOAP soapRequest = null;
            soapRequest = new ClassSOAP(umarkert_Server, CurrentCredentials, log);
            soapRequest.BypassWebProxy = true;
            return soapRequest.SendRequest(soapXML);           
        }
                 
        /// <summary>
        /// Get a value from an XML response 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public static string GetXMLSessionValue(string response, string section)
        {
            string value = string.Empty ;
            XDocument doc = XDocument.Parse(response);

            foreach (XElement element in doc.Descendants(section))            
                value = element.Value;

            return value;
        }
        #endregion


    }

}
