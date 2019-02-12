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

        // TODO: cache
        protected TComponent Component => GetPresenter<WidgetPresenter<TComponent>>().Component;
    }

    public abstract class BlazorWidgetMediator<TComponent, TState> : BlazorWidgetMediator<TComponent>
        where TComponent : class, IComponent
    {
        // TODO: cache
        protected TState State => GetState<TState>();
    }
}