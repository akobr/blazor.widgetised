using System;

namespace Blazor.Widgetised
{
    public class WidgetInfo : IWidgetIdentifier
    {
        public WidgetInfo(Guid id, string key, object mediator)
        {
            Id = id;
            Key = key;
            Mediator = mediator;
        }

        public Guid Id { get; }

        public string Key { get; }

        public object Mediator { get; }
    }
}
