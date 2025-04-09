using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement.Element.CoreJavaScript
{
    internal class NodeProperties
    {
        [JsonPropertyName("result")]
        public List<NodeProperty>? Result { get; set; }

        [JsonPropertyName("internalProperties")]
        public List<NodeProperty>? InternalProperties { get; set; }
    }
}
