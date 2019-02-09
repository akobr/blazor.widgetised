using Blazor.Client.Widgets.Counter;
using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blazor.Client
{
    public static class WidgetRegister
    {
        public static void RegisterWidgets(this IComponentsApplicationBuilder appBuilder)
        {
            WidgetFactory widgetFactory = appBuilder.Services.GetService<WidgetFactory>();
            widgetFactory.RegisterWidget("Counter", new WidgetVariant
            {
                MediatorType = typeof(CounterWidgetMediator),
                PresenterType = typeof(CounterPresenter),
                StateType = typeof(CounterWidgetState)
            });
        }
    }
}
