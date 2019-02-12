using Blazor.Core.Interactions;
using Blazor.Core.Messaging;
using Microsoft.AspNetCore.Components;
using System;

namespace Blazor.Core.Widgets
{
    public abstract class WidgetMediator : IWidgetBuildContract, IInitialisable, IActivatable<string>, IActivatable<Action<RenderFragment>>, IDisposable
    {
        private readonly InteractionPipe interactionPipe;
        private IWidgetPresenter presenter;
        private object state;
        private object customisation;
        private bool isActive;

        public WidgetMediator()
        {
            interactionPipe = new InteractionPipe(null);
        }

        protected bool IsActive => isActive;

        protected IInteractionPipe InteractionPipe => interactionPipe;

        protected IMessageBus MessageBus { get; private set; }

        public void Initialise()
        {
            OnInitialise();
        }

        public void Activate(string containerKey)
        {
            if (isActive)
            {
                return;
            }

            isActive = true;
            presenter?.ActivateInContainer(new PresenterActivateInContainerContext
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
            if (isActive)
            {
                return;
            }

            isActive = true;
            presenter?.ActivateInline(new PresenterActivateInlineContext
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
            if (!isActive)
            {
                return;
            }

            isActive = false;
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

        void IWidgetBuildContract.SetCustomisation(object newCustomisation)
        {
            customisation = newCustomisation;
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