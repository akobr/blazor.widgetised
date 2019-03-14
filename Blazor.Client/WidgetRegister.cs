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

            widgetFactory.Register(WidgetVariants.SHOW_WIDGET, new WidgetVariant<ButtonWidgetMediator, ButtonWidgetPresenter>
            {
                Customisation = new ButtonWidgetCustomisation
                {
                    // Hind: Example of usage of strategy (delegate) in widget mediator
                    Title = "Activate text widget in first container!",
                    ClickStrategy = () =>
                    {
                        // Hind: Manual widget build and activation, in most of cases would be easier to use IWidgetManagementService
                        WidgetInfo info = widgetFactory.Build(WidgetVariants.TEXT_FIRST);
                        IActivatable<string> activatable = (IActivatable<string>)info.Mediator;
                        activatable.Activate("FIRST_WIDGET_CONTAINER");
                    }
                }
            });

            var textVariant = new CustomisedWidgetVariant<TextWidgetMediator, TextWidgetCustomisation>();
            textVariant.Customisation.Text = "This text has been activated from runtime!";
            widgetFactory.Register(WidgetVariants.TEXT_FIRST, textVariant);

            widgetFactory.Register(WidgetVariants.RANDOM_UPDATER, new WidgetVariant<RandomUpdaterWidgetMediator>());

            widgetFactory.Register(WidgetVariants.COUNTER, new WidgetVariant<CounterWidgetMediator, CounterWidgetPresenter, CounterWidgetState>());

            widgetFactory.Register(WidgetVariants.LAYOUT, new WidgetVariant(typeof(ExampleLayoutWidget))
            {
                StateType = typeof(LayoutState)
            });
        }
    }
}
