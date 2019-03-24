using Blazor.Widgetised.Configuration;

namespace Blazor.Client.Widgets.Customisable
{
    // Simple customisation model
    public class ExampleCustomisation : IExampleCustomisation
    {
        public ExampleCustomisation()
        {
            // Default values
            Text = "default";
            Number = 42;
            Flag = true;
        }

        public string Text { get; set; }

        public int Number { get; set; }

        public bool Flag { get; set; }
    }
}
