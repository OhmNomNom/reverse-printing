using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Orca.Models;

namespace Orca
{
    public class DB
    {
        SqlConnection conn;

        public void addDonation(Addition val, string actor)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Donation(AUBnet,Kilos,Actor) VALUES(@aubnet,@kilos,@actor)";

            command.Parameters.Add("@aubnet", System.Data.SqlDbType.Char, val.AUBnet.Length).Value = val.AUBnet;

            SqlParameter kilos = new SqlParameter("@kilos", System.Data.SqlDbType.Decimal, 18);
            kilos.Precision = 3;

            command.Parameters.Add("@actor", System.Data.SqlDbType.Char, actor.Length).Value = actor;

            command.Parameters.Add(kilos).Value = val.Kilos;

            command.Prepare();
            command.ExecuteNonQuery();
        }

        public SqlDataReader getDonations(DateTime StartDate, DateTime EndDate)
        {
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT AUBnet, Kilos, Timestamp FROM Donation WHERE Timestamp BETWEEN @start and @end";


            command.Parameters.Add("@start", System.Data.SqlDbType.Date, 8).Value = StartDate.Subtract(TimeSpan.FromSeconds(1));


            command.Parameters.Add("@end", System.Data.SqlDbType.Date, 8).Value = EndDate.AddDays(1);

            command.Prepare();
            return command.ExecuteReader();
        }

        public DB()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString);
            conn.Open();
        }

        ~DB()
        {
            //conn.Close();
            //conn.Dispose();
        }


    }
}