using System;

namespace Blazor.Core.Widgets
{
    public class WidgetVariant
    {
        public Type MediatorType { get; set; }

        public Type PresenterType { get; set; }

        public Type StateType { get; set; }

        public object Customisation { get; set; }
    }
}
