using Newtonsoft.Json;
using RestApiTest.Data;
using RestApiTest.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTest.ApiUtils
{
    public static class ApiUtil
    {
        private static RestClient client = new RestClient(DataProvider.GetConfigData().ApiUrl);
        private static RestRequest request;
        private static string content;
        private const string postsPath = "/posts";
        private const string usersPath = "/users";
        public static PostResponse GetPost(int numberOfPost)
        {
            request = new RestRequest($"{postsPath}/{numberOfPost}", DataFormat.Json);
            IRestResponse response = client.Get(request);
            content = GetContent(response);
            PostResponse postResponse = new PostResponse();
            postResponse.Post = JsonConvert.DeserializeObject<Post>(content);
            postResponse.StatusCode = response.StatusCode;
            return postResponse;
        }

        public static PostResponse GetPosts()
        {
            request = new RestRequest($"{postsPath}", DataFormat.Json);
            IRestResponse response = client.Get(request);
            content = GetContent(response);
            PostResponse postResponse = new PostResponse();
            postResponse.Posts = JsonConvert.DeserializeObject<List<Post>>(content);
            postResponse.StatusCode = response.StatusCode;
            return postResponse;
        }

        public static UserResponse GetUser(int numberOfUser)
        {
            request = new RestRequest($"{usersPath}/{numberOfUser}", DataFormat.Json);
            IRestResponse response = client.Get(request);
            content = GetContent(response);
            UserResponse userResponse = new UserResponse();
            userResponse.User = JsonConvert.DeserializeObject<User>(content);
            userResponse.StatusCode = response.StatusCode;
            return userResponse;
        }

        public static UserResponse GetUsers()
        {
            request = new RestRequest($"{usersPath}", DataFormat.Json);
            IRestResponse response = client.Get(request);
            content = GetContent(response);
            UserResponse userResponse = new UserResponse();
            userResponse.Users = JsonConvert.DeserializeObject<List<User>>(content);
            userResponse.StatusCode = response.StatusCode;
            return userResponse;
        }

        public static PostResponse CreatePost(Post post)
        {
            request = new RestRequest($"{postsPath}", DataFormat.Json);
            request.AddJsonBody(post);
            IRestResponse response = client.Post(request);
            content = GetContent(response);
            PostResponse postResponse = new PostResponse();
            postResponse.Post = JsonConvert.DeserializeObject<Post>(content);
            postResponse.StatusCode = response.StatusCode;
            return postResponse;
        }

        public static string GetContent(IRestResponse restResponse) => restResponse.Content;

        public static RestRequest GetRestRequest() => request;
    }
}
