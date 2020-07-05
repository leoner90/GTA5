using GTANetworkAPI;
using MySql.Data.MySqlClient;
namespace Leoner
{
    class Authorisation : Script
    {
        [RemoteEvent]
        public void loginDataToServer(Client player, int state, string user, string email, string psw, string pswRepeat)
        {
            string myConnectionString = "Database=users;Data Source=localhost;User Id=leoner;Password=jata1234";
            MySqlConnection myConnection = new MySqlConnection(myConnectionString);
            myConnection.Open();
            MySqlCommand command = myConnection.CreateCommand();
            switch (state)
            {
                case 0:
                    // Login State     
                    command.CommandText = "SELECT * FROM accounts WHERE UserName = @username ";
                    command.Parameters.AddWithValue("@username", user);
                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            string pass = reader.GetString("password");
                            if (pass == psw)
                            {
                                player.TriggerEvent("loginHandler", "success");
                                myConnection.Close();
                            } else {
                                player.TriggerEvent("loginHandler", "incorrectinfo");
                            }
                        }
                    } catch {
                        player.TriggerEvent("loginHandler", "incorrectinfo");
                    }
                    break;
                case 1:
                    string[] errors = new string[4];
                    // Registration State
                    if (user.Length <= 3 && psw.Length <= 5)
                    {
                        errors[0] = "Логин или пароль слишком короткий";

                    }
                    if (psw != pswRepeat)
                    {
                        errors[1] = "Пароли не совпадают";
                    }
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(email);
                        //+ REG EXP TO DO
                    }
                    catch
                    {
                        errors[2] = "Email введен неверно";
                    }
                    //Check if user exist in db -> Error 3 if not


                    if (errors[0] == null && errors[1] == null && errors[2] == null && errors[3] == null)
                    {
                        //check user allready exist    
                        command.CommandText = "INSERT INTO accounts (username , password , money) VALUES (@username , @password , '0')";
                        command.Parameters.AddWithValue("@username", user);
                        command.Parameters.AddWithValue("@password", psw);
                        command.ExecuteNonQuery();
                        player.TriggerEvent("loginHandler", "registered");
                    } else {
                        player.TriggerEvent("incorrectinfo");
                    }
                    break;
                default:
                    //default
                    break;
            }    
        }
    }
}