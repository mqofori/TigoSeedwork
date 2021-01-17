using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace PhoneDetection
{
    class Program
    {
        static void Main(string[] args)
        {
            string response = string.Empty;
            int i = 0;

            IList<Data> datas = Repository.GetAllRequests();
            foreach (var data in datas)
            {
                
                response = GetFullIMEI(GetIMESI(data.msisd));
                                
                data.imei = GetOTAIMEI(response);
               // data.imsi = GetIMESI(response);
                data.make = GetOTAMake(response);
                data.model = GetOTAModel(response);
              //  response = RemoveSpecialCharacters(response);
              //  Data dt = new Data();
                //dt = (Data)CreateObject(response, dt);

                Repository.UpdateTable(data);
                Console.WriteLine(i++);
            }

          //  string response = GetIMESI(data);
        }


        public static string GetIMESI(string phoneNumber)
        {
            string url = string.Format("http://p12192.mobilethink.net/subscriber-service/subscribers/{0}", phoneNumber);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            return stream.ReadToEnd().Trim();
        }


        //public static string CreateXML(Data YourClassObject)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
        //    // Initializes a new instance of the XmlDocument class.          
        //    XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
        //    // Creates a stream whose backing store is memory. 
        //    using (MemoryStream xmlStream = new MemoryStream())
        //    {
        //        xmlSerializer.Serialize(xmlStream, YourClassObject);
        //        xmlStream.Position = 0;
        //        //Loads the XML document from the specified string.
        //        xmlDoc.Load(xmlStream);
        //        return xmlDoc.InnerXml;
        //    }
        //}


        public static Object CreateObject(string XMLString,Object YourClassObject)
        {            
            XmlSerializer oXmlSerializer =new XmlSerializer(YourClassObject.GetType()); 

                //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString)); 
                //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;

        }

        public  static string GetFullIMEI(string MSISDN)
        {

            char[] array = MSISDN.ToCharArray();
            int total = 0;
            int realtot = 0;

            for (int i = 1; i < array.Length; i += 2)
            {
                char num = array[i];
                int val = (int)Char.GetNumericValue(num);
                //   Console.WriteLine(val * 2);
                total = total + (val * 2);

                if (total > 9)
                {
                    int firstDigit = total / 10;
                    int secondDigit = total % 10;
                    total = firstDigit + secondDigit;
                }

                realtot = realtot + total;
            }

            for (int i = 0; i < array.Length; i += 2)
            {
                char num = array[i];
                int val = (int)Char.GetNumericValue(num);
                // Console.WriteLine(val);
                realtot = realtot + val;
            }
            int check = (realtot * 9) % 10;

            return MSISDN + check.ToString();

        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9')    
                    || (str[i] >= 'A' && str[i] <= 'z'
                        || (str[i] == '.' || str[i] == '_') ||
                        (str[i] == '<' || str[i] == '>' || str[i] == '/' || str[i] == '"' || str[i] == '?' || str[i] == ' ' )))
                {
                    sb.Append(str[i]);
                    
                }
            }

            return sb.ToString();
        }

        public static string GetOTAIMEI(string xml)
        {
            string ret = "N/A";
            if (xml.Contains("<imei>"))
            {
                int ind = xml.IndexOf("<imei>") + 6;
                xml = xml.Substring(ind, 14);
            }
            if (xml.Contains("?xml"))
            { xml = ret; }
            return xml;

        }
        public static string GetOTAMake(string xml)
        {
            string ret = "N/A";
            if (xml.Contains("<make>"))
            {
                int ind = xml.IndexOf("<make>") + 6;
                xml = xml.Substring(ind, xml.IndexOf("</make>") - ind);
            }
            if (xml.Contains("?xml"))
            { xml = ret; }
            return xml;

        }
        public static string GetOTAModel(string xml)
        {
            string ret = "N/A";
            if (xml.Contains("<model>"))
            {
                int ind = xml.IndexOf("<model>") + 7;
                xml = xml.Substring(ind, xml.IndexOf("</model>") - ind);
            }
            if (xml.Contains("?xml"))
            { xml = ret; }
            return xml;

        }



    }
}
