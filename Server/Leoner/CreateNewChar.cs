using GTANetworkAPI;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using System;
using System.IO;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Leoner 
{
    class CreateNewChar : Script
    {
        public uint creatorDimension = 1;
       public void loadCharacter(Client player ,string json)
        {
            dynamic Jony = JObject.Parse(json);

            //Gender
            int gender = Jony.gender;
            NAPI.Entity.SetEntityModel(player, gender);

            //Hair
            int hair = Jony.hair;
            NAPI.Player.SetPlayerClothes(player, 2, hair, 0);

            //Skin and Parents
            byte mother = Jony.mother;
            byte father = Jony.father;
            byte skinColor = Jony.skinColor;

            //MOTHER FATHER AND SKIN COLOR
            HeadBlend zaza = new HeadBlend();
            zaza.ShapeFirst = mother; //mother
            zaza.ShapeMix = 0; //works
            zaza.ShapeSecond = father; // father
            zaza.ShapeThird = 0;
            zaza.SkinFirst = skinColor; //color 1 
            zaza.SkinMix = 0;//works
            zaza.SkinSecond = skinColor; // color 2
            zaza.SkinThird = 0;
            zaza.ThirdMix = 0;
            NAPI.Player.SetPlayerHeadBlend(player, zaza);

            byte eyeBrows = Jony.eyeBrows;
            byte faceHair = Jony.faceHair;
            byte hairColor = Jony.hairColor;
            byte lipstickColor = Jony.lipstickColor;
            byte eyeColor = Jony.eyeColor;

            //eyeBrows
            HeadOverlay xxx = new HeadOverlay();
            xxx.Index = eyeBrows;
            xxx.Color = hairColor;
            xxx.Opacity = 1;
            xxx.SecondaryColor = hairColor;
            NAPI.Player.SetPlayerHeadOverlay(player, 2, xxx); // 8 какая фича (губы) а индекс  (какие губы)
                                                              //faceHair
            xxx.Index = faceHair;
            NAPI.Player.SetPlayerHeadOverlay(player, 1, xxx);
            //lipstickColor
            xxx.Index = lipstickColor;
            xxx.Color = 0;
            NAPI.Player.SetPlayerHeadOverlay(player, 8, xxx); // 8 какая фича (губы) а индекс  (какие губы)
                                                              //hair color
            NAPI.Player.SetPlayerHairColor(player, hairColor, hairColor);
            //Eye color
            NAPI.Player.SetPlayerEyeColor(player, eyeColor);

            //SET FACE APP FaceFeature 20 штук
            for (int i = 0; i < 20; i++)
            {
                float raz = Jony.FaceAppearance[i];
                NAPI.Player.SetPlayerFaceFeature(player, 1, raz);
            }
        }
        //TELEPORT TO CHARACTER CREATOR
        [RemoteEvent]
        public void charTp(Client player)
        {
            NAPI.Player.SpawnPlayer(player, new Vector3(402.8664, -996.4108, -99.00027) , 180);
            NAPI.Entity.SetEntityDimension(player, creatorDimension);
            creatorDimension++;
        }

        //SAVE CHARACTER DETAILS
        [RemoteEvent]
        public void saveCharacter(Client player,  string user,string name , string surname,  string myjson)
        {
            if (name.Length < 3 || surname.Length < 3)
            {
                player.TriggerEvent("nameErr");
            } else {              
                MySql newConnection = new MySql();
                MySqlConnection mysqlConnection = newConnection.Initialize();
                MySqlCommand command = mysqlConnection.CreateCommand();
                newConnection.OpenConnection();
                command.CommandText = "INSERT INTO charac (name, surname ,json) VALUES (@name, @surname, @json) ; SELECT LAST_INSERT_ID(); ";
                command.Parameters.AddWithValue("@json", myjson);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                ulong lastId = (ulong)command.ExecuteScalar();      
                command.CommandText = "UPDATE accounts SET character1 = @id WHERE login = @username;";
                command.Parameters.AddWithValue("@id", lastId);
                command.Parameters.AddWithValue("@username", user);
                command.ExecuteNonQuery();
                newConnection.CloseConnection();

                //Load character with new data
                player.TriggerEvent("CharLoadFinish");
                player.TriggerEvent("LoadMoney", 45);
                NAPI.Entity.SetEntityDimension(player, 0);
                NAPI.Player.SpawnPlayer(player, new Vector3(2075.4, 5002.588, 41.00666));
                loadCharacter(player, myjson);
            }
        }
        //Load character data
        [RemoteEvent]
        public void charDataLoading(Client player, string user, string password)
        {
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
            command.CommandText = "SELECT * FROM charac WHERE id = @id ";
            command.Parameters.AddWithValue("@id", character1ID);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                string json = reader.GetString("json");
                int amount = reader.GetInt32("money");
                float posX = reader.GetInt32("posX");
                float posY = reader.GetInt32("posY");
                float posZ = reader.GetInt32("posZ");
                player.TriggerEvent("LoadMoney", amount);
                loadCharacter(player, json);
                player.TriggerEvent("CharLoadFinish");
                NAPI.Entity.SetEntityDimension(player, 0);
                NAPI.Player.SpawnPlayer(player, new Vector3(posX, posY, posZ));    
            }
        }
    }
}
