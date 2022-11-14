using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneCcnversations
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaulConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString.ToString());
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("select abonents.surName as abonentSurName, abonents.lastName as abonentLastName," +
                "abonents.fisrtName as abonentFisrtName, cities.name as cityName," +
                "dateConversation,duration from conversations inner join abonents on conversations.abonentId=abonents.id " +
                "inner join cities on conversations.cityId=cities.id", conn);
            SqlDataReader dr = await cmd.ExecuteReaderAsync();
            Console.WriteLine("{0,-35} {1,-15} {2,-10} {3,-20}\n", "Abonent", "City", "Duration", "Date conversation");
            while (dr.Read())
            {
                string abonentSurName = dr["abonentSurName"].ToString() + " " + dr["abonentLastName"].ToString() + " " + dr["abonentFisrtName"].ToString();
                string cityName = dr["cityName"].ToString();
                string dateConversation = dr["dateConversation"].ToString();
                string duration = dr["duration"].ToString() + " min";
                Console.WriteLine("{0,-35} {1,-15} {2,-10} {3,-20}", abonentSurName, cityName, duration, dateConversation);
            }
            dr.Close();
            conn.Close();
            Console.Read();
        }
    }
 }
