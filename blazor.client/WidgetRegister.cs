using Blazor.Client.Widgets;
using Blazor.Client.Widgets.Button;
using Blazor.Client.Widgets.Counter;
using Blazor.Core.Widgets;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Client
{
    public static class WidgetRegister
    {
        public static void RegisterWidgets(this IServiceCollection services)
        {
            services.AddTransient<ButtonWidgetMediator>();
            services.AddTransient<ButtonWidgetPresenter>();

            services.AddTransient<CounterWidgetMediator>();
            services.AddTransient<CounterWidgetPresenter>();
            services.AddTransient<CounterWidgetState>();
        }

        public static void RegisterWidgetVariants(this IComponentsApplicationBuilder appBuilder)
        {
            IWidgetFactory widgetFactory = appBuilder.Services.GetService<IWidgetFactory>();

            widgetFactory.Register(WidgetVariants.SHOW_WIDGET, new WidgetVariant
            {
                MediatorType = typeof(ButtonWidgetMediator),
                PresenterType = typeof(ButtonWidgetPresenter)
            });

            widgetFactory.Register(WidgetVariants.COUNTER, new WidgetVariant
            {
                MediatorType = typeof(CounterWidgetMediator),
                PresenterType = typeof(CounterWidgetPresenter),
                StateType = typeof(CounterWidgetState)
            });
        }
    }
}
