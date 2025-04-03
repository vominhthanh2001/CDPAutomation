using CDPAutomation.Models.Page;
using System.Text.Json.Serialization;

namespace CDPAutomation.Models.JavaScript
{
    internal class ExecuteJavaScriptParams : PageTargetIdInfoParams
    {
        [JsonPropertyName("expression")]
        public string? Expression { get; set; }

        [JsonPropertyName("returnByValue")]
        public bool ReturnByValue { get; set; }
    }
}
