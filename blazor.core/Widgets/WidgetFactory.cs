using Blazor.PureMvc;
using Blazor.PureMvc.Messaging;
using Blazor.PureMvc.Widgets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Blazor.Core.Widgets
{
    public class WidgetFactory : IWidgetFactory
    {
        private readonly IServiceProvider provider;
        private readonly IDictionary<string, WidgetVariant> map;

        public WidgetFactory(IServiceProvider provider)
        {
            this.provider = provider;
            map = new Dictionary<string, WidgetVariant>();
        }

        public void Register(string variantKey, WidgetVariant variant)
        {
            map[variantKey] = variant;
        }

        public object Build(string variantKey)
        {
            if (!map.TryGetValue(variantKey, out WidgetVariant variant))
            {
                return null;
            }

            return BuildMediator(variant);
        }

        private object BuildMediator(WidgetVariant variant)
        {
            Type mediatorType = variant.MediatorType;
            object mediator = provider.GetService(mediatorType);

            TryFillMediatorContract(mediator, variant);
            TryInitialise(mediator);

            return mediator;
        }

        private void TryFillMediatorContract(object mediator, WidgetVariant variant)
        {
            if (!(mediator is IWidgetBuildContract contract))
            {
                return;
            }

            contract.SetMessageBus(provider.GetService<IMessageBus>());

            if (TryGetState(variant.StateType, out object state))
            {
                contract.SetState(state);
            }

            IWidgetPresenter presenter = (IWidgetPresenter)provider.GetService(variant.PresenterType);
            TryFillPresenterContract(presenter);
            TryInitialise(presenter);
            contract.SetPresenter(presenter);
        }

        private void TryFillPresenterContract(IWidgetPresenter presenter)
        {
            if (!(presenter is IWidgetPresenterBuildContract contract))
            {
                return;
            }

            contract.SetWidgetContainerManagement(provider.GetService<IWidgetContainerManagement>());
        }

        private bool TryGetState(Type stateType, out object state)
        {
            if (stateType == null)
            {
                state = null;
                return false;
            }

            state = provider.GetService(stateType);
            return true;
        }

        private void TryInitialise(object subject)
        {
            if (!(subject is IInitialisable initialisable))
            {
                return;
            }

            initialisable.Initialise();
        }
    }
}
