using CDPAutomation.Models.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Browser
{
    public interface INavigate
    {
        Task GoToUrlAsync(string url, OptionNavigateModel? option = null);
        Task GoToBackAsync(OptionNavigateModel? option = null);
        Task GoToForwardAsync(OptionNavigateModel? option = null);
        Task RefreshAsync(OptionNavigateModel? option = null);
    }
}
