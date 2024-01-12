using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCodes.Pages.CityPage
{
    public partial class CityPage : WebPage
    {
        public CityPage(IWebDriver driver) : base(driver)
        {
        }

        public void SearchInGoogleMapsByCoordinates(string coordinates)
        {
            Driver.Navigate().GoToUrl($"https://www.google.com/maps?q={coordinates}");

            var acceptCookiesButton = FindElementIfExists(Driver, By.CssSelector("div.VtwTSb > form:nth-child(2) > div > div > button"));
            if (acceptCookiesButton != null)
            {
                acceptCookiesButton.Click();
            }

            var searchBar = WaitAndFindElement(By.Id("searchboxinput"));
            searchBar.SendKeys(coordinates);

            var searchButton = WaitAndFindElement(By.Id("searchbox-searchbutton"));
            searchButton.Click();
        }

        public void TakeScreeenshot(string cityName, string state, string zipCode)
        {
            Screenshot screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile($"C:\\Users\\ATP-1\\Pictures\\Screenshots\\{cityName}-{state}-{zipCode}.jpg");
        }
    }
}