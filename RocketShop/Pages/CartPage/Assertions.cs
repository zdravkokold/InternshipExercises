using NUnit.Framework;

namespace RocketShop.Pages
{
    public partial class CartPage
    {
        public void AssertCouponAppliedSuccessfully()
        {
            Assert.AreEqual("Coupon code applied successfully.", MessageAlert.Text);
        }

        public void AssertTotalPrice(string expectedPrice)
        {
            Assert.AreEqual(expectedPrice, TotalSpan.Text);
        }
    }
}
