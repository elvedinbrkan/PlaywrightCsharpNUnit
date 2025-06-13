using AventStack.ExtentReports;
using CrashCoursePlaywrightC_.ExtentReportsDemo;
using CrashCoursePlaywrightC_.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Tests.LoginTestScenarios
{
    [TestFixture]
    public class LoginClassFailure : BaseTest
    {
        [Test]
        [Description("Testing if validation will trigger when invalid Password is used")]
        [Category("Negative")]
        public async Task LoginTest_InvalidPassword_Failure()
        {
            //Running test using LoginPageUpgraded page class
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            _extentTest.Info("Entering Invalid Password");
            await loginpage.Login(username, "wrongPassword"); //Intentionally filled invalid password
            Assert.That(await loginpage.IsErrorMessageDisplayed(), Is.True, "Error message is not displayed?");
            _extentTest.Info("Error message /Podaci nisu tacni/ successfully displayed");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfLoginTestScenarios\\InvalidPassowrdFail.png" });

            await Page.WaitForTimeoutAsync(8000);
        }

        [Test]
        [Description("Testing if validation will trigger when invalid Username is used")]
        [Category("Negative")]
        public async Task LoginTest_InvalidUsername_Failure()
        {
            //Running test using LoginPageUpgraded page class
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            _extentTest.Info("Entering Invalid Username");
            await loginpage.Login("wrongUsername", password); //Intentionally filled invalid username
            Assert.That(await loginpage.IsErrorMessageDisplayed(), Is.True, "Error message is not displayed?");
            _extentTest.Info("Error message /Podaci nisu tacni/ successfully displayed");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfLoginTestScenarios\\InvalidUsernameFail.png" });

            await Page.WaitForTimeoutAsync(8000);
        }

        [Test]
        [Description("Verifying if Status code will be 422 when invalid Password is used")]
        [Category("Negative")]
        public async Task LoginFailureNetworkAPIResposne()
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            //Verify the API resposne with status code after login
            var response = await Page.RunAndWaitForResponseAsync(async () =>
            {
                await loginpage.ClickPrijaviSebtn();
                await loginpage.Login(username, "wrongPassword"); //Intentionally filled invalid password
            }, x => x.Url.Contains("/login") && x.Status == 422,
            new() { Timeout = 10000 }
            );
            _extentTest.Info("Invalid password input resulted in 422 HTTP resposne status code (Unprocessable Entity)");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfLoginTestScenarios\\LoginFailAPI.png" });
        }

    }
}
