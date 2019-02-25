using System;
using System.Collections.Generic;
using Blazor.Widgetised;

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
            public string VariantName { get; set; }

            public string ContainerKey { get; set; }
        }
    }
}
