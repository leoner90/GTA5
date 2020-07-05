using System;
using MySql.Data.MySqlClient;

namespace Leoner
{
    public class MySql
    {
        public MySql()
        {
            string myConnectionString = "Database=users;Data Source=localhost;User Id=leoner;Password=jata1234";
            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
 