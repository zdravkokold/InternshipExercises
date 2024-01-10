using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TestingBluehost
{
    public partial class BluehostTests
    {
        protected IWebDriver Driver { get; set; }
        protected WebDriverWait WebDriverWait { get; set; }

        public BluehostTests()
        {
            Driver = new ChromeDriver();
            WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        [Test]
        public void LoggingWithIncorrectEmailAndPassword_ShouldShowErrorMessage()
        {
            LoadBluehost();

            Username.SendKeys("test");
            Password.SendKeys("test");
            LoginButton.Click();
            WaitForAjax();

            Assert.AreEqual(InvalidLoginMessage.Text, "We could not log you in. Invalid user credentials");
        }

        [Test]
        public void LoggingWithBlankEmailAndPassword_ShouldShowErrorMessage()
        {
            LoadBluehost();

            Username.SendKeys("");
            Password.SendKeys("");
            LoginButton.Click();
            WaitForAjax();

            Assert.AreEqual(UsernameErrorMessage.Text, "Username is not valid");
            Assert.AreEqual(PasswordErrorMessage.Text, "Password cannot be empty");
        }
    }
}