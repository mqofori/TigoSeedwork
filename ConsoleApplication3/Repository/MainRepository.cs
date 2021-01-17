using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SubscriberManagement.Domain;

using ConsoleApplication3.Model;
namespace ConsoleApplication3.Repository
{
    public static class MainRepository
    {
        static SqlConnection con;

        # region Bad pratice and temp fix
        static string conString = "Data Source='10.1.5.137';Initial Catalog='TigoCashWelcomPackage';User ID='tigocash';Password='TigoCash@1234'"; 
        #endregion 


        public static IList<TempModel> TempGetAllRequests()
        {
            con = new SqlConnection(conString);
            string qry = "SELECT * FROM Temp1 WHERE Status=0";

            IList<TempModel> requests = new List<TempModel>();

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all

                // int i = 1;
                while (temp_data.Read())
                {
                    TempModel req = new TempModel();
                    req.phoneNumber = temp_data[0].ToString();
                    req.amount = temp_data[1].ToString();
                

                    requests.Add(req);
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return requests;
        }

        public static void TempUpdateNumber(TempModel request)
        {
            SqlConnection con = new SqlConnection(conString);
                string qry = string.Format("UPDATE Temp1 SET Status=5 WHERE phoneNumber='{0}'  AND Status=0", request.phoneNumber);
                // qry = "SELECT DISTINCT PhoneNumber, AppendantProduct, ProductName   FROM DeactivationDetail WHERE Status=0";


                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand qryCmd_tmp = new SqlCommand(qry, con);
                var temp_data1 = qryCmd_tmp.ExecuteNonQuery();
           

            if (con.State == ConnectionState.Open)
                con.Close();
        }        




        public static IList<RequestModel> GetAllRequests()
        {            
            con = new SqlConnection(conString);
            string qry = "SELECT * FROM TigoCashRegistrationRequest WHERE Status=0";
                       
            IList<RequestModel> requests = new List<RequestModel>();

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all
                
               // int i = 1;
                while (temp_data.Read())
                {
                    RequestModel req = new RequestModel();
                    req.ID = int.Parse(temp_data[0].ToString());
                    req.RequestBy =string.Format("0{0}", SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(temp_data[2].ToString()));
                    req.SubscriberNumber = string.Format("0{0}", SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(temp_data[3].ToString()));
                    req.SubscriberAddress = string.Format("0{0}", SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(temp_data[3].ToString()));
                    req.SubscriberName = temp_data[4].ToString();
                    req.SubscriberAddress = temp_data[5].ToString();
                    req.SubscriberIdNumber = temp_data[6].ToString();
                    req.SubscriberIdType= temp_data[7].ToString();
                    DateTime dt= DateTime.Parse(temp_data[8].ToString());
                   // 22071983
                   // 20012014
                    //dd/mm/yyyy
                    string day= dt.Day.ToString();

                    if (day.Length< 2 )
                        day= string.Format("0{0}",day);

                    string month = dt.Month.ToString();

                    if (month.Length < 2)
                        month = string.Format("0{0}", month);

                    string dob = string.Format("{0}{1}{2}", day , month, dt.Year);
                    req.DOB = dob;
                    req.Status =int.Parse( temp_data[9].ToString());

                    requests.Add(req);
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return requests;
        }
        
        public static int GetCurrentReceiptNumber()
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = "SELECT CurrentReceiptCount FROM ReceiptCount";
           
            IList<RequestModel> requests = new List<RequestModel>();
            int count = 0;
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all
                while (temp_data.Read())
                    count = int.Parse(temp_data[0].ToString());
            }

            if (con.State == ConnectionState.Open)
                con.Close();
            return count;               
        }

        public static void UpdateReceiptNumber(int currentCount)
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = String.Format("Update ReceiptCount SET CurrentReceiptCount = '{0}'", currentCount);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                 var tmp= qryCmd.ExecuteNonQuery();
             }

            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public static void UpdateTigoCashRequest(RequestModel request, int status)
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = String.Format("Update TigoCashRegistrationRequest SET Status = {1} WHERE RequestRecordID='{0}'", request.ID, status);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                var tmp = qryCmd.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public static void UpdateNumberOfTries(RequestModel request)
        {
            int count = 0;
            
            con = new SqlConnection(conString);
            string qry = String.Format("Select NumberOfTries FROM TigoCashRegistrationRequest  WHERE RequestRecordID='{0}'", request.ID);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all
                try
                {
                    while (temp_data.Read())
                        count = int.Parse(temp_data[0].ToString());
                }
                catch (Exception e)
                { };

                qry= string.Format("UPDATE TigoCashRegistrationRequest SET NumberOfTries='{0}'", (count+1).ToString());
               // qry = "SELECT DISTINCT PhoneNumber, AppendantProduct, ProductName   FROM DeactivationDetail WHERE Status=0";
                temp_data.Close();
                SqlCommand qryCmd_tmp = new SqlCommand(qry, con);
                 var  temp_data1 = qryCmd_tmp.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }        

        public static bool OnWhileList(RequestModel request)
        {
            int count = 0;
            con = new SqlConnection(conString);
            string qry = String.Format("Select * FROM Whitelist  WHERE phoneNumber='{0}'", SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(request.RequestBy));

            bool onlist= false;
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {

                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all

                while (temp_data.Read())
                {
                    string num = SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(temp_data[1].ToString());
                    string comp = SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(request.RequestBy.ToString());
                    if (comp == num)
                        onlist = true;
                }

                if (con.State == ConnectionState.Open)
                    con.Close();
                return onlist;
            }                        
        }
        
        public static bool CheckIfAlreadyProcessed(string phoneNumber) 
        {
            int count = 0;
            con = new SqlConnection(conString);
            string qry = String.Format("Select * FROM TigoCashRegistrationRequest  WHERE SubscriberMsisdn='{0}' AND Status=1",phoneNumber) ;

            bool onlist = false;
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {

                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all

                while (temp_data.Read())
                {
                    string num = SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(temp_data[1].ToString());
                    string comp = SubscriberManagement.Domain.GeneralService.OCSFormatMSISDN(phoneNumber);
                    if (comp == num)
                        onlist = true;
                }

                if (con.State == ConnectionState.Open)
                    con.Close();
                return onlist;
            } 
        }

        public static ReceiptModel GetReceipt()
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = "SELECT * FROM Receipt WHERE Status=0";

            
            ReceiptModel request = new ReceiptModel();
            int count = 0;
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all
                while (temp_data.Read())
                {
                    request.ID = int.Parse(temp_data[0].ToString());
                    request.ReceiptNumber = temp_data[0].ToString();
                    request.Status = temp_data[1].ToString();
                    break;
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return request;
        }

        public static void UpdateReceiptNumber(string number,int status)
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = String.Format("Update Receipt SET Status={0} WHERE ReceiptNumber='{1}'", status.ToString(), number);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var tmp = qryCmd.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }
    
    }
}
