using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Fingerprint
{
    public interface IFingerprint
    {
        /// <summary>
        /// Khởi tạo fingerprint (User-Agent, WebGL, WebRTC, Screen ...)
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Lấy User-Agent hiện tại
        /// </summary>
        string GetUserAgent();

        /// <summary>
        /// Thiết lập User-Agent mới
        /// </summary>
        Task SetUserAgentAsync(string userAgent);

        /// <summary>
        /// Lấy danh sách Plugin giả mạo (navigator.plugins)
        /// </summary>
        Task<List<string>> GetPluginsAsync();

        /// <summary>
        /// Thiết lập danh sách Plugin giả mạo
        /// </summary>
        Task SetPluginsAsync(List<string> plugins);

        /// <summary>
        /// Lấy thông tin WebGL giả mạo
        /// </summary>
        Task<string> GetWebGLInfoAsync();

        /// <summary>
        /// Giả lập WebRTC (IP Address, Local IP)
        /// </summary>
        Task SetWebRTCAsync(string ip);

        /// <summary>
        /// Lấy thông tin màn hình (Screen Width, Height, Color Depth)
        /// </summary>
        Task<(int Width, int Height, int ColorDepth)> GetScreenInfoAsync();

        /// <summary>
        /// Giả lập Canvas Fingerprinting
        /// </summary>
        Task SetCanvasFingerprintingAsync(bool enable);

        /// <summary>
        /// Giả lập Audio Fingerprinting
        /// </summary>
        Task SetAudioFingerprintingAsync(bool enable);
    }

}
