using MySql.Data.MySqlClient;
using RestApiTest.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.DataBase.Tables
{
    public class TestTable
    {
        private readonly string _connectionString;
        private readonly string insertQuery = "INSERT INTO test(name,status_id,method_name,project_id,session_id,start_time,end_time,env,browser,author_id) VALUES(?NAME,?STATUSID,?METHODNAME,?PROJECTID,?SESSIONID,?STARTTIME,?ENDTIME,?ENV,?BROWSER,?AUTHOR_ID)";
        private readonly string queryToSelectLastItem = "SELECT* FROM {0} WHERE id=(SELECT max(id) FROM {0});";
        private readonly string queryToCopyTestsById = "INSERT INTO test (name, status_id, method_name, project_id, session_id, start_time, end_time, env, browser, author_id) " +
                                                       "SELECT name, status_id, method_name, {0}, session_id, start_time, end_time, env, browser, {1} " +
                                                       "FROM test WHERE id IN ({2})";
        private string queryToSelectAllIds = "SELECT {0} FROM {1}";
        private string queryToUpdate = "UPDATE test SET status_id=?STATUS_ID,start_time=?START_TIME,end_time=?END_TIME WHERE id =?ID";
        private string queryToSelectItemsPerId = "SELECT * FROM {0} WHERE id={1}";
        private readonly string queryToDeleteTestsById = "DELETE FROM test WHERE id IN ({0})";
        private string strNumbers;
        private MySqlCommand command;

        public TestTable(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Test test)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                command = new MySqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("?NAME", test.Name);
                command.Parameters.AddWithValue("?STATUSID", test.StatusId);
                command.Parameters.AddWithValue("?METHODNAME", test.MethodName);
                command.Parameters.AddWithValue("?PROJECTID", test.ProjectId);
                command.Parameters.AddWithValue("?SESSIONID", test.SessionId);
                command.Parameters.AddWithValue("?STARTTIME", test.StartTime);
                command.Parameters.AddWithValue("?ENDTIME", test.EndTime);
                command.Parameters.AddWithValue("?ENV", test.Env);
                command.Parameters.AddWithValue("?BROWSER", test.Browser);
                command.Parameters.AddWithValue("?AUTHOR_ID", test.AutorId);
                return command.ExecuteNonQuery();
            }
        }

        public Test Read()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                Test test = new Test();
                connection.Open();
                command = new MySqlCommand(String.Format(queryToSelectLastItem, "test"), connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    test.Name = reader[$"name"].ToString();
                    test.StatusId = int.Parse(reader[$"status_id"].ToString());
                    test.MethodName = reader[$"method_name"].ToString();
                    test.ProjectId = int.Parse(reader[$"project_id"].ToString());
                    test.SessionId = int.Parse(reader[$"session_id"].ToString());
                    test.StartTime = DateTime.Parse(reader[$"start_time"].ToString());
                    test.EndTime = DateTime.Parse(reader[$"end_time"].ToString());
                    test.Env = reader[$"env"].ToString();
                    test.Browser = reader[$"browser"].ToString().Length != 0 ? reader[$"browser"].ToString() : null;
                    test.AutorId = reader[$"author_id"].ToString().Length != 0 ? int.Parse(reader[$"author_id"].ToString()) : null;
                }
                return test;
            }
        }

        public int Copy(Test test, List<int> numbers)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var item in numbers)
                {
                    strNumbers += item.ToString() + ",";
                }
                command = new MySqlCommand(String.Format(queryToCopyTestsById, test.ProjectId, test.AutorId, strNumbers.Remove(strNumbers.Length - 1, 1)), connection);
                return command.ExecuteNonQuery();
            }
        }

        public List<int> Get(string param)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                command = new MySqlCommand(String.Format(queryToSelectAllIds, param, "test"), connection);
                var reader = command.ExecuteReader();
                List<int> values = new List<int>();
                while (reader.Read())
                {
                    values.Add(int.Parse(reader[$"{param}"].ToString()));
                }
                return values;
            }
        }

        public Test Update(Test test)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                Test updatedTest = new Test();
                connection.Open();
                command = new MySqlCommand(queryToUpdate, connection);
                command.Parameters.AddWithValue("?STATUS_ID", test.StatusId);
                command.Parameters.AddWithValue("?START_TIME", test.StartTime);
                command.Parameters.AddWithValue("?END_TIME", test.EndTime);
                command.Parameters.AddWithValue("?ID", test.Id);
                command.ExecuteNonQuery();
                var readCommand = new MySqlCommand(String.Format(queryToSelectItemsPerId, "test", test.Id), connection);
                var reader = readCommand.ExecuteReader();
                while (reader.Read())
                {
                    updatedTest.Id = int.Parse(reader[$"id"].ToString());
                    updatedTest.StatusId = int.Parse(reader[$"status_id"].ToString());
                    updatedTest.StartTime = DateTime.Parse(reader[$"start_time"].ToString());
                    updatedTest.EndTime = DateTime.Parse(reader[$"end_time"].ToString());
                }
                return updatedTest;
            }
        }

        public int Delete(List<int> ids)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var item in ids)
                {
                    strNumbers += item.ToString() + ",";
                }
                command = new MySqlCommand(String.Format(queryToDeleteTestsById, strNumbers.Remove(strNumbers.Length - 1, 1)), connection);
                return command.ExecuteNonQuery();
            }
        }
    }
}
