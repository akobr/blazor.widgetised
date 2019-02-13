using Blazor.Client.Widgets.RandomUpdator;
using Blazor.Client.Widgets.Text;
using Blazor.Core;
using Blazor.Core.Messaging;
using Blazor.Core.Widgets;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Blazor.Client.Widgets.List
{
    public class ListWidgetMediator : BlazorWidgetMediator<ListWidgetComponent>
    {
        private readonly Random randomGenerator;
        private readonly IWidgetFactory factory;
        private readonly Stopwatch stopwatch;

        public ListWidgetMediator(IWidgetFactory factory)
        {
            this.factory = factory;
            randomGenerator = new Random();
            stopwatch = new Stopwatch();
        }

        protected override void OnInitialise()
        {
            InteractionPipe.Register<Messages.Click>((m) => { BuildItems(); });
        }

        protected override void InitialRender()
        {
            BuildItems();
        }

        private async Task BuildItems()
        {
            int count = Component.UseLargeList ? 1420 : randomGenerator.Next(5, 16);
            await Component.CreateContainersAsync(count);
            await Task.Delay(7);
            BuildWidgets(count);
        }

        private void BuildWidgets(int count)
        {
            stopwatch.Start();

            for (int i = 0; i < count; i++)
            {
                string containerKey = $"ITEM_CONTAINER_{i}";

                if (randomGenerator.NextDouble() <= 0.62)
                {
                    BuildTextWidget(containerKey, $"Item number {i:00}");
                }
                else
                {
                    BuildRandomUpdatorWidget(containerKey);
                }
            }

            stopwatch.Stop();
            Component.SetRenderTime(stopwatch.Elapsed);
        }

        private void BuildTextWidget(string containerKey, string text)
        {
            WidgetVariant variant = new WidgetVariant
            {
                MediatorType = typeof(TextWidgetMediator),
                Customisation = new TextWidgetCustomisation { Text = text }
            };

            BuildWidget(containerKey, variant);
        }

        private void BuildRandomUpdatorWidget(string containerKey)
        {
            WidgetVariant variant = new WidgetVariant
            {
                MediatorType = typeof(RandomUpdatorWidgetMediator)
            };

            BuildWidget(containerKey, variant);
        }

        private void BuildWidget(string containerKey, WidgetVariant variant)
        {
            IActivatable<string> widget = (IActivatable<string>)factory.Build(variant);
            widget.Activate(containerKey);
        }
    }
}
