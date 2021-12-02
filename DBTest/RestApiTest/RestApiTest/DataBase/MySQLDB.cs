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

        private MySQLDB()
        {
        }

        public static MySQLDB Connect()
        {
            if (db == null)
                db = new MySQLDB();
            return db;
        }

        public AuthorTable AuthorTable() => new AuthorTable(connectionString);

        public SessionTable SessionTable() => new SessionTable(connectionString);

        public TestTable TestTable() => new TestTable(connectionString);
    }
}
