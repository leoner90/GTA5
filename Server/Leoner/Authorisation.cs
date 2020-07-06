using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;

namespace Leoner
{
    class Authorisation : Script
    {
        [RemoteEvent]
        public void loginDataToServer(Client player, int state, string user, string email, string psw, string pswRepeat)
        {
            string[] errors = new string[4];
            MySql newConnection = new MySql();
            MySqlConnection mysqlConnection = newConnection.Initialize();
            MySqlCommand command = mysqlConnection.CreateCommand();
            mysqlConnection.Open();
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
                                newConnection.CloseConnection();
                            } else {
                                errors[0] = "Неверные данные";
                                player.TriggerEvent("loginHandler", "incorrectinfo" , errors);
                            }
                        }
                    } catch {
                        errors[0] = "Неверные данные";
                        player.TriggerEvent("loginHandler", "incorrectinfo", errors);
                    }
                    break;
                case 1: 
                    // Registration State
                      
                    if (user.Length < 5 || psw.Length < 5)
                    {
                        errors[0] = "Логин или пароль слишком короткий";
                    }

                    if (psw != pswRepeat)
                    {
                        errors[1] = "Пароли не совпадают";
                    }

                    //EMAIL
                    bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase); ;
                    if (!isEmail)
                    {
                        errors[2] = "Email введен неверно";
                    }

                    //check user allready exist   
                    MySqlCommand check_User_Name = new MySqlCommand("SELECT count(*) FROM accounts WHERE username = @username", mysqlConnection);
                    check_User_Name.Parameters.AddWithValue("@username", user);
                    int UserExist = (int)(long)check_User_Name.ExecuteScalar();
                    if (UserExist > 0)
                    {
                        errors[3] = "Этот логин уже используется";
                    }
            

                    if (errors[0] == null && errors[1] == null && errors[2] == null && errors[3] == null)
                    {
                        command.CommandText = "INSERT INTO accounts (username , password , money) VALUES (@username , @password , '0')";
                        command.Parameters.AddWithValue("@username", user);
                        command.Parameters.AddWithValue("@password", psw);
                        command.ExecuteNonQuery();
                        newConnection.CloseConnection();
                        player.TriggerEvent("loginHandler", "registered");
                    } else {
                        player.TriggerEvent("loginHandler", "incorrectinfo" , errors);
                    }
                    break;
                default:
                    //default
                    break;
            } 
        }
    }
}