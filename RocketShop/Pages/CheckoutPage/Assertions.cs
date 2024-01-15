using NUnit.Framework;

namespace RocketShop.Pages
{
    public partial class CheckoutPage
    {
        public void AssertOrderIsPresentInAccount()
        {
            WaitUntilPageLoadsCompletely();
            MyAccount.Click();

            WaitUntilPageLoadsCompletely();
            MyOrders.Click();

            WaitUntilPageLoadsCompletely();
            Assert.AreEqual("On hold", OrderStatus.Text);
        }
    }
}
