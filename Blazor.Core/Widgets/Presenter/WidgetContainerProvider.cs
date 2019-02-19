using System.Collections.Generic;
using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public class WidgetContainerManagement : IWidgetContainerManagement
    {
        private readonly IDictionary<string, IRenderingContainer> map;

        public WidgetContainerManagement()
        {
            map = new Dictionary<string, IRenderingContainer>();
        }

        public IRenderingContainer Get(string containerKey)
        {
            map.TryGetValue(containerKey, out IRenderingContainer container);
            return container;
        }

        public void Register(string key, IRenderingContainer container)
        {
            map[key] = container;
        }

        public void Unregister(string key)
        {
            map.Remove(key);
        }
    }
}
