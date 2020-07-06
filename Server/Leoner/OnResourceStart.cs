using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;

namespace Leoner
{
    class OnResourceStart : Script
    {
        //BASIC SERVER SETUPS
        [ServerEvent(Event.ResourceStart)]

        public void onServerStart()
        {      
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(false);
        }
    }
}
