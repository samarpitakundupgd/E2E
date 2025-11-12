using E2ETests.Fixtures;
using E2ETests.Interfaces;
using E2ETests.Pages;
using FourDDashboard.SeleniumTests.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace E2ETests.Tests
{
    /// <summary>
    /// Base class for Selenium-based end-to-end tests. 
    /// This class sets up the WebDriver and initializes the various page objects used in the tests.
    /// Services required for the tests come from the E2ETestFixture.
    /// </summary>
    public class SeleniumTestBase : IClassFixture<E2ETestFixture>, IDisposable
    {
        protected IWebDriver Driver { get; private set; }
        protected WebDriverWait Wait { get; private set; }
        protected LoginPage LoginPage { get; private set; }
        protected CaseDashboard CaseDashboard { get; private set; }
        protected ReportDownload ReportDownload { get; private set; }
        protected ViewerScreen ViewerScreen { get; private set; }
        protected Notification Notification { get; private set; }
        protected ExportToExcel ExportToExcel { get; private set; }
        protected Logout Logout { get; private set; }
        protected StaticPages StaticPages { get; private set; }

        protected IConfiguration Configuration { get; private set; }
        protected readonly ITestOutputHelper Output;

        protected string BaseUrl;
        protected string AzureDropZone;
        protected string AzureStorageConnectionString;

        // Services
        protected IDropZoneService _dropZoneService;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the SeleniumTestBase class, setting up the test configuration, services, and
        /// web driver for end-to-end Selenium tests.
        /// </summary>
        /// <param name="output">The test output helper used to capture and display test output during execution.</param>
        /// <param name="testFixture">The test fixture providing configuration settings and service dependencies required for the test
        /// environment.</param>
        /// <exception cref="Exception">Thrown if services are not available in the test fixture's service provider.</exception>
        public SeleniumTestBase(ITestOutputHelper output, E2ETestFixture testFixture)
        {
            Output = output;
            Output.WriteLine("========== 🏗️ TestBase Setup Started ==========");

            Configuration = testFixture.Configuration;
            _dropZoneService = testFixture.ServiceProvider.GetService<IDropZoneService>()
                ?? throw new Exception("Drop Zone Service is missing");

            InitializeConfiguration();
            InitializeWebDriver();
            InitializePages();
        }

        private void InitializeConfiguration()
        {
            BaseUrl = Configuration["AppSettings:BaseUrl"]
                ?? throw new Exception("BaseUrl is missing");
        }

        private void InitializeWebDriver()
        {
            Output.WriteLine("Launching ChromeDriver...");
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");
            //options.AddArgument("--headless=new");

            Driver = new ChromeDriver(options);
            Driver.Manage().Window.Maximize();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(90));
        }

        private void InitializePages()
        {
            LoginPage = new LoginPage(Driver);
            CaseDashboard = new CaseDashboard(Driver);
            ReportDownload = new ReportDownload(Driver);
            ViewerScreen = new ViewerScreen(Driver);
            Notification = new Notification(Driver);
            ExportToExcel = new ExportToExcel(Driver);
            Logout = new Logout(Driver);
            StaticPages = new StaticPages(Driver);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Output.WriteLine("========== 🧹 TestBase Teardown ==========");
                    try
                    {
                        Driver?.Quit();
                        Output.WriteLine("✅ WebDriver closed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine($"[ERROR] WebDriver quit failed: {ex.Message}");
                    }
                }
                // Free unmanaged resources (none in this class)
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
