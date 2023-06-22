using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace A_Chess_App
{
    public class SQLCommunication
    {
        public static SqlConnection conn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = true;");
        public static SqlCommand cmd = new SqlCommand("", conn);

        public static void CreateSql() //SQL Database Set Up
        {
            conn.Close();
            if (!CheckDatabaseExists()) //Check if database exists
            {
                conn.Close();
                CreateDatabase(); //Create the database if it doesn't exist
            }
            else
            {
                conn.Close();
                conn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = true; Database = ChessDatabase;"; //Set up ConnectionString id database exists
            }
        }

        private static bool CheckDatabaseExists() //Checks if the database exists
        {
            cmd.CommandText = $"SELECT db_id('{"ChessDatabase"}')";
            conn.Open();
            return cmd.ExecuteScalar() != DBNull.Value;
        }

        public static void CreateDatabase() //If the database doesn't exist alredy, create it
        {
            #region CreateDatabase
            conn.Open();
            cmd.CommandText = "Create Database ChessDatabase;";
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.ConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = true; Database = ChessDatabase;"; //ConnectionString setup
            #endregion

            #region CreateTables
            conn.Open();
            cmd.CommandText = "Create Table Players(PlayerID int Primary Key Identity(1,1), Username char(50), Pass char(500), Elo int, " +
                "Gamesplayed int, Gameswon int, Gameslost int, Registerdate Date, Highestelo int, Lowestelo int);";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "Create Table Games(GameID int Primary Key Identity(1,1), Playerwhite int, Playerblack int, Timecontrol int, " +
                "Winner int);";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "Create Table Moves(GameID int, Moves char(1000));";
            cmd.ExecuteNonQuery();
            conn.Close();
            #endregion
        }

        public static void CreateUser(string name, string pass) //After registering, create an user with the username and password
        {
            DateTime currentdate = DateTime.Now; //Creationdate
            conn.Close();
            conn.Open();
            cmd.CommandText = "Insert Into Players (Username, Pass, Elo, Gamesplayed, Gameswon, Gameslost, Registerdate, " +
            "Highestelo, Lowestelo) values ('" + name + "','" + BCrypt.HashPassword(pass, BCrypt.GenerateSalt(5)) + "', 1500, 0, 0, 0, '" + currentdate.ToString("yyyy-MM-dd") + "' , 0, 0);";
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int LoginUser(string name, string pass, bool usedregister) //Checks if the username and name of a given user exist -> Used for login and register
        {
            conn.Close();
            conn.Open();
            cmd.CommandText = "Select Username, Pass from Players;";
            SqlDataReader read = cmd.ExecuteReader();

            if (usedregister) //Used for login -> Checks if username and password are the same of an already saved user
            {
                while (read.Read())
                {
                    if (name.TrimEnd().Equals(read.GetString(0).TrimEnd()))
                    {
                        return 0;
                    }
                }
                return -1;
            }
            if (!usedregister) //USed for register -> Checks if a usernae already exists -> Usernames can only be given to one user
            {
                while (read.Read())
                {
                    if (name.Equals(read.GetString(0).TrimEnd()) && BCrypt.CheckPassword(pass, read.GetString(1).TrimEnd()))
                    {
                        return 1;
                    }
                }
                return -1;
            }

            return 0;
        }
    }
}
