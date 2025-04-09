using CDPAutomation.Enums.FindElement;
using CDPAutomation.Extensions;
using CDPAutomation.Interfaces.Actions;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement.Element.CoreJavaScript;
using System.Drawing;

namespace CDPAutomation.Implementation.Actions
{
    internal class KeyboardActionsImplementation(ICDP cdp, NodeProperty nodeProperty) : IKeyboardActions
    {
        private readonly ICDP _cdp = cdp;
        private readonly NodeProperty _nodeProperty = nodeProperty;
        private readonly MouseActionsImplementation _mouseActions = new(cdp, nodeProperty);

        public async Task SendKeysAsync(Rectangle startPosition, Rectangle endPosition, string value, KeyboardAction keyboardMode)
        {
            await _mouseActions.ClickAsync(startPosition, endPosition, MouseClickAction.Left, MouseMoveAction.Default);
            await Task.Delay(new Random().Next(1000));

            foreach (char @char in value)
            {
                string key = @char.ToString();
                int keyCode = (int)@char;

                await _cdp.SendAsync(new()
                {
                    Method = "Input.dispatchKeyEvent",
                    Params = new DispatchKeyEventParams
                    {
                        Type = "rawKeyDown",
                        Key = key,
                        Code = "Key" + char.ToUpper(@char),
                        //KeyCode = keyCode,
                        //WindowsVirtualKeyCode = keyCode,
                        //NativeVirtualKeyCode = keyCode,
                        Text = key,
                        UnmodifiedText = key
                    }
                });

                await _cdp.SendAsync(new()
                {
                    Method = "Input.dispatchKeyEvent",
                    Params = new DispatchKeyEventParams
                    {
                        Type = "char",
                        Key = key,
                        Code = "Key" + char.ToUpper(@char),
                        //KeyCode = keyCode,
                        //WindowsVirtualKeyCode = keyCode,
                        //NativeVirtualKeyCode = keyCode,
                        Text = key,
                        UnmodifiedText = key
                    }
                });

                await _cdp.SendAsync(new()
                {
                    Method = "Input.dispatchKeyEvent",
                    Params = new DispatchKeyEventParams
                    {
                        Type = "keyUp",
                        Key = key,
                        Code = "Key" + char.ToUpper(@char),
                        KeyCode = keyCode,
                        WindowsVirtualKeyCode = keyCode,
                        NativeVirtualKeyCode = keyCode,
                        Text = key,
                        UnmodifiedText = key
                    }
                });

                await Task.Delay(keyboardMode.GetDelay());
            }
        }
    }
}
