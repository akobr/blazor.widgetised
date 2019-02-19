using System;
using Blazor.Widgetised.Interactions;
using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Messaging;
using Blazor.Widgetised.Presenters;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Mediators
{
    public abstract class WidgetMediator : IActivatable<string>, IActivatable<Action<RenderFragment>>, IWidgetMediatorBuildContract, IDisposable
    {
        private readonly InteractionPipe interactionPipe;
        private IWidgetPresenter presenter;
        private object state;
        private object customisation;

        public WidgetMediator()
        {
            interactionPipe = new InteractionPipe(null);
        }

        protected bool IsActive { get; private set; }

        protected IInteractionPipe InteractionPipe => interactionPipe;

        protected IMessageBus MessageBus { get; private set; }

        public void Activate(string containerKey)
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            ConsoleLogger.Debug($"DEBUG: A widget '{GetType().Name}' is activating in container '{containerKey}'.");
            presenter?.Activate(new PresenterInContainerActivationContext
            {
                ContainerKey = containerKey,
                InteractionPipe = interactionPipe,
                ContinueWith = () =>
                {
                    OnActivate();
                    InitialRender();
                }
            }); 
        }

        public void Activate(Action<RenderFragment> context)
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            ConsoleLogger.Debug($"DEBUG: A widget '{GetType().Name}' is inlining.");
            presenter?.Activate(new PresenterInlineActivationContext
            {
                RenderAction = context,
                InteractionPipe = interactionPipe,
                ContinueWith = () =>
                {
                    OnActivate();
                    InitialRender();
                }
            });
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            presenter?.Deactivate();
            OnDeactivate();
        }

        public void Dispose()
        {
            Deactivate();
            OnDestroy();
            interactionPipe?.Dispose();
            MessageBus?.UnregisterAll(this);
            MessageBus = null;
        }

        protected TCustomisation GetCustomisation<TCustomisation>()
        {
            return (TCustomisation)customisation;
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

        protected virtual void OnDestroy()
        {
            // no operation ( template method )
        }

        protected virtual void InitialRender()
        {
            // no operation ( template method )
        }

        void IWidgetMediatorBuildContract.SetPresenter(IWidgetPresenter newPresenter)
        {
            presenter?.Deactivate();
            presenter = newPresenter;
        }
        
        void IWidgetMediatorBuildContract.SetState(object newState)
        {
            state = newState;
        }

        void IWidgetMediatorBuildContract.SetCustomisation(object newCustomisation)
        {
            customisation = newCustomisation;
        }

        void IWidgetMediatorBuildContract.SetMessageBus(IMessageBus bus)
        {
            MessageBus = bus;
        }
    }

    public abstract class WidgetMediator<TPresenter> : WidgetMediator
    {
        private TPresenter typedPresenter;

        protected TPresenter Presenter
        {
            get
            {
                if (typedPresenter == null)
                {
                    typedPresenter = GetPresenter<TPresenter>();
                }

                return typedPresenter;
            }
        }
    }

    public abstract class WidgetMediator<TPresenter, TState> : WidgetMediator<TPresenter>
    {
        private TState typedState;

        protected TState State
        {
            get
            {
                if (typedState == null)
                {
                    typedState = GetState<TState>();
                }

                return typedState;
            }
        }
    }
}