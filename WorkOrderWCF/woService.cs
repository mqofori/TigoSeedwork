using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;


namespace WorkOrderWCF
{
    public class woService
    {
        public static string CallWebService(string WorkOrderType, string SubscriberNo)
        {
            var _url = "http://10.81.0.59:7777/osb/services/WorkOrder";
            var _action = "OrderRequest";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(WorkOrderType, SubscriberNo);
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

            if (soapResult.Contains("<cbs:ResultDesc>Success</cbs:ResultDesc>"))
                return "Success";

            return "Fail";
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

        private static XmlDocument CreateSoapEnvelope(string WorkOrderType, string SubscriberNo)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            string xml = string.Empty;

            //xml += "<soapenv:Envelope xmlns:wsa=\"http://www.w3.org/2005/08/addressing\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\">";
            //xml += "   <soapenv:Body>";
            //xml += "      <workOrder xmlns=\"http://oss.huawei.com/business/intf/webservice/ivr/ivrinterface\">";
            //xml += "         <WorkOrderRequest>";
            //xml += "            <WorkOrderType>41</WorkOrderType>";
            //xml += "            <SubscriberNo>277105051</SubscriberNo>";
            //xml += "            <SerialNo>172330101</SerialNo>";
            //xml += "            <OperReason>0</OperReason>";
            //xml += "            <ServiceID>120</ServiceID>";
            //xml += "         </WorkOrderRequest>";
            //xml += "      </workOrder>";
            //xml += "   </soapenv:Body>";
            //xml += "</soapenv:Envelope>";

            xml += "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:cbs=\"http://oss.huawei.com/business/intf/webservice/cbs\" xmlns:cor=\"http://soa.mic.co.af/coredata_1\">";
            xml += "   <soapenv:Header xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            xml += "      <cor:debugFlag>true</cor:debugFlag>";
            xml += "      <wsse:Security>";
            xml += "         <wsse:UsernameToken>";
            xml += "            <wsse:Username>Test_mw_osb</wsse:Username>";
            xml += "            <wsse:Password>tigo1234</wsse:Password>";
            xml += "         </wsse:UsernameToken>";
            xml += "      </wsse:Security>";
            xml += "   </soapenv:Header>";
            xml += "   <soapenv:Body>";
            xml += "      <cbs:WorkOrderRequestMsg>";
            xml += "         <cbs:WorkOrderRequest>";
            xml += "            <cbs:msisdn>233" + SubscriberNo + "</cbs:msisdn>";
            xml += "            <cbs:WorkOrderType>" + WorkOrderType + "</cbs:WorkOrderType>";
            xml += "         </cbs:WorkOrderRequest>";
            xml += "      </cbs:WorkOrderRequestMsg>";
            xml += "   </soapenv:Body> ";

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