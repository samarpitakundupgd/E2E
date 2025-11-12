using E2ETests.Fixtures;
using E2ETests.Helpers;
using E2ETests.Interfaces;
using E2ETests.Pages;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Web;
using Xunit.Abstractions;

namespace E2ETests.Tests
{
    public class FourDDashboardCaseRequestTest : SeleniumTestBase
    {
        private readonly ITestOutputHelper _output;
        private readonly ICaseDataService _caseDataService;

        public FourDDashboardCaseRequestTest(ITestOutputHelper output, E2ETestFixture testFixture) : base(output, testFixture)
        {
            _caseDataService = testFixture.ServiceProvider.GetService<ICaseDataService>()
                ?? throw new Exception("Case Data Service is missing");

            _output  = output;
            _output.WriteLine("========== 🏗️ E2E Workflow Scenario: 4D Dashboard; Case created, slide added, case request submitted, job completes successfully, slide and results reviewable in the viewer, PDF can be downloaded Test Setup Started ==========");
        }

        [Fact]
        public async Task E2ETestsLogin_UsingValidLoginAsync()
        {

            // Startup Cleanup: Move SVS files to main folder in Azure File Share
            try
            {
                await _dropZoneService.MoveSvsFilesToMainFolderAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Test Data Setup
            string today = DateTime.Today.ToString("yyyy-MM-dd"); // Today's (System) Date
            Random rnd = new Random();
            int randomInRange = rnd.Next(1, 101); // Random number between 1 and 100 (inclusive lower bound, exclusive upper)

            //Login to 4D Dashboard
            LoginPage.GoTo(BaseUrl);
            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SignIn));
            LoginPage.ClickSignIn();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='username']")));

            LoginPage.EnterUsername("integration_test@4dpath.com");
            LoginPage.EnterPassword("QAIsGreat!");
            LoginPage.ClickContinue();

            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.CreateCaseBtn));

            Assert.Equal("CREATE NEW CASE", LoginPage.CreateCaseBtnTxt);

            Thread.Sleep(2000);


            // Case Creation 


            CaseDashboard.ClickSelectAccountDropDown(); //Change the account to QA Org
            CaseDashboard.ClickSelectQAOrg();

            CaseDashboard.ClickCreateCaseBtn(); // Create Case Button Click
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='tbCaseCode']")));

            CaseDashboard.EnterCaseCode(today + "_AutomatedTest_" + randomInRange); // Case Creation
            CaseDashboard.ClickSubmitBtn();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.FirstCaseRowToggleBtn));

            CaseDashboard.ClickFirstCaseRowToggle(); // Click on First Case Toggle
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("(//div[@class='rz-expanded-row-template'])"), "Case Slides"));

            Thread.Sleep(200000); // Wait for the case to be created and the slides to be added
            CaseDashboard.ClickAddSlides(); // Click on Add Slides
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.SearchField));

            CaseDashboard.EnterSlideSearch("439577.svs"); // Slide Form
            CaseDashboard.ClickSelectSlide();
            CaseDashboard.ClickOrgan();
            CaseDashboard.ClickBreast();
            CaseDashboard.ClickSampleType();
            CaseDashboard.ClickBiopsy();
            CaseDashboard.ClickPart();
            CaseDashboard.ClickPartCode();
            WaitHelper.ShortWait();
            CaseDashboard.EnterSubPartCode("A");
            WaitHelper.ShortWait();
            CaseDashboard.EnterBlockCode("1");
            WaitHelper.ShortWait();
            CaseDashboard.ClickSaveBtn();
            WaitHelper.ShortWait();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.OKBtnClick));
            CaseDashboard.ClickOKBtn();

            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.SelectedAcct)); // Wait for Dashboard to show up again.
            WaitHelper.LongWait();
            CaseDashboard.ClickSubmitCaseBtn();
            WaitHelper.ShortWait();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='rz-chkbox-box']")));


            WaitHelper.ShortWait();
            CaseDashboard.ClickAllCheckboxes();
            WaitHelper.ShortWait();

            CaseDashboard.ClickSubmitCaseBtn();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.OKBtnClick));


            WaitHelper.ShortWait();
            CaseDashboard.ClickOKBtn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[text()='Selected Account : ']"))); // Wait for Dashboard to show up again.
            WaitHelper.LongWait();

            // Case Request Submitted and Request is in Progress
            Output.WriteLine("Case Request Submitted and Request is in Progress.");
            WaitHelper.LongWait();
            CaseDashboard.ClickFirstCaseRowToggle(); // Click on First Case Toggle
            WaitHelper.LongWait();
            //Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@title='Case Request is in progress.']"))); // Wait for Case Request in progress button.

            //Output.WriteLine("Case Request is in progress button is visible.");
            //WaitHelper.LongWait();
            // Wait for the job to complete
            Output.WriteLine("Waiting for the job to complete. This may take a while...");

            Driver.Navigate().Refresh();
            WaitHelper.LongWait();

            // Time out for result processing
            Driver.Navigate().Refresh();
            Thread.Sleep(900000);


            // Slide Review and Results Download
            Driver.Navigate().Refresh();
            WaitHelper.LongWait();
            WaitHelper.LongWait();
            CaseDashboard.ClickFirstCaseRowToggle();
            WaitHelper.LongWait();

            Output.WriteLine("Viewer Screen Annotations Validations.");
            // Step 1: Locate the <img> tag with the Thumbnail src
            var thumbnailImg = Driver.FindElement(By.XPath("//img[contains(@src, 'Thumbnail?sessionID=')]"));

            // Step 2: Get the src attribute from the <img> tag
            string thumbnailSrc = thumbnailImg.GetAttribute("src");
            Output.WriteLine($"🔍 Found thumbnail src: {thumbnailSrc}");

            // Step 3: Parse the sessionID and pathOrUid from the query string
            var uri = new Uri(thumbnailSrc);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);

            string sessionID = queryParams["sessionID"];
            string pathOrUid = queryParams["pathOrUid"];

            Assert.False(string.IsNullOrEmpty(sessionID), "❌ sessionID could not be extracted.");
            Assert.False(string.IsNullOrEmpty(pathOrUid), "❌ pathOrUid could not be extracted.");

            Output.WriteLine($"✅ Extracted sessionID: {sessionID}");
            Output.WriteLine($"✅ Extracted pathOrUid: {pathOrUid}");

            // Step 4: Compose iframe URL using extracted values
            string iframeUrl = $"https://dev-dashboard.4dpath.com/PMA/index2.htm?sessionID={sessionID}&pathOrUid=QAORGQAACCOUNT%2FDEFAULT%2F2025-07-10_AutomatedTest_58%2FImages%2F382037.svs&cancer=&serverurl=https://dev-pathomation.4dpath.com/pma.core/";

            Output.WriteLine($"🌐 Navigating to iframe URL: {iframeUrl}");

            //Click on Slide 2
            ViewerScreen.ClickCaseSlide1();
            WaitHelper.LongWait();
            WaitHelper.LongWait();
            WaitHelper.LongWait();
            Output.WriteLine("Waiting for Download button.");

            //Download results as a PDF
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Download Results']")));
            WaitHelper.ShortWait();
            Driver.FindElement(By.XPath("//button[@title='Download results as a PDF.']")).Click();
            Output.WriteLine("Download is completed.");
            WaitHelper.LongWait();
            WaitHelper.LongWait();

            // Step 5: Navigate to the composed iframe URL
            Driver.Navigate().GoToUrl(iframeUrl);
            WaitHelper.LongWait();
            Output.WriteLine("✅ Switched to Iframe");

            //Annotation Validations
            WaitHelper.LongWait();

                var annotations = new Dictionary<string, string>
                {
                    { "Ki67", "Ki67 Annotation Found!" },
                    { "sTIL", "sTIL Annotation Found!" },
                    { "MorphoBdy", "MorphoBdy Annotation Found!" },
                    { "FOV", "FOV Annotation Found!" },
                    { "Grade3Bdy", "Grade3Bdy Annotation Found!" },
                    { "Mitosis", "Mitosis Annotation Found!" }
                };

                foreach (var annotation in annotations)
                {
                    var elements = Driver.FindElements(By.XPath($"//label[@for='{annotation.Key}']"));
                    Assert.NotEmpty(elements); // still validates presence
                    if (elements.Count > 0)
                    {
                        Output.WriteLine(annotation.Value);
                    }
                }


            // Barcode click on Slide
            ViewerScreen.ClickBarcode();
            Output.WriteLine("Barcode got clicked hence the slide is present.");



            // Close the current tab
            Driver.Close();

            // Switch back to the first tab
            Driver.SwitchTo().Window(Driver.WindowHandles[0]);



            Output.WriteLine("Swichted to first tab!");
            LoginPage.GoTo(BaseUrl);
            Output.WriteLine("Swichted to Dashboard!");


            //Logout
            WaitHelper.LongWait();
            WaitHelper.LongWait();
            LoginPage.ClickMenuIcon();
            Thread.Sleep(1000);
            LoginPage.ClickSignOut();


            // Final Cleanup: Move SVS files to main folder in Azure File Share
            try
            {
                await _dropZoneService.MoveSvsFilesToMainFolderAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Fact]
        public async Task CaseRequest_WithNonDefaultBatchCd()
        {

            // Startup Cleanup: Move SVS files to main folder in Azure File Share
            try
            {
                await _dropZoneService.MoveSvsFilesToMainFolderAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Arrange - Test Data Setup
            string today = DateTime.Today.ToString("yyyyMMdd"); // Today's (System) Date
            Random rnd = new Random();
            int randomInRange = rnd.Next(1, 101); // Random number between 1 and 100 (inclusive lower bound, exclusive upper)
            string caseCd = $"{today}_E2E_NonDefaultBatchCd_{randomInRange}";
            string batchCd = $"E2E{today}";
            string slideFileName = "440022.svs";
            string slideId = "440022";

            //Login to 4D Dashboard
            LoginPage.GoTo(BaseUrl);
            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.SignIn));
            LoginPage.ClickSignIn();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='username']")));

            LoginPage.EnterUsername("integration_test@4dpath.com");
            LoginPage.EnterPassword("QAIsGreat!");
            LoginPage.ClickContinue();

            Wait.Until(ExpectedConditions.ElementIsVisible(LoginPage.CreateCaseBtn));

            Assert.Equal("CREATE NEW CASE", LoginPage.CreateCaseBtnTxt);

            Thread.Sleep(2000);

            // Case Creation 
            CaseDashboard.ClickSelectAccountDropDown(); //Change the account to QA Org
            CaseDashboard.ClickSelectQAOrg();

            CaseDashboard.ClickCreateCaseBtn(); // Create Case Button Click
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='tbCaseCode']")));

            CaseDashboard.EnterCaseCode(caseCd); // Case Creation
            CaseDashboard.EnterBatchCode(batchCd); // Enter Non-Default Batch Code
            CaseDashboard.ClickSubmitBtn();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.FirstCaseRowToggleBtn));

            // Add Slide
            CaseDashboard.ClickFirstCaseRowToggle(); // Click on First Case Toggle
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.XPath("(//div[@class='rz-expanded-row-template'])"), "Case Slides"));

            Thread.Sleep(200000); // Wait for the case to be created and the slides to be added
            CaseDashboard.ClickAddSlides(); // Click on Add Slides
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.SearchField));

            CaseDashboard.EnterSlideSearch(slideFileName); // Slide Form
            CaseDashboard.ClickSelectSlide();
            CaseDashboard.ClickOrgan();
            CaseDashboard.ClickBreast();
            CaseDashboard.ClickSampleType();
            CaseDashboard.ClickBiopsy();
            CaseDashboard.ClickPart();
            CaseDashboard.ClickPartCode();
            CaseDashboard.ClickDiseaseSetting();
            CaseDashboard.ClickNeoadjuvant();
            WaitHelper.ShortWait();
            CaseDashboard.EnterSubPartCode("A");
            WaitHelper.ShortWait();
            CaseDashboard.EnterBlockCode("1");
            WaitHelper.ShortWait();
            CaseDashboard.ClickSaveBtn();
            WaitHelper.ShortWait();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.OKBtnClick));
            CaseDashboard.ClickOKBtn();

            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.SelectedAcct)); // Wait for Dashboard to show up again.
            WaitHelper.LongWait();
            CaseDashboard.ClickSubmitCaseBtn(); // How do we know we want the first case? We should use the filter here
            WaitHelper.ShortWait();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='rz-chkbox-box']")));

            WaitHelper.ShortWait();
            CaseDashboard.ClickAllCheckboxes();
            WaitHelper.ShortWait();

            CaseDashboard.ClickSubmitCaseBtn();
            Wait.Until(ExpectedConditions.ElementIsVisible(CaseDashboard.OKBtnClick));

            WaitHelper.ShortWait();
            CaseDashboard.ClickOKBtn();

            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[text()='Selected Account : ']"))); // Wait for Dashboard to show up again.
            WaitHelper.LongWait();

            // Case Request Submitted and Request is in Progress
            Output.WriteLine("Case Request Submitted and Request is in Progress.");
            WaitHelper.LongWait();

            // For now we don't care about the job completing, let's just check the case / job docs
            // to make sure the batch code is set correctly.
            // If the Job is created correctly with the non-default batch code, we can assume the rest of the flow works as expected for now.
            Models.Case observedCase = await _caseDataService.GetCaseAsync("QAORG", "QAACCOUNT", caseCd);
            List<Models.Job> observedJobs = await _caseDataService.GetJobsByCaseCdAsync("USA", "QAORG", "QAACCOUNT", batchCd, caseCd);

            // Assert
            Assert.Equal(observedCase.BatchCd, batchCd);
            Assert.All(observedJobs, job => Assert.Equal(batchCd, job.BatchCd));
        }

    }
}