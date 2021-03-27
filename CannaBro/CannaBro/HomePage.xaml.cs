using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using CannaBro.Models;
using System.ComponentModel;
using Xamarin.CommunityToolkit.UI.Views;

namespace CannaBro
{
    public partial class HomePage : ContentPage
    {
        private List<string> filters = new List<string>();
        private List<string> effects = new List<string>();
        private List<string> flavors = new List<string>();
        private List<StrainData> strains = new List<StrainData>();

        public HomePage()
        {
            InitializeComponent();

            effectExpander.Tapped += (s, e) => { RotateIcon(effectExpander, effectExpandIcon); };
            flavorExpander.Tapped += (s, e) => { RotateIcon(flavorExpander, flavorExpandIcon); };
            effectsList.ItemTapped += FilterList_ItemTapped;
            flavorsList.ItemTapped += FilterList_ItemTapped;
            findButton.Clicked += FindButton_Clicked;

            newsCarousel.IndicatorView = indicatorView;

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<FilterData>>(this, "Filters", (sender) =>
<<<<<<< HEAD
            {
                Console.WriteLine("Home page recieved filter data");
                effectsList.ItemsSource = sender.Where(i => i.Type == "Effect").OrderBy(x => X).ToList();
                flavorsList.ItemsSource = sender.Where(i => i.Type == "Flavor").OrderBy(x => X).ToList();

                MessagingCenter.Unsubscribe<FilterData>(this, "Filters");
=======
            {              
                Console.WriteLine("Home page recieved filter data");
                effectsList.ItemsSource = sender.Where(i => i.Type == "Effect").OrderBy(x => X).ToList();
                flavorsList.ItemsSource = sender.Where(i => i.Type == "Flavor").OrderBy(x => X).ToList();
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            });

            MessagingCenter.Subscribe<List<NewsData>>(this, "News", (sender) =>
            {
                Console.WriteLine("Home page recieved news data");
                newsCarousel.ItemsSource = sender.ToList();
<<<<<<< HEAD

                MessagingCenter.Unsubscribe<NewsData>(this, "News");
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            });

            MessagingCenter.Subscribe<List<StrainData>>(this, "Strains", (sender) =>
            {
<<<<<<< HEAD
                Console.WriteLine("Home page recieved strain data");
                strains = sender.OrderBy(x => X).ToList();

                MessagingCenter.Unsubscribe<StrainData>(this, "Strains");
=======

                Console.WriteLine("Home page recieved strain data");
                strains = sender.OrderBy(x => X).ToList();
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            });
        }

        private void FilterList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as FilterData;

            // Select or deselect filter.
            item.Selected = !item.Selected;
        }

        private void Toggle_OnToggled(object sender, ItemTappedEventArgs e)
        {
<<<<<<< HEAD
            Console.WriteLine("Toggled");
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            var toggle = (Switch)sender;
            var dataContext = ((Grid)toggle.Parent).BindingContext;
            FilterData dataItem = (FilterData)dataContext;

            // Add selected filter to list.
            if (dataItem.Selected == true)
            {
                filters.Add(dataItem.Name.ToString());

                if (dataItem.Type == "Effect")
                {
                    effects.Add(dataItem.Name.ToString());
                }
                else
                {
                    flavors.Add(dataItem.Name.ToString());
                }
            }
            else
            {
                filters.Remove(dataItem.Name.ToString());

                if (dataItem.Type == "Effect")
                {
                    effects.Remove(dataItem.Name.ToString());
                }
                else
                {
                    flavors.Remove(dataItem.Name.ToString());
                }
            }

            if (dataItem.Type == "Effect")
            {
                effectCount.Text = $"{effects.Count()}";

                // Update count label visibility.
                if (effects.Count > 0)
                {
                    effectFrame.IsVisible = true;
                }
                else
                {
                    effectFrame.IsVisible = false;
                }
            }
            else
            {
                flavorCount.Text = $"{flavors.Count()}";

                // Update count label visibility.
                if (flavors.Count > 0)
                {
                    flavorFrame.IsVisible = true;
                }
                else
                {
                    flavorFrame.IsVisible = false;
                }
            }

            // Enable find button if filters are set.
            if (filters.Count > 0)
            {
                findButton.IsEnabled = true;
            }
            else
            {
                findButton.IsEnabled = false;
            }

            Console.WriteLine(filters.Count);
            Console.WriteLine(dataItem.Name);
            Console.WriteLine(dataItem.Selected);
        }

        private async void FindButton_Clicked(object sender, EventArgs e)
        {
            DataManager dm = new DataManager();

            string filterString = String.Join(",", filters);
            await dm.FilterStrains(filterString, strains);

            // Navigate to results page.
            _ = Navigation.PushAsync(new ResultsPage());

            MessagingCenter.Send(dm.filteredList, "Filters");

            effectExpander.IsExpanded = false;
            flavorExpander.IsExpanded = false;
        }

<<<<<<< HEAD
        private void RecentsButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to recently viewed page.
            _ = Navigation.PushAsync(new RecentsPage());
        }

=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
        private void RotateIcon(Expander expander, Image image)
        {
            if (expander.State == ExpandState.Expanding)
            {
                image.RotateXTo(180);
            }
            else
            {
                image.RotateXTo(0);
            }
<<<<<<< HEAD
        }

        private void ViewArticle(object sender, EventArgs e)
        {
            var articleData = ((Frame)sender).BindingContext;
            NewsData article = (NewsData)articleData;

            Console.WriteLine("Carousel Tapped");

            // Navigate to article page.
            _ = Navigation.PushAsync(new ArticlePage());

            MessagingCenter.Send(article, "Article");
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
        }
    }
}
