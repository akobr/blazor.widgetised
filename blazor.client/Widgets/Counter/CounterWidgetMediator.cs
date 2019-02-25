using Blazor.Widgetised;
using Blazor.Widgetised.Mediators;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterWidgetMediator : WidgetMediator<ICounterWidgetPresenter, CounterWidgetState>, IInitialisable
    {
        private bool isStateRestored;

        public void Initialise()
        {
            // Set interactions
            InteractionPipe.Register<CounterMessage.Increment>((m) => { State.Count++; RenderCount(); });
            InteractionPipe.Register<CounterMessage.Decrement>((m) => { State.Count--; RenderCount(); });

            // Set message
            MessageBus.Register<CounterMessage.Add>(this, HandleAddToCount);

            // Initialisation of the state
            isStateRestored = State.IsRestored;
            if(!isStateRestored)
            {
                State.Count = 42;
                State.IsRestored = true;
            }
        }

        // Initial render when a widget is activated in a container
        protected override void InitialRender()
        {
            if (isStateRestored)
            {
                Presenter.SetIsRestored();
            }

            RenderCount();
        }

        private void RenderCount()
        {
            Presenter.SetCount(State.Count);
        }

        // A handler for CounterMessage.Add message
        private void HandleAddToCount(CounterMessage.Add message)
        {
            State.Count += message.Amount;
            RenderCount();
        }
    }
}
