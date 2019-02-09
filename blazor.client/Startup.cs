using Blazor.Client;
using Blazor.Core.Widgets;
using Blazor.PureMvc.Messaging;
using Blazor.PureMvc.Widgets;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace blazor.client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Message bus
            services.AddSingleton<IMessageBus, MessageBus>();

            // Widget container register and provider 
            WidgetContainerProvider containerProvider = new WidgetContainerProvider();
            services.AddSingleton<IWidgetContainerProvider>(containerProvider);
            services.AddSingleton<IWidgetContainerRegister>(containerProvider);

            // Widget factory
            services.AddSingleton<IWidgetFactory, WidgetFactory>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
            app.RegisterWidgets();
        }
    }
}
