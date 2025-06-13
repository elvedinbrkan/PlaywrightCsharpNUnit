using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IPage page) : base(page) { }
        private ILocator AcceptTermsBtn => Page.Locator("#accept-btn");
        private ILocator PrijaviSeBtn => Page.Locator("a[href = '/login']");
        //Locator written by codegen:
        //private ILocator _btnPrijaviSe => _page.GetByRole(AriaRole.Link, new () {Name="prijava" });
        private ILocator UsernameTxt => Page.Locator("//input[@name=\"username\"]");
        private ILocator PasswordTxt => Page.Locator("//input[@name=\"password\"]");
        private ILocator PrijavaFinalBtn => Page.Locator(".my-lg");

        //private ILocator _lnkRegistracija => _page.GetByRole(AriaRole.Link, new() {Name = "registracija" });
        private ILocator RegistracijaLnk => Page.Locator("a[href = '/register']");
        //private ILocator _loginErrorMessage => _page.Locator("//p[contains(text(), \"Podaci nisu\")]");
        private ILocator LoginErrorMsg => Page.GetByText("Podaci nisu tačni.");


        public async Task ClickAcceptTermsbtn()
        {
            await AcceptTermsBtn.ClickAsync();
        }

       // public async Task<bool> IsOLXDisplayed() => await RegistracijaLnk.IsVisibleAsync();
        public async Task<bool> IsOLXDisplayed() => await IsWebElementVisible(RegistracijaLnk);

        public async Task ClickPrijaviSebtn()
        {
            //Listening Network Event - when _btnPrijaviSe is clicked, will the focus
            // be changed to Url = "https://olx.ba/login"
            await Page.RunAndWaitForNavigationAsync(async () =>
            {
                await PrijaviSeBtn.ClickAsync();
            }, new PageRunAndWaitForNavigationOptions
            {
               Url = "**/login" //It can be written like this:s Url = "https://olx.ba/login
            });

            //await _page.RunAndWaitForNavigationAsync(async () =>
            //{
            //    await _btnPrijaviSe.ClickAsync();
            //}, new PageRunAndWaitForNavigationOptions
            //{
            //    UrlString ="**/login"
            //});
        }

        public async Task<HomePage> Login(string username, string password)
        {   
            //This method takes us to the next page so it returns the new object of that page
            await UsernameTxt.FillAsync(username);
            await PasswordTxt.FillAsync(password);
            await PrijavaFinalBtn.ClickAsync();
            return new HomePage(Page);
        }

        public async Task<bool> IsErrorMessageDisplayed() => await IsWebElementVisible(LoginErrorMsg);
        
    }
}
