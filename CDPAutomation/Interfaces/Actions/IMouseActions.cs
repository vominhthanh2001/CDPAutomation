using CDPAutomation.Enums.FindElement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Actions
{
    internal interface IMouseActions
    {
        Task MoveToAsync(Rectangle startPosition, Rectangle endPosition, MouseMoveAction moveMode);
        Task ClickAsync(Rectangle startPosition, Rectangle endPosition, MouseClickAction clickMode, MouseMoveAction moveMode);
    }
}
