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
    public class ObjaviAutomobilSuccess : BaseTest
    {
        [Test]
        [Description("Testing if user is able to successfully /Objaviti oglas/")]
        [Category("Positive")]
        [TestCase("Audi", "A3")]
        public async Task ObjaviAutomobilTest_Success(string proizvodjac, string model)
        {
            LoginPage loginpage = new LoginPage(Page);
            await loginpage.ClickAcceptTermsbtn();
            Assert.That(await loginpage.IsOLXDisplayed(), Is.True, "OLX page is not displayed");
            await loginpage.ClickPrijaviSebtn();
            _extentTest.Info("Entering Username and Password");
            HomePage homepage = await loginpage.Login(username, password);
;
            Assert.That(await homepage.DoMojiOglasiExist(), Is.True, "User is not logged in");
            await homepage.ClickObjaviOglasbtn();
            _extentTest.Info("Click Objavi oglas button");

            //Verify the API resposne with status code after selecting Automobili category
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await homepage.ClickItemAutomobilbtn();
            }, x => x.Url.Contains("/categories/18/brands") && x.Status == 200, new() {Timeout = 10000});
            _extentTest.Info("API response: 200 OK");

            //Assert page title is visible/displayed
            ObjaviAutomobilPage objaviautomobilpage = new ObjaviAutomobilPage(Page);
            Assert.That(await objaviautomobilpage.IsPageTitleVisible(), Is.True,
                        "Objava oglasa page is not displayed");

            //Assert located page title is equal to expected page title
            var expectedTitle = "Objava oglasa u kategoriji Automobili";
            Assert.That(await objaviautomobilpage.GetPageTitle(), Is.EqualTo(expectedTitle), "Objava oglasa page is not displayed");

            //Select Proizvodjac i model. (Audi a3)
            //await objaviautomobilpage.SelectProizvodjac_i_Model(); //send here values for proizvodjac i model? Use enums?
            await objaviautomobilpage.SelectProizvodjac(proizvodjac);
            await objaviautomobilpage.SelectModel(model);
            _extentTest.Info("Selecting Proizvodjac i model");

            await objaviautomobilpage.SelectObavezneInformacije();
            _extentTest.Info("Selecting Obavezne informacije");

            await objaviautomobilpage.SelectOsnovniPodaci();
            _extentTest.Info("Selecting Osnovni Podaci");

            await objaviautomobilpage.SelectDodatneInformacije();
            _extentTest.Info("Selecting Dodatne Informacije");

            MojiOglasiPage mojioglasipage = await objaviautomobilpage.SelectFotografije();
            _extentTest.Info("Uploading of image/s");

            //Assert if published item is displayed under Moji oglasi/Neaktivni
            await mojioglasipage.SelectNeaktivniOglasi();
            var itemName = "Audi A3 2010";
            Assert.That(await mojioglasipage.IsPublishedItemDisplayed(), Is.EqualTo(itemName), "Your item is not published nor displayed!");
            _extentTest.Info("Item successfully published");

            //take a screenshot when item is published
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfObjaviOglasTestScenarios\\ObjavljeniArtiakl.png" });

            //Delete published item
            await mojioglasipage.DeletePublishedItem();
            Assert.That(await mojioglasipage.IsAlertMessageDisplayedAfterDelete(), Is.True, "Popup message is not displayed? Item not deleted");
            Assert.That(await mojioglasipage.IsPublishedItemVisible(), Is.False, "Item is not successfully deleted. Item is still displayed?");
            _extentTest.Info("Item successfully deleted");

            //take a screenshot when TC is finished
            await Page.ScreenshotAsync(new PageScreenshotOptions
            { Path = "C:\\Users\\ElvedinBrkan\\source\\repos\\CrashCoursePlaywrightC#\\CrashCoursePlaywrightC#\\Screenshots\\ScreenshotsOfObjaviOglasTestScenarios\\ObjavljeniArtiaklIzbrisan.png" });

            await Page.WaitForTimeoutAsync(8000);

        }
        
    }
}
