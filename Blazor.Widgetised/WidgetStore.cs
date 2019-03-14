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

        public object? Get(Guid id)
        {
            widgets.TryGetValue(id, out object widgetMediator);
            return widgetMediator;
        }

        public void Add(Guid id, object widgetMediator)
        {
            if (widgetMediator == null || id == Guid.Empty)
            {
                return;
            }

            widgets[id] = widgetMediator;
        }

        public void Remove(Guid id)
        {

            widgets.Remove(id);
        }
    }
}
