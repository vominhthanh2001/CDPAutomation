using CDPAutomation.Enums.FindElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement.Options
{
    public class OptionSendKeys : OptionMouse
    {
        public KeyboardAction Keyboard { get; set; } = KeyboardAction.Fast;
    }
}
