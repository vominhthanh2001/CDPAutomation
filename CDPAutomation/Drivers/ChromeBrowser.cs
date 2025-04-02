using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Implementation;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.Fingerprint;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.Page;

namespace CDPAutomation.Drivers
{
    public class ChromeBrowser : IBrowser, IDisposable
    {
        private ProcessResult? _processInitializeBrowser;
        private ProcessResult? _processDebuggerPage;
        private ProcessResult? _processDebuggerBrowser;
        private StartOption? _option;
        private List<DebuggerPageResponse>? _debuggerListPage;
        private DebuggerPageResponse? _debuggerPageMain => _debuggerListPage?.FirstOrDefault(x => x.Type == "page");
        private DebuggerBrowserResponse? _debuggerBrowser;

        public ICDP CDP { get; }

        private IPage? _pageTarget = default;
        public IPage? PageTarget
        {
            get => _pageTarget;
            set
            {
                _pageTarget = value;
                _pageTarget?.ActivateAsync().Wait();
            }
        }
        public IFingerprint Fingerprint { get; }

        public ChromeBrowser()
        {
            this.CDP = new CDPImplementation();
            this.Fingerprint = new FingerprintImplementation(this.CDP);
        }

        public async Task StartAsync(StartOption? option = null)
        {
            if (option is null)
            {
                option = StartOption.StartOptionInstance();
            }

            _option = option;

            // Lấy cổng trống để sử dụng
            int? port = BrowserHelper.GetFreePort() ?? throw new Exception("No free port available");
            option.AddArgument($"--remote-debugging-port={port}");

            // Thêm các đối số vào tùy chọn khởi động trình duyệt
            option.AddArguments(new List<string>
            {
                "--remote-allow-origins=*", // Cho phép tất cả các nguồn gốc cho gỡ lỗi từ xa
                "--disable-blink-features=AutomationControlled", // Vô hiệu hóa các tính năng điều khiển tự động
                "--no-sandbox", // Vô hiệu hóa sandbox cho tất cả các loại quy trình thường được sandbox
                "--disable-infobars", // Ngăn chặn các thanh thông tin xuất hiện
                "--test-type", // Vô hiệu hóa kiểm tra trình duyệt mặc định
                "--disable-features=IsolateOrigins,site-per-process", // Vô hiệu hóa cô lập trang web
                "--disable-gpu", // Vô hiệu hóa tăng tốc phần cứng GPU
                "--no-first-run", // Bỏ qua các trình hướng dẫn lần đầu chạy
                "--no-default-browser-check", // Bỏ qua kiểm tra trình duyệt mặc định
                "--new-window-url=about:blank", // Mở một trang trống trong cửa sổ mới
                "--disable-popup-blocking", // Vô hiệu hóa chặn popup
                "--disable-notifications", // Vô hiệu hóa thông báo
                "--disable-background-networking", // Vô hiệu hóa mạng nền
                "--disable-background-timer-throttling", // Vô hiệu hóa giảm tốc độ bộ hẹn
                "--disable-backgrounding-occluded-windows", // Vô hiệu hóa cửa sổ bị che khuất
                "--disable-breakpad", // Vô hiệu hóa báo cáo lỗi
                "--disable-component-extensions-with-background-pages", // Vô hiệu hóa tiện ích mở rộng với trang nền
                "--disable-default-apps", // Vô hiệu hóa ứng dụng mặc định
                "--disable-dev-shm-usage", // Vô hiệu hóa sử dụng bộ nhớ chia sẻ /dev/shm
                "--disable-extensions", // Vô hiệu hóa tiện ích mở rộng
                "--disable-features=TranslateUI,BlinkGenPropertyTrees", // Vô hiệu hóa tính năng dịch và BlinkGenPropertyTrees
                "--disable-hang-monitor", // Vô hiệu hóa theo dõi treo
                "--disable-ipc-flooding-protection", // Vô hiệu hóa bảo vệ tràn IPC
                "--disable-prompt-on-repost", // Vô hiệu hóa cảnh báo khi gửi lại
                "--disable-renderer-backgrounding", // Vô hiệu hóa nền trình kết xuất
                "--disable-sync", // Vô hiệu hóa đồng bộ hóa
                "--disable-web-security", // Vô hiệu hóa bảo mật web
                //"--user-agent=\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/134.0.6998.178 Safari/537.36\"" // Đặt chuỗi user-agent
            });

            // kiểm tra nếu không có đường dẫn thực thi thì lấy đường dẫn mặc định
            if (string.IsNullOrWhiteSpace(option.ExecutablePath))
                option.ExecutablePath = BrowserHelper.GetBrowserPath() ?? throw new Exception("No browser path found");

            _processInitializeBrowser = ProcessHelper.Execute(option.ExecutablePath, string.Join(" ", option.Arguments ?? new List<string>()), false) ?? throw new Exception("Can not start browser");
            if (_processInitializeBrowser.Process is null) throw new Exception("Can not start browser");

            _processDebuggerBrowser = ProcessHelper.CurlExecute($"http://localhost:{port}/json/version", true) ?? throw new Exception("Can not get json version");
            if (_processDebuggerBrowser.Process is null) throw new Exception("Can not get json version");
            if (_processDebuggerBrowser.ProcessExitCode != 0) throw new Exception("Can not get json version");
            _debuggerBrowser = JsonHelper.Deserialize<DebuggerBrowserResponse>(_processDebuggerBrowser.Output ?? string.Empty, jsonTypeInfo: (System.Text.Json.Serialization.Metadata.JsonTypeInfo<DebuggerBrowserResponse>)JsonContext.Default.DebuggerBrowserResponse) ?? throw new Exception("Can not deserialize json version");

            _processDebuggerPage = ProcessHelper.CurlExecute($"http://localhost:{port}/json", true) ?? throw new Exception("Can not get json");
            if (_processDebuggerPage.Process is null) throw new Exception("Can not get json");
            if (_processDebuggerPage.ProcessExitCode != 0) throw new Exception("Can not get json");
            _debuggerListPage = JsonHelper.Deserialize<List<DebuggerPageResponse>>(_processDebuggerPage.Output ?? string.Empty, jsonTypeInfo: (System.Text.Json.Serialization.Metadata.JsonTypeInfo<List<DebuggerPageResponse>>)JsonContext.Default.ListDebuggerPageResponse) ?? throw new Exception("Can not deserialize json");

            await this.CDP.ConnectAsync(_debuggerPageMain?.WebSocketDebuggerUrl);

            List<IPage> pages = await this.Pages();
            this.PageTarget = pages.FirstOrDefault() ?? throw new Exception("Can not get page target");

            await this.Fingerprint.InitializeAsync();
        }

        public Task CloseAsync()
        {
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
                    if (System.IO.Directory.Exists(_option.UserDataDir))
                        System.IO.Directory.Delete(_option.UserDataDir, true);
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

        public async Task<List<IPage>> Pages()
        {
            List<TargetInfo> targetInfos = await this.CDP.GetPagesInfoAsync();
            List<IPage> pages = targetInfos.Select(t => (IPage)new PageImplementation(this.CDP, t)).ToList();
            return pages;
        }

        public async Task<IPage> NewPageAsync()
        {
            string? createTarget = await this.CDP.SendInstantAsync(
                method: "Target.createTarget",
                parameters: new CreateTargetParams { Url = "about:blank" });
            if (createTarget is not null)
            {
                CDPResponse? response = JsonHelper.Deserialize(createTarget, JsonContext.Default.CDPResponse);
                if (response is null || response?.Result is null) throw new Exception("Can not create target");

                TargetInfo? targetInfo = JsonHelper.Deserialize(response?.Result?.ToString() ?? throw new Exception(), JsonContext.Default.TargetInfo);
                if (targetInfo is null) throw new Exception("Can not create target");

                return (IPage)new PageImplementation(this.CDP, targetInfo);
            }
            throw new Exception("Can not create target");
        }

        public Task SwitchPage(IPage? page)
        {
            ArgumentNullException.ThrowIfNull(page);

            page.ActivateAsync();

            return Task.CompletedTask;
        }

        public async Task SwitchPage(int? index)
        {
            ArgumentNullException.ThrowIfNull(index);

            List<IPage> pages = await this.Pages();
            if (pages.Count < index) throw new Exception("Index out of range");

            await pages[index.Value].ActivateAsync();
        }
    }
}
