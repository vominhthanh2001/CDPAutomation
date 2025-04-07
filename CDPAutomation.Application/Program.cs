using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();
IPage page = browser.PageTarget;

await page.Navigate().GoToUrlAsync("https://www.facebook.com/");

await Task.Delay(2000);
await browser.CloseAsync();

Console.ReadLine();
