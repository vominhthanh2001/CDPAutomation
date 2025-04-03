using CDPAutomation.Interfaces.FindElement.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.FindElement
{
    public interface IElement
    {
        Task ClickAsync(OptionClick? option = null);
        Task SendKeysAsync(string value, OptionSendKeys? option = null);
        Task ClearAsync(OptionClear? option = null);

        Task<bool> IsDisplayedAsync();
        Task<bool> IsEnabledAsync();
        Task<bool> IsSelectedAsync();

        Task<string?> GetTextAsync();
        Task<string?> GetAttributeAsync(string attributeName);
        Task<string?> GetCssValueAsync(string cssProperty);

        Task HoverAsync();
        Task DragAndDropAsync(IElement targetElement);
        Task ScrollIntoViewAsync();
        Task RightClickAsync();
        Task DoubleClickAsync();

        Task SelectByTextAsync(string text);
        Task SelectByValueAsync(string value);
        Task SelectByIndexAsync(int index);

        Task<byte[]> CaptureScreenshotAsync();
        Task<Rectangle> GetBoundingBoxAsync();
    }
}
