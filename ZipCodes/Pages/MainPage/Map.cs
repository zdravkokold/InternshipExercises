using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ZipCodes.Pages.MainPage
{
    public partial class MainPage
    {
        public IWebElement CityInput => WaitAndFindElement(By.XPath("//input[@placeholder='City' and @name='fld-city']"));
        public IWebElement FindZipcodesButton => WaitAndFindElement(By.XPath("//*[@id='ui-id-8']//input[@value='Find ZIP Codes']"));
    }
}