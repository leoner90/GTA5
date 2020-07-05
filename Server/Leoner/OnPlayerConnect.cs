using GTANetworkAPI;

namespace Leoner
{
    class OnPlayerConnect : Script
    {
        //AT SPAWN
        [ServerEvent(Event.PlayerConnected)]
        public void newPlayerConn(Client client)
        {
            NAPI.Player.SpawnPlayer(client, new Vector3(2075.4, 5002.588, 41.00666));
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~ system call  /help");
  
            //Clothes
            NAPI.Player.SetPlayerClothes(client, 11, 10, 0);
            NAPI.Player.SetPlayerClothes(client, 8, 0, 0);
            NAPI.Player.SetPlayerClothes(client, 3, 4, 0);
            NAPI.Player.SetPlayerHairColor(client, 255, 255);
        }
    }
}
