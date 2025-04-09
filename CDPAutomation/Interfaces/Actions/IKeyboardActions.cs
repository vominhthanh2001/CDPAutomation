using CDPAutomation.Enums.FindElement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Actions
{
    internal interface IKeyboardActions
    {
        Task SendKeysAsync(Rectangle startPosition, Rectangle endPosition, string value, KeyboardAction keyboardMode);
    }
}
