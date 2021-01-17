using BankIntegration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using BankIntegration.Services;

namespace BankIntegration
{
    /// <summary>
    /// Summary description for BankIntegration
    /// </summary>
    [WebService(Namespace = "https://www.tigo.com.gh/tigocash/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BankIntegration : System.Web.Services.WebService
    {

        [WebMethod(Description = "Request for a Session ID")]
        [SoapDocumentMethod(Action = "GetSessionID")]
        public BankIntegrationService.GetResults GetSessionID(string Username, string Password, string RequestType)
        {
            return new BankIntegrationService().GetSessionID(Username, Password, RequestType);
        }

        [WebMethod(Description = "Merchant CashIn")]
        [SoapDocumentMethod(Action = "CashIn")]
        public List<string> CashIn(string SessionID, string AgentID, string Amount, string RequestType, string ExternalID, string Comment, string Branch)
        {
            return new BankIntegrationService().ValidateSessionID(SessionID, AgentID, Amount, RequestType, ExternalID, Comment, Branch).ValidationResults;

        }

        [WebMethod(Description = "Merchant CashOut")]
        [SoapDocumentMethod(Action = "CashOut")]
        public List<string> CashOut(string SessionID, string AgentID, string Amount, string RequestType, string ExternalID, string Comment, string Branch)
        {
            return new BankIntegrationService().ValidateSessionID(SessionID, AgentID, Amount, RequestType, ExternalID, Comment, Branch).ValidationResults;

        }
    }
}
