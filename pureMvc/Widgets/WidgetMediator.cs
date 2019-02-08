namespace Blazor.PureMvc
{
    public abstract class WidgetMediator : IMediator, IWidgetBuildContract
    {
        protected internal IPresenter presenter;
        protected internal object state;

        public abstract string Key { get; }

        public void Activate(string containerKey)
        {
            presenter?.Activate(containerKey);
            OnActivate();
            InitialRender();
        }

        public void Deactivate()
        {
            presenter?.Deactivate();
            OnDeactivate();
        }

        protected TState GetState<TState>()
        {
            return (TState)state;
        }

        protected TPresenter GetPresenter<TPresenter>()
        {
            return (TPresenter)presenter;
        }

        protected virtual void OnActivate()
        {
            // no operation ( template method )
        }

        protected virtual void OnDeactivate()
        {
            // no operation ( template method )
        }

        protected virtual void InitialRender()
        {
            // no operation ( template method )
        }

        void IWidgetBuildContract.SetState(object newState)
        {
            state = newState;
        }

        void IWidgetBuildContract.SetPresenter(IPresenter newPresenter)
        {
            presenter?.Deactivate();
            presenter = newPresenter;
        }
    }

    public abstract class WidgetMediator<TPresenter> : WidgetMediator
    {
        // TODO: cache
        protected TPresenter Presenter => GetPresenter<TPresenter>();
    }

    public abstract class WidgetMediator<TPresenter, TState> : WidgetMediator<TPresenter>
    {
        // TODO: cache
        protected TState State => GetState<TState>();
    }
}