using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
namespace ConsoleApplication3.SoapClasses
{
    public static class GetXMLCommands
    {


        public static string GetRegisterPayment(string phoneNumber, string amount)
        {
            string xml="<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v1=\"http://xmlns.tigo.com/RegisterPaymentRequest/V1\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\" xmlns:v2=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">";

            xml += "<soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            xml += "<cor:debugFlag>true</cor:debugFlag>";
            xml += "<wsse:Security>";
            xml += "<wsse:UsernameToken>";
            xml += "<wsse:Username>test_mw_osb</wsse:Username>";
            xml += "<wsse:Password>tigo1234</wsse:Password>";
            xml += "</wsse:UsernameToken>";
            xml += "</wsse:Security>";
            xml += "</soapenv:Header>";
            xml += "<soapenv:Body>";
            xml += "<v1:RegisterPaymentRequest>";
            xml += "<v3:RequestHeader>";
            xml += "<v3:GeneralConsumerInformation>";
              
            xml += "<v3:consumerID>POS</v3:consumerID>";
               
            xml += "<v3:transactionID>47832</v3:transactionID>";
            xml += "<v3:country>GHA</v3:country>";
            xml += "<v3:correlationID>Pos_repayment</v3:correlationID>";
            xml += "</v3:GeneralConsumerInformation>";
            xml += "</v3:RequestHeader>";
            xml += "<v1:RequestBody>";
     
            xml += "<v1:msisdn>" + phoneNumber + "</v1:msisdn>"     ; 
            xml += "<v1:paymentDetail>";
            xml += "<v1:paymentMethod>Cash</v1:paymentMethod>";
            xml += "<v1:amount>"+ amount +"</v1:amount>";
            xml += "<v1:remark>POS Manuel reprocessing</v1:remark>";
            xml += "</v1:paymentDetail>";
            xml += "</v1:RequestBody>";
            xml += "</v1:RegisterPaymentRequest>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";

            return xml;
        }

        /// <summary>
        /// Get UMarket session
        /// </summary>
        /// <returns></returns>
        public static string GetSessionXML()
        {
              string xml= "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\">";
           xml += "<soapenv:Header/>";
           xml += "<soapenv:Body>";
           xml += "<urn:createsession/>";
           xml += "</soapenv:Body>";
           xml += "</soapenv:Envelope>";

           return xml;
        }


        /// <summary>
        /// Get login XML
        /// </summary>
        /// <param name="msisdn"></param>
        /// <param name="pin"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static string GetLoginXML(string msisdn, string pin, string session)
        {
            string xml="<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
               xml += "<soapenv:Header/>";
               xml += "<soapenv:Body>";
               xml +=   "<urn:login>";
               xml+=  "<urn:loginRequest>";
              
               xml +="<urn:sessionid>"+ session +"</urn:sessionid>";            
               xml += "<urn:initiator>" +  msisdn + "</urn:initiator>";
               xml += "<urn:pin>" + pin + "</urn:pin>";
               xml += "</urn:loginRequest>";
               xml += "</urn:login>";
               xml += "</soapenv:Body>";
               xml += "</soapenv:Envelope>";

               return xml;
        }
              
        /// <summary>
        /// Get register xml
        /// </summary>
        /// <param name="register"></param>
        /// <param name="session"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="nationality"></param>
        /// <param name="dob"></param>
        /// <param name="idType"></param>
        /// <param name="idNumber"></param>
        /// <returns></returns>
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
                xml += "<misc:value>" + register +"</misc:value>";
                xml += "</misc:keyValuePairs>";
                xml += "</std:extra_trans_data>";
                xml += "<urn:sessionid>"  + session + "</urn:sessionid>";
                xml += "<urn:agent>" + phoneNumber + "</urn:agent>";
                xml += "<urn:name>" + name   + "</urn:name>";
                xml += "<urn:ad_first_name>" + name + "</urn:ad_first_name>";
                xml += "<urn:ad_address>" +  address + "</urn:ad_address>";
                xml += "<urn:agent_category>cashsubscriber</urn:agent_category>";
                xml += "<urn:ad_nationality>" + nationality+ "</urn:ad_nationality>";
                xml += "<urn:ad_dob>"+ dob +"</urn:ad_dob>";
               xml += "<urn:ad_alt_id_type>" + idType + "</urn:ad_alt_id_type>";
               xml += "<urn:ad_alt_id>" + idNumber +"</urn:ad_alt_id>";
               xml += "<urn:ad_tigo_bank_agent>" + agent + "</urn:ad_tigo_bank_agent>";
               xml += "</urn:registerRequest>";
               xml += "</urn:register>";
               xml += "</soapenv:Body>";
               xml += "</soapenv:Envelope>";

               return xml;
        }

        public static string GetActivateXML(string session, string phoneNumber, string init, string pin, string receiptNumber)
         {          
            string xml= "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml +="<soapenv:Body>";
            xml += "<urn:activate>";
            xml += "<urn:activateRequest>";
            
            xml += "<std:extra_trans_data>";
               
            xml +="<misc:keyValuePairs>";
            xml +="<misc:key>WelcomePack</misc:key>";
            xml +="<misc:value>WelcomePack</misc:value>";
            xml +="</misc:keyValuePairs>";
            xml +="</std:extra_trans_data>";
            xml +="<urn:sessionid>" + session +"</urn:sessionid>";
            xml += "<urn:initiator>" + init + "</urn:initiator>";
            xml +="<urn:target>" +  phoneNumber  + "</urn:target>";
            xml +="<urn:pin>" +   pin + "</urn:pin>";
            xml +="<urn:reg_no>" + receiptNumber.ToString() + "</urn:reg_no>";
            xml +="</urn:activateRequest>";
            xml +="</urn:activate>";
            xml +="</soapenv:Body>";
            xml +="</soapenv:Envelope>";

            return xml;
         }


       /// <summary>
       /// Adjust the current wallet 
       /// </summary>
       /// <param name="session">UMarket session</param>
       /// <param name="source">source wallet</param>  
       /// <param name="target">target wallet</param>
       /// <returns></returns>
        public static string GetAdjustWalletXML(string session, string source, string target)
        {
            string xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:urn=\"urn:UMARKETSCWS\" xmlns:std=\"http://www.utiba.com/delirium/ws/StdQuery\" xmlns:misc=\"http://www.utiba.com/delirium/ws/Misc\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<urn:adjustWalletRequest>";
            xml += "<urn:adjustWalletRequestType>";

            xml += "<std:extra_trans_data>";

            xml += "<misc:keyValuePairs>";
            xml += "<misc:key>WelcomePack</misc:key>";
            xml += "<misc:value>WelcomePackage</misc:value>";
            xml += "</misc:keyValuePairs>";
            xml += "</std:extra_trans_data>";
            xml += "<urn:sessionid>" + session + "</urn:sessionid>";


            xml += "<urn:amount> 1.0</urn:amount>";

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
            xml +="<soapenv:Header/>";
            xml +="<soapenv:Body>";
            xml +="<urn:getAgentByReference>";
            xml +="<urn:getAgentByReferenceRequest autogen=\"false\">";
       
            xml +="<std:sessionid>"+ session + "</std:sessionid>";
            xml +="<urn:reference>"+ phoneNumber +"</urn:reference>";
           
            xml +="</urn:getAgentByReferenceRequest>";
            xml += "</urn:getAgentByReference>";
            xml +="</soapenv:Body>";
            xml +="</soapenv:Envelope>";

            return xml;
        }
        

    }
}
