using AventStack.ExtentReports;
using CrashCoursePlaywrightC_.ExtentReportsDemo;
using CrashCoursePlaywrightC_.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Interfaces;
using RazorEngine.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Enumeration;
//using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Policy;


namespace CrashCoursePlaywrightC_.Tests.LoginTestScenarios
{
    [TestFixture]
    public class LoginClassSuccess : BaseTest
    {
            [Test]
            [Description("Verifying if user is able to login with valid username and password.")]
            [Category("Positive")]
            public async Task LoginTest_Success()
            {
                LoginPage loginpage = new LoginPage(Page);
                await loginpage.ClickAcceptTermsbtn();
                Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
                await loginpage.ClickPrijaviSebtn();
                _extentTest.Info("Entering Username and Password");
                HomePage homePage = await loginpage.Login(username, password);
                Assert.That(await homePage.DoMojiOglasiExist(), Is.True, "User is not logged in");

                //take a screenshot when TC is finished
                await Page.ScreenshotAsync(new PageScreenshotOptions
                { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfLoginTestScenarios\\Login.png" });

                await Page.WaitForTimeoutAsync(8000);

            }

        [Test]
        [Description("Verifying if Status code will be 200 OK and URL value when user is logged in")]
        [Category("Positive")]
        public async Task LoginTest_Success_NetworkAPIResposne()
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");

            //Verify the URL before login
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await loginpage.ClickPrijaviSebtn();

            }, x => x.Url.Contains("login")
            );

            HomePage homePage = await loginpage.Login(username, password);
            Assert.That(await homePage.DoMojiOglasiExist(), Is.True, "User is not logged in");

            //Verify the API resposne with status code and URL after login
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await homePage.ClickMojiOglasibtn();
            }, x => x.Url.Contains("/elvoMo") && x.Status == 200,
            new() { Timeout = 10000 }
            );

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfLoginTestScenarios\\LoginAPIsuccess.png" });

            await Page.WaitForTimeoutAsync(8000);
        }

    }
}
