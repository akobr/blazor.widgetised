using Blazor.Widgetised.Presenters;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Mediators
{
    public abstract class RazorWidgetMediator<TComponent> : WidgetMediator, IWidgetPresenterProvider
        where TComponent : class, IComponent
    {
        private readonly WidgetPresenter<TComponent> autoPresenter;

        protected RazorWidgetMediator()
        {
            autoPresenter = new WidgetPresenter<TComponent>();
        }

        IWidgetPresenter IWidgetPresenterProvider.Presenter => autoPresenter;

#pragma warning disable CS8603 
        // CS8603: Possible null reference return.
        // Reason: Can be ignored because is part of widget concept and will be set on widget activation.

        /// <summary>
        /// Gets the razor component responsible for UI part of the widget.
        /// Would be null before activation, firstly can be used in <see cref="WidgetMediator.OnActivate"/> method.
        /// </summary>
        protected TComponent Component => autoPresenter.Component;
#pragma warning restore CS8603 // Possible null reference return.
    }

#pragma warning disable CS8618
    // CS8618: Non-nullable field is uninitialized.
    // Reason: typedState field will be filled in as part of build process in widget factory.
    public abstract class RazorWidgetMediator<TComponent, TState> : RazorWidgetMediator<TComponent>
        where TComponent : class, IComponent
    {
        private TState typedState;

        /// <summary>
        /// Gets the state of the widget, this would be injected during widget build process. Potentially could be null.
        /// </summary>
        protected TState State
        {
            get
            {
                if (typedState == null)
                {
                    typedState = GetState<TState>();
                }

                return typedState;
            }
        }
    }
}