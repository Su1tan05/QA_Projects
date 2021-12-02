using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.DataProvider
{
    public class TestDataProvider
    {
        private static readonly string jsonFileName = "TestData.json";
        private static TestData testData;
        public static TestData GetData()
        {
            string objectJsonFile = File.ReadAllText(jsonFileName);
            testData = JsonConvert.DeserializeObject<TestData>(objectJsonFile);
            return testData;
        }
    }
}
