using CDPAutomation.Enums.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal static class BrowserHelper
    {
        internal static int? GetFreePort()
        {
            var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
            listener.Start();
            var port = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        internal static string? GetUserDataDir()
        {
            string tempPath = System.IO.Path.GetTempPath();
            string userDataDir = System.IO.Path.Combine(tempPath, System.Guid.NewGuid().ToString());
            System.IO.Directory.CreateDirectory(userDataDir);
            return userDataDir;
        }

        internal static string? GetBrowserPath(WebDriverMode mode = WebDriverMode.Chrome)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (mode)
                {
                    case WebDriverMode.Chrome:
                        string[] chromePaths = new[]
                        {
                            @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                            @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\Application\chrome.exe"),
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome Beta\Application\chrome.exe"),
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome Dev\Application\chrome.exe")
                        };
                        return chromePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Edge:
                        string[] edgePaths = new[]
                        {
                            @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe",
                            @"C:\Program Files\Microsoft\Edge\Application\msedge.exe",
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Microsoft\Edge\Application\msedge.exe")
                        };
                        return edgePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Firefox:
                        string[] firefoxPaths = new[]
                        {
                            @"C:\Program Files\Mozilla Firefox\firefox.exe",
                            @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"
                        };
                        return firefoxPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Opera:
                        string[] operaPaths = new[]
                        {
                            @"C:\Program Files\Opera\launcher.exe",
                            @"C:\Program Files (x86)\Opera\launcher.exe",
                            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Programs\Opera\launcher.exe")
                        };
                        return operaPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.InternetExplorer:
                        string[] iePaths = new[]
                        {
                            @"C:\Program Files\Internet Explorer\iexplore.exe",
                            @"C:\Program Files (x86)\Internet Explorer\iexplore.exe"
                        };
                        return iePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Safari:
                        throw new PlatformNotSupportedException("Internet Explorer is not supported on Windows");
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                switch (mode)
                {
                    case WebDriverMode.Chrome:
                        string[] chromePaths = {
                            "/usr/bin/google-chrome",
                            "/usr/bin/google-chrome-stable",
                            "/usr/bin/chromium-browser",
                            "/usr/bin/chromium"
                        };
                        return chromePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Firefox:
                        string[] firefoxPaths = {
                            "/usr/bin/firefox",
                            "/usr/lib/firefox/firefox"
                        };
                        return firefoxPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Edge:
                        string[] edgePaths = {
                            "/usr/bin/microsoft-edge",
                            "/usr/bin/microsoft-edge-stable"
                        };
                        return edgePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Opera:
                        string[] operaPaths = {
                            "/usr/bin/opera",
                            "/usr/bin/opera-stable"
                        };
                        return operaPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Safari:
                        throw new PlatformNotSupportedException("Internet Explorer is not supported on Linux");

                    case WebDriverMode.InternetExplorer:
                        throw new PlatformNotSupportedException("Internet Explorer is not supported on Linux");
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                switch (mode)
                {
                    case WebDriverMode.Chrome:
                        string[] chromePaths = {
                            "/Applications/Google Chrome.app/Contents/MacOS/Google Chrome",
                            $"/Users/{Environment.UserName}/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
                        };
                        return chromePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Firefox:
                        string[] firefoxPaths = {
                            "/Applications/Firefox.app/Contents/MacOS/firefox",
                            $"/Users/{Environment.UserName}/Applications/Firefox.app/Contents/MacOS/firefox"
                        };
                        return firefoxPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Edge:
                        string[] edgePaths = {
                            "/Applications/Microsoft Edge.app/Contents/MacOS/Microsoft Edge",
                            $"/Users/{Environment.UserName}/Applications/Microsoft Edge.app/Contents/MacOS/Microsoft Edge"
                        };
                        return edgePaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Safari:
                        string[] safariPaths = {
                            "/Applications/Safari.app/Contents/MacOS/Safari",
                            "/System/Applications/Safari.app/Contents/MacOS/Safari"
                        };
                        return safariPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.Opera:
                        string[] operaPaths = {
                            "/Applications/Opera.app/Contents/MacOS/Opera",
                            $"/Users/{Environment.UserName}/Applications/Opera.app/Contents/MacOS/Opera"
                        };
                        return operaPaths.FirstOrDefault(File.Exists);

                    case WebDriverMode.InternetExplorer:
                        throw new PlatformNotSupportedException("Internet Explorer is not supported on macOS");
                }
            }

            return default;
        }
    }
}
