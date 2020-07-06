using System;
using MySql.Data.MySqlClient;

namespace Leoner
{
    public class MySql
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        string connectionString;
        public MySql()
        {
            Initialize();
        }
        public MySqlConnection Initialize()
        {
            server = "localhost";
            database = "users";
            uid = "leoner";
            password = "jata1234";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            return connection = new MySqlConnection(connectionString);
        }
        //Open connection
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("MYSQL CONNECTED");
                return true;
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection closed");
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }    
        
    }
}
 