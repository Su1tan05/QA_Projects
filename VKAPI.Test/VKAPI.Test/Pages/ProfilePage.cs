using System;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VKAPI.Test.Pages
{
    public class ProfilePage : Form
    {
        private static readonly By ProfilePageLocator = By.XPath("//div[@id='page_avatar']");
        private int _userId;

        public ProfilePage(int userId) : base(ProfilePageLocator, "ProfilePage")
        {
            _userId = userId;
        }

        private static ILink post;
        private static ITextBox content;
        public static readonly string postLocator = "//div[@data-post-id='{0}_{1}']";
        public static readonly string postContentLocator = "//div[contains(text(),'{0}')]";
        public static readonly string showCommentButtonLocator = "//div[@id='replies{0}_{1}']/a";
        public static readonly string commentLocator = "//div[@id='post{0}_{1}']";
        public static readonly string likeButtonLocator = "//div[@class='like_wrap _like_wall{0}_{1} ']/div/div/a";
        public static readonly string checkPostkLikeLocator = "//input[@id='like_real_count_wall{0}_{1}']";

        public bool isPostCreated(string postContent, int postId)
        {
            post = ElementFactory.GetLink(By.XPath(String.Format(postLocator, _userId, postId)), "Post author");
            content = ElementFactory.GetTextBox(By.XPath(String.Format(postContentLocator, postContent)), "PostContent");
            post.State.WaitForDisplayed();
            content.State.WaitForDisplayed();
            return post.State.IsDisplayed && content.State.IsDisplayed;
        }

        public bool isTextChanged() => content.State.WaitForNotDisplayed();

        public bool isCommentCreated(string commentContent, int postId)
        { 
            IButton showCommentButton = ElementFactory.GetButton(By.XPath(String.Format(showCommentButtonLocator, _userId, postId)), "Show next comment");
            showCommentButton.Click();
            ILink commentAuthor = ElementFactory.GetLink(By.XPath(String.Format(commentLocator, _userId, postId)), "Comment author");
            ITextBox comment = ElementFactory.GetTextBox(By.XPath(String.Format(postContentLocator, commentContent)), "Comment");
            return commentAuthor.State.WaitForDisplayed() && comment.State.WaitForDisplayed();
        }

        public void ClickLikeButton(int postId)
        {
            IButton likeButton = ElementFactory.GetButton(By.XPath(String.Format(likeButtonLocator, _userId, postId)), "LikeButton");
            likeButton.Click();
        }

        public bool isPostLiked(int postId)
        {
            IButton checkLike = ElementFactory.GetButton(By.XPath(String.Format(checkPostkLikeLocator, _userId, postId)), "Check post like");
            return checkLike.State.WaitForExist();
        }
        
        public bool isPostDeleted(int postId)
        {
            post = ElementFactory.GetLink(By.XPath(String.Format(postLocator, _userId, postId)), "Post author");
            return post.State.WaitForNotDisplayed();
        }
    }
}
