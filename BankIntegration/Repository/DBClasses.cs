using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BankIntegration.Model;


namespace BankIntegration.Repository
{
    public static class DBClasses
    {
        static SqlConnection con;
        public static string conString = ConfigurationManager.ConnectionStrings["db_partnerbank"].ToString();

        public static bool ValidateBank(string User, string Pass)
        {
            con = new SqlConnection(conString);
            string dbUser = string.Empty;
            string qry = String.Format("SELECT BankUserName FROM [TCPartnerBank].[dbo].[UserBank] WHERE BankUserName = '{0}' and BankPassword = HASHBYTES('MD5', '{1}')", User, Pass);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); 

                while (temp_data.Read())
                {
                    dbUser = temp_data[0].ToString();
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            if (dbUser == User) return true;

            return false;
        }

        public static int GetCurrentReceiptNumber()
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = "SELECT CurrentReceiptCount FROM ReceiptCount";

            IList<UserBankModel> requests = new List<UserBankModel>();
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
            string qry = String.Format("Update ReceiptCount SET CurrentReceiptCount = {0}", currentCount);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var tmp = qryCmd.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }

        //public static void UpdateTigoCashRequest(UserBankModel request, int status)
        //{
        //    SqlConnection con = new SqlConnection(conString);
        //    // string qry = String.Format("Update Temp_Recon SET Status = {1} WHERE AutoID={0}", request.RequestID, status);
        //    string qry = String.Format("Update Request SET Status = {1} WHERE RequestID={0}", request.RequestID, status);
        //    using (SqlCommand qryCmd = new SqlCommand(qry, con))
        //    {
        //        if (con.State != ConnectionState.Open)
        //            con.Open();
        //        var tmp = qryCmd.ExecuteNonQuery();
        //    }

        //    if (con.State == ConnectionState.Open)
        //        con.Close();
        //}

        //public static void UpdateNumberOfTries(UserBankModel request)
        //{
        //    int count = 0;

        //    con = new SqlConnection(conString);
        //    string qry = String.Format("Select NumberOfTries FROM TigoCashRegistrationRequest  WHERE RequestRecordID={0}", request.RequestID);

        //    using (SqlCommand qryCmd = new SqlCommand(qry, con))
        //    {
        //        if (con.State != ConnectionState.Open)
        //            con.Open();

        //        var temp_data = qryCmd.ExecuteReader(); //get all
        //        try
        //        {
        //            while (temp_data.Read())
        //                count = int.Parse(temp_data[0].ToString());
        //        }
        //        catch (Exception e)
        //        { };

        //        qry = string.Format("UPDATE TigoCashRegistrationRequest SET NumberOfTries={0}", (count + 1).ToString());
        //        // qry = "SELECT DISTINCT PhoneNumber, AppendantProduct, ProductName   FROM DeactivationDetail WHERE Status=0";
        //        temp_data.Close();
        //        SqlCommand qryCmd_tmp = new SqlCommand(qry, con);
        //        var temp_data1 = qryCmd_tmp.ExecuteNonQuery();
        //    }

        //    if (con.State == ConnectionState.Open)
        //        con.Close();
        //}

        public static bool CheckIfAlreadyProcessed(string phoneNumber)
        {
            int count = 0;
            con = new SqlConnection(conString);
            string qry = String.Format("Select * FROM TigoCashRegistrationRequest  WHERE SubscriberMsisdn={0} AND Status=1", phoneNumber);

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

        public static void UpdateReceiptNumber(string number, int status)
        {
            SqlConnection con = new SqlConnection(conString);
            string qry = String.Format("Update Receipt SET Status={0} WHERE ReceiptNumber={1}", status.ToString(), number);

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var tmp = qryCmd.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }


        //international buy airtime
        public static class InternationalAirtime
        {
            static SqlConnection con;

            #region Bad practice and temp fix
            // static string conString = "Data Source='10.1.5.137';Initial Catalog='TcDirectTopUp';User ID='tigocash';Password='TigoCash@1234'";
            #endregion'

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static IList<AirTimePrice> GetAllRequests()
            {
                con = new SqlConnection(conString);
                string qry = "SELECT * FROM UssdPriceInput WHERE ProcessStatus=0";

                IList<AirTimePrice> requests = new List<AirTimePrice>();

                AirTimePrice ussdPrice; // = new AirTimePrice();

                using (SqlCommand qryCmd = new SqlCommand(qry, con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    var temp_data = qryCmd.ExecuteReader(); //get all

                    // int i = 1;
                    while (temp_data.Read())
                    {
                        ussdPrice = new AirTimePrice();

                        //string qry = string.Empty;
                        //if (menu == 0)
                        //    qry = string.Format("SELECT * FROM UssdPriceInput Where sessionID='{0}'", session);
                        //else
                        //    qry = string.Format("SELECT * FROM UssdPriceInput Where sessionID='{0}' AND MenuId='{1}'", session, menu);

                        //SqlConnection con = new SqlConnection(conSession);
                        //  string qry = string.Format("SELECT * FROM UssdPriceInput Where sessionID='{0}' AND MenuId='{1}'", session, menu);

                        ussdPrice.ctmin = temp_data[1].ToString();
                        ussdPrice.ctmax = temp_data[2].ToString();
                        ussdPrice.session = temp_data[3].ToString();
                        ussdPrice.type = temp_data[4].ToString();
                        ussdPrice.menuId = int.Parse(temp_data[5].ToString());
                        ussdPrice.ctminLong = temp_data[6].ToString();
                        ussdPrice.ctmaxLong = temp_data[7].ToString();
                        ussdPrice.network = temp_data[8].ToString();
                        ussdPrice.networkName = temp_data[9].ToString();
                        ussdPrice.RecordId = int.Parse(temp_data[0].ToString());
                        ussdPrice.inputPrice = temp_data[10].ToString();
                        ussdPrice.reciever = temp_data[11].ToString();

                        requests.Add(ussdPrice);

                    }

                }

                if (con.State == ConnectionState.Open)
                    con.Close();

                return requests;

            }


            public static void UpdateTigoCashRequest(AirTimePrice request, int status)
            {
                SqlConnection con = new SqlConnection(conString);
                // string qry = String.Format("Update Temp_Recon SET Status = {1} WHERE AutoID={0}", request.RequestID, status);
                string qry = String.Format("Update UssdPriceInput SET ProcessStatus = {1} WHERE ID={0}", request.RecordId, status);
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
}