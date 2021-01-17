using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Linq;
using System.Web;


using System.Data.SqlClient;
using System.Data;

namespace PhoneDetection
{
    public class Data
    {
        public int AutoID { get; set; }
        public string msisd { get; set; }
        public int status { get; set; }
        public string imsi { get; set; }
        public string imei { get; set; }
        public string model { get; set; }
        public string make{get; set;}
    }
        
}

namespace PhoneDetection
{
    public  static class Repository
    {
        static SqlConnection con;

        # region Bad pratice and temp fix
        static string conString = "Data Source='10.1.4.242';Initial Catalog='temp';User ID='sa';Password='P@55w0rd'";

        //static string directTopUpConnectionString = "Data Source='10.1.5.137';Initial Catalog='TcDirectTopUp';User ID='tigocash';Password='P@$$w0rd1'";
        #endregion

        public static void UpdateTable(Data data)
        {
          
            con = new SqlConnection(conString);
            string qry = string.Format("UPDATE tmp SET status='{0}', imei='{1}', model='{2}' , make='{3}' WHERE AutoId='{4}'", "1", data.imei , data.model,data.make,data.AutoID);

            // qry = string.Format(qry, session, number, step);
            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var tmp = qryCmd.ExecuteNonQuery();
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }



        public static IList<Data> GetAllRequests()
        {
            con = new SqlConnection(conString);
            string qry = "SELECT * FROM tmp WHERE Status=0";

            IList<Data> requests = new List<Data>();

            using (SqlCommand qryCmd = new SqlCommand(qry, con))
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                var temp_data = qryCmd.ExecuteReader(); //get all

                // int i = 1;
                while (temp_data.Read())
                {
                    Data req = new Data();
                    req.AutoID = int.Parse(temp_data[0].ToString());
                    req.msisd = string.Format("233{0}", temp_data[1].ToString());
                    req.status = int.Parse(temp_data[2].ToString());
                    req.imsi = temp_data[3].ToString();
                    req.imei = temp_data[4].ToString();
                    req.model = temp_data[5].ToString();
                    req.make = temp_data[6].ToString();
                    requests.Add(req);
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();


            return requests;
        }
    }

}