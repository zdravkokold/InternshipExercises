using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace VirtualPiano
{
    [TestFixture]
    public class PianoTests
    {
        private ChromeDriver driver;
        private const int NORMAL_PAUSE_BETWEEN_NOTES_MILLISECONDS = 300;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Quit();
        }

        [Test]
        public void PlayPianoSongs()
        {
            driver.Navigate().GoToUrl("https://virtualpiano.net/?song-post-10686#dismissed");
            driver.Manage().Cookies.AddCookie(new Cookie("complianz_consent_status", "allow"));
            driver.Navigate().Refresh();

            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("key_51")));
            webDriverWait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));

            var songPatternDiv = driver.FindElement(By.Id("song-pattern"));
            var allPatternSpans = songPatternDiv.FindElements(By.TagName("span"));
            foreach (var span in allPatternSpans)
            {
                if (span.GetAttribute("class") == "pause")
                {
                    Thread.Sleep(NORMAL_PAUSE_BETWEEN_NOTES_MILLISECONDS);
                }

                else
                {
                    Actions actions = new Actions(driver);

                    foreach (char note in span.Text)
                    {
                        actions = actions.KeyDown(note.ToString());
                    }

                    foreach (char note in span.Text)
                    {
                        actions = actions.KeyUp(note.ToString());
                    }

                    actions.Perform();
                }
            }
        }
    }
}