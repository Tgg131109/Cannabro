﻿using System;
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

                // Update favorites button text.
                if (sender.Favorited == true)
                {
                    favoritesButton.Text = "REMOVE FROM FAVORITES";
                }

                // Check if strain is already on the recenlty viewed list.
                if (File.Exists(sender.RecFilename))
                {
                    // Overwrite existing strain file with new date.
                    File.WriteAllText(sender.RecFilename, $"{DateTime.Now},{index}");

                    // Update straindata information.
                    sender.DateViewed = DateTime.Now;
                }
                // Action if strain is not on the recently viewed list.
                else
                {
                    // Add strain to recently viewed file.
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{CurrentUserData.userID}_recent.txt");
                    File.WriteAllText(filename, $"{DateTime.Now},{index}");

                    // Update straindata information.
                    sender.RecFilename = filename;
                    sender.DateViewed = DateTime.Now;

                    // Add strain to recently viewed list.
                    DataManager.recents.Add(sender);
                }

                // Update recents page.
                MessagingCenter.Send(DataManager.recents, "Recents Set");
            });
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // Dismiss this view.
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void FavoritesButton_Clicked(object sender, EventArgs e)
        {
            // Find strain in strains list.
            var strain = DataManager.strains.FirstOrDefault(x => x.Index == index);

            // Check if strain is found.
            if (strain != null)
            {
                if (favoritesButton.Text == "ADD TO FAVORITES")
                {
                    // Add strain favorites file.
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.{CurrentUserData.userID}.txt");
                    File.WriteAllText(filename, $"{DateTime.Now},{index}");

                    // Update parameters.
                    strain.Favorited = true;
                    strain.FavFilename = filename;
                    strain.DateSaved = DateTime.Now;

                    // Add strain to favorites list.
                    DataManager.favorites.Add(strain);

                    // Update favorites button.
                    favoritesButton.Text = "REMOVE FROM FAVORITES";

                    //// Display alert.
                    //await DisplayAlert("Added", $"{strain.Name} has been added to your favorites", "OK");
                }
                // Action if removing from favorites.
                else
                {
                    if (File.Exists(strain.FavFilename))
                    {
                        // Remove strain favorites file.
                        File.Delete(strain.FavFilename);

                        // Update favorited parameter.
                        strain.Favorited = false;

                        // Remove strain from favorites list.
                        DataManager.favorites.Remove(strain);

                        // Update favorites button.
                        favoritesButton.Text = "ADD TO FAVORITES";

                        //// Display alert.
                        //await DisplayAlert("Removed", $"{strain.Name} has been removed from your favorites", "OK");
                    }
                }

                // Update favorites page.
                MessagingCenter.Send(DataManager.favorites, "Favorites Set");
            }
        }
    }
}
