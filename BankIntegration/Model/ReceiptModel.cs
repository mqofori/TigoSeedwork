using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankIntegration.Model
{
    public class ResponseModel
    {
        //{"message":"Sorry your request cannot be processed now. Try again later","balance":98.66,"local-trxn-code":"28004524720666233634","status-code":"1","trxn":"TC_Transaction","status":"FAILED"}

        public int ID{ get; set;}
        public string message { get; set; }
        public string balance { get; set; }      
        //public string local-trxn-code { get; set; } 
        //public string status-code { get; set; } 
        public string trxn { get; set; } 
        public string status { get; set; } 
    }
}
