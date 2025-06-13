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
    public class SearchSuccess : BaseTest
    {
        [Test]
        [Description("Verifying if user is able to successfully search for item")]
        [Category("Positive")]
        public async Task SearchItemTest_Success()
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            HomePage homePage = await loginpage.Login(username, password);
            Assert.That(await homePage.DoMojiOglasiExist(), Is.True, "User is not logged in");

            _extentTest.Info("Searching for audi a3");
            await homePage.selectItemFromSearchResult("audi a3");

            //do Assert for when search item is clicked
            Assert.That(await homePage.IsSearchResultDisplayed(), Is.True, "No search result displayed");
            _extentTest.Info("Search result successfully displayed.");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfSearchTestScenarios\\SearchPass.png" });

            await Page.WaitForTimeoutAsync(8000);

        }

    }

}
