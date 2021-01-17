using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
namespace BankIntegration.SoapClasses
{
    public static class GetXMLCommands
    {

        public static string GetSessionXML()
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:createsession/>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }

        public static string GetLoginXML(string msisdn, string pin, string session)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:login>";
            xml += "<urn:loginRequest>";

            xml += "<urn:sessionid>" + session + "</urn:sessionid>";
            xml += "<urn:initiator>" + msisdn + "</urn:initiator>";
            xml += "<urn:pin>" + pin + "</urn:pin>";
            xml += "</urn:loginRequest>";
            xml += "</urn:login>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }


        public static string GetRegisterXML(string register, string session, string phoneNumber, string name, string address, string nationality, string dob, string idType, string idNumber)
        {
            string agent = ConfigurationManager.AppSettings["reg_agent"].ToString();
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:register>";
            xml += "<urn:registerRequest>";

            xml += "<std:extra_trans_data>";

            xml += "<misc:keyValuePairs>";
            xml += "<misc:key>Register</misc:key>";
            xml += "<misc:value>" + register + "</misc:value>";
            xml += "</misc:keyValuePairs>";
            xml += "</std:extra_trans_data>";
            xml += "<urn:sessionid>" + session + "</urn:sessionid>";
            xml += "<urn:agent>" + phoneNumber + "</urn:agent>";
            xml += "<urn:name>" + name + "</urn:name>";
            xml += "<urn:ad_first_name>" + name + "</urn:ad_first_name>";
            xml += "<urn:ad_address>" + address + "</urn:ad_address>";
            xml += "<urn:agent_category>cashsubscriber</urn:agent_category>";
            xml += "<urn:ad_nationality>" + nationality + "</urn:ad_nationality>";
            xml += "<urn:ad_dob>" + dob + "</urn:ad_dob>";
            xml += "<urn:ad_alt_id_type>" + idType + "</urn:ad_alt_id_type>";
            xml += "<urn:ad_alt_id>" + idNumber + "</urn:ad_alt_id>";
            xml += "<urn:ad_tigo_bank_agent>" + agent + "</urn:ad_tigo_bank_agent>";
            xml += "</urn:registerRequest>";
            xml += "</urn:register>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }

        public static string GetActivateXML(string session, string phoneNumber, string init, string pin, string receiptNumber)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:activate>";
            xml += "<urn:activateRequest>";

            xml += "<std:extra_trans_data>";

            xml += "<misc:keyValuePairs>";
            xml += "<misc:key>WelcomePack</misc:key>";
            xml += "<misc:value>WelcomePack</misc:value>";
            xml += "</misc:keyValuePairs>";
            xml += "</std:extra_trans_data>";
            xml += "<urn:sessionid>" + session + "</urn:sessionid>";
            xml += "<urn:initiator>" + init + "</urn:initiator>";
            xml += "<urn:target>" + phoneNumber + "</urn:target>";
            xml += "<urn:pin>" + pin + "</urn:pin>";
            xml += "<urn:reg_no>" + receiptNumber.ToString() + "</urn:reg_no>";
            xml += "</urn:activateRequest>";
            xml += "</urn:activate>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }


        /// <summary>
        /// Adjust the current wallet 
        /// </summary>
        /// <param name="session">UMarket session</param>
        /// <param name="source">source wallet</param>
        /// <param name="target">target wallet</param>
        /// <returns></returns>
        public static string GetAdjustWalletXML(string session, string source, string target, string comments, double amount)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:adjustWalletRequest>";
            xml += "<urn:adjustWalletRequestType>";

            xml += "<std:extra_trans_data>";

            xml += "<misc:keyValuePairs>";
            xml += "<misc:key>DirectopUp</misc:key>";
            xml += "<misc:value>" + comments + "</misc:value>";
            xml += "</misc:keyValuePairs>";
            xml += "</std:extra_trans_data>";
            xml += "<urn:sessionid>" + session + "</urn:sessionid>";


            xml += "<urn:amount>" + amount.ToString() + "</urn:amount>";

            xml += "<urn:source>" + source + "</urn:source>";

            xml += "<urn:target>" + target + "</urn:target>";

            xml += "<urn:type>1</urn:type>";
            xml += "</urn:adjustWalletRequestType>";
            xml += "</urn:adjustWalletRequest>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetAgentStatusXML(string phoneNumber, string session)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:getAgentByReference>";
            xml += "<urn:getAgentByReferenceRequest autogen=\"false\">";

            xml += "<std:sessionid>" + session + "</std:sessionid>";
            xml += "<urn:reference>" + phoneNumber + "</urn:reference>";

            xml += "</urn:getAgentByReferenceRequest>";
            xml += "</urn:getAgentByReference>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }

        public static string GetPaymentXML(string session, string amount, string pay_from, string pay_to)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:payment>";
            xml = "<urn:paymentRequest>";
            xml += "<urn:sessionid>" + session + "</urn:sessionid>";
            xml += "<urn:to>" + pay_to + "</urn:to>";
            xml += "<urn:amount>" + amount + "</urn:amount>";
            //<!--Optional:-->
            // <!--<urn:type>?</urn:type>-->
            // <!--Optional:-->
            //<urn:message>testing</urn:message>
            xml += "<urn:from>" + pay_from + "</urn:from>";
            // <!--Optional:-->
            //<urn:fee>0.05</urn:fee>
            //<!--Optional:-->
            //<urn:fee_wallet>payment_fee</urn:fee_wallet>
            xml += "</urn:paymentRequest>";
            xml += "</urn:payment>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;

        }
    }
}
