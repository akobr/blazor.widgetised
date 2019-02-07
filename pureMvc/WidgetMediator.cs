namespace Blazor.PureMvc
{
    public abstract class WidgetMediator : IMediator, IWidgetBuildContract
    {
        protected IPresenter presenter;
        protected object state;

        public abstract string Key { get; }

        public void Activate(string containerKey)
        {
            presenter?.Activate(containerKey);
        }

        public void Deactivate()
        {
            presenter?.Deactivate();
        }


        public void SetState(object newState)
        {
            state = newState;
        }

        public void SetPresenter(IPresenter newPresenter)
        {
            presenter?.Deactivate();
            presenter = newPresenter;
        }
    }
}