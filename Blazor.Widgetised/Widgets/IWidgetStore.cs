using System;

namespace Blazor.Widgetised
{
    public interface IWidgetStore
    {
        Guid GetNewGuid();

        Guid GetGuid(string key);

        void Add(Guid id, string key, object widgetMediator);

        object Get(Guid id);

        object Get(string key);

        void Remove(Guid id);

        void Remove(string key);
    }
}