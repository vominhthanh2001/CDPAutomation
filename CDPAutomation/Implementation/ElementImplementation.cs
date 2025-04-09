using CDPAutomation.Abstracts;
using CDPAutomation.Enums.FindElement;
using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Implementation.Actions;
using CDPAutomation.Interfaces.Actions;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Element;
using CDPAutomation.Interfaces.FindElement.Options;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element;
using CDPAutomation.Models.FindElement.Element.CoreJavaScript;
using System.Drawing;
using static CDPAutomation.Models.FindElement.Element.CoreJavaScript.CaptureScreenshotParams;

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
        private readonly MouseActionsImplementation _mouseActions = new(cdp, nodeProperty);
        private readonly KeyboardActionsImplementation _keyboardActions = new(cdp, nodeProperty);

        public async Task ClickAsync(OptionClick? option = null)
        {
            option ??= new();

            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            await ScrollToAsync();

            Rectangle? rectangle = await GetBoundingBoxAsync();
            ArgumentNullException.ThrowIfNull(rectangle);

            Rectangle startPosition = await _javaScriptExecutor.GetMousePosition();
            Rectangle endPosition = option.PositionInElement.GetEndPosition(rectangle.Value);

            await _mouseActions.ClickAsync(startPosition, endPosition, option.MouseClick, option.MouseMove);
        }

        public async Task SendKeysAsync(string value, OptionSendKeys? option = null)
        {
            option ??= new();

            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);

            await ScrollToAsync();

            Rectangle? rectangle = await GetBoundingBoxAsync();
            ArgumentNullException.ThrowIfNull(rectangle);

            Rectangle startPosition = await _javaScriptExecutor.GetMousePosition();
            Rectangle endPosition = option.PositionInElement.GetEndPosition(rectangle.Value);

            await _keyboardActions.SendKeysAsync(startPosition, endPosition, value, option.Keyboard);
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

        public async Task ScrollToAsync()
        {
            while (await IsDisplayedAsync() == false)
            {
                await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                    window.scrollTo({{
                        top: 50,
                        left: 0,
                        behavior: 'smooth'
                    }});
                ");

                await Task.Delay(new Random().Next(100, 500));
            }

            for (int i = 0; i < 3; i++)
            {
                await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                    window.scrollTo({{
                        top: 50,
                        left: 0,
                        behavior: 'smooth'
                    }});
                ");

                await Task.Delay(new Random().Next(100, 500));
            }
        }

        public async Task<byte[]> CaptureScreenshotAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value.ObjectId);

            Rectangle? rectangle = await GetBoundingBoxAsync();
            ArgumentNullException.ThrowIfNull(rectangle);

            Rectangle rectanglevalue = rectangle.Value;

            CDPRequest @params = new()
            {
                Method = "Page.captureScreenshot",
                Params = new CaptureScreenshotParams
                {
                    Format = "png",
                    Clip = new ClipRequestModel
                    {
                        X = rectanglevalue.X,
                        Y = rectanglevalue.Y,
                        Width = rectanglevalue.Width,
                        Height = rectanglevalue.Height,
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

        public async Task<Rectangle?> GetBoundingBoxAsync()
        {
            ArgumentNullException.ThrowIfNull(_nodeProperty);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value);
            ArgumentNullException.ThrowIfNull(_nodeProperty.Value.ObjectId);

            object? rectObject = await _javaScriptExecutor.ExecuteJavaScriptAsync($@"
                (function() {{
                    let element = document.querySelector(`{_nodeProperty.Value.Description}`);
                    if (!element) return null;
                    const rect = element.getBoundingClientRect();
                    return {{
                        x: rect.x,
                        y: rect.y,
                        width: rect.width,
                        height: rect.height
                    }};
                }})();
            ", true);

            ArgumentNullException.ThrowIfNull(rectObject);

            string? rectString = rectObject.ToString();
            ArgumentNullException.ThrowIfNullOrWhiteSpace(rectString);

            ElementRectangleModel? rect = JsonHelper.Deserialize(rectString, JsonContext.Default.ElementRectangleModel);
            ArgumentNullException.ThrowIfNull(rect);

            Rectangle rectangle = rect.ToRectangle();
            return rectangle;
        }
    }
}
