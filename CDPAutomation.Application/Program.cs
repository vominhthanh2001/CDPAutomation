using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using System.Security.Cryptography;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); // khởi tạo trình duyệt

IPage page = browser.PageTarget; // trang hiện tại

await page.Navigate().GoToUrlAsync("https://www.facebook.com/"); // truy cập vào trang

IElement? element = await page.FindElement().FindById("email") ?? throw new Exception("Element not found");
await element.SendKeysAsync("thành đẹp trai");
await element.IsDisplayedAsync();

await browser.CloseAsync();

Console.ReadLine();
