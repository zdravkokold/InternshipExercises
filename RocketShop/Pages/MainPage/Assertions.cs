using NUnit.Framework;

namespace RocketShop.Pages
{
    public partial class MainPage
    {
        public void AssertProductBoxLink(string name, string expectedLink)
        {
            string actualLink = GetProductBoxByName(name).GetAttribute("href");

            Assert.AreEqual(expectedLink, actualLink);
        }
    }
}
