using Newtonsoft.Json;
using RestApiTest.DataBase.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Data
{
    public class DataProvider
    {
        private static readonly string _nameOfTestJsonFile = "RestApiTestData.json";
        private static readonly string _nameOfConfigJsonFile = "Config.json";
        private static readonly string _nameOFDbConnectionSettings = "DBConnectionSettings.json";
        private static readonly string _nameOFDbTestDataFile = "DBTestData.json";

        private static TestData testData;
        private static ConfigData configData;
        private static DBConnectionData dBConnection;
        private static DBTestData dBTestData;


        public static TestData GetTestData()
        {
            string objectJsonFile = File.ReadAllText(_nameOfTestJsonFile);
            testData = JsonConvert.DeserializeObject<TestData>(objectJsonFile);
            return testData;
        }

        public static ConfigData GetConfigData()
        {
            string objectJsonFile = File.ReadAllText(_nameOfConfigJsonFile);
            configData = JsonConvert.DeserializeObject<ConfigData>(objectJsonFile);
            return configData;
        }
        public static DBConnectionData GetDBSettingsData()
        {
            string objectJsonFile = File.ReadAllText(_nameOFDbConnectionSettings);
            dBConnection = JsonConvert.DeserializeObject<DBConnectionData>(objectJsonFile);
            return dBConnection;
        }

        public static DBTestData GetDBTestData()
        {
            string objectJsonFile = File.ReadAllText(_nameOFDbTestDataFile);
            dBTestData = JsonConvert.DeserializeObject<DBTestData>(objectJsonFile);
            return dBTestData;
        }
    }
}
