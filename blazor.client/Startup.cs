using Blazor.Client.Widgets.Counter;
using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace blazor.client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            WidgetContainerProvider containerProvider = new WidgetContainerProvider();
            services.AddSingleton<IWidgetContainerProvider>(containerProvider);
            services.AddSingleton<IWidgetContainerRegister>(containerProvider);
            
            WidgetFactory widgetFactory = new WidgetFactory();
            widgetFactory.RegisterWidget("Counter", new WidgetVariant
            {
                MediatorType = typeof(CounterWidgetMediator),
                PresenterType = typeof(CounterPresenter),
                StateType = typeof(CounterState)
            });
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
