using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Request
{
    public interface IRequest
    {
        Task<bool> WaitUrlAsync(string? url);
    }
}
