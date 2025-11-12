using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2ETests.Pages
{
    public class ViewerScreen
    {
        private readonly IWebDriver _driver;

        // Constructor
        public ViewerScreen(IWebDriver driver)
        {
            _driver = driver;
        }

        // Page Elements
        private IWebElement CaseSlide1 => _driver.FindElement(By.XPath("(//span[@class='rz-cell-data']//a)[1]"));
        private IWebElement CaseSlide2 => _driver.FindElement(By.XPath("(//span[@class='rz-cell-data']//a)[2]"));
        private IWebElement NoAnnotation => _driver.FindElement(By.XPath("//p[text()='No annotations.']"));
        private IWebElement NoData => _driver.FindElement(By.XPath("//label[text()='There is no data for this case']"));
        private IWebElement Canvas => _driver.FindElement(By.XPath("(//canvas)[1]"));
        private IWebElement Barcode => _driver.FindElement(By.XPath("//button[@title='Barcode']"));
        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickCaseSlide1()
        {
            CaseSlide1.Click();
        }
        public void ClickCaseSlide2()
        {
            CaseSlide2.Click();
        }
        public void ClickBarcode()
        {
            Barcode.Click();
        }

    }
}
