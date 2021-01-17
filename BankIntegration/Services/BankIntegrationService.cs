using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using BankIntegration.Repository;

namespace BankIntegration.Services
{
    public class BankIntegrationService
    {
        public GetResults GetSessionID(string Username, string Password, string RequestType)
        {
            var results = new GetResults();
            results.ResultCode = "99";
            results.ResultDescription = "Authentication Failed. Username or Password Incorrect";

            if (DBClasses.ValidateBank(Username, Password) == false)
                return results;

            RequestType = RequestType.ToLower();
            string userSecurityStamp = Guid.NewGuid().ToString().Replace("-", "");
            // string AgentID = "123";

            byte[] _time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] _key = Encoding.ASCII.GetBytes(userSecurityStamp.Substring(0, 8));
            byte[] _Password = Encoding.ASCII.GetBytes(Password.ToString() + userSecurityStamp.Substring(8, 8));
            byte[] _RequestType = Encoding.ASCII.GetBytes(RequestType + userSecurityStamp.Substring(16, 8));
            byte[] data = new byte[_time.Length + _key.Length + _RequestType.Length + _Password.Length];

            System.Buffer.BlockCopy(_time, 0, data, 0, _time.Length);
            System.Buffer.BlockCopy(_key, 0, data, _time.Length, _key.Length);
            System.Buffer.BlockCopy(_RequestType, 0, data, _time.Length + _key.Length, _RequestType.Length);
            System.Buffer.BlockCopy(_Password, 0, data, _time.Length + _key.Length + _RequestType.Length, _Password.Length);


            results.ResultCode = "0";
            results.ResultDescription = "Success";
            results.SessionID = Convert.ToBase64String(data);

            return results;

        }

        public GetResults ValidateSessionID(string SessionID, string AgentID, string Amount, string RequestType, string ExternalID, string Comment, string Branch)
        {
            string Password = "123";

            string r = string.Empty;
            string i = string.Empty;

            var result = new GetResults();

            try
            {
                byte[] data = Convert.FromBase64String(SessionID);
                byte[] _time = data.Take(8).ToArray();
                byte[] _key = data.Skip(8).Take(8).ToArray();

                if (RequestType.ToLower() == "cashin")
                {
                    byte[] _RequestType = data.Skip(16).Take(6).ToArray();
                    byte[] _Password = data.Skip(30).ToArray();

                    r = System.Text.Encoding.ASCII.GetString(_RequestType);
                    i = System.Text.Encoding.ASCII.GetString(_Password);
                }
                else if (RequestType.ToLower() == "cashout")
                {
                    byte[] _RequestType = data.Skip(16).Take(7).ToArray();
                    byte[] _Password = data.Skip(31).ToArray();

                    r = System.Text.Encoding.ASCII.GetString(_RequestType);
                    i = System.Text.Encoding.ASCII.GetString(_Password);
                }

                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(_time, 0));


                if (when < DateTime.UtcNow.AddSeconds(-120))
                {
                    result.ValidationResults.Add(SessionIDValidationStatus.ExpiredSessionID.ToString());
                }
                #region commented out
                //Guid gKey = Guid.NewGuid();
                //
                //if (gKey.ToString() != userSecurityStamp)
                //{
                //    result.ValidationResults.Add(SessionIDValidationStatus.WrongGuid.ToString());
                //}
                #endregion
                else if (RequestType.ToLower() != (r))
                {
                    result.ValidationResults.Add(SessionIDValidationStatus.InvalidRequestType.ToString());
                }
                else if (Password.ToString() != (i.Remove(i.Length - 8)))
                {
                    result.ValidationResults.Add(SessionIDValidationStatus.InvalidUser.ToString());
                }
                else
                {
                    result.ValidationResults.Add(SessionIDValidationStatus.Successful.ToString());
                }
            }
            catch
            {
                result.ValidationResults.Add(SessionIDValidationStatus.InvalidSessionID.ToString());
            }

            result.ResultDescription = "Processing Complete";

            return result;
        }

        public class GetResults
        {
            public bool Validated { get { return ValidationResults.Count == 0; } }
            public readonly List<string> ValidationResults = new List<string>();
            public string ResultCode { get; set; }
            public string ResultDescription { get; set; }
            public string SessionID { get; set; }

        }

        public enum SessionIDValidationStatus { ExpiredSessionID, InvalidUser, InvalidSessionID, InvalidRequestType, WrongGuid, Successful }

        #region commented out
        //   public class SessionIDValidationResults
        //{
        //    public bool Validated { get { return ValidationResults.Count == 0; } }

        //    public string ResultDesc { get; set; }

        //    public readonly List<string> ValidationResults = new List<string>();
        //}

        //public class ResultCode
        //{
        //    private string _code;

        //    public ResultCode() { }

        //    public ResultCode(string code)
        //    {
        //        _code = code;
        //    }

        //    public static implicit operator ResultCode(string code)
        //    {
        //        // While not technically a requirement; see below why this is done.
        //        if (code == null)
        //            return null;

        //        return new ResultCode(code);
        //    }
        //}
        #endregion

    }
}