using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Navigate
{
    internal class NavigateGoToUrlParams : PageTargetIdInfoParams
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
