using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class ExportToExcel
    {
        private readonly IWebDriver _driver;

        // Constructor
        public ExportToExcel(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement MenuIcon => _driver.FindElement(By.XPath("(//div[@class='rz-navigation-item-link'])[1]"));
        private IWebElement ExportCSVLink => _driver.FindElement(By.XPath("(//a[@href='Export'])[1]"));
        private IWebElement ExportBtn => _driver.FindElement(By.XPath("//button[@id='btnExportToExcel']"));
        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickMenuIcon()
        {
            MenuIcon.Click();
        }
        public void ClickExportCSVLink()
        {
            ExportCSVLink.Click();
        }
        public void ClickExportBtn()
        {
            ExportBtn.Click();
        }

    }
}
