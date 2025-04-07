using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.FindElement
{
    internal class EvaluateParams
    {
        [JsonPropertyName("expression")]
        public string? Expression { get; set; }

        [JsonPropertyName("returnByValue")]
        public bool ReturnByValue { get; set; }
    }
}
