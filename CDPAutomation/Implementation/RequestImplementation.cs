using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Request;

namespace CDPAutomation.Implementation
{
    public class RequestImplementation : IRequest
    {
        private readonly ICDP _cdp;
        public RequestImplementation(ICDP cdp)
        {
            _cdp = cdp;
        }

        public Task<bool> WaitUrlAsync(string? url)
        {
            throw new NotImplementedException();
        }
    }
}
