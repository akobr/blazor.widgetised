using System;
using System.Collections.Generic;

namespace Blazor.Client.Widgets.Layout
{
    public class LayoutState
    {
        public LayoutState()
        {
            Widgets = new Dictionary<string, LayoutItem>();
        }

        public IDictionary<string, LayoutItem> Widgets { get; }

        public class LayoutItem
        {
            public LayoutItem(Guid widgetId, string variantName, string containerKey)
            {
                WidgetId = widgetId;
                VariantName = variantName;
                ContainerKey = containerKey;
            }

            public Guid WidgetId { get; }

            public string VariantName { get; }

            public string ContainerKey { get; }
        }
    }
}
