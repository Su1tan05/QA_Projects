using Aquality.Selenium.Browsers;

namespace TDTask.Utils
{
    public static class AuthorizeUtils
    {
        private static readonly Browser Browser = AqualityServices.Browser;
        private const string Protocol = "http://";
        private static string _url;

        public static void Authorize(string username, string password)
        {
            AqualityServices.Logger.Info("Authorization on the site");
            _url = $"{Protocol}{username}:{password}@{Browser.Driver.Url.Replace($"{Protocol}", "")}";
            Browser.GoTo(_url);
        }
    }
}
