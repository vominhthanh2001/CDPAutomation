using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Exceptions
{
    public class WebDriverException : Exception
    {
        public WebDriverException()
        {
        }

        public WebDriverException(string? message) : base(message)
        {
        }

        public WebDriverException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
