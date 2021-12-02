using OpenQA.Selenium;
using Aquality.Selenium.Browsers;

namespace TDTask.Utils
{
    public static class CookieUtils
    {
        private static Cookie Cookie(string name, string value) => new Cookie(name, value);

        public static void AddCookie(string name, string value)
        {
            AqualityServices.Logger.Info("Adding cookies to the site");
            AqualityServices.Browser.Driver.Manage().Cookies.AddCookie(Cookie(name, value));
        }
    }
}
