using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Newtonsoft.Json;
using RestSharp;
using VKAPI.Test.Models;
using VKAPI.Test.DataProvider;
using System.IO;
using VKAPI.Test.Models.SaveImageModel;

namespace VKAPI.Test.ApiUtils
{
    public static class WallApiUtil
    {
        private static ISettingsFile Settings => new JsonSettingsFile("settings.custom.json");
        private static RestClient client = new RestClient(Settings.GetValue<string>("ApiClientUrl"));
        private static RestRequest request;
        private static string content;
        private static string imageUrl;
        private static readonly string token = TestDataProvider.GetData().Token;
        private static readonly int userId = TestDataProvider.GetData().UserId;
        private static readonly string version = $"v=5.131";
        private static readonly string rootDirPath = Settings.GetValue<string>("RootDirectoryPath");
        private static readonly string downloadedImageName = Settings.GetValue<string>("DownloadedImageName");
        private static readonly string createPostRequest = $"wall.post?owner_id={userId}&access_token={token}&{version}" + "&message={0}";
        private static readonly string editPostRequest = $"wall.edit?owner_id={userId}&access_token={token}&{version}" + "&post_id={0}&message={1}";
        private static readonly string saveWallPhotoRequest = $"photos.saveWallPhoto?owner_id={userId}&access_token={token}&{version}" + "&server={0}&photo={1}&hash={2}";
        private static readonly string getWallUploadServerRequest = $"photos.getWallUploadServer?user_id ={userId}&access_token={token}&{version}";
        private static readonly string createCommentRequest = $"wall.createComment?owner_id={userId}&access_token={token}&{version}" + "&post_id={0}&message={1}";
        private static readonly string deletePostRequest = $"wall.delete?owner_id={userId}&access_token={token}&{version}" + "&post_id={0}";
        
        public static int CreatePost(string message)
        {
            request = new RestRequest(String.Format(createPostRequest, message), DataFormat.Json);
            IRestResponse response = client.Post(request);
            content = response.Content;
            PostResponse postResponse = JsonConvert.DeserializeObject<PostResponse>(content);
            return postResponse.post.PostId;
        }

        public static void EditPost(int postId, string message)
        {
            request = new RestRequest(String.Format(editPostRequest, postId, message), DataFormat.Json);
            IRestResponse response = client.Post(request);
            content = response.Content;
        }

        public static string EditPost(int postId, string message, string imagePath)
        {
            string attachments = SaveImage(imagePath);
            request = new RestRequest(String.Format(editPostRequest, postId, message) + $"&attachments={attachments}", DataFormat.Json);
            IRestResponse response = client.Post(request);
            content = response.Content;
            return imageUrl;
        }

        public static void DeletePost(int postId)
        {
            request = new RestRequest(String.Format(deletePostRequest, postId), DataFormat.Json);
            client.Post(request);
        }

        public static void DownloadPostImage(string imageUrl)
        {
            RestClient restClient = new RestClient(imageUrl);
            var fileBytes = restClient.DownloadData(new RestRequest("#", Method.GET));
            File.WriteAllBytes(Path.Combine(rootDirPath, downloadedImageName), fileBytes);
        }

        private static string GetUploadImageUrl()
        {
            request = new RestRequest(getWallUploadServerRequest, DataFormat.Json);
            IRestResponse response = client.Get(request);
            content = response.Content;
            UploadImageResponse uploadImageResponse = JsonConvert.DeserializeObject<UploadImageResponse>(content);
            return uploadImageResponse.UploadImage.UploadUrl;
        }

        public static string SaveImage(string imagePath)
        {
            Image image = UploadImageToUrl(imagePath);
            request = new RestRequest(String.Format(saveWallPhotoRequest, image.Server, image.Photo, image.Hash), DataFormat.Json);
            IRestResponse response = client.Post(request);
            content = response.Content;
            SaveImageResponse saveImageResponse = JsonConvert.DeserializeObject<SaveImageResponse>(content);
            imageUrl = saveImageResponse.SaveImage[0].Sizes.Where(x => x.Height >= 1000).Select(u => u.Url).ToList()[0].ToString();
            return $"photo{saveImageResponse.SaveImage[0].OwnerId}_{saveImageResponse.SaveImage[0].Id}";
        }

        private static Image UploadImageToUrl(string imagePath)
        {
            var client = new RestClient(GetUploadImageUrl());
            var request = new RestRequest(Method.POST);
            request.AddFile("photo", imagePath);
            IRestResponse response = client.Execute(request);
            content = response.Content;
            Image image = JsonConvert.DeserializeObject<Image>(content);
            return image;
        }

        public static void CreateComment(int postId, string message)
        {
            request = new RestRequest(String.Format(createCommentRequest, postId, message), DataFormat.Json);
            IRestResponse response = client.Post(request);
        }
    }
}
