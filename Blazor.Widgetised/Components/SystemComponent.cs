using System;
using System.ComponentModel;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Components
{
    public abstract class SystemComponent<TModel> : SystemComponent
        where TModel : class
    {
        private INotifyPropertyChanged? registeredModel;

        protected TModel? Model { get; private set; }

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
        private IInteractionPipe interactionPipe;

        protected IInteractionPipe InteractionPipe => interactionPipe;

        public SystemComponent()
        {
            interactionPipe = new InteractionPipe();
        }

        public void Dispose()
        {
            OnDispose();
            interactionPipe.Clear();
        }

        protected virtual void OnDispose()
        {
            // no operation ( template method )
        }

        protected void ChildReferenceCaptured(object component)
        {
            if(component is IComponentBuildContract childContract)
            {
                childContract.SetInteractionPipe(new InteractionPipe(interactionPipe));
            }
        }

        void IComponentBuildContract.SetInteractionPipe(IInteractionPipe newPipe)
        {
            interactionPipe = newPipe;
        }
    }
}
