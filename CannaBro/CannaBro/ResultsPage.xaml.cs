using System;
using System.Collections.Generic;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();

            strainList.ItemTapped += StrainList_ItemTapped;

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Filters", (sender) =>
            {
                Console.WriteLine("Results page recieved filter data");
                strainList.ItemsSource = sender.OrderBy(x => X).ToList();

                countLabel.Text = $"{sender.Count} results";
            });
        }

        private void StrainList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as StrainData;

            Console.WriteLine(item.Index);
            Console.WriteLine(item.Name);

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
    }
}
