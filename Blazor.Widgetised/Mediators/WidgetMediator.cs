using System;
using Blazor.Widgetised.Interactions;
using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Presenters;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Mediators
{
    public abstract class WidgetMediator : IActivatable<string>, IActivatable<Action<RenderFragment>>, IWidgetMediatorBuildContract, IDisposable
    {
        private readonly InteractionPipe interactionPipe;
        private IWidgetPresenter? presenter;
        private object? state;
        private object? customisation;

        protected WidgetMediator()
        {
            Logger = NotAssigned.Logger;
            interactionPipe = new InteractionPipe();
        }

        protected bool IsActive { get; private set; }

        protected IInteractionPipe InteractionPipe => interactionPipe;

        protected ILogger Logger { get; private set; }

        public void Activate(string containerKey)
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            Logger.Debug($"A widget [{GetType().Name}] is activating in container [{containerKey}].");
            presenter?.Activate(new PresenterInContainerActivationContext(
                containerKey,
                () =>
                {
                    OnActivate();
                    InitialRender();
                },
                interactionPipe
            )); 
        }

        public void Activate(Action<RenderFragment> context)
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            Logger.Trace($"A widget [{GetType().Name}] is inlining.");
            presenter?.Activate(new PresenterInlineActivationContext(
                context,
                () =>
                {
                    OnActivate();
                    InitialRender();
                },
                interactionPipe
            ));
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
            Logger.Trace($"A widget [{GetType().Name}] has been deactivated.");
        }

        public void Dispose()
        {
            Deactivate();
            OnDestroy();

            interactionPipe?.Dispose();
            Logger.Trace($"A widget [{GetType().Name}] has been destroy.");
        }

#pragma warning disable CS8600, CS8603
        // CS8600: Cannot convert null to non-nullable reference. 
        // CS8603: Possible null reference return.
        // Reason: Can be ignored because is part of widget concept and will be set in widget factory.

        /// <summary>
        /// Gets a typed customisation model of the widget.
        /// This function can return null or even throw InvalidCastException. 
        /// </summary>
        /// <typeparam name="TCustomisation">Type of customisation model.</typeparam>
        /// <returns>Typed customisation model of the widget.</returns>
        protected TCustomisation GetCustomisation<TCustomisation>()
        {
            return (TCustomisation)customisation;
        }

        /// <summary>
        /// Gets a typed state of the widget.
        /// This function can return null or even throw InvalidCastException. 
        /// </summary>
        /// <typeparam name="TState">Type of state.</typeparam>
        /// <returns>Typed state of the widget.</returns>
        protected TState GetState<TState>()
        {
            return (TState)state;
        }

        /// <summary>
        /// Gets a typed presenter of the widget.
        /// This function can return null or even throw InvalidCastException. 
        /// </summary>
        /// <typeparam name="TPresenter">Type of presenter.</typeparam>
        /// <returns>Typed presenter of the widget.</returns>
        protected TPresenter GetPresenter<TPresenter>()
        {
            return (TPresenter)presenter;
        }

#pragma warning restore CS8600, CS8603

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

        void IWidgetMediatorBuildContract.SetLogger(ILogger logger)
        {
            Logger = logger;
        }
    }

#pragma warning disable CS8618
    // CS8618: Non-nullable field is uninitialized.
    // Reason: typedPresenter field will be filled in as part of build process in widget factory.
    public abstract class WidgetMediator<TPresenter> : WidgetMediator
#pragma warning restore CS8618
    {
        private TPresenter typedPresenter;

        /// <summary>
        /// Gets the presenter of the widget, this would be injected during widget build process.
        /// Potentially could be null.
        /// </summary>
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

#pragma warning disable CS8618
    // CS8618: Non-nullable field is uninitialized.
    // Reason: typedState field will be filled in as part of build process in widget factory.
    public abstract class WidgetMediator<TPresenter, TState> : WidgetMediator<TPresenter>
#pragma warning restore CS8618
    {
        private TState typedState;

        /// <summary>
        /// Gets the state of the widget, this would be injected during widget build process.
        /// Potentially could be null.
        /// </summary>
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