using System;
using System.ComponentModel;
<<<<<<< HEAD
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.IO;
=======
using System.Runtime.CompilerServices;
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

namespace CannaBro.Models
{
    public class StrainData : INotifyPropertyChanged
    {
        private bool favorited { get; set; }
<<<<<<< HEAD
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

=======
        public event PropertyChangedEventHandler PropertyChanged;

        public string Filename { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public double Rating { get; set; }
        public string Effects { get; set; }
        public string Flavors { get; set; }
        public string Description { get; set; }
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
        public bool Favorited
        {
            get { return favorited; }
            set
            {
                favorited = value;
                OnPropertyChanged(nameof(Favorited));
            }
<<<<<<< HEAD
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
=======

        }
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
<<<<<<< HEAD

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
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
    }
}
