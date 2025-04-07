Khởi tạo trình duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt
```

Tắt Trình Duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt
await browser.CloseAsync(); //Tắt Trình Duyệt
```

Tạo Tab Mới
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync();

IPage pageTarget = browser.PageTarget; // tab đang sử dụng
IPage newPage = await browser.NewPageAsync(); // tạo tab mới
```

Điều Hướng Trình Duyệt
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync(""); // URL cần truy cập
await pageTarget.Navigate().GoToForwardAsync(); // chuyển tới
await pageTarget.Navigate().GoToBackAsync(); // quay lại
```

Tìm Element
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Navigate().GoToUrlAsync("https://www.facebook.com/");

IElement? id = await pageTarget.FindElement().FindById("id"); // Tìm kiếm phần tử bằng id
IElement? name = await pageTarget.FindElement().FindById("name"); // Tìm kiếm phần tử bằng name
IElement? className = await pageTarget.FindElement().FindByClassName("className"); // Tìm kiếm phần tử bằng className
IElement? xpath = await pageTarget.FindElement().FindByXPath("//input[@id='id']"); // Tìm kiếm phần tử bằng XPath
IElement? cssSelector = await pageTarget.FindElement().FindByCssSelector("#id"); // Tìm kiếm phần tử bằng CssSelector

List<IElement> ids = await pageTarget.FindElement().FindsById("id"); // Tìm kiếm tất cả phần tử bằng id
List<IElement> names = await pageTarget.FindElement().FindsById("name"); // Tìm kiếm tất cả phần tử bằng name
List<IElement> classNames = await pageTarget.FindElement().FindsByClassName("className"); // Tìm kiếm tất cả phần tử bằng className
List<IElement> xpaths = await pageTarget.FindElement().FindsByXPath("//input[@id='id']"); // Tìm kiếm tất cả phần tử bằng XPath
List<IElement> cssSelectors = await pageTarget.FindElement().FindsByCssSelector("#id"); // Tìm kiếm tất cả phần tử bằng CssSelector
```

Javascript
```csharp
IBrowser browser = WebDriverFactory.CreateWebDriver(WebDriverMode.Chrome);
await browser.StartAsync(); //Khởi tạo trình duyệt

IPage pageTarget = browser.PageTarget; // tab đang sử dụng

await pageTarget.Javascript().ExecuteJavaScriptAsync("alert('hello world')"); // Thực thi đoạn mã javascript trên tab hiện tại
```
