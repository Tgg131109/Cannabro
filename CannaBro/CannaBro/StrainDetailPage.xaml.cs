using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class StrainDetailPage : ContentPage
    {
        private int index;

        public StrainDetailPage()
        {
            InitializeComponent();

            closeButton.Clicked += CloseButton_Clicked;
            favoritesButton.Clicked += FavoritesButton_Clicked;

            // Recieve messages to update displayed strain information.
            MessagingCenter.Subscribe<StrainData>(this, "Selected Strain", (sender) =>
            {
                this.Title = sender.Name;
                index = sender.Index;
                descriptionInfo.Text = sender.Description;
                effectInfo.Text = sender.Effects.Replace(",", " | ");
                flavorInfo.Text = sender.Flavors.Replace(",", " | ");

                if (sender.Favorited == true)
                {
                    favoritesButton.Text = "REMOVE FROM FAVORITES";
                }
            });

            Console.WriteLine(DataManager.strains.Count);
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // Dismiss this view.
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void FavoritesButton_Clicked(object sender, EventArgs e)
        {
            // Find strain in strains list.
            var obj = DataManager.strains.FirstOrDefault(x => x.Index == index);

            // Check if strain is found.
            if (obj != null)
            {
                if (favoritesButton.Text == "ADD TO FAVORITES")
                {
                    // Add strain favorites file.
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{CurrentUserData.userID}.txt");
                    File.WriteAllText(filename, $"{DateTime.Now},{index}");
                    
                    // Update parameters.
                    obj.Favorited = true;
                    obj.Filename = filename;

                    // Add strain to favorites list.
                    DataManager.favorites.Add(obj);

                    // Update favorites button.
                    favoritesButton.Text = "REMOVE FROM FAVORITES";

                    // Display error.
                    await DisplayAlert("Added", $"{obj.Name} has been added to your favorites", "OK");

                }
                // Action if removing from favorites.
                else
                {
                    if (File.Exists(obj.Filename))
                    {
                        // Remove strain favorites file.
                        File.Delete(obj.Filename);

                        // Update favorited parameter.
                        obj.Favorited = false;

                        // Remove strain from favorites list.
                        DataManager.favorites.Remove(obj);

                        // Update favorites button.
                        favoritesButton.Text = "ADD TO FAVORITES";

                        // Display error.
                        await DisplayAlert("Removed", $"{obj.Name} has been removed from your favorites", "OK");
                    }
                }

                // Update favorites page.
                MessagingCenter.Send(DataManager.favorites, "Favorites Set");
            }
        }
    }
}
