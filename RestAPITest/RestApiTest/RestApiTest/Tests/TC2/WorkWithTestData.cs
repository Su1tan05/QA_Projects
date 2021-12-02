using NUnit.Framework;
using RestApiTest.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.Tests.TC2
{
    public class WorkWithTestData : BaseTest
    {
        [Test]
        public void TestFromDBInfo()
        {
            testId = db.TestTable().Get("id").OrderByDescending(x=>x).Take(countOfTests).ToList();
            test = new Test();
            foreach (var item in testId)
            {
                test.Id = item;
                test.StatusId = 2;
                test.StartTime = DateTime.Now;
                test.EndTime = DateTime.Now.AddSeconds(5);
                updatedTest = db.TestTable().Update(test);
                Assert.IsTrue(test.Equals(updatedTest), "Information not updated");
            }
        }
    }
}
