using System;

namespace Blazor.Core.Widgets
{
    public class WidgetVariant
    {
        public Type MediatorType { get; set; }

        public Type PresenterType { get; set; }

        public Type StateType { get; set; }

        public virtual object Customisation { get; set; }
    }

    public class WidgetVariant<TCustomisation> : WidgetVariant
        where TCustomisation : new()
    {
        public WidgetVariant()
        {
            Customisation = new TCustomisation();
        }

        public new TCustomisation Customisation { get; set; }
    }
}

