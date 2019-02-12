using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterWidgetMediator : WidgetMediator<ICounterWidgetPresenter, CounterWidgetState>
    {
        protected override void OnInitialise()
        {
            // Set interactions
            InteractionPipe.Register<CounterMessage.Increment>((m) => { State.Count++; RenderCount(); });
            InteractionPipe.Register<CounterMessage.Decrement>((m) => { State.Count--; RenderCount(); });

            // Set message
            MessageBus.Register<CounterMessage.Add>(this, HandleAddToCount);

            // Initialisation of the state
            State.Count = 42;
        }

        // Initial render when a widget is activated in a container
        protected override void InitialRender()
        {
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
