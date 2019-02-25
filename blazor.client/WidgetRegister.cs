using Blazor.Client.Widgets;
using Blazor.Client.Widgets.Button;
using Blazor.Client.Widgets.Counter;
using Blazor.Client.Widgets.Layout;
using Blazor.Client.Widgets.RandomUpdater;
using Blazor.Client.Widgets.Text;
using Blazor.Widgetised;
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

            services.AddTransient<RandomUpdaterWidgetMediator>();

            services.AddTransient<CounterWidgetMediator>();
            services.AddTransient<CounterWidgetPresenter>();
            services.AddTransient<CounterWidgetState>();

            services.AddTransient<ExampleLayoutWidget>();
            services.AddTransient<LayoutState>();
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
                        WidgetInfo info = widgetFactory.Build(WidgetVariants.TEXT_FIRST);
                        IActivatable<string> activatable = (IActivatable<string>)info.Mediator;
                        activatable.Activate("FIRST_WIDGET_CONTAINER");
                    }
                }
            });

            widgetFactory.Register(WidgetVariants.TEXT_FIRST, new WidgetVariant
            {
                MediatorType = typeof(TextWidgetMediator),
                Customisation = new TextWidgetCustomisation { Text = "This text has been activated from runtime!" }
            });

            widgetFactory.Register(WidgetVariants.RANDOM_UPDATER, new WidgetVariant
            {
                MediatorType = typeof(RandomUpdaterWidgetMediator)
            });

            widgetFactory.Register(WidgetVariants.COUNTER, new WidgetVariant
            {
                MediatorType = typeof(CounterWidgetMediator),
                PresenterType = typeof(CounterWidgetPresenter),
                StateType = typeof(CounterWidgetState)
            });

            widgetFactory.Register(WidgetVariants.LAYOUT, new WidgetVariant
            {
                MediatorType = typeof(ExampleLayoutWidget),
                StateType = typeof(LayoutState)
            });
        }
    }
}
