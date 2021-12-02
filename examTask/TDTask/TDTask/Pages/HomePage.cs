using System.Text.RegularExpressions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace TDTask.Pages
{
    public class HomePage : Form
    {

        private static readonly By HomePageLocator = By.XPath("//div[contains(@class,'list-group')]");

        public HomePage() : base(HomePageLocator, "HomePage")
        {
        }

        private static readonly IButton AddProjectButton = ElementFactory.GetButton(By.XPath("//div[@class='panel-heading']//a"), "AddProjectButton");

        private static IButton ProjectButton(string projectName) => 
            ElementFactory.GetButton(By.XPath($"//div[contains(@class,'list-group')]//a[contains(text(),'{projectName}')]"), $"{projectName}ProjectButton");

        private static ITextBox VersionText(int version) =>
            ElementFactory.GetTextBox(By.XPath($"//span[contains(text(),'Version: {version}')]"), "Version");

        public bool IsVariantDisplayed(int variant) => VersionText(variant).State.IsDisplayed;

        public void ClickProjectButton(string projectName) => ProjectButton(projectName).Click();

        public int GetProjectId(string projectName) => int.Parse(Regex.Match(ProjectButton(projectName).GetAttribute("href"), @"\d[\d.]*$").Value);

        public void ClickAddProjectButton() => AddProjectButton.WaitAndClick();

        public bool IsProjectDisplayed(string projectName) => ProjectButton(projectName).State.WaitForDisplayed();
    }
}
