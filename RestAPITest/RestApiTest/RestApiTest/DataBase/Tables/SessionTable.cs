using MySql.Data.MySqlClient;
using RestApiTest.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.DataBase.Tables
{
    public class SessionTable
    {
        private string _connectionString;
        private string insertQuery = "INSERT INTO session(session_key,created_time,build_number) VALUES(?SESSION_KEY,?CREATED_TIME,?BUILD_NUMBER)";
        private string queryToSelectLastItem = "SELECT* FROM {0} WHERE id=(SELECT max(id) FROM {0});";
        private MySqlCommand command;
        private MySqlConnection _connection;

        public SessionTable(MySqlConnection connection)
        {
            _connection = connection;
        }

        public int Add(Session session)
        {
            try
            {
                command = new MySqlCommand(insertQuery, _connection);
                command.Parameters.AddWithValue("?SESSION_KEY", session.SessionKey);
                command.Parameters.AddWithValue("?CREATED_TIME", session.CreatedTime);
                command.Parameters.AddWithValue("?BUILD_NUMBER", session.BuildNumber);
                return command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Session Read()
        {
            try
            {
                Session session = new Session();
                command = new MySqlCommand(String.Format(queryToSelectLastItem, "session"), _connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    session.id = int.Parse(reader[$"id"].ToString());
                    session.SessionKey = int.Parse(reader[$"session_key"].ToString());
                    session.CreatedTime = DateTime.Parse(reader[$"created_time"].ToString());
                    session.BuildNumber = int.Parse(reader[$"build_number"].ToString());
                }
                reader.Close();
                return session;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
