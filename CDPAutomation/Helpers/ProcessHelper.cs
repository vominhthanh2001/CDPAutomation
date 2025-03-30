using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    public class ProcessResult
    {
        public Process? Process { get; set; }
        public int? ProcessId
        {
            get
            {
                if (Process is null) return default;

                return Process.Id;
            }
        }
        public string? Output { get; set; }
        public int? ProcessExitCode { get; set; }

        public string? ErrorOutput { get; set; }
        public int? ProcessExitCodeError { get; set; }
    }

    public class ProcessHelper
    {
        public static ProcessResult? Execute(string filename, string command, bool isWaitForExit = true)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        Arguments = command,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    },
                    EnableRaisingEvents = true
                };

                // Prepare output collection
                var outputBuilder = new StringBuilder();
                var errorBuilder = new StringBuilder();

                // Set up output handling before starting the process
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        outputBuilder.AppendLine(e.Data);
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                        errorBuilder.AppendLine(e.Data);
                };

                // Start the process
                process.Start();

                // Begin reading output and error asynchronously
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                if (isWaitForExit)
                {
                    // Wait for process to exit
                    process.WaitForExit();

                    // Create the result
                    return new ProcessResult
                    {
                        Process = process,
                        Output = outputBuilder.ToString(),
                        ProcessExitCode = process.HasExited ? process.ExitCode : null,
                        ErrorOutput = errorBuilder.ToString(),
                        ProcessExitCodeError = process.HasExited ? process.ExitCode : null
                    };
                }
                else
                {
                    // For non-waiting mode, return the process with current output
                    return new ProcessResult
                    {
                        Process = process,
                        Output = outputBuilder.ToString(),
                        ErrorOutput = errorBuilder.ToString()
                    };
                }
            }
            catch (Exception)
            {
                // Consider logging the exception here
            }

            return default;
        }

        public static ProcessResult? CurlExecute(string command, bool isWaitForExit = true)
        {
            return Execute("curl", command, isWaitForExit);
        }

        public static ProcessResult? CmdExecute(string command, bool isWaitForExit = true)
        {
            return Execute("cmd", command, isWaitForExit);
        }
    }
}
