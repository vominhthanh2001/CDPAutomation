using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using System.Diagnostics;

namespace CDPAutomation.Test
{
    [TestClass]
    public sealed class Debug
    {
        [TestMethod]
        public void DebugTest()
        {
            Assert.IsTrue(true);

            IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
            browser.StartAsync().Wait();
        }
    }
}
