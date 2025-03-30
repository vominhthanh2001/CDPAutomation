using CDPAutomation.Drivers;
using CDPAutomation.Enums.WebDriver;
using CDPAutomation.Exceptions;
using CDPAutomation.Interfaces.Browser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Factorys
{
    public class WebDriverFactory
    {
        public static IBrowser CreateWebDriver(WebDriverMode mode)
        {
            IBrowser? browser = null;
            switch (mode)
            {
                case WebDriverMode.Chrome:
                    browser = new ChromeBrowser();
                    break;
                case WebDriverMode.Firefox:
                    throw new WebDriverException("WebDriver is not supported.");
                case WebDriverMode.Edge:
                    throw new WebDriverException("WebDriver is not supported.");
                case WebDriverMode.Safari:
                    throw new WebDriverException("WebDriver is not supported.");
                case WebDriverMode.Opera:
                    throw new WebDriverException("WebDriver is not supported.");
                case WebDriverMode.InternetExplorer:
                    throw new WebDriverException("WebDriver is not supported.");
            }

            return browser ?? throw new WebDriverException("WebDriver is not supported.");
        }
    }
}
