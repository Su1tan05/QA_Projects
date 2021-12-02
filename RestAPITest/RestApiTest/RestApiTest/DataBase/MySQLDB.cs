using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestApiTest.DataBase.Models;
using RestApiTest.DataBase.Tables;
using RestApiTest.Data;
using MySql.Data.MySqlClient;


namespace RestApiTest.DataBase
{
    public class MySQLDB
    {
        private static readonly string _server = DataProvider.GetDBSettingsData().Server;
        private static readonly string _username = DataProvider.GetDBSettingsData().Username;
        private static readonly string _password = DataProvider.GetDBSettingsData().Password;
        private static readonly string _database = DataProvider.GetDBSettingsData().Database;
        private static readonly string connectionString = $"server={_server};username={_username};password={_password};database={_database};SSL Mode=None";
        private static MySQLDB db;
        private static MySqlConnection connection = new MySqlConnection(connectionString);

        private MySQLDB()
        {
        }

        public static MySQLDB GetDB()
        {
            if (db == null)
                db = new MySQLDB();
            return db;
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AuthorTable AuthorTable() => new AuthorTable(connection);

        public SessionTable SessionTable() => new SessionTable(connection);

        public TestTable TestTable() => new TestTable(connection);
    }
}
