using blazor.client.Widgets.Counter;
using Blazor.Core.Widgets;

namespace Blazor.Client.Widgets.Counter
{
    public class CounterPresenter : WidgetPresenter<CounterComponent>, ICounterPresenter
    {
        public void SetCount(int count)
        {
            Component.Count = count;
        }
    }
}
