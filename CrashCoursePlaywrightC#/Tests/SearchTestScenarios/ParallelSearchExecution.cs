using AventStack.ExtentReports;
using CrashCoursePlaywrightC_.Pages;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrashCoursePlaywrightC_.Tests.BaseTest;

namespace CrashCoursePlaywrightC_.Tests.SearchTestScenarios
{
    [TestFixture]
    [Parallelizable]
    public class ParallelSearchExecution
    {
        protected IPlaywright playwright;
        public AppSettings _settings;
        public string username;
        public string password;

        [SetUp]
        public async Task BeforeEachTest()
        {
            //Playwright
            playwright = await Playwright.CreateAsync();
            var json = File.ReadAllText("C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\appsettings.json");
            _settings = JsonConvert.DeserializeObject<AppSettings>(json);
            username = _settings.Credentials.Username;
            password = _settings.Credentials.Password;
        }

        [Test]
        [Description("Verifying if user is able to successfully search for item using Test Parametarization")]
        [Category("Positive")]
        [TestCase("audi a5")]
        [TestCase("audi a6")]
        [TestCase("iphone 15")]
        [Parallelizable(ParallelScope.All)]
        public async Task SearchTest_Success_Paralellizable(string SearchTerm)
        {
            IBrowser browser;
            IPage Page;
            //Browser
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Devtools = false,
                SlowMo = 1000
                //Channel= "msedge"
            });

            //browserContext
            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });

            //Page
            Page = await browserContext.NewPageAsync();
            await Page.GotoAsync("https://olx.ba/");

            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            HomePage homePage = await loginpage.Login(username, password);
            await Task.Delay(2000);
            Assert.That(await homePage.DoMojiOglasiExist(), Is.True, "User is not logged in");

            await homePage.searchAndselectItemFromSearchResult(SearchTerm);

            //do Assert for when search item is clicked
            Assert.That(await homePage.IsSearchResultDisplayed(), Is.True, "No search result displayed");

            await Page.WaitForTimeoutAsync(8000);

        }
        public class AppSettings
        {
            public Credentials Credentials { get; set; }
        }
        public class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
