using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class StaticPages
    {
        private readonly IWebDriver _driver;

        // Constructor
        public StaticPages(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement AboutToggle => _driver.FindElement(By.XPath("(//li[@id='miAbout'])[1]"));
        private IWebElement UDILink => _driver.FindElement(By.XPath("//a[@href='About']"));
        private IWebElement PolicyLink => _driver.FindElement(By.XPath("//a[@href='Policy']"));
        private IWebElement FeedbackLink => _driver.FindElement(By.XPath("//a[@href='Feedback']"));
        private IWebElement QuickStartLink => _driver.FindElement(By.XPath("//a[@href='download/4D_Dashboard_Quick_Start_Guide.pdf']"));
        private IWebElement FeedbackFormTitle => _driver.FindElement(By.XPath("//input[@id='tbTitle']"));
        private IWebElement FeedbackFormDesc => _driver.FindElement(By.XPath("//textarea[@id='taFeedbackArea']"));
        private IWebElement FeebbackSubmitBtn => _driver.FindElement(By.XPath("//button[@id='btnSubmit']"));

        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickAboutToggle()
        {
            AboutToggle.Click();
        }
        public void ClickUDILink()
        {
            UDILink.Click();
        }
        public void ClickPolicyLink()
        {
            PolicyLink.Click();
        }
        public void ClickFeedbackLink()
        {
            FeedbackLink.Click();
        }
        public void ClickQuickStartLink()
        {
            QuickStartLink.Click();
        }
        public void ClickFeebbackSubmitBtn()
        {
            FeebbackSubmitBtn.Click();
        }




        //Page Actions
        public void EnterFeedbackFormTitle(string feedbackformtitle)
        {
            FeedbackFormTitle.SendKeys(feedbackformtitle);
        }
        public void EnterFeedbackFormDesc(string feedbackformdesc)
        {
            FeedbackFormDesc.SendKeys(feedbackformdesc);
        }
    }
}
