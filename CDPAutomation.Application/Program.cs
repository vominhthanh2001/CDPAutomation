using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Factorys;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;

var optionNavigate = new OptionNavigate
{
    Timeout = 60,
    WaitUntilPageLoad = true
};

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();

IPage? page = browser.PageTarget;
if (page == null) throw new Exception("Page is null.");

await page.Navigate().GoToUrlAsync("https://www.facebook.com/", option: optionNavigate);

var cookies = await page.Cookies().GetCookiesAsync();

await Task.Delay(2000);
await browser.CloseAsync();

Console.ReadLine();
