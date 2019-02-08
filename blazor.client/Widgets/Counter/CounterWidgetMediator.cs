using Blazor.PureMvc;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterWidgetMediator : WidgetMediator<ICounterPresenter, CounterState>, IInitialisable
    {
        public override string Key => "Counter";

        public void Initialise()
        {
            State.Count = 1;
        }

        protected override void InitialRender()
        {
            RenderCount();
        }

        private void RenderCount()
        {
            Presenter.SetCount(State.Count);
        }
    }
}
