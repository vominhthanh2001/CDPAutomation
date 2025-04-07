using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Extensions
{
    internal static class ElementExtension
    {
        public static async Task Forcus(this ICDP cdp, NodeProperty nodeProperty)
        {
            ArgumentNullException.ThrowIfNull(cdp);
            ArgumentNullException.ThrowIfNull(nodeProperty);
            ArgumentNullException.ThrowIfNull(nodeProperty.Value);

            CDPRequest @params = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams 
                {
                    Expression = $"document.querySelector(`{nodeProperty.Value.Description}`).focus()",
                    ReturnByValue = false,
                }
            };

            await cdp.SendInstantAsync(@params);
        }

        public static Rectangle ToRectangle(this int[] coordinates)
        {
            if (coordinates == null || coordinates.Length != 8)
            {
                throw new ArgumentException("Invalid coordinates array.");
            }

            int minX = Math.Min(Math.Min(coordinates[0], coordinates[2]), Math.Min(coordinates[4], coordinates[6]));
            int minY = Math.Min(Math.Min(coordinates[1], coordinates[3]), Math.Min(coordinates[5], coordinates[7]));
            int maxX = Math.Max(Math.Max(coordinates[0], coordinates[2]), Math.Max(coordinates[4], coordinates[6]));
            int maxY = Math.Max(Math.Max(coordinates[1], coordinates[3]), Math.Max(coordinates[5], coordinates[7]));

            return new Rectangle(minX, minY, maxX - minX, maxY - minY);
        }
    }
}
