using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Events
{
    internal interface INavigateEvent
    {
        event EventHandler<string>? FrameStartedNavigating;
        event EventHandler<string>? FrameNavigated;
        event EventHandler<string>? FrameStoppedLoading;
    }
}
