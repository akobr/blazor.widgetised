using System;
using System.Collections.Generic;

namespace Blazor.Widgetised
{
    public class WidgetStore : IWidgetStore
    {
        private readonly IDictionary<Guid, object> widgets;

        public WidgetStore()
        {
            widgets = new Dictionary<Guid, object>();
        }

        public Guid GetNewGuid()
        {
            return Guid.NewGuid();
        }

        public object Get(Guid key)
        {
            widgets.TryGetValue(key, out object widgetMediator);
            return widgetMediator;
        }

        public void Add(Guid key, object widgetMediator)
        {
            if (widgetMediator == null)
            {
                return;
            }

            widgets[key] = widgetMediator;
        }

        public void Remove(Guid key)
        {
            widgets.Remove(key);
        }
    }
}
