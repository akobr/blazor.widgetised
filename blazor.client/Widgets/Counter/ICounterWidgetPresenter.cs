namespace Blazor.Client.Widgets.Counter
{
    public interface ICounterWidgetPresenter
    {
        void SetCount(int count);

        void SetIsRestored();
    }
}
