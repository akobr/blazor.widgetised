using Blazor.Core.Widgets;
using Blazor.PureMvc.Messaging;
using Blazor.PureMvc.Widgets;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, MessageBus>();
            services.AddSingleton<IWidgetContainerManagement, WidgetContainerManagement>();
            services.AddSingleton<IWidgetFactory, WidgetFactory>();

            services.RegisterWidgets();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.RegisterWidgetVariants();
        }
    }
}
