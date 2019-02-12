using Blazor.Client.Widgets;
using Blazor.Client.Widgets.Button;
using Blazor.Client.Widgets.Counter;
using Blazor.Client.Widgets.RandomUpdator;
using Blazor.Client.Widgets.Text;
using Blazor.Core;
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

            services.AddTransient<TextWidgetMediator>();

            services.AddTransient<RandomUpdatorWidgetMediator>();

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
                PresenterType = typeof(ButtonWidgetPresenter),
                Customisation = new ButtonWidgetCustomisation
                {
                    // Hind: Example of usage of strategy (delegate) in widget mediator
                    Title = "Activate text widget in first container!",
                    ClickStrategy = () =>
                    {
                        object mediator = widgetFactory.Build(WidgetVariants.TEXT_FIRST);
                        IActivatable<string> activatable = (IActivatable<string>)mediator;
                        activatable.Activate("FIRST_WIDGET_CONTAINER");
                    }
                }
            });

            widgetFactory.Register(WidgetVariants.TEXT_FIRST, new WidgetVariant
            {
                MediatorType = typeof(TextWidgetMediator),
                Customisation = new TextWidgetCustomisation { Text = "This text has been activated from runtime!" }
            });

            widgetFactory.Register(WidgetVariants.RANDOM_UPDATOR, new WidgetVariant
            {
                MediatorType = typeof(RandomUpdatorWidgetMediator)
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
