using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankIntegration.Model
{
    public class UserBankModel
    {
        public int BankID { get; set; }
        public string BankDescription { get; set; }
        public string BankUserName { get; set; }
        public string BankPassword { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}