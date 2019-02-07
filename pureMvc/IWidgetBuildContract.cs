namespace Blazor.PureMvc
{
    internal interface IWidgetBuildContract
    {
        void SetState(object state);

        void SetPresenter(IPresenter presenter);
    }
}