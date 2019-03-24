using System.Collections.Generic;
using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;

namespace Blazor.Client.Widgets.Customisable
{
    public class CustomisableWidgetMediator : RazorWidgetMediator<CustomisableWidgetLayout>, IInitialisable
    {
        private IExampleCustomisation customisation;

        public CustomisableWidgetMediator()
        {
            customisation = new ExampleCustomisation();
        }

        public void Initialise()
        {
            customisation = GetCustomisation<IExampleCustomisation>() ?? customisation;
        }

        void ExampleOfCustomisation()
        {
            var variant = new CustomisedWidgetVariant<CustomisableWidgetMediator, ExampleCustomisation>();
            variant.Customisation.Text = "text value";
            variant.Customisation.Number = 2019;
            variant.Customisation.Flag = false;
        }

        void ExampleOfDynamicCustomisationChange()
        {
            var changes = new Dictionary<string, object>()
            {
                { nameof(ExampleCustomisation.Text), "new value" },
                { nameof(ExampleCustomisation.Number), 7 }
            };


            // Partial customisation change can be defined by dynamic type Customisation<TCustomisation>
            // Dynamic types're required two packages: Microsoft.CSharp and System.Dynamic.Runtime

            // dynamic changes = new Customisation<IExampleCustomisation>();
            // changes.Text = "new value";
            // changes.Number = 7;

            WidgetDescription description = new WidgetDescription()
            {
                VariantName = "MyWidgetVariant",
                Customisation = changes
            };
        }

        void ExampleOfTypedCustomisationChange()
        {
            // Example of typed customisation which can be partly updated
            var description = new WidgetDescription<TypedPartialExampleCustomisation>()
            {
                VariantName = "MyWidgetVariant"
            };

            description.Customisation.Text = "new value";
            description.Customisation.Number = 7;
        }
    }
}
