using System;
using System.Collections.Generic;
using System.ComponentModel;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

using IComponent = Microsoft.AspNetCore.Components.IComponent;

namespace Blazor.Widgetised.Components
{
    public abstract class SystemComponent<TModel> : SystemComponent
    {
        private INotifyPropertyChanged registeredModel;

        protected TModel Model { get; private set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            UpdateModelParameter();
        }

        public void SetModel(TModel model)
        {
            if (Model != null && !IsModelModified(model))
            {
                return;
            }

            Model = model;
            UpdateModelParameter();
            OnModelChanged();
            StateHasChanged();
        }

        protected virtual bool IsModelModified(TModel newModel)
        {
            return true;
        }

        protected virtual void OnModelChanged()
        {
            // no operation ( template method )
        }

        private void UpdateModelParameter()
        {
            if (ReferenceEquals(registeredModel, Model))
            {
                return;
            }

            if (Model is INotifyPropertyChanged notifier)
            {
                registeredModel = notifier;
                notifier.PropertyChanged += (o, e) => StateHasChanged();
            }
        }
    }

    public abstract class SystemComponent : ComponentBase, IComponentBuildContract, IDisposable
    {
        private ICollection<IComponent> children;
        private InteractionPipe interactionPipe;

        protected IInteractionPipe InteractionPipe => interactionPipe;

        public void Dispose()
        {
            OnDispose();

            interactionPipe?.Dispose();
            interactionPipe = null;
            children = null;
        }

        protected virtual void OnDispose()
        {
            // no operation ( template method )
        }

        protected void RegisterChild(IComponent component)
        {
            if (children == null)
            {
                children = new List<IComponent>();
            }

            children.Add(component);
        }

        protected override void OnAfterRender()
        {
            TryFillBuildContractOfChildren();
        }

        void IComponentBuildContract.SetInteractionPipe(InteractionPipe newPipe)
        {
            interactionPipe = newPipe;
        }

        private void TryFillBuildContractOfChildren()
        {
            if (interactionPipe == null || children == null)
            {
                return;
            }

            foreach (IComponent child in children)
            {
                if (child is IComponentBuildContract contract)
                {
                    contract.SetInteractionPipe(new InteractionPipe(interactionPipe));
                }
            }
        }
    }
}
