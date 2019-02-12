using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public abstract class BlazorWidgetMediator<TComponent> : WidgetMediator, IWidgetPresenterProvider
        where TComponent : class, IComponent
    {
        private readonly WidgetPresenter<TComponent> autoPresenter;

        public BlazorWidgetMediator()
        {
            autoPresenter = new WidgetPresenter<TComponent>();
        }

        IWidgetPresenter IWidgetPresenterProvider.Presenter => autoPresenter;

        protected TComponent Component => autoPresenter.Component;
    }

    public abstract class BlazorWidgetMediator<TComponent, TState> : BlazorWidgetMediator<TComponent>
        where TComponent : class, IComponent
    {
        private TState typedState;

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