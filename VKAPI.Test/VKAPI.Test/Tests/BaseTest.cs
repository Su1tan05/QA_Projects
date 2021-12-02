using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPI.Test.Tests
{
    public class BaseTest
    {
        protected static ISettingsFile CustomSettings => new JsonSettingsFile("settings.custom.json");
        protected readonly Browser browser = AqualityServices.Browser;

        [SetUp]
        public void Setup()
        {
            browser.Maximize();
            browser.GoTo(CustomSettings.GetValue<string>("Url"));
        }

        [TearDown]
        protected void DoAfterAllTest()
        {
            browser.Quit();
        }
    }
}
