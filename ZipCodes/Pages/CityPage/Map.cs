using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipCodes.Pages.CityPage
{
    public partial class CityPage
    {
        public IWebElement ZipCode => WaitAndFindElement(By.XPath("//div[@id='info']//table[@class='striped']//tr[1]/td[2]/a"));
        public IWebElement Coordinates => WaitAndFindElement(By.XPath("//table[@class='striped']//tr[9]/td[2]"));
        public string CityName => WaitAndFindElement(By.XPath("//*[@id='breadcrumbs']/ol/li[4]/a/span")).Text.Split(", ").ToArray()[0];
        public string State => WaitAndFindElement(By.XPath("//*[@id='breadcrumbs']/ol/li[4]/a/span")).Text.Split(", ").ToArray()[1];
    }
}