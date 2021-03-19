using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CannaBro.Models
{
    public class FilterData : INotifyPropertyChanged
    {
        private bool selected;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Type { get; set; }
        public string Name { get; set; }

        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
