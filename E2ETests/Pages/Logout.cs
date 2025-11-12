using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class Logout
    {
        private readonly IWebDriver _driver;

        // Constructor
        public Logout(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement SignOut => _driver.FindElement(By.XPath("//div[@class='rz-navigation-item-link' and .//span[text()='Sign Out']]"));
        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickSignOut()
        {
            SignOut.Click();
        }
    }
}
