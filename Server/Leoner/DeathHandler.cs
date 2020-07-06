using GTANetworkAPI;
using System.Threading.Tasks;
namespace Leoner
{
    class DeathHandler : Script
    {
        //PLAYER DEATH HANDLER
        [ServerEvent(Event.PlayerDeath)]
        public void onPlayerDeath(Client client, Client killer, uint reason)
        {
            void resurrect(Client client)
            {
                NAPI.Task.Run(() =>
                {
                    NAPI.Player.SetPlayerHealth(client, 1000);
                    NAPI.Player.SpawnPlayer(client, new Vector3(256.9603, -1359.258, 24.5378));
                    NAPI.Chat.SendChatMessageToPlayer(client, "~g~ You have been respawned!");
                    NAPI.Chat.SendChatMessageToPlayer(client, "~y~system call / help");
                }, delayTime: 10000); // delay 10 seconds
            }
            if (killer != null)
            {
                //SUICIDE
                if (killer.Name == client.Name)
                {
                    if (client.GetData("arested") != true)
                    {
                        NAPI.Chat.SendChatMessageToAll($"{client.Name} committed suicide");
                        NAPI.Chat.SendChatMessageToPlayer(client, "~r~ Suicide...... ");
                        NAPI.Chat.SendChatMessageToPlayer(client, "You will be spawned in 10 seconds");
                        resurrect(client);
                    }
                // Player have been killed
                } else {
                    NAPI.Chat.SendChatMessageToPlayer(killer, "~g~ You have been arrested!");
                    NAPI.Player.SetPlayerHealth(killer, 100);
                    NAPI.Player.SpawnPlayer(killer, new Vector3(460.2378, -994.386, 24.91486));
                    killer.SetData("arested", true);
                    NAPI.Chat.SendChatMessageToPlayer(killer, "~y~system call / help");
                    NAPI.Chat.SendChatMessageToPlayer(client, $" ~r~ Your life has been taken by {killer.Name}. ");
                    NAPI.Chat.SendChatMessageToPlayer(client, "You will be spawned in 10 seconds");
                    resurrect(client);
                }
            //Just Died
            } else {
                resurrect(client);
            }
        }
    }
}
