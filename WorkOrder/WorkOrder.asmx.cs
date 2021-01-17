using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WorkOrder
{
    /// <summary>
    /// Summary description for WorkOrder
    /// </summary>
    [WebService(Namespace = "http://oss.huawei.com/business/intf/webservice/ivr/ivrinterface")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WorkOrder : System.Web.Services.WebService
    {

        [WebMethod]
        [SoapDocumentMethod(Action = "workOrder")]
        public WorkOrderResult workOrder(WorkOrderRequest WorkOrderRequest)
        {
            return SendRequest.CallWebService(WorkOrderRequest.WorkOrderType, WorkOrderRequest.SubscriberNo);
        }

        public class WorkOrderRequest
        {
            public string WorkOrderType { get; set; }
            public string SubscriberNo { get; set; }
            public string SerialNo { get; set; }
            public string OperReason { get; set; }
            public string ServiceID { get; set; }

        }

        public class WorkOrderResult
        {
            public string ResultCode { get; set; }
            public string ResultDesc { get; set; }
        }

    }
}
