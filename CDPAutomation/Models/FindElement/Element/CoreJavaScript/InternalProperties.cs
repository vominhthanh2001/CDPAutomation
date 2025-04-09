using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    internal class InternalProperties
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

    }
}
