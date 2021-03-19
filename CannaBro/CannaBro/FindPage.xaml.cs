using System;
using System.Collections.Generic;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class FindPage : ContentPage
    {
        public FindPage()
        {
            InitializeComponent();

            searchBar.TextChanged += SearchBar_TextChanged;
            strainList.ItemTapped += StrainList_ItemTapped;

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Strains", (sender) =>
            {

                Console.WriteLine("Find page recieved strain data");
                strainList.ItemsSource = sender.OrderBy(x => X).ToList();

                countLabel.Text = $"{sender.Count} results";
            });
        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Reset list if no search criteria is entered.
            if (string.IsNullOrWhiteSpace(searchBar.Text))
            {
                strainList.ItemsSource = DataManager.strains.OrderBy(x => X).ToList();

                countLabel.Text = $"{DataManager.strains.Count} results";
            }
            else
            {
                //SearchBar search = (SearchBar)sender;
                strainList.ItemsSource = Search(searchBar.Text).OrderBy(x => X).ToList();
            }
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

        private List<StrainData> Search(string criteria)
        {
            List<StrainData> results = new List<StrainData>();

            // Search for the criteria in the strains list.
            foreach(StrainData sd in DataManager.strains)
            {
                if (sd.Name.Contains(criteria))
                {
                    // Add matches to results list.
                    results.Add(sd);
                }
            }

            countLabel.Text = $"{results.Count} results";

            return results;
        }
    }
}
