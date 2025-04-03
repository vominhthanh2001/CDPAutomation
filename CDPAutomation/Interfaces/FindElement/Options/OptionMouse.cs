using CDPAutomation.Enums.FindElement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement.Options
{
    public class OptionMouse
    {
        public MouseClickAction MouseClick { get; set; } = MouseClickAction.Left;
        public MouseMoveAction MouseMove { get; set; } = MouseMoveAction.Default;
        public MousePositionInElement PositionInElement { get; set; } = MousePositionInElement.Random;
    }
}
