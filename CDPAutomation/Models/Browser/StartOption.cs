using CDPAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Models.Browser
{
    public class StartOption
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

        public WindowInfomation? Window { get; set; }
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

        public void AddArguments(List<string> arguments)
        {
            if (Arguments is null) Arguments = new List<string>();

            foreach (var argument in arguments)
                AddArgument(argument);
        }

        public void AddArgument(string argument)
        {
            if (Arguments is null) Arguments = new List<string>();

            //check if argument already exists
            if (Arguments.Contains(argument)) return;

            Arguments.Add(argument);
        }

        internal static StartOption StartOptionInstance()
        {
            return new StartOption
            {
                ExecutablePath = BrowserHelper.GetBrowserPath(),
                UserDataDir = BrowserHelper.GetUserDataDir()
            };
        }
    }
}
