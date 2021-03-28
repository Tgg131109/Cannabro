using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class FindPage : ContentPage
    {
        private bool raceFiltered = false;
        private string raceFilter = "";

        public FindPage()
        {
            InitializeComponent();

            searchBar.TextChanged += SearchBar_TextChanged;
            strainList.ItemTapped += StrainList_ItemTapped;
            indicaButton.Clicked += (s, e) => { Filter(indicaButton, "indica"); };
            sativaButton.Clicked += (s, e) => Filter(sativaButton, "sativa");
            hybridButton.Clicked += (s, e) => Filter(hybridButton, "hybrid");
            infoButton.Clicked += InfoButton_Clicked;
           
            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Strains", (sender) =>
            {
                Console.WriteLine("Find page recieved strain data");
                strainList.ItemsSource = sender.ToList();

                countLabel.Text = $"{sender.Count} results";
            });
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Action if no search criteria is entered.
            if (string.IsNullOrWhiteSpace(searchBar.Text))
            {
                // Action if a race filter is set.
                if (raceFiltered == true)
                {
                    // Update list to only show results filtered by the selected race.
                    strainList.ItemsSource = DataManager.strains.Where(s => s.Race.ToLower() == raceFilter).ToList();
                }
                // Action if no race filter is set.
                else
                {
                    // Reset strain list
                    strainList.ItemsSource = DataManager.strains.ToList();
                }
            }
            // Action if search bar has text.
            else
            {
                // Action if a race filter is set.
                if (raceFiltered == true)
                {
                    // Update list to show search bar criteria filtered by selected race.
                    strainList.ItemsSource = DataManager.strains.Where(s => s.Race.ToLower() == raceFilter && s.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();
                }
                // Action if no race filter is set.
                else
                {
                    // Reset strain list
                    strainList.ItemsSource = DataManager.strains.Where(s => s.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();
                }
            }

            // Update count display.
            countLabel.Text = $"{((List<StrainData>)strainList.ItemsSource).Count} results";
        }

        private void InfoButton_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Show race info.");

            NewsData article = new NewsData()
            {
                URL = "https://www.leafly.com/news/cannabis-101/sativa-indica-and-hybrid-differences-between-cannabis-types",
                Source = "Leafly"
            };

            // Navigate to article page.
            _ = Navigation.PushAsync(new ArticlePage());

            MessagingCenter.Send(article, "Article");
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

        private void Filter(Button button, string race)
        {
            // Action if sender is not the current filter.
            if (raceFilter != race)
            {
                button.BackgroundColor = button.BorderColor;
                button.TextColor = Color.FromHex("121212");

                // Check if another race filter is set.
                if (raceFiltered == true)
                {
                    // Action if search bar is empty.
                    if (string.IsNullOrWhiteSpace(searchBar.Text))
                    {
                        // Turn off other race filter.
                        strainList.ItemsSource = DataManager.strains.Where(s => s.Race.ToLower() == race).ToList();
                    }
                    // Action if search bar has text.
                    else
                    {
                        // Turn off other race filter but keep search bar criteria.
                        strainList.ItemsSource = DataManager.strains.Where(s => s.Race.ToLower() == race && s.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();
                    }
                }
                // Action if another race filter is not set.
                else
                {
                    // Action if search bar is empty.
                    if (string.IsNullOrWhiteSpace(searchBar.Text))
                    {
                        // Update strain list.
                        strainList.ItemsSource = ((List<StrainData>)strainList.ItemsSource).Where(s => s.Race.ToLower() == race).ToList();
                    }
                    // Action if search bar has text.
                    else
                    {
                        // Turn off other race filter but keep search bar criteria.
                        strainList.ItemsSource = DataManager.strains.Where(s => s.Race.ToLower() == race && s.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();
                    }
                }

                // Deselect other buttons.
                switch (race)
                {
                    case "indica":
                        if (sativaButton.BackgroundColor == sativaButton.BorderColor)
                        {
                            sativaButton.BackgroundColor = Color.FromHex("121212");
                            sativaButton.TextColor = sativaButton.BorderColor;
                        }

                        if (hybridButton.BackgroundColor == hybridButton.BorderColor)
                        {
                            hybridButton.BackgroundColor = Color.FromHex("121212");
                            hybridButton.TextColor = hybridButton.BorderColor;
                        }
                        break;

                    case "sativa":
                        if (indicaButton.BackgroundColor == indicaButton.BorderColor)
                        {
                            indicaButton.BackgroundColor = Color.FromHex("121212");
                            indicaButton.TextColor = indicaButton.BorderColor;
                        }

                        if (hybridButton.BackgroundColor == hybridButton.BorderColor)
                        {
                            hybridButton.BackgroundColor = Color.FromHex("121212");
                            hybridButton.TextColor = hybridButton.BorderColor;
                        }
                        break;

                    case "hybrid":
                        if (indicaButton.BackgroundColor == indicaButton.BorderColor)
                        {
                            indicaButton.BackgroundColor = Color.FromHex("121212");
                            indicaButton.TextColor = indicaButton.BorderColor;
                        }

                        if (sativaButton.BackgroundColor == sativaButton.BorderColor)
                        {
                            sativaButton.BackgroundColor = Color.FromHex("121212");
                            sativaButton.TextColor = sativaButton.BorderColor;
                        }
                        break;

                    default:
                        break;
                }

                raceFiltered = true;
                raceFilter = race;
            }
            // Action if sender is the current filter.
            else
            {
                button.BackgroundColor = Color.FromHex("121212");
                button.TextColor = button.BorderColor;

                // Action if search bar has text.
                if (!string.IsNullOrWhiteSpace(searchBar.Text))
                {
                    // Remove race filter and return to search bar filter.
                    strainList.ItemsSource = DataManager.strains.Where(s => s.Name.ToLower().Contains(searchBar.Text.ToLower())).ToList();
                }
                // Action if search bar is empty.
                else
                {
                    // Reset strain list
                    strainList.ItemsSource = DataManager.strains.ToList();
                }

                raceFiltered = false;
                raceFilter = "";
            }

            // Update count display.
            countLabel.Text = $"{((List<StrainData>)strainList.ItemsSource).Count} results";
        }

        private void RecentsButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to recently viewed page.
            _ = Navigation.PushAsync(new RecentsPage());
        }
    }
}
