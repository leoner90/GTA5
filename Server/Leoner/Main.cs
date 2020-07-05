using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;

namespace Leoner
{
    public class Main : Script
    {
        Colshapes Colshapes = new Colshapes(); // Triggers server events when player enters appropriate colshape
        Authorisation Authorisation = new Authorisation(); // Server authorisation Sign in / Register new Acc.
        OnResourceStart OnResourceStart = new OnResourceStart(); // On Server Loading Events.
        OnPlayerConnect OnPlayerConnect = new OnPlayerConnect(); // When new player connected Events.
        AdminCommands AdminCommands = new AdminCommands(); //Remote Events For Admin.
        DeathHandler DeathHandler = new DeathHandler(); //On Player Death And killer arrest
    }
}