using System;
using System.ComponentModel;
using Blazor.Widgetised.Interactions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Components
{
    public abstract class SystemComponent<TViewModel> : SystemComponent
        where TViewModel : class
    {
        private INotifyPropertyChanged? registeredModel;

        [Parameter]
        protected TViewModel? ViewModel { get; private set; }

        protected override void OnParametersSet()
        {
            if (ReferenceEquals(registeredModel, ViewModel))
            {
                return;
            }

            base.OnParametersSet();
            UpdateViewModelParameter();
        }

        public void SetViewModel(TViewModel model)
        {
            if (ViewModel != null && !IsModelModified(model))
            {
                return;
            }

            ViewModel = model;
            UpdateViewModelParameter();
            OnViewModelChanged();
            StateHasChanged();
        }

        protected virtual bool IsModelModified(TViewModel newModel)
        {
            return true;
        }

        protected virtual void OnViewModelChanged()
        {
            // no operation ( template method )
        }

        private void UpdateViewModelParameter()
        {
            if (ReferenceEquals(registeredModel, ViewModel))
            {
                return;
            }

            if (ViewModel is INotifyPropertyChanged notifier)
            {
                registeredModel = notifier;
                notifier.PropertyChanged += (o, e) => StateHasChanged();
            }
        }
    }

    public abstract class SystemComponent : ComponentBase, IComponentBuildContract, IDisposable
    {
        private readonly InteractionPipe interactionPipe;

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
            if (component is IComponentBuildContract childContract)
            {
                childContract.SetParentInteractionPipe(interactionPipe);
            }
        }

        void IComponentBuildContract.SetParentInteractionPipe(IInteractionPipe? parentPipe)
        {
            interactionPipe.SetParent(parentPipe);
        }
    }
}
