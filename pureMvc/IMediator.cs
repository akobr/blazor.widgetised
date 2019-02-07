namespace Blazor.PureMvc
{
    public interface IMediator : IActivatable<string>
    {
        string Key { get; }
    }
}