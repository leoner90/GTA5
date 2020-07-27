using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Leoner
{
    class Authorisation : Script
    {
        [RemoteEvent]
        public void loginDataToServer(Client player, int state, string user, string email, string psw, string pswRepeat)
        {
            /*TODO
                Autorisation
	            login check is username exist or not , not throught try catch
	            2 longin at same time fix. (if there is player with this id - disconect him  and connect new one , or throught db check status online or offline and disconect )
	            email confirmation
	            mail reset password/login
	            RageEXP for login/password
	            Password md5      
	            teleport player to sky and freeze , too other people couldn't see him
	            Exit button if possible
                email code upadte
            */
            player.Name = user;
            string[] errors = new string[4];
            bool isError = false;
            MySql newConnection = new MySql();
            MySqlConnection mysqlConnection = newConnection.Initialize();
            MySqlCommand command = mysqlConnection.CreateCommand();
            newConnection.OpenConnection();
            switch (state)
            {
                case 0:
                    // Login State     
                    command.CommandText = "SELECT * FROM accounts WHERE login = @username ";
                    command.Parameters.AddWithValue("@username", user);
                    try
                    {
                        MySqlDataReader reader = command.ExecuteReader();       
                        reader.Read();
                        string pass = reader.GetString("password");
                        int character1 = reader.GetInt32("character1");
             
                        if (pass == psw)
                        {
                            if(character1 != 0)
                            {
                                reader.Close();
                                command.CommandText = "SELECT * FROM charac WHERE id = @id ";
                                command.Parameters.AddWithValue("@id", character1);
                                MySqlDataReader character = command.ExecuteReader();
                                character.Read();
                                string name = character.GetString("name");
                                string surname = character.GetString("surname");
                                player.TriggerEvent("loginHandler", "success", 0, user, psw, character1 , name , surname); // 0 = errrors
                                newConnection.CloseConnection();
                                character.Close();
                            } else
                            {       
                                player.TriggerEvent("loginHandler", "success", 0 , user , psw , character1 ); // 0 = errrors
                                newConnection.CloseConnection();
                                
                            }
                        } else {
                            errors[0] = "Неверные данные";
                            player.TriggerEvent("loginHandler", "incorrectinfo" , errors);
                            reader.Close();
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
                        isError = true;
                        errors[0] = "Логин или пароль слишком короткий";
                    }
                    if (psw != pswRepeat)
                    {
                        isError = true;
                        errors[1] = "Пароли не совпадают";
                    }
                    //EMAIL
                    bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase); ;
                    if (!isEmail)
                    {
                        isError = true;
                        errors[2] = "Email введен неверно";
                    }
                    //check user allready exist   
                    MySqlCommand check_User_Name = new MySqlCommand("SELECT count(*) FROM accounts WHERE login = @username", mysqlConnection);
                    check_User_Name.Parameters.AddWithValue("@username", user);
                    int UserExist = (int)(long)check_User_Name.ExecuteScalar();
                    if (UserExist > 0)
                    {
                        isError = true;
                        errors[3] = "Этот логин уже используется";
                    }
                    //If error send them back , otherwise register
                    if (isError)
                    {
                        player.TriggerEvent("loginHandler", "incorrectinfo", errors);
                    } else {
                        command.CommandText = "INSERT INTO accounts (login , email, password , character1 ,character2 ) VALUES (@login, @email , @password , '0' , '0')";
                        command.Parameters.AddWithValue("@login", user);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", psw);
                        command.ExecuteNonQuery();
                        newConnection.CloseConnection();
                        int character1 = 0;
                        player.TriggerEvent("loginHandler", "registered",0 , user, psw , character1 );
                    }
                    break;
                default:
                    //default
                    break;
            } 
        }
    }
}