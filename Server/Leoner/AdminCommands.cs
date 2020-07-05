using Google.Protobuf.WellKnownTypes;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Leoner
{
    class AdminCommands : Script
    {
        //CHAT COMMANDS
        [Command("help")]
        public void help(Client client)
        {
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~/getveh + vehicle name (besra ,shotaro , lazer,dominator3 ,scorcher , blazer , dune4 , shamal, cargoplane)  ");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~/goto + location name (carpark , roof , sky )");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~/getgun + Gun name (RPG , Knife , MG , Parachute)");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~/getpos");
        }

        [Command("getgun")]
        public void createGun(Client client, string gun)
        {
            string weaponName = gun;
            WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(weaponName);
            NAPI.Player.GivePlayerWeapon(client, weaponHash, 3000);
        }

        [Command("getveh")]
        public void createVehicle(Client client, string veh)
        {
            string vehName = veh;
            Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(client);
            VehicleHash vehicle2 = NAPI.Util.VehicleNameToModel(vehName);
            NAPI.Vehicle.CreateVehicle(vehicle2, new Vector3(PlayerPos.X + 3, PlayerPos.Y, PlayerPos.Z), 99f, 0, 0);
        }

        [Command("getpos")]
        public void getPosition(Client client)
        {
            Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(client);
            NAPI.Chat.SendChatMessageToPlayer(client, "X: " + PlayerPos.X + " Y: " + PlayerPos.Y + " Z: " + PlayerPos.Z);
        }

        [Command("weather")]
        public void changeWeather(Client player, string weather)
        {
            NAPI.World.SetWeather(weather);
        }

        [Command("setfree")]
        public void setFree(Client player)
        {
            player.SetData("arested", false);
        }

        [Command("goto")]
        public void gotoLocation(Client client, string location)
        {
            if (location == "carpark")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(212.0034, 1231.699, 225.46)); //carpark
            }
            else if (location == "roof")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(-60.77951, -816.3001, 322.3302)); //roof
            }
            else if (location == "sky")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(-60.77951, -816.3001, 1500)); //sky
            }
            else if (location == "prison")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(1668.442, 2583.331, -114.1638)); //prison
            }
            else if (location == "boat")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(-2030.716, -1030.466, 5.882062)); //prison
            }
            else if (location == "flat")
            {
                NAPI.Player.SpawnPlayer(client, new Vector3(-765.6955, 319.5362, 175.39482)); //prison
            }
        }
    }
}
