using NUnit.Framework;
using VKAPI.Test.Pages;
using VKAPI.Test.Utils;
using VKAPI.Test.ApiUtils;
using VKAPI.Test.DataProvider;
using System.Threading;
namespace VKAPI.Test.Tests
{
    public class VkTest : BaseTest
    {
        private const int textLength = 20;
        private const string imagePath = @"./images/photo.jpg";
        private readonly string randomText = RandomUtils.GetRandomText(textLength);
        private readonly string comment = RandomUtils.GetRandomText(textLength);
        private readonly string downloadImagePath = $@"./Images/{CustomSettings.GetValue<string>("DownloadedImageName")}";

        [Test]
        [TestCaseSource("TestData")]
        public void VkApiTest(string login, string password, int UserId)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.InputLogin(login);
            loginPage.InputPassword(password);
            loginPage.ClickSignInButton();

            MenuForm menuForm = new MenuForm();
            menuForm.ClickMenuButton(MenuElements.MyProfile);
            int postId = WallApiUtil.CreatePost(randomText);
            ProfilePage profile = new ProfilePage(UserId);
            Assert.IsTrue(profile.isPostCreated(randomText, postId), "There was no post on the wall");
            string imageUrl = WallApiUtil.EditPost(postId, RandomUtils.GetRandomText(textLength), imagePath);
            Assert.IsTrue(profile.isTextChanged(), "The post text has not changed");
            WallApiUtil.DownloadPostImage(imageUrl);
            Assert.IsTrue(ImageComparisonUtil.CompareImages(imagePath, downloadImagePath), "Current and uploaded images are not similar");
            WallApiUtil.CreateComment(postId, comment);
            Assert.IsTrue(profile.isCommentCreated(comment, postId),"Comment is not created");
            profile.ClickLikeButton(postId);
            Assert.IsTrue(profile.isPostLiked(postId),"Post is not liked");
            WallApiUtil.DeletePost(postId);
            Assert.IsTrue(profile.isPostDeleted(postId),"Post is not deleted");
        }

        private static readonly object[] TestData =
        {
            new object[] { 
                TestDataProvider.GetData().Login,
                TestDataProvider.GetData().Password,
                TestDataProvider.GetData().UserId
            },
        };

    }
}