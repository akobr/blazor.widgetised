using System;
using System.Threading.Tasks;
using Blazor.Widgetised.Mediators;

namespace Blazor.Client.Widgets.RandomUpdater
{
    public class RandomUpdaterWidgetMediator : BlazorWidgetMediator<RandomUpdatorComponent>
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
