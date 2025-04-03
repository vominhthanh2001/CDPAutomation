using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Extensions
{
    internal static class TaskExtension
    {
        internal static void ThrowIfNull(Task task, bool isWait = true)
        {
            ArgumentNullException.ThrowIfNull(task);

            if (isWait)
            {
                task.Wait();
            }
        }
    }
}
