using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WorkOrderWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWorkOrder" in both code and config file together.
    [ServiceContract(Namespace = "http://oss.huawei.com/business/intf/webservice/ivr/ivrinterface", Name = "workOrder")]
    public interface IWorkOrder
    {
        [OperationContract(Name = "workOrder", Action = "workOrder")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "workOrder")]
        string workOrder(WorkOrderRequest WorkOrderRequest);


    }

    public class WorkOrderRequest
    {
        public string WorkOrderType { get; set; }
        public string SubscriberNo { get; set; }
        public string SerialNo { get; set; }
        public string OperReason { get; set; }
        public string ServiceID { get; set; }

    }
}
