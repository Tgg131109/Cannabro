using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            InitializeComponent();

            strainList.ItemTapped += StrainList_ItemTapped;

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Favorites Set", (sender) =>
            {
                strainList.ItemsSource = null;
                strainList.ItemsSource = sender.OrderBy(d => d.DateViewed);
                countLabel.Text = $"{sender.Count} results";

                noItemsGrid.IsVisible = false;
                strainList.IsVisible = true;
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

        private void RecentsButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to recently viewed page.
            _ = Navigation.PushAsync(new RecentsPage());
        }

        private void EditFavorite(object sender, EventArgs e)
        {
            StrainData.EditFavorite(sender, e);
        }
    }
}
