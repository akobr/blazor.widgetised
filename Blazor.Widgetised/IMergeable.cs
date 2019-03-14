namespace Blazor.Widgetised
{
    public interface IMergeable<TContent>
    {
        void Merge(TContent content);
    }
}
