using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Messaging;

namespace Blazor.Client.Widgets.Mvvm
{
    public class MvvmWidget : BlazorWidgetMediator<MvvmWidgetLayout>, IInitialisable
    {
        private ViewModel model = new ViewModel();

        public void Initialise()
        {
            model = GetCustomisation<ViewModel>() ?? model;
            InteractionPipe.Register<Messages.Click>(HandleAddItem);
        }

        protected override void OnActivate()
        {
            Component.SetViewModel(model);
        }

        private void HandleAddItem(Messages.Click message)
        {
            if (message.Name != "AddItem"
                || message.Content == null)
            {
                return;
            }

            model.Items = model.Items.Add(message.Content.ToString());
        }
    }
}
