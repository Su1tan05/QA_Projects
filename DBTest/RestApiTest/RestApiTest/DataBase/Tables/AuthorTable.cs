using MySql.Data.MySqlClient;
using RestApiTest.DataBase.Models;
using System;

namespace RestApiTest.DataBase.Tables
{
    public class AuthorTable
    {
        private string insertQuery = "INSERT INTO author(name,login,email) VALUES(?NAME,?LOGIN,?EMAIL)";
        private string queryToSelectLastItem = "SELECT* FROM {0} WHERE id=(SELECT max(id) FROM {0});";
        private string _connectionString;
        private MySqlCommand command;

        public AuthorTable(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Author author)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("?NAME", author.Name);
                command.Parameters.AddWithValue("?LOGIN", author.Login);
                command.Parameters.AddWithValue("?EMAIL", author.Email);
                return command.ExecuteNonQuery();
            }
        }

        public Author Read()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                Author author = new Author();
                connection.Open();
                command = new MySqlCommand(String.Format(queryToSelectLastItem, "author"), connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    author.Id = int.Parse(reader[$"id"].ToString());
                    author.Name = reader[$"name"].ToString();
                    author.Login = reader[$"login"].ToString();
                    author.Email = reader[$"email"].ToString();
                }
                return author;
            }
        }
    }
}
