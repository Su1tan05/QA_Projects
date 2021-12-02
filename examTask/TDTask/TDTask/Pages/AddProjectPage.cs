using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace TDTask.Pages
{
    public class AddProjectPage: Form
    {
        public AddProjectPage() : base(InputProjectNameTesTextBox.Locator, "AddProjectPage")
        {
        }

        private static readonly ITextBox InputProjectNameTesTextBox = ElementFactory.GetTextBox(By.XPath("//input[@id='projectName']"), "InputProjectNameTesTextBox");
        private static readonly IButton SaveProjectButton = ElementFactory.GetButton(By.XPath("//button[@type='submit']"), "SaveProjectButton");
        private static readonly ITextBox SuccessSaveProjectTextBox = ElementFactory.GetTextBox(By.XPath("//div[contains(@class,'alert alert-success')]"), "SaveProjectButton");

        public void InputProjectName(string projectName) => InputProjectNameTesTextBox.SendKeys(projectName);

        public void ClickSaveProjectButton() => SaveProjectButton.Click();

        public bool IsProjectSaveSuccess() => SuccessSaveProjectTextBox.State.IsDisplayed;
    }
}
