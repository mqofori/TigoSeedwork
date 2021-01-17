using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankIntegration.Model
{
    public class RequestModel
    {
        public int RequestID { get; set; }
        public string RequesterNumber { get; set; }
        public int Network { get; set; }
        public double Amount { get; set; }
        public string ReceiverNumber { get; set; }
        public int Status { get; set; }
    }
}
