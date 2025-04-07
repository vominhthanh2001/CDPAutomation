using CDPAutomation.Abstracts;
using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.FindElement.Options;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CDPAutomation.Models.FindElement.Element.CaptureScreenshotParams;

namespace CDPAutomation.Implementation
{
    internal class ElementImplementation(
        ICDP cdp,
        IJavaScriptExecutor javaScriptExecutor,
        DebuggerPageResult debuggerPageResponse,
        NodeProperty nodeProperty,
        RequestNodeResult? requestNodeResult) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IElement
    {
        private readonly ICDP _cdp = cdp;
        private readonly IJavaScriptExecutor _javaScriptExecutor = javaScriptExecutor;
        private readonly DebuggerPageResult _debuggerPageResponse = debuggerPageResponse;
        private readonly NodeProperty _nodeProperty = nodeProperty;
        private readonly RequestNodeResult? _requestNodeResult = requestNodeResult;

        public async Task ClickAsync(OptionClick? option = null)
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            await _javaScriptExecutor.ExecuteJavaScriptAsync(@$"document.querySelector(`{_nodeProperty.Value.Description}`).click()", false);
        }

        public async Task SendKeysAsync(string value, OptionSendKeys? option = null)
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            await _javaScriptExecutor.ExecuteJavaScriptAsync(@$"document.querySelector(`{_nodeProperty.Value.Description}`).value = '{value}'", false);
        }

        public async Task ClearAsync(OptionClear? option = null)
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);


            await _javaScriptExecutor.ExecuteJavaScriptAsync(@$"document.querySelector(`{_nodeProperty.Value.Description}`).value = ''", false);
            await _javaScriptExecutor.ExecuteJavaScriptAsync(@$"document.querySelector(`{_nodeProperty.Value.Description}`).innerHTML = ''", false);
            await _javaScriptExecutor.ExecuteJavaScriptAsync(@$"document.querySelector(`{_nodeProperty.Value.Description}`).innerText = ''", false);
        }

        public async Task<bool> IsDisplayedAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            object? isDisplayed = await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                (function() {{
                    let element = document.querySelector(`{_nodeProperty.Value.Description}`);
                    if (!element) return null;

                    const isDisplayed = element && window.getComputedStyle(element).display !== 'none' && window.getComputedStyle(element).visibility !== 'hidden';
                    return isDisplayed;
                }})();
            ", true);

            if (isDisplayed == null)
                return false;

            return bool.Parse(isDisplayed?.ToString() ?? "false");
        }

        public async Task<bool> IsEnabledAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            object? isEnabled = await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                (function() {{
                    let element = document.querySelector(`{_nodeProperty.Value.Description}`);
                    if (!element) return null;
                    const isEnabled = element && !element.disabled;
                    return isEnabled;
                }})();
            ", true);

            if (isEnabled == null)
                return false;

            return bool.Parse(isEnabled?.ToString() ?? "false");
        }

        public async Task<bool> IsSelectedAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            object? isSelected = await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                (function() {{
                    let element = document.querySelector(`{_nodeProperty.Value.Description}`);
                    if (!element) return null;
                    const isSelected = element && element.checked;
                    return isSelected;
                }})();
            ", true);

            if (isSelected == null)
                return false;

            return bool.Parse(isSelected?.ToString() ?? "false");
        }

        public async Task<string?> GetTextAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            CDPRequest @params = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelector(`{_nodeProperty.Value.Description}`).value;",
                    ReturnByValue = true
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            string? value = JsonHelper.GetProperty(response.Result.ToString(), "value");
            return value;
        }

        public async Task<string?> GetAttributeAsync(string attributeName)
        {
            ArgumentNullException.ThrowIfNull(attributeName);
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            CDPRequest @params = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelector(`{_nodeProperty.Value.Description}`).getAttribute('{attributeName}');",
                    ReturnByValue = true
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            string? result = JsonHelper.GetProperty(response.Result.ToString(), "result");
            string? value = JsonHelper.GetProperty(result, "value");
            return value;
        }

        public async Task<string?> GetCssValueAsync(string cssProperty)
        {
            ArgumentNullException.ThrowIfNull(cssProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            CDPRequest @params = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelector(`{_nodeProperty.Value.Description}`).style.{cssProperty};",
                    ReturnByValue = true
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            string? result = JsonHelper.GetProperty(response.Result.ToString(), "result");
            string? value = JsonHelper.GetProperty(result, "value");
            return value;
        }

        public async Task<byte[]> CaptureScreenshotAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value.ObjectId);

            GetBoxModelResult? getBoxModelResult = await GetBoundingBoxAsync();
            ArgumentNullException.ThrowIfNull(getBoxModelResult);
            ArgumentNullException.ThrowIfNull(getBoxModelResult.Content);

            Rectangle rectangle = getBoxModelResult.Content.ToRectangle();

            CDPRequest @params = new()
            {
                Method = "Page.captureScreenshot",
                Params = new CaptureScreenshotParams
                {
                    Format = "png",
                    Clip = new ClipRequestModel
                    {
                        X = rectangle.X,
                        Y = rectangle.Y,
                        Width = rectangle.Width,
                        Height = rectangle.Height,
                        Scale = 1.0
                    }
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);

            string? data = JsonHelper.GetProperty(response.Result.ToString(), "data");
            ArgumentNullException.ThrowIfNullOrWhiteSpace(data);

            byte[] imageBytes = Convert.FromBase64String(data);
            return imageBytes;
        }

        /// <summary> 
        /// Lấy tọa độ của phần tử
        /// </summary>
        /// <returns>Danh sách các hình chữ nhật đại diện cho các phần tử</returns>
        public async Task<GetBoxModelResult?> GetBoundingBoxAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value.ObjectId);

            Console.WriteLine($"ObjectId: {_nodeProperty.Value.ObjectId}");

            CDPRequest @params = new()
            {
                Method = "DOM.getBoxModel",
                Params = new GetBoxModelParams
                {
                    ObjectId = _nodeProperty.Value.ObjectId
                }
            };

            CDPResult? response = await _cdp.SendInstantAsync(@params);
            ArgumentNullException.ThrowIfNull(response);
            ArgumentNullException.ThrowIfNull(response.Result);

            string? modelResponse = JsonHelper.GetProperty(response.Result.ToString(), "model");
            ArgumentNullException.ThrowIfNullOrWhiteSpace(modelResponse);

            GetBoxModelResult? getBoxModelResult = JsonHelper.Deserialize(modelResponse, JsonContext.Default.GetBoxModelResult);
            return getBoxModelResult;
        }
    }
}
