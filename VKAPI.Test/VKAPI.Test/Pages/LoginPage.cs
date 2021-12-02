using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VKAPI.Test.Pages
{
    public class LoginPage : Form
    {
        private static readonly By SignInButtonLocator = By.XPath("//button[@id='index_login_button']");

        public LoginPage() : base(SignInButtonLocator, "LoginPage")
        {
        }

        private readonly IButton SignInButton = ElementFactory.GetButton(SignInButtonLocator, "Sign in button");
        private readonly ITextBox LoginInput = ElementFactory.GetTextBox(By.XPath("//input[@id='index_email']"), "LoginInput TextBox");
        private readonly ITextBox PasswordInput = ElementFactory.GetTextBox(By.XPath("//input[@id='index_pass']"), "PasswordInput TextBox");

        public void InputLogin(string login)
        {
            LoginInput.ClearAndType(login);
        }
        public void InputPassword(string password)
        {
            PasswordInput.ClearAndType(password);
        }
        public void ClickSignInButton() => SignInButton.Click();
    }
}
