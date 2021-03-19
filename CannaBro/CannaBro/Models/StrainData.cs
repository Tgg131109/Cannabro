using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CannaBro.Models
{
    public class StrainData : INotifyPropertyChanged
    {
        private bool favorited { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public double Rating { get; set; }
        public string Effects { get; set; }
        public string Flavors { get; set; }
        public string Description { get; set; }
        public bool Favorited
        {
            get { return favorited; }
            set
            {
                favorited = value;
                OnPropertyChanged(nameof(Favorited));
            }

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
