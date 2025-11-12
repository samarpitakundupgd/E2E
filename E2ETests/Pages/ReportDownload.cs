using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class ReportDownload
    {
        private readonly IWebDriver _driver;

        // Constructor
        public ReportDownload(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement CaseCDFilterIcon => _driver.FindElement(By.XPath("(//i[@class='rzi rz-grid-filter-icon '])[7]"));
        private IWebElement CaseCDFilterInput => _driver.FindElement(By.XPath("(//input[@class='rz-textbox rz-state-empty'])[11]"));
        private IWebElement CaseCDFilterApplyButton => _driver.FindElement(By.XPath("(//button[@type='submit'])[7]"));
        private IWebElement DownloadResultBtn => _driver.FindElement(By.XPath("//button[@title='Download results as a PDF.']"));
        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickCaseCDFilterIcon()
        {
            CaseCDFilterIcon.Click();
        }
        public void ClickCaseCDFilterApplyButton()
        {
            CaseCDFilterApplyButton.Click();
        }

        public void ClickDownloadResultBtn()
        {
            DownloadResultBtn.Click();
        }

        public void EnterCaseId(string caseid)
        {
            CaseCDFilterInput.SendKeys(caseid);
        }

    }
}
