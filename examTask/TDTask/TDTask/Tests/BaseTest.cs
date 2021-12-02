using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using NUnit.Framework;
using TDTask.Utils;

namespace TDTask.Tests
{
    public class BaseTest
    {
        private static ISettingsFile CustomSettings => new JsonSettingsFile("settings.custom.json");
        protected readonly Browser Browser = AqualityServices.Browser;
        protected string sessionId;
        protected string testName;
        protected string methodName;
        protected string browserName;
        protected string environment = CustomSettings.GetValue<string>("Env");
        protected string logPath = CustomSettings.GetValue<string>("TestLogPath");

        [SetUp]
        public void Setup()
        {
            sessionId = RandomUtils.GenerateText(CustomSettings.GetValue<int>("SessionIdLength"));
            browserName = Browser.Driver.GetType().ToString();
            Browser.Maximize();
            Browser.GoTo(CustomSettings.GetValue<string>("Url"));
        }

        [TearDown]
        protected void DoAfterAllTest()
        {
            Browser.Quit();
        }
    }
}
