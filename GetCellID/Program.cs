using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

 
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using SubscriberManagement.Domain.SMS;

using GetCellID.ServiceReference1;


using System.Threading;
namespace GetCellID
{
    class Program
    {
        static void Main(string[] args)
        {
            int i=1;
            while (i == 1)
            {
                GetCellIdSoapClient getCellIdSoapClient = new GetCellIdSoapClient();
                //GetCellIDRequest req = new GetCellIDRequest();
                //GetCellIDRequestBody body = new GetCellIDRequestBody();

                //body.msisdn = "233571139474";

                //req.Body = body;

                // GetCellIDResponse response = getCellIdSoapClient.GetCellID("233571139474");

                string msg = string.Empty;

                if (getCellIdSoapClient.State != System.ServiceModel.CommunicationState.Opened)
                    getCellIdSoapClient.Open();


                string response = getCellIdSoapClient.GetCellID("233576768417"); // Chamberlain 

                msg = msg + "C-" + GetLocation(response);

                response = getCellIdSoapClient.GetCellID("233578549528");

                msg = msg + "=E-" + GetLocation(response);
                //233574956552-n
                response = getCellIdSoapClient.GetCellID("233275188225");

                msg = msg + "=M-" + GetLocation(response);

                response = getCellIdSoapClient.GetCellID("233576227495");
                msg = msg + "=O-" + GetLocation(response);

                SMSService.SendSMSMiddleware("Tigo", "0270369883", msg);
                System.Threading.Thread.Sleep(690000);
            }
        }


        public static string GetLocation(string cellId)
        {
            string location; 

            SqlConnection con;                    
            string conString = "Data Source='10.1.1.95';Initial Catalog='tmp_PSO';User ID='sa';Password='Millicom123'";
            

            con = new SqlConnection(conString);
            string qry = "SELECT * FROM celltable where cellid='" + cellId +"'";
                        
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all

                // int i = 1;
                while (temp_data.Read())
                {
                    location = string.Format("{0}-{1}", temp_data[1].ToString(), temp_data[2].ToString()); 

                    if (con.State == ConnectionState.Open)
                        con.Close();

                    return location;
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            return "None";
        }
    }
}
