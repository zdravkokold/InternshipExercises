using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingBluehost
{
    public partial class BluehostTests
    {
        private IWebElement Username => WaitAndFindElement(By.Id("username"));
        private IWebElement Password => WaitAndFindElement(By.Id("password"));
        private IWebElement UsernameErrorMessage => WaitAndFindElement(By.Id("username-helper-text"));
        private IWebElement PasswordErrorMessage => WaitAndFindElement(By.Id("password-helper-text"));
        private IWebElement LoginButton => WaitAndFindElement(By.XPath("//button[contains(text(), 'Log In')]"));
        private IWebElement InvalidLoginMessage => WaitAndFindElement(By.XPath("//div[contains(text(), 'We could not log you in.')]"));

        public IWebElement WaitAndFindElement(By locator)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }

        public void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)Driver;
            WebDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").ToString() == "0");
        }

        public void LoadBluehost() => Driver.Navigate().GoToUrl("https://my.bluehost.com/web-hosting/cplogin");
    }
}