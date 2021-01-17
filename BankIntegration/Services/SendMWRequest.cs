using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace WorkOrder
{
    public class SendRequest
    {
        public static string CallCashOut(string initiatorMsisdn, string customerMsisdn, string amount, string reference)
        {
            string ret = string.Empty;

            var _url = "http://10.81.0.59:7777/osb/services/PurchaseInitiate_1_0";
            var _action = "http://xmlns.tigo.com/Service/PurchaseInitiate/V1/PurchaseInitiatePortType/PurchaseInitiateRequest";

            XmlDocument soapEnvelopeXml = CashoutEnvelope(initiatorMsisdn, customerMsisdn, amount, reference);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.

            string soapResult = string.Empty;
            try
            {
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }


                }
            }
            catch { }

            return ret;
        }

        public static string CallCashIn(string initiatorMsisdn, string customerMsisdn, string amount, string reference)
        {
            string ret = string.Empty;

            var _url = "http://10.11.14.4:7004/osb/services/WalletManagement_2_0";
            var _action = "http://xmlns.tigo.com/Service/WalletManagement/V2/WalletManagementPortType/CashinRequest";

            XmlDocument soapEnvelopeXml = CashinEnvelope(initiatorMsisdn, customerMsisdn, amount, reference);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.

            string soapResult = string.Empty;
            try
            {
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }


                }
            }
            catch { }

            return ret;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CashoutEnvelope(string initiatorMsisdn, string customerMsisdn, string amount, string reference)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            string xml = string.Empty;

            xml += "<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v1=\"http://xmlns.tigo.com/MFS/PurchaseInitiateRequest/V1\" xmlns:v2=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\">";
            xml += "  <SOAP-ENV:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            xml += "    <cor:debugFlag xmlns:cor=\"http://soa.mic.co.af/coredata_1\">true</cor:debugFlag>";
            xml += "    <wsse:Security>";
            xml += "      <wsse:UsernameToken>";
            xml += "        <wsse:Username>live_mw_csd</wsse:Username>";
            xml += "        <wsse:Password>tiGOcSD@123</wsse:Password>";
            xml += "      </wsse:UsernameToken>";
            xml += "    </wsse:Security>";
            xml += "  </SOAP-ENV:Header>";
            xml += "  <SOAP-ENV:Body>";
            xml += "    <v1:PurchaseInitiateRequest>";
            xml += "      <v3:RequestHeader>";
            xml += "        <v3:GeneralConsumerInformation>";
            xml += "          <v3:consumerID>TIGO</v3:consumerID>";
            xml += "          <v3:transactionID>" + reference + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "</v3:transactionID>";
            xml += "          <v3:country>GHA</v3:country>";
            xml += "          <v3:correlationID>" + reference + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "</v3:correlationID>";
            xml += "        </v3:GeneralConsumerInformation>";
            xml += "      </v3:RequestHeader>";
            xml += "      <v1:requestBody>";
            xml += "        <v1:customerAccount>";
            xml += "          <v1:msisdn>233" + customerMsisdn + "</v1:msisdn>";
            xml += "        </v1:customerAccount>";
            xml += "        <v1:initiatorAccount>";
            xml += "          <v1:msisdn>233" + initiatorMsisdn + "</v1:msisdn>";
            xml += "        </v1:initiatorAccount>";
            xml += "        <v1:paymentReference>" + reference + "</v1:paymentReference>";
            xml += "        <v1:externalCategory>default</v1:externalCategory>";
            xml += "        <v1:externalChannel>default</v1:externalChannel>";
            xml += "        <v1:webUser>tigocash_csd</v1:webUser>";
            xml += "        <v1:webPassword>Master@12</v1:webPassword>";
            xml += "        <v1:merchantName>" + reference + "</v1:merchantName>";
            xml += "        <v1:itemName>cashout</v1:itemName>";
            xml += "        <v1:amount>" + amount + "</v1:amount>";
            xml += "        <v1:minutesToExpire>7</v1:minutesToExpire>";
            xml += "        <v1:notificationChannel>2</v1:notificationChannel>";
            xml += "      </v1:requestBody>";
            xml += "    </v1:PurchaseInitiateRequest>";
            xml += "  </SOAP-ENV:Body>";
            xml += "  </SOAP-ENV:Envelope>";

            soapEnvelop.LoadXml(xml);

            return soapEnvelop;
        }

        private static XmlDocument CashinEnvelope(string initiatorMsisdn, string customerMsisdn, string amount, string reference)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            string xml = string.Empty;

            xml += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:v2=\"http://xmlns.tigo.com/MFS/WalletManagementRequest/V2\" xmlns:v3=\"http://xmlns.tigo.com/RequestHeader/V3\" xmlns:v21=\"http://xmlns.tigo.com/ParameterType/V2\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">";
            xml += "   <soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            xml += "      <cor:debugFlag>true</cor:debugFlag>";
            xml += "      <wsse:Security>";
            xml += "         <wsse:UsernameToken>";
            xml += "            <wsse:Username>test_mw_osb</wsse:Username>";
            xml += "            <wsse:Password>tigo1234</wsse:Password>";
            xml += "         </wsse:UsernameToken>";
            xml += "      </wsse:Security>";
            xml += "   </soapenv:Header>";
            xml += "   <soapenv:Body>";
            xml += "      <v2:MoneyTransferRequest>";
            xml += "      <v3:RequestHeader>";
            xml += "        <v3:GeneralConsumerInformation>";
            xml += "          <v3:consumerID>TIGO</v3:consumerID>";
            xml += "          <v3:transactionID>" + reference + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "</v3:transactionID>";
            xml += "          <v3:country>GHA</v3:country>";
            xml += "          <v3:correlationID>" + reference + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "</v3:correlationID>";
            xml += "        </v3:GeneralConsumerInformation>";
            xml += "      </v3:RequestHeader>";
            xml += "         <v2:requestBody>";
            xml += "            <v2:sourceWallet>";
            xml += "               <v2:msisdn>" + customerMsisdn + "</v2:msisdn>";
            xml += "            </v2:sourceWallet>";
            xml += "            <v2:targetWallet>";
            xml += "               <v2:msisdn>" + initiatorMsisdn + "</v2:msisdn>";
            xml += "            </v2:targetWallet>";
            xml += "            <v2:password>8002</v2:password>";
            xml += "            <v2:amount>" + amount + "</v2:amount>";
            xml += "         </v2:requestBody>";
            xml += "      </v2:MoneyTransferRequest>";
            xml += "   </soapenv:Body>";
            xml += "</soapenv:Envelope>";


            soapEnvelop.LoadXml(xml);

            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}