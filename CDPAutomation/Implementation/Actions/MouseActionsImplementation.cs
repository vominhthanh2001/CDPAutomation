using CDPAutomation.Enums.FindElement;
using CDPAutomation.Extensions;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.Actions;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement.Element.CoreJavaScript;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Implementation.Actions
{
    internal class MouseActionsImplementation(ICDP cdp, NodeProperty nodeProperty) : IMouseActions
    {
        private readonly ICDP _cdp = cdp;
        private readonly NodeProperty _nodeProperty = nodeProperty;

        public async Task ClickAsync(Rectangle startPosition, Rectangle endPosition, MouseClickAction clickMode, MouseMoveAction moveMode)
        {
            await MoveToAsync(startPosition, endPosition, moveMode);

            await _cdp.SendAsync(new()
            {
                Method = "Input.dispatchMouseEvent",
                Params = new DispatchMouseEventParams
                {
                    Type = clickMode switch
                    {
                        MouseClickAction.Right => "mousePressed",
                        MouseClickAction.Left => "mousePressed",
                        MouseClickAction.DoubleClickRight => "mousePressed",
                        MouseClickAction.DoubleClickLeft => "mousePressed",
                        _ => throw new NotImplementedException(),
                    },
                    X = endPosition.X,
                    Y = endPosition.Y,
                    Button = clickMode switch
                    {
                        MouseClickAction.Right => "right",
                        MouseClickAction.Left => "left",
                        MouseClickAction.DoubleClickRight => "right",
                        MouseClickAction.DoubleClickLeft => "left",
                        _ => throw new NotImplementedException(),
                    },
                }
            });

            await _cdp.SendAsync(new()
            {
                Method = "Input.dispatchMouseEvent",
                Params = new DispatchMouseEventParams
                {
                    Type = clickMode switch
                    {
                        MouseClickAction.Right => "mouseReleased",
                        MouseClickAction.Left => "mouseReleased",
                        MouseClickAction.DoubleClickRight => "mouseReleased",
                        MouseClickAction.DoubleClickLeft => "mouseReleased",
                        _ => throw new NotImplementedException(),
                    },
                    X = endPosition.X,
                    Y = endPosition.Y,
                    Button = clickMode switch
                    {
                        MouseClickAction.Right => "right",
                        MouseClickAction.Left => "left",
                        MouseClickAction.DoubleClickRight => "right",
                        MouseClickAction.DoubleClickLeft => "left",
                        _ => throw new NotImplementedException(),
                    },
                }
            });

            await Task.Delay(new Random().Next(100, 300));
        }

        public async Task MoveToAsync(Rectangle startPosition, Rectangle endPosition, MouseMoveAction moveMode)
        {
            List<MousePosition> mousePositions = MouseMovementGenerator.GeneratePath(options: new MouseMoveOptions
            {
                StartX = startPosition.X,
                StartY = startPosition.Y,
                EndX = endPosition.X,
                EndY = endPosition.Y,
                Action = moveMode,
            });

            foreach (var position in mousePositions)
            {
                CDPRequest @params = new()
                {
                    Method = "Input.dispatchMouseEvent",
                    Params = new DispatchMouseEventParams
                    {
                        Type = "mouseMoved",
                        X = position.X,
                        Y = position.Y,
                        //Button = "none",
                    }
                };

                await _cdp.SendAsync(@params);

                await Task.Delay(position.Delay);
            }

            await Task.Delay(new Random().Next(100, 300));
        }
    }
}
