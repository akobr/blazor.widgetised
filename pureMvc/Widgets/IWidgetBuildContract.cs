namespace Blazor.PureMvc
{
    public interface IWidgetBuildContract
    {
        void SetState(object state);

        void SetPresenter(IPresenter presenter);
    }
}