using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RestApiTest.Data;
using RestApiTest.DataBase;
using RestApiTest.DataBase.Models;
using RestApiTest.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestApiTest.Tests.TC1
{
    public class BaseTest
    {
        private static string testStatus;
        protected static DateTime testStartTime;
        private static int statusId;
        private const int sessionKeyMaxValue = 999999999;
        private const int buildNumberMaxValue = 9;
        private int projectId = DataProvider.GetDBTestData().ProjectId;
        private string env = DataProvider.GetDBTestData().Env;
        private Test test;
        private Session session;
        private MySQLDB db = MySQLDB.Connect();

        [TearDown]
        protected void DoAfterAllTest()
        {
            session = AddSessionData();
            db.SessionTable().Add(session);
            test = AddCurrentTestInfo(db.SessionTable().Read().id);
            db.TestTable().Add(test);
            Assert.IsTrue(test.Equals(db.TestTable().Read()), "Information not added or does not match");
        }

        private Session AddSessionData()
        {
            session = new Session();
            session.SessionKey = RandomUtil.GetRandomNumber(sessionKeyMaxValue);
            session.CreatedTime = testStartTime;
            session.BuildNumber = RandomUtil.GetRandomNumber(buildNumberMaxValue);
            return session;
        }

        private Test AddCurrentTestInfo(int sessionId)
        {
            test = new Test();
            testStatus = TestContext.CurrentContext.Result.Outcome.Status.ToString();
            test.Name = TestContext.CurrentContext.Test.ClassName;
            switch (testStatus)
            {
                case "Passed":
                    statusId = 1;
                    break;
                case "Failed":
                    statusId = 2;
                    break;
                default:
                    statusId = 3;
                    break;
            }
            test.StatusId = statusId;
            test.MethodName = TestContext.CurrentContext.Test.MethodName;
            test.ProjectId = projectId;
            test.SessionId = sessionId;
            test.StartTime = testStartTime;
            test.EndTime = DateTime.Now;
            test.Env = env;
            test.Browser = null;
            return test;
        }
    }
}
