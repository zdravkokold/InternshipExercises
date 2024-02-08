using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using ZipCodes.Pages.MainPage;
using ZipCodes.Pages.ResultsPage;
using ZipCodes.Pages.CityPage;
using System.Reflection.Emit;

namespace ZipCodes
{
    public class ZipCodesTests
    {
        private static IWebDriver driver;
        private static MainPage mainPage;
        private static ResultsPage resultsPage;
        private static CityPage cityPage;

        public ZipCodesTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            resultsPage = new ResultsPage(driver);
            cityPage = new CityPage(driver);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            driver.Quit();
        }

        [Test]
        public void SearchForCityAndTakeScreenshotOnGoogleMaps()
        {
            for (int i = 1; i <= 10; i++)
            {
                mainPage.Load();
                mainPage.CityInput.SendKeys("rav");
                mainPage.FindZipcodesButton.Click();

                resultsPage.GetCityNameByRowNumber(i).Click();

                var cityInfo = new CityInfo()
                {
                    CityName = cityPage.CityName,
                    State = cityPage.State,
                    ZipCode = cityPage.ZipCode.Text,
                    Coordinates = cityPage.Coordinates.Text,
                };

                cityPage.SearchInGoogleMapsByCoordinates(cityInfo.Coordinates);
                cityPage.TakeScreeenshot(cityInfo.CityName, cityInfo.State, cityInfo.ZipCode);

                if (i < 10)
                {
                    mainPage.SwitchToNewTab();
                }
            }

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Screenshots");
            string[] jpgFiles = Directory.GetFiles(filePath, "*.jpg");

            Assert.AreEqual(10, jpgFiles.Length, $"Expected 10 .jpg files, but found {jpgFiles.Length}");
        }
    }
}