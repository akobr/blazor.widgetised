using blazor.client.Widgets.Counter;
using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterPresenter : WidgetPresenter<CounterComponent>, ICounterWidgetPresenter
    {
        public void SetCount(int count)
        {
            Component.Count = count;
        }
    }
}
