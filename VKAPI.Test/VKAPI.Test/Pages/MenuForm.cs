using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;

namespace VKAPI.Test.Pages
{
    public enum MenuElements
    {
        MyProfile = 1,
        News = 2,
        Messenger = 3,
        Friends = 4,
        Communities = 5,
        Photos = 6,
        Mysic = 7,
        Videos = 8,
        Clips = 9,
        Games = 10,
        Classfields = 11,
        MiniApps = 12,
        VkPay = 13
    }

    public class MenuForm : Form
    {
        private static readonly By MenuFormLocator = By.XPath("//li[@id='l_pr']");
        private readonly string menuElementsLocator = "//ol[@class='side_bar_ol']/li[{0}]";

        public MenuForm() : base(MenuFormLocator, "MenuForm")
        {
        }

        public void ClickMenuButton(MenuElements e)
        {
            IButton menuElementButton = ElementFactory.GetButton(By.XPath(String.Format(menuElementsLocator, (int)e)), $"{e}Button");
            menuElementButton.WaitAndClick();
        }
    }
}
