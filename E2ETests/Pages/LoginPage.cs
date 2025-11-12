using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace E2ETests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        public IReadOnlyCollection<IWebElement> Logos => _driver.FindElements(By.XPath("//img[@alt='4D Path']"));
        public string WelcomeTxt => _driver.FindElement(By.XPath("//h1[text()='Welcome']")).Text;
        public string WelcomePara => _driver.FindElement(By.XPath("(//p)[1]")).Text;
        private IWebElement SignInBtn => _driver.FindElement(By.XPath("//a[text()='Sign in']"));
        public By SignIn => By.XPath("//a[text()='Sign in']");
        private IWebElement Username => _driver.FindElement(By.XPath("//input[@id='username']"));
        private IWebElement Password => _driver.FindElement(By.XPath("//input[@id='password']"));
        private IWebElement ContinueBtn => _driver.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement ErrorMsg => _driver.FindElement(By.XPath("//span[@id='error-element-password']"));
        private IWebElement ForgotPasswordLink => _driver.FindElement(By.XPath("//a[text()='Reset password']"));
        private IWebElement UserEmail => _driver.FindElement(By.XPath("//input[@id='email']"));
        public string ForgetEmailSentSuccessMsg => _driver.FindElement(By.XPath("//h1[text()='Check Your Email']")).Text;
        public By ForgetEmailSentSuccessLocator => By.XPath("//h1[text()='Check Your Email']");
        public By CreateCaseBtn => By.XPath("//span[text()='Create New Case']");
        public string CreateCaseBtnTxt => _driver.FindElement(By.XPath("//span[text()='Create New Case']")).Text;
        private IWebElement MenuIcon => _driver.FindElement(By.XPath("(//div[@class='rz-navigation-item-link'])[1]"));
        private IWebElement SignOut => _driver.FindElement(By.XPath("//li[@id='miSignOut']"));


        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickSignIn()
        {
            SignInBtn.Click();
        }

        public void ClickForgotPasswordLink()
        {
            ForgotPasswordLink.Click();
        }

        public void ClickContinue()
        {
            ContinueBtn.Click();
        }

        public void ClickMenuIcon()
        {
            MenuIcon.Click();
        }

        public void ClickSignOut()
        {
            SignOut.Click();
        }

        public void EnterUsername(string username)
        {
            Username.SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Password.SendKeys(password);
        }

        public void EnterUserEmail(string useremail)
        {
            UserEmail.SendKeys(useremail);
        }

        public string GetErrorMessage()
        {
            return ErrorMsg.Text;
        }
    }
}
