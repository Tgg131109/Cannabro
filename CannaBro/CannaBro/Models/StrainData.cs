using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.IO;

namespace CannaBro.Models
{
    public class StrainData : INotifyPropertyChanged
    {
        private bool favorited { get; set; }
        //private bool recent { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public string FavFilename { get; set; }
        public string RecFilename { get; set; }
        public int Index { get; set; }
        public DateTime DateViewed { get; set; }
        public DateTime DateSaved { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string RaceInitial { get; set; }
        public Color RaceColor { get; set; }
        public double Rating { get; set; }
        public string Star_1 { get; set; }
        public string Star_2 { get; set; }
        public string Star_3 { get; set; }
        public string Star_4 { get; set; }
        public string Star_5 { get; set; }
        public string Effects { get; set; }
        public string Flavors { get; set; }
        public string Description { get; set; }

        //public bool FavSwipe
        //{
        //    get { return !Favorited; }
        //}

        public bool Favorited
        {
            get { return favorited; }
            set
            {
                favorited = value;
                OnPropertyChanged(nameof(Favorited));
            }
        }

        //public bool Recent
        //{
        //    get { return recent; }
        //    set
        //    {
        //        recent = value;
        //        OnPropertyChanged(nameof(Recent));
        //    }
        //}

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static void EditFavorite(object sender, EventArgs e)
        {
            Console.WriteLine("From StrainData");
            var image = (Image)sender;
            var dataContext = ((Grid)image.Parent).BindingContext;
            StrainData strain = (StrainData)dataContext;

            Console.WriteLine(strain.Name);

            // Action if strain is not favorited.
            if (strain.Favorited == false)
            {
                // Add strain favorites file.
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{CurrentUserData.userID}.txt");
                File.WriteAllText(filename, $"{DateTime.Now},{strain.Index}");

                // Update parameters.
                strain.Favorited = true;
                strain.FavFilename = filename;
                strain.DateSaved = DateTime.Now;

                // Add strain to favorites list.
                DataManager.favorites.Add(strain);
            }
            // Action if strain is favorited.
            else
            {
                // Remove strain favorites file.
                File.Delete(strain.FavFilename);

                // Update favorited parameter.
                strain.Favorited = false;

                // Remove strain from favorites list.
                DataManager.favorites.Remove(strain);
            }

            // Update favorites page.
            MessagingCenter.Send(DataManager.favorites, "Favorites Set");
        }
    }
}
