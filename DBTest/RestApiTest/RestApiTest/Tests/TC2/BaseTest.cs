using NUnit.Framework;
using RestApiTest.Data;
using RestApiTest.DataBase;
using RestApiTest.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiTest.Tests.TC2
{
    public class BaseTest
    {
        private int projectId = DataProvider.GetDBTestData().ProjectId;
        protected int authorId;
        protected const int countOfTests = 5;
        protected static MySQLDB db = MySQLDB.Connect();
        protected List<int> testId;
        private List<int> numbers;
        private Random random = new Random();
        protected Test test;
        protected Test updatedTest;
        private Author author;

        [SetUp]
        protected void Setup()
        {
            author = AddAuthor();
            db.AuthorTable().Add(author);
            authorId = db.AuthorTable().Read().Id;
            numbers = db.TestTable().Get("id").Select(x => random.Next(1, db.TestTable().Get("id").Count)).Where(x=>x>10).Where(x => x/100 % 10 == x/10%10 || x / 10 % 10 ==x % 10).Take(countOfTests).OrderBy(x=>x).ToList();
            test = new Test();
            test.ProjectId = projectId;
            test.AutorId = authorId;
            db.TestTable().Copy(test, numbers);
        }

        [TearDown]
        protected void DoAfterEachTest()
        {
            db.TestTable().Delete(testId);
            Assert.IsFalse(testId.Equals(db.TestTable().Get("id").OrderByDescending(x => x).Take(countOfTests).ToList()), "Entries not deleted");
        }

        private Author AddAuthor()
        {
            author = new Author();
            author.Name = DataProvider.GetDBTestData().AuthorName;
            author.Login = DataProvider.GetDBTestData().Login;
            author.Email = DataProvider.GetDBTestData().Email;
            return author;
        }
    }
}
