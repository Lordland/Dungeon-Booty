using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBOoTy
{
    class Mysql
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Mysql()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "bootybot.c8suk4minlgf.eu-central-1.rds.amazonaws.com";
            database = "bootydungeon";
            uid = "bootybot";
            password = "VodkaneitoR93";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        break;

                    case 1045:
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //Insert statement of a character. It works
        public void InsertCharacter(Character ch, int id)
        {
            string query = "INSERT INTO personaje (iduser, name, level,dexterity,strength,intelligence,poder,luck,speed) VALUES("+id+",'"+ch.Name+ "',"+ch.Level+ "," + ch.Dex+ "," + ch.Str+ "," + ch.Int+ "," + ch.Pow+ "," + ch.Lck+ "," + ch.Spd+")";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public bool ExistsCharacter(int id)
        {
            string query = "SELECT * FROM personaje WHERE iduser = "+id;
            bool exist = false;
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                var x = cmd.ExecuteReader();
                if (x.HasRows)
                    exist = true;
                
                x.Close();
                //close connection
                this.CloseConnection();
               
            }
            return exist;
        }

        //Returns the character of the user that asked for it
        public Character ReadCharacter(int id)
        {
            string query = "SELECT * FROM personaje WHERE iduser = " + id;
            Character character = new Character();
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                var x = cmd.ExecuteReader();
                while (x.Read())
                {
                    character.Name = x[3].ToString();
                    character.Description = x[4].ToString();
                    character.Level = int.Parse(x[5].ToString());
                    character.Dex = int.Parse(x[6].ToString());
                    character.Str = int.Parse(x[7].ToString());
                    character.Int = int.Parse(x[8].ToString());
                    character.Pow = int.Parse(x[9].ToString());
                    character.Lck = int.Parse(x[10].ToString());
                    character.Spd = int.Parse(x[11].ToString());
                    character.Exp = int.Parse(x[12].ToString());
                    character.PV = int.Parse(x[2].ToString());
                }
                x.Close();
                //close connection
                this.CloseConnection();

            }
            return character;
        }

        //Update statement for the name of a character
        public void UpdateNameCharacter(String name, int id)
        {
            string query = "UPDATE personaje set name = '"+ name +"' WHERE iduser = " + id;
            Character character = new Character();
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();

            }
        }

        //Update statement for the name of a character
        public void UpdateDescriptionCharacter(String des, int id)
        {
            string query = "UPDATE personaje set description = '" + des + "' WHERE iduser = " + id;
            Character character = new Character();
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();

            }
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
       /* public List<string>[] Select()
        {
        }

        //Count statement
        public int Count()
        {
        }*/

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }

    }
}
