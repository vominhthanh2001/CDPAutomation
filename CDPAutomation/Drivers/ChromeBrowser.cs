using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Implementation;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Fingerprint;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Page;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace CDPAutomation.Drivers
{
    internal class ChromeBrowser : IBrowser, IDisposable
    {
        private ProcessResult? _processInitializeBrowser;
        private ProcessResult? _processDebuggerPage;
        private ProcessResult? _processDebuggerBrowser;
        private StartOptionModel? _option;

        private List<DebuggerPageResponse> _debuggerPageResponse = [];
        private DebuggerPageResponse? _debuggerPageMainResponse;
        private DebuggerBrowserResponse? _debuggerBrowserMainResponse;
        private int? _port;

        public ICDP CDP { get; }

        private IPage _pageTarget = default!;

        public IPage PageTarget
        {
            get => _pageTarget;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                _pageTarget = value;
                _pageTarget.ActivateAsync().Wait();
            }
        }

        public ChromeBrowser()
        {
            this.CDP = new CDPImplementation();
        }

        public async Task StartAsync(StartOptionModel? option = null)
        {
            option ??= StartOptionModel.StartOptionInstance();

            _option = option;

            // Lấy cổng trống để sử dụng
            int? port = _port = BrowserHelper.GetFreePort() ?? throw new Exception("No free port available");
            option.AddArgument($"--remote-debugging-port={port}");

            // Thêm các đối số vào tùy chọn khởi động trình duyệt
            option.AddArguments(
            [
                "--remote-allow-origins=*", // Cho phép tất cả các nguồn gốc cho gỡ lỗi từ xa
                "--disable-blink-features=AutomationControlled", // Vô hiệu hóa các tính năng điều khiển tự động
                "--no-sandbox", // Vô hiệu hóa sandbox cho tất cả các loại quy trình thường được sandbox
                "--disable-infobars", // Ngăn chặn các thanh thông tin xuất hiện
                "--test-type", // Vô hiệu hóa kiểm tra trình duyệt mặc định
                "--no-first-run", // Bỏ qua các trình hướng dẫn lần đầu chạy
                "--new-window-url=about:blank", // Mở một trang trống trong cửa sổ mới
                "--disable-popup-blocking", // Vô hiệu hóa chặn popup
            ]);

            // kiểm tra nếu không có đường dẫn thực thi thì lấy đường dẫn mặc định
            if (string.IsNullOrWhiteSpace(option.ExecutablePath))
                option.ExecutablePath = BrowserHelper.GetBrowserPath() ?? throw new Exception("No browser path found");

            _processInitializeBrowser = ProcessHelper.Execute(option.ExecutablePath, string.Join(" ", option.Arguments!), false) ?? throw new Exception("Can not start browser");
            if (_processInitializeBrowser.Process is null) throw new Exception("Can not start browser");

            _processDebuggerBrowser = ProcessHelper.CurlExecute($"http://localhost:{port}/json/version", true) ?? throw new Exception("Can not get json version");
            if (_processDebuggerBrowser.Process is null) throw new Exception("Can not get json version");
            if (_processDebuggerBrowser.ProcessExitCode != 0) throw new Exception("Can not get json version");
            _debuggerBrowserMainResponse = JsonHelper.Deserialize(_processDebuggerBrowser.Output ?? string.Empty, jsonTypeInfo: JsonContext.Default.DebuggerBrowserResponse) ?? throw new Exception("Can not deserialize json version");

            await this.CDP.ConnectAsync(_debuggerBrowserMainResponse.WebSocketDebuggerUrl);

            _processDebuggerPage = ProcessHelper.CurlExecute($"http://localhost:{port}/json", true) ?? throw new Exception("Can not get json");
            if (_processDebuggerPage.Process is null) throw new Exception("Can not get json");
            if (_processDebuggerPage.ProcessExitCode != 0) throw new Exception("Can not get json");

            _debuggerPageResponse = JsonHelper.Deserialize(_processDebuggerPage.Output ?? string.Empty, jsonTypeInfo: JsonContext.Default.ListDebuggerPageResponse) ?? throw new Exception("Can not deserialize json");
            _debuggerPageMainResponse = _debuggerPageResponse?.FirstOrDefault(x => x.Type == "page");

            //page đầu tiên khi được khởi tạo
            IPage pageFirstInitialize = PageTarget = new PageImplementation(CDP, _debuggerPageMainResponse!);

            PageObjectManager.AddPage(new PageObjectModel
            {
                Page = pageFirstInitialize,
                DebuggerPage = _debuggerPageMainResponse!,
                Port = _port.Value,
                Active = true
            });
        }

        public Task CloseAsync()
        {
            this.CDP.DisconnectAsync().Wait();

            // Kiểm tra nếu quá trình khởi tạo trình duyệt không null thì tiến hành kill
            if (_processInitializeBrowser?.Process is not null)
                _processInitializeBrowser.Process.Kill();

            // Kiểm tra nếu quá trình lấy json không null thì tiến hành kill
            if (_processDebuggerPage?.Process is not null)
                _processDebuggerPage.Process.Kill();

            // Kiểm tra nếu quá trình lấy json version không null thì tiến hành kill
            if (_processDebuggerBrowser?.Process is not null)
                _processDebuggerBrowser.Process.Kill();

            // Kiểm tra nếu tùy chọn không null thì tiến hành xóa thư mục dữ liệu người dùng
            if (_option is not null)
            {
                if (!string.IsNullOrWhiteSpace(_option.UserDataDir))
                {
                    if (Directory.Exists(_option.UserDataDir))
                        Directory.Delete(_option.UserDataDir, true);
                }
            }

            _processInitializeBrowser?.Process?.Dispose();
            _processDebuggerPage?.Process?.Dispose();
            _processDebuggerBrowser?.Process?.Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            CloseAsync().Wait();
            GC.SuppressFinalize(this);
        }

        public Task<List<IPage>> Pages()
        {
            ArgumentNullException.ThrowIfNull(_port);

            //_processDebuggerPage = ProcessHelper.CurlExecute($"http://localhost:{_port}/json", true) ?? throw new Exception("Can not get json");
            //if (_processDebuggerPage.Process is null) throw new Exception("Can not get json");
            //if (_processDebuggerPage.ProcessExitCode != 0) throw new Exception("Can not get json");

            //_debuggerPageResponse = JsonHelper.Deserialize(_processDebuggerPage.Output ?? string.Empty, jsonTypeInfo: JsonContext.Default.ListDebuggerPageResponse) ?? throw new Exception("Can not deserialize json");

            //List<IPage> pageImplementations = _debuggerPageResponse.Where(d => d.Type == "page")
            //                                                       .Select(d => (IPage)new PageImplementation(CDP, d))
            //                                                       .ToList() ?? [];
            //pageImplementations.Reverse();

            //sort _openPages có index nhỏ đến lớn 
            List<IPage> pageImplementations = PageObjectManager.Pages().OrderBy(x => x.Index)
                                                        .Select(x => x.Page)
                                                        .ToList() ?? [];

            return Task.FromResult(pageImplementations);
        }

        public Task<IPage> NewPageAsync()
        {
            ArgumentNullException.ThrowIfNull(_port);

            CDPRequest @params = new()
            {
                Method = "Target.createTarget",
                Params = new NewPageParams
                {
                    Url = "about:blank"
                }
            };

            Task<CDPResponse?>? taskNewPage = this.CDP.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(taskNewPage);

            CDPResponse? response = taskNewPage.Result;
            ArgumentNullException.ThrowIfNull(response);

            NewPageResult? result = response.Deserialize(JsonContext.Default.NewPageResult, true);

            CDPImplementation newCDP = new();
            newCDP.ConnectAsync($"ws://localhost:{_port}/devtools/page/{result!.TargetId}").Wait();

            //new Connect
            //_socketPage = $"ws://localhost:{_port}/devtools/page/{result!.TargetId}";
            //CDP.ConnectAsync(_socketPage)!.Wait();

            //Task<DebuggerPageResponse?> taskGetTargetInfo = this.CDP.GetTargetInfo(result);
            //ArgumentNullException.ThrowIfNull(taskGetTargetInfo);

            //DebuggerPageResponse? debuggerPage = taskGetTargetInfo.Result;
            //ArgumentNullException.ThrowIfNull(debuggerPage);

            DebuggerPageResponse? debuggerPage = new()
            {
                Id = result!.TargetId,
            };

            IPage page = PageTarget = new PageImplementation(newCDP, debuggerPage!);

            PageObjectManager.AddPage(new()
            {
                Page = page,
                DebuggerPage = debuggerPage,
                Port = _port.Value,
                Active = true
            });

            return Task.FromResult<IPage>(page);
        }

        public Task SwitchPage(IPage? page)
        {
            ArgumentNullException.ThrowIfNull(page);

            Task taskActivate = page.ActivateAsync();
            ArgumentNullException.ThrowIfNull(taskActivate);
            taskActivate.Wait();

            return Task.CompletedTask;
        }

        public Task SwitchPage(int? index)
        {
            ArgumentNullException.ThrowIfNull(index);

            Task<List<IPage>> taskPages = this.Pages();
            ArgumentNullException.ThrowIfNull(taskPages);

            List<IPage> pages = taskPages.Result;
            ArgumentNullException.ThrowIfNull(pages);

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be greater than or equal to 0");

            if (index >= pages.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be less than the number of pages");

            IPage page = PageTarget = pages[index.Value];

            Task taskActivate = page.ActivateAsync();
            ArgumentNullException.ThrowIfNull(taskActivate);
            taskActivate.Wait();

            return Task.CompletedTask;
        }
    }
}
