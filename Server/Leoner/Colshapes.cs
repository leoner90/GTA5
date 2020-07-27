using GTANetworkAPI;

namespace Leoner
{
    class Colshapes : Script
    {
        [RemoteEvent]
        public void teleport(Client player, object[] arguments)
        {
            NAPI.Player.SpawnPlayer(player, new Vector3(-60.77951, -816.3001, 1500));
        }

        [RemoteEvent]
        public void hospitalExit(Client player, object[] arguments)
        {
            NAPI.Player.SpawnPlayer(player, new Vector3(212.0034, 1231.699, 225.46));
        }

        [RemoteEvent]
        public void isArrested(Client player)
        {
            if (player.GetData("arested") == true)
            {
                NAPI.Player.SpawnPlayer(player, new Vector3(460.2378, -994.386, 24.91486));
                NAPI.Chat.SendChatMessageToPlayer(player, "~r~ Arrested!");
            }
        }
    }
}
