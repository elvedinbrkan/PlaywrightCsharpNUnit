using AventStack.ExtentReports;
using CrashCoursePlaywrightC_.ExtentReportsDemo;
using CrashCoursePlaywrightC_.Pages;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Tests.SearchTestScenarios
{
    [TestFixture]
    public class SearchFailure : BaseTest
    {
        [Test]
        [Description("Verifying search result for unexistent item")]
        [Category("Negative")]
        [TestCase("123!45@67#abc")]
        public async Task SearchTest_Failure(string unexistentItem)
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            HomePage homePage = await loginpage.Login(username, password);
            await Task.Delay(2000);

            Assert.That(await homePage.DoMojiOglasiExist(), Is.True, "User is not logged in");
            _extentTest.Info("Searching for 123!45@67#abc / nonexistent item");
            await homePage.EnterUnexistentSearchTerm(unexistentItem);

            var expectedTitle = "Nema rezultata za traženi pojam";
            Assert.That(await homePage.NoSearchResultTitle(), Is.EqualTo(expectedTitle), "Search results displayed unexpectedly?");
            _extentTest.Info("Nema rezultata za trazeni pojame - message displayed");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfSearchTestScenarios\\SearchFailure.png" });
            
            await Page.WaitForTimeoutAsync(8000);

        }

    }
}
