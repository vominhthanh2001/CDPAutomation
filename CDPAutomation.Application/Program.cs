using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using System.Security.Cryptography;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();
IPage page = browser.PageTarget;

await page.Navigate().GoToUrlAsync("https://www.facebook.com/");

IElement? element = await page.FindElement().FindById("email") ?? throw new Exception("Element not found");
await element.SendKeysAsync("thành đẹp trai");
await element.IsDisplayedAsync();

await browser.CloseAsync();

Console.ReadLine();
