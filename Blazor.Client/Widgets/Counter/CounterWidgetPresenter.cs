using Blazor.Widgetised.Presenters;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterWidgetPresenter : WidgetPresenter<CounterComponent>, ICounterWidgetPresenter
    {
        public void SetCount(int count)
        {
            Component.Count = count;
        }
    }
}
