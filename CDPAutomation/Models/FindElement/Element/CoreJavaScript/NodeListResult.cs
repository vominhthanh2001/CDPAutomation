using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    internal class NodeListResult
    {
        [JsonPropertyName("result")]
        public NodeResult? Result { get; set; }
    }
}
