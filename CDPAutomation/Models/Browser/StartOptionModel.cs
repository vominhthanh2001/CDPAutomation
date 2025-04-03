using CDPAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class StartOptionModel
    {
        public string? ExecutablePath { get; set; } = BrowserHelper.GetBrowserPath();
        public string? UserDataDir { get; set; } = BrowserHelper.GetUserDataDir();
        public string? ProfileDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UserDataDir)) return string.Empty;

                return Path.GetFileName(UserDataDir);
            }
        }

        public WindowInfomationModel? Window { get; set; }
        public List<string>? Arguments { get; set; }
        public List<string>? Headers { get; set; }

        public int? Port
        {
            get
            {
                if (Arguments is null) return default;

                var port = Arguments.FirstOrDefault(x => x.StartsWith("--remote-debugging-port="));
                if (port is null) return default;

                return int.Parse(port.Split("=")[1]);
            }
        }

        public bool InjectMouseInBrowser { get; set; }

        public void AddArguments(List<string> arguments)
        {
            Arguments ??= [];

            foreach (var argument in arguments)
                AddArgument(argument);
        }

        public void AddArgument(string argument)
        {
            Arguments ??= [];

            //check if argument already exists
            if (Arguments.Contains(argument)) return;

            Arguments.Add(argument);
        }

        internal static StartOptionModel StartOptionInstance()
        {
            return new StartOptionModel
            {
                ExecutablePath = BrowserHelper.GetBrowserPath(),
                UserDataDir = BrowserHelper.GetUserDataDir()
            };
        }
    }
}
