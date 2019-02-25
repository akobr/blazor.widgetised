using System;
using System.Collections.Generic;

namespace Blazor.Widgetised
{
    public class WidgetStore : IWidgetStore
    {
        private readonly IDictionary<string, Guid> keyMap;
        private readonly IDictionary<Guid, object> widgets;

        public WidgetStore()
        {
            keyMap = new Dictionary<string, Guid>();
            widgets = new Dictionary<Guid, object>();
        }

        public Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }

        public Guid GetGuid(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Guid.Empty;
            }

            keyMap.TryGetValue(key, out Guid guid);
            return guid;
        }

        public object Get(Guid id)
        {
            widgets.TryGetValue(id, out object widgetMediator);
            return widgetMediator;
        }

        public object Get(string key)
        {
            return Get(GetGuid(key));
        }

        public void Add(Guid id, string key, object widgetMediator)
        {
            if (widgetMediator == null || id == Guid.Empty)
            {
                return;
            }

            if (!string.IsNullOrEmpty(key))
            {
                keyMap[key] = id;
            }

            widgets[id] = widgetMediator;
        }

        public void Remove(Guid id)
        {
            widgets.Remove(id);
        }

        public void Remove(string key)
        {
            Remove(GetGuid(key));
        }
    }
}
