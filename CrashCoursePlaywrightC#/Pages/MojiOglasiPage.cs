using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Pages
{
    public class MojiOglasiPage : BasePage
    {
        public MojiOglasiPage(IPage page) : base(page) { }
        private ILocator MojiOglasiBtn => Page.Locator("a[href='/mojolx/artikli/aktivni']");
        private ILocator MojiOglasiNeaktivniBtn => Page.Locator("a[href='/mojolx/artikli/neaktivni']");
        private ILocator ArtikalItem => Page.GetByRole(AriaRole.Heading, new() { Name = "Audi A3 2010" });
        private ILocator IzbrisiItemHoverBtn => Page.GetByText("Izbriši", new() { Exact = true });
        private ILocator PotvrdiBrisanjeBtn => Page.Locator(".option-button.delete");
        private ILocator UspjenoIzbrisanItemPopupMsg => Page.GetByText("Uspješno ste izbrisali oglas");

        public async Task SelectNeaktivniOglasi()
        {
            await MojiOglasiBtn.WaitForAsync();
            await MojiOglasiBtn.ClickAsync();
            await MojiOglasiNeaktivniBtn.ClickAsync();
        }

        public async Task<string> IsPublishedItemDisplayed() => await ArtikalItem.InnerTextAsync();
    
        public async Task<bool> IsPublishedItemVisible() => await IsWebElementVisible(ArtikalItem);

        public async Task DeletePublishedItem()
        {
            await ArtikalItem.HoverAsync();
            await IzbrisiItemHoverBtn.ClickAsync();
            await PotvrdiBrisanjeBtn.ClickAsync();
        }

        public async Task<bool> IsAlertMessageDisplayedAfterDelete() => await IsWebElementVisible(UspjenoIzbrisanItemPopupMsg);

    }
}
