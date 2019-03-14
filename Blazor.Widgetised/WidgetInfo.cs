using System;

namespace Blazor.Widgetised
{
    public class WidgetInfo
    {
        public WidgetInfo(Guid id, object mediator)
        {
            Id = id;
            Mediator = mediator;
        }

        public Guid Id { get; }

        public object Mediator { get; }
    }
}
