using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class OptionNavigate
    {
        public int Timeout { get; set; } = 60;
        public bool WaitUntilPageLoad { get; set; } = true;
    }
}
