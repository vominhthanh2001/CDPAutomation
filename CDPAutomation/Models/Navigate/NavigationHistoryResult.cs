using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Page
{
    internal class NavigationHistoryResult
    {
        [JsonPropertyName("currentIndex")]
        public int CurrentIndex { get; set; }

        [JsonPropertyName("entries")]
        public List<NavigationHistoryEntriesResult>? entries { get; set; }
    }
}
