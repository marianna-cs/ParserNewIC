using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using ParserNewIc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParserNewIC
{
    public class UserAuthorization
    {
        private IWebDriver _driver = new ChromeDriver();

        private string Email = "Support@bro-car.com.ua";

        private string Password = "153426Aa!";

        private string Linq = "https://sso.intercars.eu/oxauth/login.htm";

        private void LoadLinq()
        {
            _driver.Url = Linq;
            /*WaitHelpers.WaitUntilElementClickable(_driver, By.XPath(@".//button[@type='button']"), 40);
            var buttonCountry = _driver.FindElement(By.XPath(@".//button[@type='button']"));
            buttonCountry.Click();*/
            WaitHelpers.WaitUntilElementClickable(_driver, By.Id(@"loginForm:username"), 40);
        }

        private void LoginFilling()
        {
            var inputUser = _driver.FindElement(By.Id(@"loginForm:username"));
            inputUser.Clear();
            inputUser.SendKeys(Email);
        }

        private void PasswordFilling()
        {
            var inputPassword = _driver.FindElement(By.Id(@"loginForm:password"));
            inputPassword.Clear();
            inputPassword.SendKeys(Password);
        }

        private void RememberMe()
        {
            var inputRemember = _driver.FindElement(By.Id(@"rememberme"));
            inputRemember.Click();
        }

        public void Login()
        {
            LoadLinq();
            LoginFilling();
            PasswordFilling();
            RememberMe();
            var buttonLogin = _driver.FindElement(By.Id(@"loginForm:loginButton"));
            buttonLogin.Click();

        }
    }
}
