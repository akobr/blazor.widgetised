using Blazor.PureMvc.Interactions;
using Blazor.PureMvc.Messaging;
using System;

namespace Blazor.PureMvc.Widgets
{
    public abstract class WidgetMediator : IWidgetMediator, IWidgetBuildContract, IActivatable<string>, IDisposable
    {
        private readonly InteractionPipe interactionPipe;
        private IWidgetPresenter presenter;
        private object state;

        public WidgetMediator()
        {
            interactionPipe = new InteractionPipe(null);
        }

        public abstract string Key { get; }

        protected IInteractionPipe InteractionPipe => interactionPipe;

        protected IMessageBus MessageBus { get; private set; }

        public void Activate(string containerKey)
        {
            presenter?.Activate(new WidgetPlatformContext
            {
                ContainerKey = containerKey,
                InteractionPipe = interactionPipe
            });

            OnActivate();
            InitialRender();
        }

        public void Deactivate()
        {
            presenter?.Deactivate();
            OnDeactivate();
        }

        public void Dispose()
        {
            OnDestroy();
            interactionPipe?.Dispose();
            MessageBus?.UnregisterAll(this);
            MessageBus = null;
        }

        protected TState GetState<TState>()
        {
            return (TState)state;
        }

        protected TPresenter GetPresenter<TPresenter>()
        {
            return (TPresenter)presenter;
        }

        protected virtual void OnInitialise()
        {
            // no operation ( template method )
        }

        protected virtual void OnActivate()
        {
            // no operation ( template method )
        }

        protected virtual void OnDeactivate()
        {
            // no operation ( template method )
        }

        protected virtual void OnDestroy()
        {
            // no operation ( template method )
        }

        protected virtual void InitialRender()
        {
            // no operation ( template method )
        }

        void IWidgetBuildContract.SetPresenter(IWidgetPresenter newPresenter)
        {
            presenter?.Deactivate();
            presenter = newPresenter;
        }

        void IWidgetBuildContract.SetState(object newState)
        {
            state = newState;
        }

        void IWidgetBuildContract.SetMessageBus(IMessageBus bus)
        {
            MessageBus = bus;
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