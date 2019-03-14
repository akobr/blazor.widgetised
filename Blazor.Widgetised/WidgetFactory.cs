using System;
using System.Collections.Generic;
using Blazor.Widgetised.Logging;
using Blazor.Widgetised.Mediators;
using Blazor.Widgetised.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Widgetised
{
    public class WidgetFactory : IWidgetFactory
    {
        private readonly IServiceProvider provider;
        private readonly IWidgetStore widgetStore;
        private readonly IWidgetStateStore stateStore;
        private readonly ILogger logger;

        private readonly Dictionary<string, WidgetVariant> variants;

        public WidgetFactory(
            IWidgetStore widgetStore,
            IWidgetStateStore stateStore,
            IServiceProvider provider,
            ILogger? logger)
        {
            this.widgetStore = widgetStore ?? throw new ArgumentNullException(nameof(widgetStore));
            this.stateStore = stateStore ?? throw new ArgumentNullException(nameof(stateStore));
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            variants = new Dictionary<string, WidgetVariant>();
        }

        public void Register(string variantName, WidgetVariant variant)
        {
            variants[variantName] = variant;
        }

        public WidgetInfo Build(string variantName)
        {
            return Build(new WidgetDescription { VariantName = variantName });
        }

        public WidgetInfo Build(WidgetDescription description)
        {
            CheckAndTryUpdateDescription(description);

            object mediator = BuildMediator(description);
            Guid id = widgetStore.GetNewGuid();

            widgetStore.Add(id, mediator);
            logger.Trace($"Widget {id:B} [{WidgetDescription.BuildKey(description).key}] has been created.");
            return new WidgetInfo(id, mediator);
        }

        private void CheckAndTryUpdateDescription(WidgetDescription? description)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            if (!string.IsNullOrEmpty(description.VariantName))
            {
                if (variants.TryGetValue(description.VariantName, out WidgetVariant variant))
                {
                    description.Variant = variant;
                }
                else
                {
                    logger.Warning($"Invalid variant name [{description.VariantName}] detected.");
                    description.VariantName = null;
                }
            }

            if (description.Variant == null)
            {
                throw new ArgumentException("A widget variant or valid variant name must be specified.", nameof(description));
            }

            return;
        }

        private object BuildMediator(WidgetDescription description)
        {
            if (description.Variant == null)
            {
                throw new ArgumentException("No widget variant has been specified.", nameof(description));
            }

            Type mediatorType = description.Variant.MediatorType;
            object mediator = provider.GetRequiredService(mediatorType);

            TryFillMediatorContract(mediator, description);
            TryInitialise(mediator);

            if (mediator == null)
            {
                logger.Error($"No mediator for a widget of type [{description.Variant.MediatorType.Name}].");
            }

            return mediator;
        }

        private void TryFillMediatorContract(object mediator, WidgetDescription description)
        {
            if (!(mediator is IWidgetMediatorBuildContract contract))
            {
                return;
            }

            contract.SetLogger(logger);
            SetCustomisation(description, contract);
            SetState(description, contract);
            SetPresenter(mediator, contract, description);
        }

        private void TryFillPresenterContract(IWidgetPresenter? presenter)
        {
            if (!(presenter is IWidgetPresenterBuildContract contract))
            {
                return;
            }

            contract.SetLogger(logger);
            contract.SetWidgetContainerManagement(provider.GetService<IWidgetContainerManagement>());
        }

        private void SetCustomisation(WidgetDescription description, IWidgetMediatorBuildContract contract)
        {
            object? customisation = GetCustomisation(description);

            if (customisation != null)
            {
                contract.SetCustomisation(customisation);
            }
        }

        private void SetState(WidgetDescription description, IWidgetMediatorBuildContract contract)
        {
            object? state = BuildState(description);

            if (state != null)
            {
                contract.SetState(state);
            }
        }

        private void SetPresenter(object mediator, IWidgetMediatorBuildContract contract, WidgetDescription description)
        {
            IWidgetPresenter? presenter = BuildPresenter(description);

            if (presenter == null)
            {
                if (mediator is IWidgetPresenterProvider presenterProvider)
                {
                    // TODO: solve this in more elegant way
                    presenter = presenterProvider.Presenter;
                }
                else
                {
                    logger.Warning($"No presenter for a widget of type [{description.Variant?.MediatorType.Name}].");
                    return;
                }
            }

            TryFillPresenterContract(presenter);
            TryInitialise(presenter);
            contract.SetPresenter(presenter);
        }

        private object? GetCustomisation(WidgetDescription description)
        {
            if (description.Customisation != null)
            {
                return description.Customisation;
            }

            return description.Variant?.Customisation;
        }

        private object? BuildState(WidgetDescription description)
        {
            if (description.Variant?.StateType == null)
            {
                return null;
            }

            object? state;
            (bool stateCanBeStored, string stateKey) = WidgetDescription.BuildKey(description);

            if (stateCanBeStored
                && (state = stateStore.Get(stateKey)) != null)
            {
                logger.Trace($"The state with key [{stateKey}] has been restored.");
                return state;
            }

            state = provider.GetRequiredService(description.Variant.StateType);

            if (stateCanBeStored)
            {
                logger.Trace($"The state with key [{stateKey}] has been stored.");
                stateStore.Add(stateKey, state);
            }

            return state;
        }

        private IWidgetPresenter? BuildPresenter(WidgetDescription description)
        {
            if (description.Variant?.PresenterType == null)
            {
                return null;
            }

            return (IWidgetPresenter)provider.GetRequiredService(description.Variant.PresenterType);
        }

        private static void TryInitialise(object subject)
        {
            if (subject is IInitialisable initialisable)
            {
                initialisable.Initialise();
            }
        }
    }
}
