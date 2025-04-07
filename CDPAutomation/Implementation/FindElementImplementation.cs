using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Interfaces.Request;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CDPAutomation.Implementation
{
    internal class FindElementImplementation(ICDP cdp, IJavaScriptExecutor javaScriptExecutor, DebuggerPageResult debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IFindElement
    {
        private readonly ICDP _cdp = cdp;
        private readonly IJavaScriptExecutor _javaScriptExecutor = javaScriptExecutor;
        private readonly DebuggerPageResult _debuggerPageResponse = debuggerPageResponse;

        public async Task<IElement?> FindById(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            List<IElement> elements = await FindsById(id);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> FindByName(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            List<IElement> elements = await FindsByName(name);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> FindByClassName(string className)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(className);

            List<IElement> elements = await FindsByClassName(className);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> FindByCssSelector(string cssSelector)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cssSelector);

            List<IElement> elements = await FindsByCssSelector(cssSelector);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> FindByXPath(string xpath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(xpath);

            List<IElement> elements = await FindsByXPath(xpath);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<List<IElement>> FindsById(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            CDPRequest getNodeList = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelectorAll(`[id=""{id}""]`)",
                    ReturnByValue = false
                }
            };

            CDPResult? responseFindsById = await _cdp.SendInstantAsync(getNodeList);
            ArgumentNullException.ThrowIfNull(responseFindsById);

            NodeListResult? nodeList = responseFindsById.Deserialize(JsonContext.Default.NodeListResult);
            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = await GetPropertiesNodeList(nodeList);

            List<IElement> elements = [.. propertiesNodeList.Select(x => (IElement)new ElementImplementation(_cdp, _javaScriptExecutor, _debuggerPageResponse, x.Key, x.Value!))];
            return elements;
        }

        public async Task<List<IElement>> FindsByName(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            CDPRequest getNodeList = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelectorAll(`[name=""{name}""]`)",
                    ReturnByValue = false
                }
            };

            CDPResult? responseFindsById = await _cdp.SendInstantAsync(getNodeList);
            ArgumentNullException.ThrowIfNull(responseFindsById);

            NodeListResult? nodeList = responseFindsById.Deserialize(JsonContext.Default.NodeListResult);
            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = await GetPropertiesNodeList(nodeList);

            List<IElement> elements = [.. propertiesNodeList.Select(x => (IElement)new ElementImplementation(_cdp, _javaScriptExecutor, _debuggerPageResponse, x.Key, x.Value!))];
            return elements;
        }

        public async Task<List<IElement>> FindsByClassName(string className)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(className);
            CDPRequest getNodeList = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelectorAll(`{className}`)",
                    ReturnByValue = false
                }
            };

            CDPResult? responseFindsById = await _cdp.SendInstantAsync(getNodeList);
            ArgumentNullException.ThrowIfNull(responseFindsById);

            NodeListResult? nodeList = responseFindsById.Deserialize(JsonContext.Default.NodeListResult);
            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = await GetPropertiesNodeList(nodeList);

            List<IElement> elements = [.. propertiesNodeList.Select(x => (IElement)new ElementImplementation(_cdp, _javaScriptExecutor, _debuggerPageResponse, x.Key, x.Value!))];
            return elements;
        }

        public async Task<List<IElement>> FindsByCssSelector(string cssSelector)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cssSelector);
            CDPRequest getNodeList = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"document.querySelectorAll(`{cssSelector}`)",
                    ReturnByValue = false
                }
            };

            CDPResult? responseFindsById = await _cdp.SendInstantAsync(getNodeList);
            ArgumentNullException.ThrowIfNull(responseFindsById);

            NodeListResult? nodeList = responseFindsById.Deserialize(JsonContext.Default.NodeListResult);
            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = await GetPropertiesNodeList(nodeList);

            List<IElement> elements = [.. propertiesNodeList.Select(x => (IElement)new ElementImplementation(_cdp, _javaScriptExecutor, _debuggerPageResponse, x.Key, x.Value!))];
            return elements;
        }

        public async Task<List<IElement>> FindsByXPath(string xpath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(xpath);
            CDPRequest getNodeList = new()
            {
                Method = "Runtime.evaluate",
                Params = new EvaluateParams
                {
                    Expression = @$"
                        (function() {{
                            let elements = document.evaluate(`{xpath}`, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                            let result = [];
                            for (let i = 0; i < elements.snapshotLength; i++) {{
                                let e = elements.snapshotItem(i);
                                result.push(e);
                            }}
                            return result;
                        }})();
                    ",
                    ReturnByValue = false
                }
            };

            CDPResult? responseFindsById = await _cdp.SendInstantAsync(getNodeList);
            ArgumentNullException.ThrowIfNull(responseFindsById);

            NodeListResult? nodeList = responseFindsById.Deserialize(JsonContext.Default.NodeListResult);
            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = await GetPropertiesNodeList(nodeList);

            List<IElement> elements = [.. propertiesNodeList.Select(x => (IElement)new ElementImplementation(_cdp, _javaScriptExecutor, _debuggerPageResponse, x.Key, x.Value))];
            return elements;
        }

        private async Task<Dictionary<NodeProperty, RequestNodeResult?>> GetPropertiesNodeList(NodeListResult? nodeList)
        {
            ArgumentNullException.ThrowIfNull(nodeList);
            ArgumentNullException.ThrowIfNull(nodeList.Result);

            CDPRequest getProperties = new()
            {
                Method = "Runtime.getProperties",
                Params = new GetPropertiesParams
                {
                    ObjectId = nodeList.Result.ObjectId,
                    OwnProperties = true
                }
            };

            CDPResult? responseGetProperties = await _cdp.SendInstantAsync(getProperties);
            ArgumentNullException.ThrowIfNull(responseGetProperties);

            NodeProperties? nodeProperties = responseGetProperties.Deserialize(JsonContext.Default.NodeProperties);
            ArgumentNullException.ThrowIfNull(nodeProperties);
            ArgumentNullException.ThrowIfNull(nodeProperties.Result);
            ArgumentNullException.ThrowIfNull(nodeProperties.InternalProperties);

            Dictionary<NodeProperty, RequestNodeResult?> propertiesNodeList = [];
            List<NodeProperty> listNodeProperty = nodeProperties.Result;

            foreach (var nodeProperty in listNodeProperty)
            {
                RequestNodeResult? requestNodeResult = await RequestNode(nodeProperty);
                propertiesNodeList[nodeProperty] = requestNodeResult;
            }

            return propertiesNodeList;
        }

        private async Task<RequestNodeResult?> RequestNode(NodeProperty nodeProperty)
        {
            ArgumentNullException.ThrowIfNull(nodeProperty);
            ArgumentNullException.ThrowIfNull(nodeProperty.Value);

            if (nodeProperty.Value.ObjectId is null)
                return default;

            CDPRequest @params = new()
            {
                Method = "DOM.requestNode",
                Params = new RequestNodeParams
                {
                    ObjectId = nodeProperty.Value.ObjectId,
                }
            };

            CDPResult? responseGetRequestNode = await _cdp.SendInstantAsync(@params);
            RequestNodeResult? requestNodeResult = responseGetRequestNode.Deserialize(JsonContext.Default.RequestNodeResult);
            return requestNodeResult;
        }
    }
}
