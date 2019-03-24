using Blazor.Widgetised.Configuration;

namespace Blazor.Client.Widgets.Customisable
{
    public class TypedPartialExampleCustomisation : PartialCustomisationBase, IExampleCustomisation
    {
        private string text = string.Empty;
        private int number;
        private bool flag;

        public bool Flag
        {
            get => flag;
            set => SetProperty(ref flag, value);
        }

        public int Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
    }
}
