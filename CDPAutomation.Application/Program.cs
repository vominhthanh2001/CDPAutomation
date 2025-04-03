using CDPAutomation.Drivers;
using CDPAutomation.Enums.FindElement;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.FindElement.Options;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using System.Formats.Tar;

var optionNavigate = new OptionNavigate
{
    Timeout = 60,
    WaitUntilPageLoad = true
};

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();

await Task.Delay(2000);
await browser.CloseAsync();

Console.ReadLine();
