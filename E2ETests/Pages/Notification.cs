using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class Notification
    {
        private readonly IWebDriver _driver;

        // Constructor
        public Notification(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement BellIcon => _driver.FindElement(By.XPath("//i[@class='rzi rzi-light']"));
        private IWebElement CloseIcon => _driver.FindElement(By.XPath("//span[@class='rzi rzi-times']"));
        private IWebElement Dismiss => _driver.FindElement(By.XPath("//button[text()='Dismiss']"));
        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickBellIcon()
        {
            BellIcon.Click();
        }
        public void ClickCloseIcon()
        {
            CloseIcon.Click();
        }
        public void ClickDismiss()
        {
            Dismiss.Click();
        }

    }
}

