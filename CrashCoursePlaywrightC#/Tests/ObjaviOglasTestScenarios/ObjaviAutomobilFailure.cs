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

namespace CrashCoursePlaywrightC_.Tests.ObjaviOglasTestScenarios
{
    [TestFixture]
    public class ObjaviAutomobilFailure : BaseTest
    {
        [Test]
        [Description("Testing if validation will trigger when (Godiste) required field is not populated")]
        [Category("Negative")]
        public async Task ObjaviAutomobilTest_WithMissingRequiredFields_Failure_()
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            _extentTest.Info("Entering Username and Password");
            HomePage homepage = await loginpage.Login(username, password);

            Assert.That(await homepage.DoMojiOglasiExist(), Is.True, "User is not logged in");

            await homepage.ClickObjaviOglasbtn();
            _extentTest.Info("Click Objavi oglas button");

            //Verify the API resposne with status code after selecting Automobili category
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await homepage.ClickItemAutomobilbtn();
            }, x => x.Url.Contains("/categories/18/brands") && x.Status == 200, new() { Timeout = 10000 });
            _extentTest.Info("API response: 200 OK");

            //Assert page title is visible/displayed
            ObjaviAutomobilPage objaviautomobilpage = new ObjaviAutomobilPage(Page);
            Assert.That(await objaviautomobilpage.IsPageTitleVisible(), Is.True,
                        "Objava oglasa page is not displayed");

            //Assert located page title is equal to expected page title
            var expectedTitle = "Objava oglasa u kategoriji Automobili";
            Assert.That(await objaviautomobilpage.GetPageTitle(), Is.EqualTo(expectedTitle), "Objava oglasa page is not displayed");

            //Select Proizvodjac i model. (Audi a3)
            await objaviautomobilpage.SelectProizvodjac_i_Model(); //send here values for proizvodjac i model? Use enums?
            _extentTest.Info("Selecting Proizvodjac i model");

            //Select Obavezne informacije
            await objaviautomobilpage.PartialSelectObavezneInformacije();
            _extentTest.Info("Selecting Obavezne Infromacije without Godiste (required field) populated");
            //Assert if validation is triggered when one mandatory field is not selected (Godiste)
            Assert.That(await objaviautomobilpage.IsPoljeJeObaveznoLblVisible(), Is.True,
                    "Validation of mandatory fields does not work?");
            _extentTest.Info("Polje je obavezno - erorr message displayed. Required field validation triggered!");

            //take a screenshot when a validation error message displayed
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfObjaviOglasTestScenarios\\RequiredFieldErrorMsg.png" });

            //Assert validation error message is equal to expected error message
            var expectedErrorMsg = "Polje je obavezno";
            Assert.That(await objaviautomobilpage.GetMandatoryErrorMsg(), Is.EqualTo(expectedErrorMsg), "Validation error message is not 'Polje je obavezno'.");

            await Page.WaitForTimeoutAsync(8000);
        }
        
    }
}
