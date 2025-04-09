# 🚀 CDP Automation for C# (Easy & Friendly)

Chào bạn! 👋
Đây là thư viện hỗ trợ điều khiển trình duyệt (Chrome) bằng C#.
Bạn có thể dễ dàng khởi động trình duyệt, mở tab mới, truy cập web, tìm và tương tác với phần tử, chạy JavaScript và nhiều hơn thế nữa!

Tất cả đều được viết theo cách đơn giản, dễ hiểu và dễ tích hợp vào bất kỳ dự án nào.

> 💡 Tương lai gần, tôi sẽ tiếp tục hoàn thiện các chức năng còn thiếu để mang lại trải nghiệm đầy đủ và mượt mà hơn.
> Cảm ơn bạn đã quan tâm và sử dụng thư viện!

# 📘 Hướng Dẫn Sử Dụng CDP Automation (C#)

## Khởi tạo trình duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt
```

## Tắt Trình Duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt
await browser.CloseAsync(); //Tắt Trình Duyệt
```
## Tạo Tab Mới
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();

IPage pageTarget = browser.PageTarget; // tab đang sử dụng
IPage newPage = await browser.NewPageAsync(); // tạo tab mới
```

## Điều Hướng Trình Duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync(""); // URL cần truy cập
await pageTarget.Navigate().GoToForwardAsync(); // chuyển tới
await pageTarget.Navigate().GoToBackAsync(); // quay lại
```

## Tìm Element
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync("https://www.facebook.com/");

IElement? id = await pageTarget.FindElement().ById("id"); // Tìm kiếm phần tử bằng id
IElement? name = await pageTarget.FindElement().ById("name"); // Tìm kiếm phần tử bằng name
IElement? className = await pageTarget.FindElement().ByClassName("className"); // Tìm kiếm phần tử bằng className
IElement? xpath = await pageTarget.FindElement().ByXPath("//input[@id='id']"); // Tìm kiếm phần tử bằng XPath
IElement? cssSelector = await pageTarget.FindElement().ByCssSelector("#id"); // Tìm kiếm phần tử bằng CssSelector

List<IElement> ids = await pageTarget.FindElement().BysId("id"); // Tìm kiếm tất cả phần tử bằng id
List<IElement> names = await pageTarget.FindElement().BysId("name"); // Tìm kiếm tất cả phần tử bằng name
List<IElement> classNames = await pageTarget.FindElement().BysClassName("className"); // Tìm kiếm tất cả phần tử bằng className
List<IElement> xpaths = await pageTarget.FindElement().BysXPath("//input[@id='id']"); // Tìm kiếm tất cả phần tử bằng XPath
List<IElement> cssSelectors = await pageTarget.FindElement().BysCssSelector("#id"); // Tìm kiếm tất cả phần tử bằng CssSelector
```

## Javascript
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Javascript().ExecuteJavaScriptAsync("alert('hello world')"); // Thực thi đoạn mã javascript trên tab hiện tại
```
