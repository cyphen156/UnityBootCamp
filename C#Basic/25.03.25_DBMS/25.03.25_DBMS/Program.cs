using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace _25._03._25.DBMS
{
    class Program
    {
        static void Main(string[] args)
        {
            //login
            string connectionString = "server=localhost;user=root;database=membership;password=qwerasdf";

            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();
                MySqlCommand mySqlCommand = new MySqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.CommandText = "select * from users where user_id = @user_id and user_password = @user_password";
                mySqlCommand.Prepare();
                mySqlCommand.Parameters.AddWithValue("@user_id", "htk008");
                mySqlCommand.Parameters.AddWithValue("@user_password", "1234");

                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["user_name"] + " " + dataReader["user_email"]);
                }
                dataReader.Close();
                MySqlCommand mySqlCommand2 = new MySqlCommand();
                mySqlCommand2.Connection = mySqlConnection;

                mySqlCommand2.CommandText = "insert into users (user_id, user_password, user_name, user_email) values ( @user_id, @user_password, @user_name, @user_email)";
                mySqlCommand2.Prepare();
                mySqlCommand2.Parameters.AddWithValue("@user_id", "abc001");
                mySqlCommand2.Parameters.AddWithValue("@user_password", "2222");
                mySqlCommand2.Parameters.AddWithValue("@user_name", "신지용");
                mySqlCommand2.Parameters.AddWithValue("@user_email", "abc001@abc001.com");
                mySqlCommand2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
    }
}