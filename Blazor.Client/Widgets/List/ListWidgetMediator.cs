using Blazor.Client.Widgets.RandomUpdator;
using Blazor.Client.Widgets.Text;
using Blazor.Core;
using Blazor.Core.Messaging;
using Blazor.Core.Widgets;
using System;

namespace Blazor.Client.Widgets.List
{
    public class ListWidgetMediator : BlazorWidgetMediator<ListWidgetComponent>
    {
        private readonly Random randomGenerator;
        private readonly IWidgetFactory factory;

        public ListWidgetMediator(IWidgetFactory factory)
        {
            this.factory = factory;
            randomGenerator = new Random();
        }

        protected override void OnInitialise()
        {
            InteractionPipe.Register<Messages.Click>((m) => { BuildContainers(); });
            InteractionPipe.Register<Message<int>>(OnContainersReady);
        }

        protected override void InitialRender()
        {
            BuildContainers();
        }

        private void OnContainersReady(Message<int> message)
        {
            BuildWidgets(message.Content);
        }

        private void BuildContainers()
        {
            int count = randomGenerator.Next(5, 16);
            Component.CreateItems(count);
        }

        private void BuildWidgets(int count)
        {
            for (int i = 0; i < count; i++)
            {
                string containerKey = $"ITEM_CONTAINER_{i}";

                if (randomGenerator.NextDouble() <= 0.66)
                {
                    BuildTextWidget(containerKey, $"Item number {i:00}");
                }
                else
                {
                    BuildRandomUpdatorWidget(containerKey);
                }
            }
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
