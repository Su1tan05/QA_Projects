using MySql.Data.MySqlClient;
using RestApiTest.DataBase.Models;
using RestApiTest.DataBase;
using System;

namespace RestApiTest.DataBase.Tables
{
    public class AuthorTable
    {
        private string insertQuery = "INSERT INTO author(name,login,email) VALUES(?NAME,?LOGIN,?EMAIL)";
        private string queryToSelectLastItem = "SELECT* FROM {0} WHERE id=(SELECT max(id) FROM {0});";
        private string _connectionString;
        private MySqlCommand command;
        private MySqlConnection _connection;

        public AuthorTable(MySqlConnection connection)
        {
            _connection = connection;
        }

        public int Add(Author author)
        {
            try
            {
                command = new MySqlCommand(insertQuery, _connection);
                command.Parameters.AddWithValue("?NAME", author.Name);
                command.Parameters.AddWithValue("?LOGIN", author.Login);
                command.Parameters.AddWithValue("?EMAIL", author.Email);
                return command.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public Author Read()
        {
            try
            {
                Author author = new Author();
                command = new MySqlCommand(String.Format(queryToSelectLastItem, "author"), _connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    author.Id = int.Parse(reader[$"id"].ToString());
                    author.Name = reader[$"name"].ToString();
                    author.Login = reader[$"login"].ToString();
                    author.Email = reader[$"email"].ToString();
                }
                reader.Close();
                return author;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
