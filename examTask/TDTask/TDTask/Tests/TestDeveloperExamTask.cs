using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using NUnit.Framework;
using TDTask.Utils;
using TDTask.Pages;
using TDTask.Models;


namespace TDTask.Tests
{
    public class TestDeveloperExamTask : BaseTest
    {
        private static ISettingsFile TestDataSettings => new JsonSettingsFile("TestData.json");
        private string _token;
        private const string CookieName = "token";
        private int _projectId;
        private int _testId;
        private int _browserTabsCount;
        private List<Test> _testsFromUi;
        private List<Test> _testsFromApi;

        [Test]
        [TestCaseSource(nameof(TestData))]
        public void UnionReportingTest(string login, string password, int variant, string projectName, string newProjectName)
        {
            _token = ApplicationApi.GetToken(variant);
            Assert.IsNotNull(_token, "Token are not generated");
            
            AuthorizeUtils.Authorize(login, password);
            CookieUtils.AddCookie(CookieName, _token);
            HomePage homePage = new HomePage();
            Assert.IsTrue(homePage.State.IsExist, "ProjectsPage is not open");
            Browser.Refresh();
            Assert.IsTrue(homePage.IsVariantDisplayed(variant), $"Variant ¹{variant} is not appear in the footer");

            _projectId = homePage.GetProjectId(projectName);
            homePage.ClickProjectButton(projectName);
            ProjectPage projectPage = new ProjectPage();
            _testsFromUi = projectPage.GetTests();
            _testsFromApi = ApplicationApi.GetTests(_projectId).OrderByDescending(x=>x.StartTime).Take(_testsFromUi.Count).ToList();
            _testsFromApi.ForEach(x=>x.Status=x.Status.ToUpper());
            Assert.AreEqual(_testsFromUi.Select(x=>x.StartTime), _testsFromUi.Select(x => x.StartTime).OrderByDescending(x=>x), "Tests on the first page are not sorted in descending order of date");
            Assert.IsTrue(_testsFromUi.SequenceEqual(_testsFromApi), "Tests from page do not match those returned by the API request");

            Browser.GoBack();
            _browserTabsCount = Browser.Tabs().TabHandles.Count;
            homePage.ClickAddProjectButton();
            Browser.Tabs().SwitchToLastTab();
            AddProjectPage addProjectPage = new AddProjectPage();
            addProjectPage.InputProjectName(newProjectName);
            addProjectPage.ClickSaveProjectButton();
            Assert.IsTrue(addProjectPage.IsProjectSaveSuccess(), "The message about the successful saving of the project is not appear");
            Browser.Driver.Close();
            Assert.IsTrue(Browser.Tabs().TabHandles.Count == _browserTabsCount, "Add project window is not closed");
            Browser.Tabs().SwitchToLastTab();
            Browser.Refresh();
            Assert.IsTrue(homePage.IsProjectDisplayed(newProjectName), $"Project: '{newProjectName}' is not appear in project list");

            homePage.ClickProjectButton(newProjectName);
            testName = TestContext.CurrentContext.Test.ClassName;
            methodName = TestContext.CurrentContext.Test.MethodName;
            TestInfo testInfo = new TestInfo()
            {
                SessionId = sessionId,
                ProjectName = newProjectName,
                TestName = testName,
                MethodName = methodName,
                Env = environment,
                BrowserName = browserName
            };
            _testId = ApplicationApi.AddTest(testInfo);
            ApplicationApi.AddLogsToTest(_testId, logPath);
            ApplicationApi.AddScreenshotToTest(_testId, AqualityServices.Browser.GetScreenshot());
            Assert.IsTrue(projectPage.IsTestDisplayed(_testId), $"Test ¹{_testId} is not appear");
        }

        private static readonly object[] TestData =
        {
            new object[] {
                TestDataSettings.GetValue<string>("login"),
                TestDataSettings.GetValue<string>("password"),
                TestDataSettings.GetValue<int>("variant"),
                TestDataSettings.GetValue<string>("projectName"),
                TestDataSettings.GetValue<string>("newProjectName")
            },
        };
    }
}