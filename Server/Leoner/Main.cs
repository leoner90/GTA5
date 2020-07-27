using GTANetworkAPI;

namespace Leoner
{
    public class Main : Script
    {
        //BASIC SERVER SETUPS
        [ServerEvent(Event.ResourceStart)]
        public void onServerStart()
        {
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(false);
        }

        //AT SPAWN
        [ServerEvent(Event.PlayerConnected)]
        public void newPlayerConn(Client client)
        {
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~ system call  /help");
        }
    }
}