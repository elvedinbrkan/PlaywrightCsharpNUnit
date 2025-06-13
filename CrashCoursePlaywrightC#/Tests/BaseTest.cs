using AventStack.ExtentReports;
using CrashCoursePlaywrightC_.ExtentReportsDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrashCoursePlaywrightC_.Tests.LoginTestScenarios;
using System.Text.Json;
using Newtonsoft.Json;
using System.Runtime;


namespace CrashCoursePlaywrightC_.Tests
{

    public class BaseTest
    {
        protected IPlaywright playwright;
        protected IPage Page;
        private ExtentReports _extent;
        public ExtentTest _extentTest;
        public AppSettings _settings;
        public string username;
        public string password;

        public class AppSettings
        {
            public Credentials Credentials { get; set; }
        }
        public class Credentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        //public class ConfigurationHelper
        //{
        //    public static AppSettings LoadSettings(string filePath)
        //    {
        //        var json = File.ReadAllText(filePath);
        //        return JsonConvert.DeserializeObject<AppSettings>(json);
        //    }
        //}



        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _extent = MyExtentReports.GetInstance();
            //_settings = ConfigurationHelper.LoadSettings("C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\appsettings.json");
            //username = _settings.Credentials.Username;
            //password = _settings.Credentials.Password;

            var json = File.ReadAllText("C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\appsettings.json");
            _settings = JsonConvert.DeserializeObject<AppSettings>(json);
            username = _settings.Credentials.Username;
            password = _settings.Credentials.Password;
        }


        [SetUp]
        public async Task BeforeEachTest()
        {
            //Playwright
            playwright = await Playwright.CreateAsync();

            //Browser
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Devtools = false,
                SlowMo = 1000
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
            _extentTest = _extent.CreateTest(TestContext.CurrentContext.Test.Name);

            //string fileName = "appsettings.json";
            //using FileStream openStream = File.OpenRead(fileName);
            //Appsettings? logn = await JsonSerializer.Deserialize<Appsettings>(openStream);
            //_settings = ConfigurationHelper.LoadSettings("C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\appsettings.json");
            //username= _settings.Credentials.Username;
            //password= _settings.Credentials.Password;
        }


        [OneTimeTearDown]
        public void ReportFlush()
        {
            _extent.Flush();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
            var testStatusAfterExecuting = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatusAfterExecuting == TestStatus.Passed)
            {
                _extentTest.Pass("Passed.");
            }
            else if (testStatusAfterExecuting == TestStatus.Failed)
            {
                _extentTest.Fail("Failed.");
            }
            else if (testStatusAfterExecuting == TestStatus.Skipped)
            {
                _extentTest.Skip("Test is skipped.");
            }

        }
    }
}