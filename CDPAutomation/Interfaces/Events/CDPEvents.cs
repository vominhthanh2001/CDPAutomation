using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Events
{
    public interface CDPEvents
    {
        public event EventHandler<string>? OnMessageReceivedResponse;
        public event EventHandler<string>? OnMessageReceivedWaitMethod;
    }
}
