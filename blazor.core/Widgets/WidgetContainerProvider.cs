using System.Collections.Generic;
using Blazor.Core.Components;

namespace Blazor.Core.Widgets
{
    public class WidgetContainerProvider : IWidgetContainerProvider, IWidgetContainerRegister
    {
        private readonly IDictionary<string, IContainer> map;

        public WidgetContainerProvider()
        {
            map = new Dictionary<string, IContainer>();
        }

        public IContainer GetContainer(string containerKey)
        {
            map.TryGetValue(containerKey, out IContainer container);
            return container;
        }

        public void Register(string key, IContainer container)
        {
            map[key] = container;
        }

        public void Unregister(string key)
        {
            map.Remove(key);
        }
    }
}
