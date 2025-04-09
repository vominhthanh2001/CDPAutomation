using CDPAutomation.Abstracts;
using CDPAutomation.Helpers;
using CDPAutomation.Interfaces.CDP;
using CDPAutomation.Interfaces.Element;
using CDPAutomation.Interfaces.FindElement;
using CDPAutomation.Interfaces.JavaScript;
using CDPAutomation.Models.Browser;
using CDPAutomation.Models.CDP;
using CDPAutomation.Models.FindElement;
using CDPAutomation.Models.FindElement.Element.CoreJavaScript;

namespace CDPAutomation.Implementation
{
    internal class FindElementImplementation(ICDP cdp, DebuggerPageResult debuggerPageResponse) : AbstractInitializeImplementation(cdp, debuggerPageResponse), IFindElement
    {
        private readonly ICDP _cdp = cdp;
        private readonly IJavaScriptExecutor _javaScriptExecutor = new JavaScriptImplementation(cdp, debuggerPageResponse);
        private readonly DebuggerPageResult _debuggerPageResponse = debuggerPageResponse;

        public async Task<IElement?> ById(string id)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(id);

            List<IElement> elements = await BysId(id);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> ByName(string name)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

            List<IElement> elements = await BysName(name);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> ByClassName(string className)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(className);

            List<IElement> elements = await BysClassName(className);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> ByCssSelector(string cssSelector)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(cssSelector);

            List<IElement> elements = await BysCssSelector(cssSelector);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<IElement?> ByXPath(string xpath)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(xpath);

            List<IElement> elements = await BysXPath(xpath);
            ArgumentNullException.ThrowIfNull(elements);

            IElement? element = elements.FirstOrDefault();
            return element;
        }

        public async Task<List<IElement>> BysId(string id)
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

        public async Task<List<IElement>> BysName(string name)
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

        public async Task<List<IElement>> BysClassName(string className)
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

        public async Task<List<IElement>> BysCssSelector(string cssSelector)
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

        public async Task<List<IElement>> BysXPath(string xpath)
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
