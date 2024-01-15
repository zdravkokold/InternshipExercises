using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RocketShop.Pages;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace RocketShop
{
    public class RocketShopTests : IDisposable
    {
        private static IWebDriver driver;
        private static MainPage mainPage;
        private static CartPage cartPage;
        private static CheckoutPage checkoutPage;

        public RocketShopTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Test]
        public void PurchaseFalcon9AndAssertOrderIsPresentInAccount()
        {
            mainPage.GoTo();
            mainPage.AddRocketToShoppingCart("Falcon 9");
            cartPage.ApplyCoupon("happybirthday");
            cartPage.AssertCouponAppliedSuccessfully();
            cartPage.IncreaseProductQuantity(3);
            cartPage.AssertTotalPrice("174.00€");
            cartPage.ProceedToCheckout();

            var purchaseInfo = new PurchaseInfo()
            {
                Email = "john@abv.bg",
                FirstName = "John",
                LastName = "Johnes",
                Company = "Johnes Explores",
                Country = "Germany",
                Address1 = "123 Brandt Avenue Tiergarten",
                Address2 = "Lützowplatz 321",
                City = "Berlin",
                Zip = "11122",
                Phone = "+003388999222",
                ShouldCreateAccount = true,
            };

            checkoutPage.FillBillingInfo(purchaseInfo);
            checkoutPage.AssertOrderIsPresentInAccount();
        }
    }
}
