using Blazor.PureMvc.Interactions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Blazor.Core.Components
{
    public class SystemComponent : ComponentBase, IComponentBuildContract, IDisposable
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
            base.OnAfterRender();
            TryFillContractOfChildren();
        }

        void IComponentBuildContract.SetInteractionPipe(InteractionPipe newPipe)
        {
            interactionPipe = newPipe;
        }

        private void TryFillContractOfChildren()
        {
            if (interactionPipe == null)
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
