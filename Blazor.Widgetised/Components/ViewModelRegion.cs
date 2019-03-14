using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace Blazor.Widgetised.Components
{
    public class ViewModelRegion : ComponentBase, IDisposable
    {
        private INotifyPropertyChanged? lastViewModel;
        private string? lastFilter;
        private Regex? regexFilter;

        [Parameter]
        private RenderFragment? ChildContent { get; set; }

        [Parameter]
        private INotifyPropertyChanged? ViewModel { get; set; }

        [Parameter]
        private string? Filter { get; set; }

        public void Dispose()
        {
            if (lastViewModel != null)
            {
                lastViewModel.PropertyChanged -= OnPropertyChanged;
            }

            lastViewModel = null;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            UpdateViewModelParameter();
            UpdateFilterParameter();
        }

        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (regexFilter != null
                && !regexFilter.IsMatch(e.PropertyName))
            {
                return;
            }

            StateHasChanged();
        }

        private void UpdateViewModelParameter()
        {
            if (ReferenceEquals(lastViewModel, ViewModel))
            {
                return;
            }

            if (lastViewModel != null)
            {
                lastViewModel.PropertyChanged -= OnPropertyChanged;
            }

            lastViewModel = ViewModel;

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged += OnPropertyChanged;
            }
        }

        private void UpdateFilterParameter()
        {
            if (regexFilter != null
                && string.Equals(lastFilter, Filter))
            {
                return;
            }

            lastFilter = Filter;
            regexFilter = string.IsNullOrEmpty(Filter)
                    ? null
                    : new Regex(Filter);
        }
    }
}
