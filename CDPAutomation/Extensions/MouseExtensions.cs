using CDPAutomation.Enums.FindElement;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.FindElement.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Extensions
{
    internal static class MouseExtensions
    {
        private static string? _aero_arrow_base64 = null;
        internal static async Task<string> GetBase64ImageAsync(string imagePath)
        {
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            return $"data:image/png;base64,{base64Image}";
        }

        internal static async Task InjectMouse(this IJavaScriptExecutor javaScriptExecutor)
        {
            if (_aero_arrow_base64 is null)
            {
                string image_path = "Data\\aero_arrow.png";
                _aero_arrow_base64 = await GetBase64ImageAsync(image_path);
            }

            // Inject chuột vào document
            await javaScriptExecutor.ExecuteJavaScriptAsync($@"
                (() => {{
                    if (document.getElementById('virtual-mouse')) return;

                    let cursor = document.createElement('img');
                    cursor.id = 'virtual-mouse';
                    cursor.src = '{_aero_arrow_base64}';
                    cursor.style.position = 'fixed';
                    cursor.style.width = '32px';
                    cursor.style.height = '32px';
                    cursor.style.pointerEvents = 'none';
                    cursor.style.zIndex = '9999';
                    document.body.appendChild(cursor);

                    document.addEventListener('mousemove', (event) => {{
                        cursor.style.left = `${{event.clientX}}px`;
                        cursor.style.top = `${{event.clientY}}px`;
                    }});
                }})();
                ", true);
        }

        public static async Task SetMousePositionRandom(this IJavaScriptExecutor javaScriptExecutor)
        {
            await javaScriptExecutor.InjectMouse();
          
            await javaScriptExecutor.ExecuteJavaScriptAsync($@"
            (function() {{
                const x = Math.floor(Math.random() * (window.innerWidth - 10)) + 5; // Offset to avoid edge
                const y = Math.floor(Math.random() * (window.innerHeight - 10)) + 5; // Offset to avoid edge
                window.__lastMousePosition = {{ x, y }};

                const event = new MouseEvent('mousemove', {{
                    clientX: x,
                    clientY: y,
                    bubbles: true,
                    cancelable: true,
                    view: window
                }});

                document.dispatchEvent(event);

                if (!window.__mouseTrackerInitialized) {{
                    document.addEventListener('mousemove', (e) => {{
                        window.__lastMousePosition = {{
                            x: e.clientX,
                            y: e.clientY
                        }};
                    }});
                    window.__mouseTrackerInitialized = true;
                }}
            }})();", true);

        }

        internal static async Task<Rectangle> GetMousePosition(this IJavaScriptExecutor javaScriptExecutor)
        {
            object? result = await javaScriptExecutor.ExecuteJavaScriptAsync($@"
            (function() {{
                return window.__lastMousePosition;
            }})();", true);

            ArgumentNullException.ThrowIfNull(result);

            string? rectString = result.ToString();
            ArgumentNullException.ThrowIfNullOrWhiteSpace(rectString);

            ElementRectangleModel? rect = JsonHelper.Deserialize(rectString, JsonContext.Default.ElementRectangleModel);
            ArgumentNullException.ThrowIfNull(rect);

            Rectangle rectangle = rect.ToRectangle();
            return rectangle;
        }

        internal static Rectangle GetEndPosition(this MousePositionInElement mousePositionInElement, Rectangle elementRectangle)
        {
            const int border = 15;  // Kích thước viền 10px

            Rectangle rectangle = new();
            switch (mousePositionInElement)
            {
                case MousePositionInElement.Center:
                    rectangle = new Rectangle(elementRectangle.X + elementRectangle.Width / 2, elementRectangle.Y + elementRectangle.Height / 2, 1, 1);
                    break;
                case MousePositionInElement.TopLeft:
                    rectangle = new Rectangle(elementRectangle.X + border, elementRectangle.Y + border, 1, 1);
                    break;
                case MousePositionInElement.TopRight:
                    rectangle = new Rectangle(elementRectangle.X + elementRectangle.Width - border - 1, elementRectangle.Y + border, 1, 1);
                    break;
                case MousePositionInElement.BottomLeft:
                    rectangle = new Rectangle(elementRectangle.X + border, elementRectangle.Y + elementRectangle.Height - border - 1, 1, 1);
                    break;
                case MousePositionInElement.BottomRight:
                    rectangle = new Rectangle(elementRectangle.X + elementRectangle.Width - border - 1, elementRectangle.Y + elementRectangle.Height - border - 1, 1, 1);
                    break;
                case MousePositionInElement.Random:
                    MousePositionInElement[] mousePositionInElements = (MousePositionInElement[])Enum.GetValues(typeof(MousePositionInElement));

                    Random random = new();
                    int randomIndex = random.Next(0, mousePositionInElements.Length);
                    mousePositionInElement = mousePositionInElements[randomIndex];
                    return mousePositionInElement.GetEndPosition(elementRectangle);
            }

            return rectangle;
        }
    }
}
