using OpenQA.Selenium;

namespace RocketShop.Pages
{
    public partial class CartPage : WebPage
    {
        public CartPage(IWebDriver driver) 
            : base(driver)
        {
        }

        protected override string Url => "http://demos.bellatrix.solutions/cart/";

        public void ApplyCoupon(string coupon)
        {
            QuantityBox.Clear();
            CouponCodeTextField.SendKeys(coupon);
            ApplyCouponButton.Click();
            WaitForAjax();
        }

        public void IncreaseProductQuantity(int newQuantity)
        {
            QuantityBox.Clear();
            QuantityBox.SendKeys(newQuantity.ToString());
            UpdateCart.Click();
            WaitForAjax();
        }

        public void ProceedToCheckout()
        {
            string script = $"document.querySelector(\"[class*='checkout-button button alt wc-forward']\").click()";
            ((IJavaScriptExecutor)Driver).ExecuteScript(script);

            WaitUntilPageLoadsCompletely();
        }

        protected override void WaitForPageToLoad()
        {
            WaitAndFindElement(By.Id("coupon_code"));
        }
    }
}
