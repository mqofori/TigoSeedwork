using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication3.Model
{
    public class RequestModel
    {
        public int ID{set; get;}
        public string RequestBy { set; get; }
       // public string PhoneNumber { set; get; }
        public string SubscriberName { set; get; }
        public string SubscriberNumber { set; get; }
        
        public string SubscriberIdType { set; get; }
        public string SubscriberIdNumber { set; get; }
        public string SubscriberAddress { set; get; }
        public string DOB { set; get; }
        public int Status { get; set; }
    }

    public class TempModel
    {
        public string phoneNumber { set; get; }
        public string amount { set; get; }
    }
}
