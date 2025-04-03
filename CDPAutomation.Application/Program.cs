using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.Pages;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();

IPage page1 = await browser.NewPageAsync();
await page1.Javascript().ExecuteJavaScriptAsync("alert('hello world')", returnValue: false);

IPage page2 = await browser.NewPageAsync();
await page2.Javascript().ExecuteJavaScriptAsync("alert('hello world')", returnValue: false);

await page1.CloseAsync();

await browser.SwitchPage(0);
await browser.SwitchPage(page2);
await page2.Javascript().ExecuteJavaScriptAsync("alert('hello world')", returnValue: false);

await Task.Delay(2000);
await browser.CloseAsync();

Console.ReadLine();
