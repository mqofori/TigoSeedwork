using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankIntegration.Model
{
    public class AirTimePrice
    {
        public int RecordId { get; set; }
        public string session { get; set; }
        public string ctmin { get; set; }
        public string ctmax { get; set; }
        public string type { get; set; }
        public int menuId { get; set; }
        public string ctminLong { get; set; }
        public string ctmaxLong { get; set; }
        public string network { get; set; }
        public string networkName { get; set; }
        public string inputPrice { get; set; }
        public string reciever { get; set; }
    }
}
