using Microsoft.AspNetCore.Components;

namespace Blazor.Core.Widgets
{
    public abstract class BlazorWidgetMediator<TComponent> : WidgetMediator<WidgetPresenter<TComponent>>
        where TComponent : class, IComponent
    {
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