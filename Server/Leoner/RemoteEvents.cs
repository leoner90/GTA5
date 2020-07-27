using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace Leoner
{
    class RemoteEvents : Script
    {
        [RemoteEvent]
        public void serverTimeAndWeather(Client player, int time, string weather)
        {
            NAPI.World.SetTime(time, 0, 0);
            NAPI.World.SetWeather(weather);
        }
        //PLAYER DISCONNECTED
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
         {
            Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
            float posX = PlayerPos.X;
            float posY = PlayerPos.Y;
            float posZ = PlayerPos.Z;
            //TO DO: GET PLAYER ID in proper way instead of login assign id to user on login
            string user = player.Name;
            MySql newConnection = new MySql();
            MySqlConnection mysqlConnection = newConnection.Initialize();
            MySqlCommand command = mysqlConnection.CreateCommand();
            newConnection.OpenConnection();

            command.CommandText = "SELECT * FROM accounts WHERE login = @username ";
            command.Parameters.AddWithValue("@username", user);
            MySqlDataReader reader2 = command.ExecuteReader();
            reader2.Read();
            int character1ID = reader2.GetInt32("character1");
            reader2.Close();
            command.CommandText = "UPDATE charac SET posX = @posX , posY = @posY, posZ = @posZ WHERE id = @id;";
            command.Parameters.AddWithValue("@id", character1ID);
            command.Parameters.AddWithValue("@posX", posX);
            command.Parameters.AddWithValue("@posY", posY);
            command.Parameters.AddWithValue("@posZ", posZ);
            command.ExecuteNonQuery();
            newConnection.CloseConnection();
        }
    }
}
