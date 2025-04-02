using CDPAutomation.Enums.Window;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Browser;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.Window;
using CDPAutomation.Models.Window.FullScreen;
using CDPAutomation.Models.Window.Maximize;
using CDPAutomation.Models.Window.Minimize;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class WindowImplementation(ICDP cdp) : IWindow
    {
        private readonly ICDP _cdp = cdp;

        // Khôi phục cửa sổ về trạng thái bình thường trước khi thực hiện các hành động khác
        private async Task RestoreWindowToNormal(WindowResponse windowResponse)
        {
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            if (windowResponse.Result.Bounds.WindowState != "normal")
            {
                WindowRestoreNomarlParams windowParams = new()
                {
                    WindowId = windowResponse.Result.WindowId,
                    Bounds = new()
                    {
                        State = WindowState.Normal.ToString().ToLower()
                    }
                };

                await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
            }
        }

        public async Task WindowFullScreen()
        {
            WindowResponse? windowResponse = await _cdp.GetWindowTarget();
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            // Khôi phục trạng thái bình thường trước khi chuyển sang fullscreen
            await RestoreWindowToNormal(windowResponse);

            WindowFullScreenParams windowParams = new()
            {
                WindowId = windowResponse.Result?.WindowId ?? 0,
                Bounds = new()
                {
                    State = WindowState.Fullscreen.ToString().ToLower()
                }
            };

            await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
        }

        public async Task WindowMaximize()
        {
            WindowResponse? windowResponse = await _cdp.GetWindowTarget();
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            // Khôi phục trạng thái bình thường trước khi tối đa hóa
            await RestoreWindowToNormal(windowResponse);

            WindowMaximizeParams windowParams = new()
            {
                WindowId = windowResponse.Result?.WindowId ?? 0,
                Bounds = new()
                {
                    State = WindowState.Maximized.ToString().ToLower()
                }
            };

            await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
        }

        public async Task WindowMinimize()
        {
            WindowResponse? windowResponse = await _cdp.GetWindowTarget();
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            // Khôi phục trạng thái bình thường trước khi thu nhỏ
            await RestoreWindowToNormal(windowResponse);

            WindowMinimizeParams windowParams = new()
            {
                WindowId = windowResponse.Result.WindowId,
                Bounds = new()
                {
                    State = WindowState.Minimized.ToString().ToLower()
                }
            };

            await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
        }

        public async Task WindowPosition(int x, int y)
        {
            WindowResponse? windowResponse = await _cdp.GetWindowTarget();
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            WindowParams windowParams = new()
            {
                WindowId = windowResponse.Result.WindowId,
                Bounds = new()
                {
                    Left = x,
                    Top = y,
                    Width = windowResponse.Result.Bounds.Width,
                    Height = windowResponse.Result.Bounds.Height,
                    State = WindowState.Normal.ToString().ToLower()
                }
            };

            await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
        }

        public async Task WindowSize(int width, int height)
        {
            WindowResponse? windowResponse = await _cdp.GetWindowTarget();
            ArgumentNullException.ThrowIfNull(windowResponse);
            ArgumentNullException.ThrowIfNull(windowResponse.Result);
            ArgumentNullException.ThrowIfNull(windowResponse.Result.Bounds);

            WindowParams windowParams = new()
            {
                WindowId = windowResponse.Result.WindowId,
                Bounds = new()
                {
                    Top = windowResponse.Result.Bounds.Top,
                    Left = windowResponse.Result.Bounds.Left,
                    Width = width,
                    Height = height,
                    State = WindowState.Normal.ToString().ToLower()
                }
            };

            await _cdp.SendAsync("Browser.setWindowBounds", windowParams);
        }
    }
}
