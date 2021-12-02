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

        public SessionTable(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Session session)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("?SESSION_KEY", session.SessionKey);
                command.Parameters.AddWithValue("?CREATED_TIME", session.CreatedTime);
                command.Parameters.AddWithValue("?BUILD_NUMBER", session.BuildNumber);
                return command.ExecuteNonQuery();
            }
        }

        public Session Read()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                Session session = new Session();
                connection.Open();
                command = new MySqlCommand(String.Format(queryToSelectLastItem, "session"), connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    session.id = int.Parse(reader[$"id"].ToString());
                    session.SessionKey = int.Parse(reader[$"session_key"].ToString());
                    session.CreatedTime = DateTime.Parse(reader[$"created_time"].ToString());
                    session.BuildNumber = int.Parse(reader[$"build_number"].ToString());
                }
                return session;
            }
        }
    }
}
