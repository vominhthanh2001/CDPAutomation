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
                    browser = new FirefoxBrowser();
                    break;
                case WebDriverMode.Edge:
                    browser = new EdgeBrowser();
                    break;
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
