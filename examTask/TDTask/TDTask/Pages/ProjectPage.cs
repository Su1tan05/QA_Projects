using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using TDTask.Models;

namespace TDTask.Pages
{
    public class ProjectPage : Form
    {
        private static readonly ITextBox TestsName = ElementFactory.GetTextBox(By.XPath("//table[@class='table']//tr//td[1]"), "TestsNameList");

        public ProjectPage() : base(TestsName.Locator, "ProjectPage")
        {
                
        }

        private static  IList<ITextBox> TestsNameList => ElementFactory.FindElements<ITextBox>(TestsName.Locator, "TestsNameList");
        private static  IList<ITextBox> TestsMethodList => ElementFactory.FindElements<ITextBox>(By.XPath($"//table[@class='table']//tr//td[2]"), "TestsMethodList");
        private static  IList<ITextBox> TestsStatusList => ElementFactory.FindElements<ITextBox>(By.XPath($"//table[@class='table']//tr//td[3]"), "TestsStatusList");
        private static  IList<ITextBox> TestsStartTimeList => ElementFactory.FindElements<ITextBox>(By.XPath($"//table[@class='table']//tr//td[4]"), "TestsStartTimeList");
        private static  IList<ITextBox> TestsEndTimeList => ElementFactory.FindElements<ITextBox>(By.XPath($"//table[@class='table']//tr//td[5]"), "TestsEndTimeList");
        private static  IList<ITextBox> TestsDurationTimeList => ElementFactory.FindElements<ITextBox>(By.XPath($"//table[@class='table']//tr//td[6]"), "TestsDurationTimeList");
        private static ILink TestNameLink(int testId) => ElementFactory.GetLink(By.XPath($"//table[@id='allTests']//a[contains(@href,'testId={testId}')]"), $"Test:{testId}");

        private static List<string> _testsName;
        private static List<string> _testsMethod;
        private static List<string> _testsStatus;
        private static List<string> _testsStartTime;
        private static List<string> _testsEndTime;
        private static List<string> _testsDurationTime;
        private static Test _test;
        private static List<Test> _tests;

        public List<Test> GetTests()
        {
            TestsName.State.WaitForExist();
            _tests = new List<Test>();
            _testsName = GetTestsName();
            _testsMethod = GetTestsMethod();
            _testsStatus = GetTestsStatus();
            _testsStartTime = GetTestsStartTime();
            _testsEndTime = GetTestsEndTime();
            _testsDurationTime = GetTestsDurationTime();
            for (int i = 0; i < _testsStatus.Count; i++)
            {
                _test = new Test
                {
                    Name = _testsName[i].Length !=0 ? _testsName[i] : null,
                    Method = _testsMethod[i].Length != 0 ? _testsMethod[i] : null,
                    Status = _testsStatus[i].Length != 0 ? _testsStatus[i].ToUpper() : null,
                    StartTime = _testsStartTime[i].Length != 0 ? _testsStartTime[i] : null,
                    EndTime = _testsEndTime[i].Length != 0 ? _testsEndTime[i] : null,
                    Duration = _testsDurationTime[i].Length != 0 ? _testsDurationTime[i] : null
                };
                _tests.Add(_test);
            }
            return _tests;
        }

        private List<string> GetTestsStartTime()
        {
            _testsStartTime = new List<string>();
            foreach (var item in TestsStartTimeList)
                _testsStartTime.Add(item.Text);
            return _testsStartTime;
        }

        private List<string> GetTestsEndTime()
        {
            _testsEndTime = new List<string>();
            foreach (var item in TestsEndTimeList)
                _testsEndTime.Add(item.Text);
            return _testsEndTime;
        }

        private List<string> GetTestsDurationTime()
        {
            _testsDurationTime = new List<string>();
            foreach (var item in TestsDurationTimeList)
                _testsDurationTime.Add(item.Text);
            return _testsDurationTime;
        }

        private List<string> GetTestsStatus()
        {
            _testsStatus = new List<string>();
            foreach (var item in TestsStatusList)
                _testsStatus.Add(item.Text);
            return _testsStatus;
        }

        private List<string> GetTestsMethod()
        {
            _testsMethod = new List<string>();
            foreach (var item in TestsMethodList)
                _testsMethod.Add(item.Text);
            return _testsMethod;
        }

        private List<string> GetTestsName()
        {
            _testsName = new List<string>();
            foreach (var item in TestsNameList)
                _testsName.Add(item.Text);
            return _testsName;
        }

        public bool IsTestDisplayed(int testId) => TestNameLink(testId).State.WaitForDisplayed();
    }
}
