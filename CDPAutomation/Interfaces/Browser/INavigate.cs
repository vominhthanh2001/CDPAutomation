using CDPAutomation.Interfaces.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Browser
{
    public interface INavigate
    {
        Task GoToUrlAsync(string url, OptionNavigate? option = null);
        Task GoToBackAsync(OptionNavigate? option = null);
        Task GoToForwardAsync(OptionNavigate? option = null);
        Task RefreshAsync(OptionNavigate? option = null);
    }
}
