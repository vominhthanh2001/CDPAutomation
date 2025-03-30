using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Fingerprint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation
{
    public class FingerprintImplementation : IFingerprint
    {
        private readonly ICDP _cdp;
        public FingerprintImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task<List<string>> GetPluginsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(int Width, int Height, int ColorDepth)> GetScreenInfoAsync()
        {
            throw new NotImplementedException();
        }

        public string GetUserAgent()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetWebGLInfoAsync()
        {
            throw new NotImplementedException();
        }

        public Task SetAudioFingerprintingAsync(bool enable)
        {
            throw new NotImplementedException();
        }

        public Task SetCanvasFingerprintingAsync(bool enable)
        {
            throw new NotImplementedException();
        }

        public Task SetPluginsAsync(List<string> plugins)
        {
            throw new NotImplementedException();
        }

        public Task SetUserAgentAsync(string userAgent)
        {
            throw new NotImplementedException();
        }

        public Task SetWebRTCAsync(string ip)
        {
            throw new NotImplementedException();
        }
    }
}
