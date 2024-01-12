using System;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ZipCodes.Pages
{
    public abstract class WebPage
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;

        public WebPage(IWebDriver driver)
        {
            Driver = driver;
            WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        protected IWebDriver Driver { get; set; }
        protected WebDriverWait WebDriverWait { get; set; }

        public IWebElement WaitAndFindElement(By locator)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }

        public IWebElement FindElementIfExists(IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public void SwitchToNewTab()
        {
            Driver.SwitchTo().NewWindow(WindowType.Tab);
        }
    }
}