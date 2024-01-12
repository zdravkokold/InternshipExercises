using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCodes.Pages.ResultsPage
{
    public class ResultsPage : WebPage
    {
        public ResultsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement GetCityNameByRowNumber(int rowNumber)
        {
            return WaitAndFindElement(By.XPath($"//table[@id='tblZIP']//tbody/tr[{rowNumber}]/td[2]/a"));
        }
    }
}
