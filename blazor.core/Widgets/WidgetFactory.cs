﻿using Blazor.PureMvc;
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

        public void RegisterWidget(string variantKey, WidgetVariant variant)
        {
            map[variantKey] = variant;
        }

        public IWidgetMediator Build(string variantKey)
        {
            if (!map.TryGetValue(variantKey, out WidgetVariant variant))
            {
                return null;
            }

            return BuildMediator(variant);
        }

        private IWidgetMediator BuildMediator(WidgetVariant variant)
        {
            Type mediatorType = variant.MediatorType;
            IWidgetMediator mediator = (IWidgetMediator)provider.GetService(mediatorType);

            TryFillContract(mediator, variant);
            TryInitialise(mediator);

            return mediator;
        }

        private void TryFillContract(IWidgetMediator mediator, WidgetVariant variant)
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
            TryInitialise(presenter);
            contract.SetPresenter(presenter);
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
