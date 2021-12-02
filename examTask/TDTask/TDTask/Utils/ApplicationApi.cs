using System;
using System.Collections.Generic;
using System.IO;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Newtonsoft.Json;
using RestSharp;
using TDTask.Models;


namespace TDTask.Utils
{
    public static class ApplicationApi
    {
        private static ISettingsFile ApiSettings => new JsonSettingsFile("ApiSettings.json");
        private static IRestResponse _response;
        private static readonly string BaseUrl = ApiSettings.GetValue<string>("ApiClientUrl");
        private static readonly string GetTokenPath = ApiSettings.GetValue<string>("GetTokenPath");
        private static readonly string GetListOfTestsPath = ApiSettings.GetValue<string>("GetListOfTestsPath");
        private static readonly string AddTestPath = ApiSettings.GetValue<string>("AddTestPath");
        private static readonly string AddLogsToTestPath = ApiSettings.GetValue<string>("AddLogToTestPath");
        private static readonly string AddScreenShotToTestPath = ApiSettings.GetValue<string>("AddScreenShotToTestPath");
        private static readonly string ContentType = ApiSettings.GetValue<string>("AttachmentContentType");

        private static Dictionary<string, string> GetTokenParameters(int variant) =>
            new Dictionary<string, string>
            {
                ["variant"] = variant.ToString()
            };

        private static Dictionary<string, string> GetListOfTestsParameters(int projectId) =>
            new Dictionary<string, string>
            {
                ["projectId"]= projectId.ToString()
            };

        private static Dictionary<string, string> AddTestParameters(TestInfo test) =>
            new Dictionary<string, string>
            {
                ["SID"] = test.SessionId,
                ["projectName"] = test.ProjectName,
                ["testName"] = test.TestName,
                ["methodName"] = test.MethodName,
                ["env"] = test.Env,
                ["browser"] = test.BrowserName
            };

        private static Dictionary<string, string> AddTestParameters(int testId, string logPath) =>
            new Dictionary<string, string>
            {
                ["testId"] = testId.ToString(),
                ["content"] = File.ReadAllText(logPath)
            };

        private static Dictionary<string, string> AddScreenshotParameters(int testId, byte[] screenshot, string contentType) =>
            new Dictionary<string, string>
            {
                ["testId"] = testId.ToString(),
                ["content"] = Convert.ToBase64String(screenshot),
                ["contentType"] = contentType
            };

        public static string GetToken(int variant)
        {
            AqualityServices.Logger.Info("Token generating");
            return ApiUtils.SendPostRequest($"{BaseUrl}{GetTokenPath}", GetTokenParameters(variant), DataFormat.Json).Content;
        }

        public static List<Test> GetTests(int projectId)
        {
            AqualityServices.Logger.Info("Getting a list of Tests");
            _response = ApiUtils.SendPostRequest($"{BaseUrl}{GetListOfTestsPath}", GetListOfTestsParameters(projectId), DataFormat.Json);
            return JsonConvert.DeserializeObject<List<Test>>(_response.Content);
        }

        public static int AddTest(TestInfo test)
        {
            AqualityServices.Logger.Info($"Adding Test information to the project: '{test.ProjectName}'");
            _response = ApiUtils.SendPostRequest($"{BaseUrl}{AddTestPath}", AddTestParameters(test), DataFormat.Json);
            return int.Parse(_response.Content);
        }

        public static void AddLogsToTest(int testId, string logPath)
        {
            AqualityServices.Logger.Info($"Sending to Test №{testId} logs");
            ApiUtils.SendPostRequest($"{BaseUrl}{AddLogsToTestPath}",AddTestParameters(testId,logPath), DataFormat.Json);
        }

        public static void AddScreenshotToTest(int testId, byte[] screenshot)
        {
            AqualityServices.Logger.Info($"Adding screenshot to Test №{testId}");
            ApiUtils.SendPostRequest($"{BaseUrl}{AddScreenShotToTestPath}", AddScreenshotParameters(testId, screenshot, ContentType), DataFormat.Json);
        }
    }
}
