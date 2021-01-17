using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WorkOrderWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WorkOrder" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WorkOrder.svc or WorkOrder.svc.cs at the Solution Explorer and start debugging.
    public class WorkOrder : IWorkOrder
    {
        public string workOrder(WorkOrderRequest WorkOrderRequest)
        {
            return woService.CallWebService(WorkOrderRequest.WorkOrderType, WorkOrderRequest.SubscriberNo);
        }

       // public class WorkOrderRequest
       // {
       //     public string WorkOrderType { get; set; }
       //     public string SubscriberNo { get; set; }
       //     public string SerialNo { get; set; }
       //     public string OperReason { get; set; }
       //     public string ServiceID { get; set; }
       //
       // }
    }
}
