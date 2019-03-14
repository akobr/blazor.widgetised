using System;

namespace Blazor.Widgetised
{
    public interface IWidgetStore
    {
        Guid GetNewGuid();

        void Add(Guid id, object widgetMediator);

        object? Get(Guid id);

        void Remove(Guid id);
    }
}