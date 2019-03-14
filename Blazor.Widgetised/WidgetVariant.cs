using System;

namespace Blazor.Widgetised
{
    public class WidgetVariant
    {
        public WidgetVariant(Type mediatorType)
        {
            MediatorType = mediatorType;
        }

        public Type MediatorType { get; set; }

        public Type? PresenterType { get; set; }

        public Type? StateType { get; set; }

        public object? Customisation { get; set; }
    }

    public class WidgetVariant<TMediator> : WidgetVariant
    {
        public WidgetVariant()
            : base(typeof(TMediator))
        {
            // no operation
        }
    }

    public class WidgetVariant<TMediator, TPresenter> : WidgetVariant
    {
        public WidgetVariant()
            : base(typeof(TMediator))
        {
            PresenterType = typeof(TPresenter);
        }
    }

    public class WidgetVariant<TMediator, TPresenter, TState> : WidgetVariant
    {
        public WidgetVariant()
            : base(typeof(TMediator))
        {
            PresenterType = typeof(TPresenter);
            StateType = typeof(TState);
        }
    }

    public class CustomisedWidgetVariant<TCustomisation> : WidgetVariant
        where TCustomisation : new()
    {
        public CustomisedWidgetVariant(Type mediatorType)
            : base(mediatorType)
        {
            Customisation = new TCustomisation();
            base.Customisation = Customisation;
        }

        public new TCustomisation Customisation { get; }
    }

    public class CustomisedWidgetVariant<TMediator, TCustomisation> : CustomisedWidgetVariant<TCustomisation>
        where TCustomisation : new()
    {
        public CustomisedWidgetVariant()
            : base(typeof(TMediator))
        {
            // no operation
        }
    }

    public class CustomisedWidgetVariant<TMediator, TPresenter, TCustomisation> : CustomisedWidgetVariant<TCustomisation>
        where TCustomisation : new()
    {
        public CustomisedWidgetVariant()
            : base(typeof(TMediator))
        {
            PresenterType = typeof(TPresenter);
        }
    }

    public class CustomisedWidgetVariant<TMediator, TPresenter, TState, TCustomisation> : CustomisedWidgetVariant<TCustomisation>
        where TCustomisation : new()
    {
        public CustomisedWidgetVariant()
            : base(typeof(TMediator))
        {
            PresenterType = typeof(TPresenter);
            StateType = typeof(TState);
        }
    }
}

