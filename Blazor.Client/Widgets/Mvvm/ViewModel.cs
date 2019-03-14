using System.Collections.Immutable;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Blazor.Client.Widgets.Mvvm
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string text;
        private int number;
        private bool flag;
        private IImmutableList<string> items;

        public ViewModel()
        {
            text = "Lorem ipsum dolor sit amet";
            number = 42;
            flag = false;
            items = ImmutableList<string>.Empty.AddRange(new[] { "first item", "second item", "third item" });
        }

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public int Number
        {
            get => number;
            set => Set(ref number, value);
        }

        public bool Flag
        {
            get => flag;
            set => Set(ref flag, value);
        }

        public IImmutableList<string> Items
        {
            get => items;
            set => Set(ref items, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<TValue>(ref TValue field, TValue value, [CallerMemberName] string memberName = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
