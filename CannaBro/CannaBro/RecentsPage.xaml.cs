using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class RecentsPage : ContentPage
    {
        public RecentsPage()
        {
            InitializeComponent();

            strainList.ItemTapped += StrainList_ItemTapped;

            if (DataManager.recents.Count > 0)
            {
                strainList.ItemsSource = DataManager.recents.OrderBy(d => d.DateViewed);
                countLabel.Text = $"{DataManager.recents.Count} results";
                noItemsGrid.IsVisible = false;
                strainList.IsVisible = true;
            }

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Recents Set", (sender) =>
            {
                strainList.ItemsSource = null;
                strainList.ItemsSource = sender.OrderBy(d => d.DateViewed);
                countLabel.Text = $"{sender.Count} results";

                if (strainList.IsVisible == false)
                {
                    noItemsGrid.IsVisible = false;
                    strainList.IsVisible = true;
                }
            });
        }

        private void StrainList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as StrainData;

            // Display strain data page.
            Navigation.PushModalAsync(new NavigationPage(new StrainDetailPage())
            {
                BarBackgroundColor = Color.FromHex("121212"),
                BarTextColor = Color.FromHex("85d5bc")
            });

            // Send strain info.
            MessagingCenter.Send(item, "Selected Strain");
        }

        private void EditFavorite(object sender, EventArgs e)
        {
            StrainData.EditFavorite(sender, e);
        }

        //private void OnFavoriteSwipeItemInvoked(object sender, EventArgs e)
        //{
        //    var swipe = ((SwipeItem)sender).BindingContext;
        //    StrainData strain = (StrainData)swipe;

        //    Console.WriteLine(strain.Name);
        //    Console.WriteLine("Favorite swipe triggerred.");

        //    // Add strain favorites file.
        //    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{CurrentUserData.userID}.txt");
        //    File.WriteAllText(filename, $"{DateTime.Now},{strain.Index}");

        //    // Update parameters.
        //    strain.Favorited = true;
        //    strain.FavFilename = filename;

        //    // Add strain to favorites list.
        //    DataManager.favorites.Add(strain);

        //    // Update favorites page.
        //    MessagingCenter.Send(DataManager.favorites, "Favorites Set");
        //}

        //private void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        //{
        //    var swipe = ((SwipeItem)sender).BindingContext;
        //    StrainData strain = (StrainData)swipe;

        //    Console.WriteLine(strain.Name);
        //    Console.WriteLine("Delete swipe triggerred.");

        //    // Delete strain from recents list.
        //    DataManager.recents.Remove(strain);

        //    // Delete strain file.
        //    File.Delete(strain.RecFilename);

        //    // Update recents page.
        //    MessagingCenter.Send(DataManager.recents, "Recents Set");
        //}
    }
}
