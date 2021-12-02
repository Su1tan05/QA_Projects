using NUnit.Framework;
using RestApiTest.ApiUtils;
using System.Collections.Generic;
using RestApiTest.Models;
using RestApiTest.Utils;
using System.Net;
using RestSharp;
using RestApiTest.Data;
using RestApiTest.DataBase;
using RestApiTest.DataBase.Models;
using System;

namespace RestApiTest.Tests.TC1
{
    [TestFixture]
    public class JsonPlaceHolderApiTest : BaseTest
    {
        private string randText = RandomUtil.GetRandomText();

        [Test]
        [TestCaseSource("TestData")]
        public void Test1(User userData, int customUserId, int[] numberOfPost, int userId)
        {
            testStartTime = DateTime.Now;
            PostResponse postResponse =  ApiUtil.GetPosts();       
            Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode, "Wrong status code");
            Assert.AreEqual(DataFormat.Json, ApiUtil.GetRestRequest().RequestFormat, "List of posts is not in JSON format");
            Assert.IsTrue(SortUtil.isSortByAscending(postResponse.Posts), "The list is not sorted by ascending");

            postResponse = ApiUtil.GetPost(numberOfPost[0]);
            Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode, "Wrong status code");
            Assert.AreEqual(userId, postResponse.Post.UserId, "Wrong userId");
            Assert.AreEqual(numberOfPost[0], postResponse.Post.Id, "Wrong id");
            Assert.NotNull(postResponse.Post.Title, "Title is null");
            Assert.NotNull(postResponse.Post.Body, "Body is null");

            postResponse = ApiUtil.GetPost(numberOfPost[1]);
            Assert.AreEqual(HttpStatusCode.NotFound, postResponse.StatusCode, "Response isn't null");

            Post post = new Post() {
                Body = randText,
                Title = randText,
                UserId = 1
            };
            postResponse = ApiUtil.CreatePost(post);
            Assert.AreEqual(HttpStatusCode.Created, postResponse.StatusCode, "Post is not created");
            Assert.AreEqual(randText, postResponse.Post.Title, "Title is different");
            Assert.AreEqual(randText, postResponse.Post.Body, "Body is different");
            Assert.AreEqual(post.UserId, postResponse.Post.UserId, "UserId is different");    
            Assert.IsTrue(postResponse.Post.Id.ToString().Length>0, "Id is not present in the response");

            UserResponse userResponse = ApiUtil.GetUsers();
            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode, "Post is not created");
            Assert.AreEqual(DataFormat.Json, ApiUtil.GetRestRequest().RequestFormat, "List of users is not in JSON format");
            User user =  userResponse.Users[customUserId-1];
            Assert.IsTrue(user.Equals(userData), $"User with id:{customUserId} has different data");

            userResponse = ApiUtil.GetUser(customUserId);
            Assert.IsTrue(userResponse.User.Equals(user), "The user's data does not match the data obtained in step 5.");
        }

        private static readonly object[] TestData =
        {
            new object[] { DataProvider.GetTestData().User,
                DataProvider.GetTestData().CustomUserId,
                DataProvider.GetTestData().NumberOfPost,
                DataProvider.GetTestData().UserId
            },
        };
    }
}