using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.ComponentModel.DataAnnotations;
using OpenQA.Selenium.DevTools.V118.Emulation;

namespace Geolocation
{
    public class GeolocationTests
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        public ChromeDriver Driver { get; set; }
        public WebDriverWait WebDriverWait { get; set; }

        public GeolocationTests()
        {
            Driver = new ChromeDriver();
            WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
        }

        [Test]
        [Order(1)]
        [TestCase("Tokyo")]
        [TestCase("Oslo")]
        [TestCase("Berlin")]
        [TestCase("Ottawa")]
        [TestCase("Taipei")]
        [TestCase("Canberra")]
        [TestCase("Cape Town")]
        [TestCase("Buenos Aires")]
        public void ChangeGeolocation(string cityName)
        {
            SetGeolocation(Driver, GetGeolocation(cityName));

            Driver.Navigate().GoToUrl("https://www.gps-coordinates.net/");

            IWebElement acceptCookiesButton = FindElementIfExists(Driver, By.CssSelector("button#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            if (acceptCookiesButton != null)
            {
                acceptCookiesButton.Click();
            }

            IWebElement map = WaitAndFindElement(By.Id("map_canvas"));
            Driver.ExecuteScript("arguments[0].scrollIntoView(true);", map);
            Driver.Navigate().Refresh();

            IWebElement address = WaitAndFindElement(By.Id("iwtitle"));

            Assert.IsTrue(address.Text.Contains(cityName), "Invalid city.");
        }

        [Test]
        [Order(2)]
        [TestCase("Oslo")]
        [TestCase("Tokyo")]
        [TestCase("Ottawa")]
        [TestCase("Taipei")]
        [TestCase("Canberra")]
        [TestCase("Cape Town")]
        [TestCase("Buenos Aires")]
        public void CalculateDistance(string cityName)
        {
            SetGeolocation(Driver, GetGeolocation(cityName));

            Driver.Navigate().GoToUrl("https://www.gps-coordinates.net/distance");

            IWebElement acceptCookiesButton = FindElementIfExists(Driver, By.CssSelector("button#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
            if (acceptCookiesButton != null)
            {
                acceptCookiesButton.Click();
            }          

            IWebElement addressOne = WaitAndFindElement(By.Id("address1"));
            IWebElement addressTwo = WaitAndFindElement(By.Id("address2"));
            IWebElement distance = WaitAndFindElement(By.Id("distance"));
            IWebElement calculateButton = WaitAndFindElement(By.ClassName("btn-success"));

            addressOne.SendKeys(cityName);
            addressTwo.SendKeys("Berlin. Germany");
            calculateButton.Click();

            Assert.IsTrue(distance.Displayed, "Invalid city.");
        }

        private static void SetGeolocation(ChromeDriver driver, double[] coordinates)
        {
            driver.ExecuteCdpCommand("Emulation.setGeolocationOverride",
            new Dictionary<string, object>()
            {
                { "latitude", coordinates[0]},
                { "longitude", coordinates[1]},
                { "accuracy", 1}
            });
        }

        private static double[] GetGeolocation(string cityName)
        {
            switch (cityName)
            {
                case "Berlin":
                    return new double[] { 52.5200, 13.4050 };
                case "Buenos Aires":
                    return new double[] { -34.6118, -58.4173 };
                case "Canberra":
                    return new double[] { -35.30740563728401, 149.1918019966403 };
                case "Ottawa":
                    return new double[] { 45.4215, -75.6993 };
                case "Tokyo":
                    return new double[] { 35.6895, 139.6917 };
                case "Taipei":
                    return new double[] { 25.0320, 121.5654 };
                case "Oslo":
                    return new double[] { 59.9139, 10.7522 };
                case "Cape Town":
                    return new double[] { -33.918861, 18.423300 };
                default:
                    throw new ArgumentException("Unsupported country");
            }
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

        public IWebElement WaitAndFindElement(By locator)
        {
            return WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}