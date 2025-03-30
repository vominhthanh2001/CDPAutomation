using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Factorys;
using CDPAutomation.Interfaces.Browser;

namespace CDPAutomation.Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
            browser.StartAsync().Wait();
            browser.Pages().Wait();
        }
    }
}
