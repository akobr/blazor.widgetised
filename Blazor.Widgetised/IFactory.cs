namespace Blazor.Widgetised
{
    public interface IFactory<out TResult>
    {
        TResult Build();
    }

    public interface IFactory<out TResult, in TContext>
    {
        TResult Build(TContext context);
    }

}