using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Pages
{
    public class HomePage :BasePage
    {
        public HomePage(IPage page) : base(page) { }
        private ILocator SearchBox => Page.GetByPlaceholder("Pretraga");
        private ILocator MojiOglasiBtn => Page.Locator("a[href='/mojolx/artikli/aktivni']");

        //Locating element from dynamic list:
        //1. locate the 'parent' div of dropdown list that contains all the list items 
        //Div (class) = parent
        //List items = child
        //2. use GetByRole(AriaRole.Listitem) to specify which element we are trying to select under this Locator (class)
        //3. use .Filter(new() { HasText="audi a3"}) to filter the available listitems by specific text
        private ILocator SearchItem => Page.Locator(".searchbar-dropdown").GetByRole(AriaRole.Listitem)
                                              .Filter(new() { HasText="audi a3"});
        private ILocator ResultLbl => Page.Locator(".search-title");
        private ILocator NoSearchResultsLbl => Page.GetByText("Nema rezultata za traženi pojam");
        private ILocator ObjaviOglasBtn => Page.Locator("//button[text()=\"Objavi oglas\"]");
        private ILocator ItemAutomobilBtn => Page.Locator(".item-Automobil");

        public async Task<bool> DoMojiOglasiExist() => await IsWebElementVisible(MojiOglasiBtn);

        public async Task ClickMojiOglasibtn() => await MojiOglasiBtn.ClickAsync();

        public async Task selectItemFromSearchResult(string searchItem)
        {
            await SearchBox.FillAsync(searchItem);
            await SearchItem.First.ClickAsync(); //selects a first matching element from search dropdown
            //await _searchItem.ClickAsync(); //this line returns all search result values from dropdown
        }

        public async Task<bool> IsSearchResultDisplayed() => await IsWebElementVisible(ResultLbl);

        public async Task EnterSearchTerm(string searchTerm)
        {
            await SearchBox.FillAsync(searchTerm);
            await SearchBox.PressAsync("Enter");
        }

       public async Task<string> NoSearchResultTitle()
        {
            await NoSearchResultsLbl.WaitForAsync();
            return await NoSearchResultsLbl.InnerTextAsync();
        }

        public async Task ClickObjaviOglasbtn()
        {
            await ObjaviOglasBtn.ClickAsync();
        }

        public async Task ClickItemAutomobilbtn()
        {
            await ItemAutomobilBtn.ClickAsync();   
        }

    }
}
