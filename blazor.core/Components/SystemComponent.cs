using Blazor.Core.Interactions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Blazor.Core.Components
{
    public abstract class SystemComponent<TModel> : SystemComponent
    {
        protected TModel Model { get; private set; }

        public void SetModel(TModel model)
        {
            if (Model != null && !IsModifiedModel(model))
            {
                return;
            }
            
            OnModelChanged();
            StateHasChanged();
        }

        protected virtual bool IsModifiedModel(TModel newModel)
        {
            return true;
        }

        protected virtual void OnModelChanged()
        {
            // no operation ( template method )
        }
    }

    public abstract class SystemComponent : ComponentBase, IComponentBuildContract, IDisposable
    {
        private ICollection<IComponent> children;
        private InteractionPipe interactionPipe;
        
        public void Dispose()
        {
            interactionPipe?.Dispose();
            interactionPipe = null;
            children = null;
        }

        protected IInteractionPipe InteractionPipe => interactionPipe;

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
            if (interactionPipe == null || children?.Count < 1)
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
