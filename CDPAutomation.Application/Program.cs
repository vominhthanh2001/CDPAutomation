using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.Pages;

IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync("https://www.facebook.com/"); // Đi đến trang facebook

IElement email = await pageTarget.FindElement().FindById("email") ?? throw new Exception("Element not found"); // Tìm kiếm phần tử bằng ID
await email.SendKeysAsync("username"); // Nhập vào ô email

IElement password = await pageTarget.FindElement().FindById("pass") ?? throw new Exception("Element not found"); // Tìm kiếm phần tử bằng ID
await password.SendKeysAsync("password"); // Nhập vào ô password

IElement loginButton = await pageTarget.FindElement().FindByName("login") ?? throw new Exception("Element not found"); // Tìm kiếm phần tử bằng Name
await loginButton.ClickAsync(); // Nhấn nút đăng nhập

await Task.Delay(10000);

Console.ReadLine();