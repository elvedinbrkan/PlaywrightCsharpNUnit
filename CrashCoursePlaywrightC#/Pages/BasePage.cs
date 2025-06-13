using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashCoursePlaywrightC_.Pages
{
    public class BasePage
    {
        public IPage Page;
        public BasePage(IPage page) =>  Page = page;

        public static async Task<bool> IsWebElementVisible(ILocator webElement)
        {
            //await webElement.WaitForAsync();
            return await webElement.IsVisibleAsync();
        }

    }

   
}
