using Blazor.Widgetised.Mediators;
using System;
using System.Threading.Tasks;

namespace Blazor.Client.Widgets.RandomUpdator
{
    public class RandomUpdatorWidgetMediator : BlazorWidgetMediator<RandomUpdatorComponent>
    {
        private readonly Random randomGenerator = new Random();

        protected override void OnActivate()
        {
            Update();
        }

        private void Update()
        {
            if (!IsActive)
            {
                return;
            }

            int seconds = randomGenerator.Next(4, 13);
            Render(seconds);

            Task.Run(async () =>
            {
                await Task.Delay(seconds * 1000);
                Update();
            });
        }

        private void Render(int seconds)
        {
            Component.SetModel(DateTime.Now, seconds);
        }
    }
}
