using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.Element;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using System.Web;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync("https://www.facebook.com/", option: new OptionNavigateModel
{
    Timeout = 10
});

IElement email = await pageTarget.FindElement().ById("email") ?? throw new Exception("Element not found");
await email.SendKeysAsync("86gv86f6fr6g");

IElement password = await pageTarget.FindElement().ById("pass") ?? throw new Exception("Element not found");
await password.SendKeysAsync("n348gj43n5re8gjrg");

Console.ReadLine();
