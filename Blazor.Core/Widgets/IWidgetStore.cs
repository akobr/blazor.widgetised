using System;

namespace Blazor.Core.Widgets
{
    public interface IWidgetStore
    {
        void Add(Guid key, object widgetMediator);

        object Get(Guid key);

        Guid GetNewGuid();

        void Remove(Guid key);
    }
}