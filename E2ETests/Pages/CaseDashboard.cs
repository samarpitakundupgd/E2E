using OpenQA.Selenium;


namespace FourDDashboard.SeleniumTests.Pages
{
    public class CaseDashboard
    {
        private readonly IWebDriver _driver;

        // Constructor
        public CaseDashboard(IWebDriver driver)
        {
            _driver = driver;
        }


        //Page Elements

        //Create Case Elements
        private IWebElement CreateCaseBtn => _driver.FindElement(By.XPath("//button[@id='btnCreateNewCase']"));
        private IWebElement CaseCode => _driver.FindElement(By.XPath("//input[@id='tbCaseCode']"));
        private IWebElement BatchCode => _driver.FindElement(By.XPath("//input[@id='tbBatchCode']"));
        private IWebElement SubmitBtn => _driver.FindElement(By.XPath("//button[@id='btnSubmit']"));

        //Account to QA Org
        private IWebElement SelectAccountDropDown => _driver.FindElement(By.XPath("//label[text()='4DPATH - DEFAULT']"));
        private IWebElement SelectQAOrg => _driver.FindElement(By.XPath("//span[text()='QAORG - QAACCOUNT']"));

        //Case Elements
        private IWebElement FirstCaseRowToggle => _driver.FindElement(By.XPath("(//span[@class='rz-row-toggler rzi-chevron-circle-right'])[1]"));
        public By FirstCaseRowToggleBtn => By.XPath("(//span[@class='rz-row-toggler rzi-chevron-circle-right'])[1]");
        private IWebElement AddSlides => _driver.FindElement(By.XPath("(//button[@id='btnAddSlides'])[1]"));

        //Add Slides and Case Details to the Case Elements
        private IWebElement SlideSearch => _driver.FindElement(By.XPath("//input[@id='txtSearchText']"));
        private IWebElement SelectSlide => _driver.FindElement(By.XPath("(//td)[1]"));
        private IWebElement Organ => _driver.FindElement(By.XPath("(//label[text()='Origin Organ']//following::label)[1]"));
        private IWebElement Breast => _driver.FindElement(By.XPath("//li[@aria-label='Breast']"));
        private IWebElement SampleType => _driver.FindElement(By.XPath("(//label[text()='Sample Type']//following::label)[1]"));
        private IWebElement Biopsy => _driver.FindElement(By.XPath("//li[@aria-label='Biopsy']"));
        private IWebElement Part => _driver.FindElement(By.XPath("(//label[text()='Sample Part']//following::label)[1]"));
        private IWebElement PartCode => _driver.FindElement(By.XPath("//li[@aria-label='Lesion']"));
        private IWebElement DiseaseSetting => _driver.FindElement(By.XPath("(//label[text()='Disease Setting']//following::label)[1]"));
        private IWebElement Neoadjuvant => _driver.FindElement(By.XPath("//li[@aria-label='Neoadjuvant']"));
        private IWebElement SubPartCode => _driver.FindElement(By.XPath("//input[@id='tbSubPartCd']"));
        private IWebElement BlockCode => _driver.FindElement(By.XPath("//input[@id='tbBlockCd']"));
        private IWebElement SaveBtn => _driver.FindElement(By.XPath("//button[@id='btnSave']"));
        private IWebElement OKBtn => _driver.FindElement(By.XPath("//button[@id='btnOk']"));
        public By SearchField => By.XPath("//input[@id='txtSearchText']");
        public By SelectedAcct => By.XPath("//label[text()='Selected Account : ']");
        public By OKBtnClick => By.XPath("//button[@id='btnOk']");

        //Submit Case
        public IReadOnlyCollection<IWebElement> Checkboxes => _driver.FindElements(By.XPath("//div[@class='rz-chkbox-box']"));
        private IWebElement SubmitCaseBtn => _driver.FindElement(By.XPath("(//button[@id='btnSubmitCase'])[1]"));


        //Download Results
        private IWebElement DownloadResult => _driver.FindElement(By.XPath("(//button[@title='Download results as a PDF.'])[1]"));

        // Logout
        private IWebElement MenuIcon => _driver.FindElement(By.XPath("(//div[@class='rz-navigation-item-link'])[1]"));
        private IWebElement SignOut => _driver.FindElement(By.XPath("//li[@id='miSignOut']"));



        // Page Actions
        public void GoTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void ClickCreateCaseBtn()
        {
            CreateCaseBtn.Click();
        }
        public void ClickSelectAccountDropDown()
        {
            SelectAccountDropDown.Click();
        }
        public void ClickSelectQAOrg()
        {
            SelectQAOrg.Click();
        }

        public void ClickSubmitBtn()
        {
            SubmitBtn.Click();
        }
        public void ClickFirstCaseRowToggle()
        {
            FirstCaseRowToggle.Click();
        }

        public void ClickAddSlides()
        {
            AddSlides.Click();
        }

        public void ClickSelectSlide()
        {
            SelectSlide.Click();
        }

        public void ClickOrgan()
        {
            Organ.Click();
        }

        public void ClickBreast()
        {
            Breast.Click();
        }

        public void ClickSampleType()
        {
            SampleType.Click();
        }

        public void ClickBiopsy()
        {
            Biopsy.Click();
        }

        public void ClickPart()
        {
            Part.Click();
        }

        public void ClickPartCode()
        {
            PartCode.Click();
        }

        public void ClickDiseaseSetting()
        {
            DiseaseSetting.Click();
        }

        public void ClickNeoadjuvant()
        {
            Neoadjuvant.Click();
        }

        public void ClickSaveBtn()
        {
            SaveBtn.Click();
        }

        public void ClickOKBtn()
        {
            OKBtn.Click();
        }

        public void ClickSubmitCaseBtn()
        {
            SubmitCaseBtn.Click();
        }
        public void ClickDownloadResult()
        {
            DownloadResult.Click();
        }



        // Page Actions
        public void EnterCaseCode(string casecode)
        {
            CaseCode.SendKeys(casecode);
        }

        public void EnterBatchCode(string batchcode)
        {
            BatchCode.SendKeys(batchcode);
        }

        public void EnterSlideSearch(string slidesearch)
        {
            SlideSearch.SendKeys(slidesearch);
        }

        public void EnterSubPartCode(string subpartcode)
        {
            SubPartCode.SendKeys(subpartcode);
        }

        public void EnterBlockCode(string blockcode)
        {
            BlockCode.SendKeys(blockcode);
        }


        public void ClickAllCheckboxes()
        {
            var checkboxes = Checkboxes;
            foreach (var checkbox in checkboxes)
            {
                checkbox.Click();
            }
        }

        // Lgout
        public void ClickMenuIcon()
        {
            MenuIcon.Click();
        }

        public void ClickSignOut()
        {
            SignOut.Click();
        }

    }

}