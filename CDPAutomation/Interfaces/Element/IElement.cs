using CDPAutomation.Interfaces.FindElement.Options;
using CDPAutomation.Models.FindElement.Element;
using CDPAutomation.Models.FindElement.Element.CoreJavaScript;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Interfaces.Element
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

        Task ScrollToAsync();

        Task<byte[]> CaptureScreenshotAsync();
        Task<Rectangle?> GetBoundingBoxAsync();
    }
}
