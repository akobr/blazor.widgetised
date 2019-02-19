using System;
using System.Collections.Generic;

namespace Blazor.Core.Widgets
{
    public class WidgetStateStore : IWidgetStateStore
    {
        private readonly Dictionary<string, object> states;

        public WidgetStateStore()
        {
            states = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public object Get(string stateKey)
        {
            states.TryGetValue(stateKey, out object widgetMediator);
            return widgetMediator;
        }

        public void Add(string stateKey, object state)
        {
            if (state == null)
            {
                return;
            }

            states[stateKey] = state;
        }

        public void Remove(string stateKey)
        {
            states.Remove(stateKey);
        }
    }
}
