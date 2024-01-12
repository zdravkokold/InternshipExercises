using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage : WebPage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {        
        }

        public void Load() => Driver.Navigate().GoToUrl("https://www.zip-codes.com/search.asp?selectTab=3");
    }
}
