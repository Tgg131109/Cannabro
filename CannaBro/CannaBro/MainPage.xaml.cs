using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using CannaBro.Models;

namespace CannaBro
{
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        DataManager dm = new DataManager();
        bool hasfavorites = false;
        string[] favorites;

        public MainPage()
        {
            InitializeComponent();
            _ = GetStrains();

            // Create tab views.
            Xamarin.Forms.NavigationPage homeNavigation = new Xamarin.Forms.NavigationPage(new HomePage());
            //homeNavigation.On<iOS>().SetPrefersLargeTitles(true);
            homeNavigation.IconImageSource = "homeicon";
            homeNavigation.Title = "Home";
            homeNavigation.BarBackgroundColor = Color.FromHex("#121212");
            homeNavigation.BarTextColor = Color.FromHex("85d5bc");

            Xamarin.Forms.NavigationPage findNavigation = new Xamarin.Forms.NavigationPage(new FindPage());
            findNavigation.IconImageSource = "findicon";
            findNavigation.Title = "Find";
            findNavigation.BarBackgroundColor = Color.FromHex("#121212");
            findNavigation.BarTextColor = Color.FromHex("85d5bc");

            Xamarin.Forms.NavigationPage favoritesNavigation = new Xamarin.Forms.NavigationPage(new FavoritesPage());
            favoritesNavigation.IconImageSource = "favoritesicon";
            favoritesNavigation.Title = "Favorites";
            favoritesNavigation.BarBackgroundColor = Color.FromHex("#121212");
            favoritesNavigation.BarTextColor = Color.FromHex("85d5bc");

            Xamarin.Forms.NavigationPage profileNavigation = new Xamarin.Forms.NavigationPage(new ProfilePage());
            profileNavigation.IconImageSource = "profileicon";
            profileNavigation.Title = "Profile";
            profileNavigation.BarBackgroundColor = Color.FromHex("#121212");
            profileNavigation.BarTextColor = Color.FromHex("85d5bc");

            // Add views to tab bar.
            Children.Add(homeNavigation);
            Children.Add(findNavigation);
            Children.Add(favoritesNavigation);
            Children.Add(profileNavigation);

            //Recieve current user information.
            MessagingCenter.Subscribe<CurrentUserData>(this, "Favorites", (sender) =>
            {
                System.Console.WriteLine("Favorites Check");
                System.Console.WriteLine(sender.Favorites.Count());

                favorites = sender.Favorites;
                hasfavorites = true;
            });
        }

        private async Task GetStrains()
        {
            System.Console.WriteLine("Get Strains");

            await dm.GetNewsAsync();
            MessagingCenter.Send(dm.articles, "News");

            await dm.GetStrainsAsync();
            MessagingCenter.Send(dm.filters, "Filters");
            MessagingCenter.Send(DataManager.strains, "Strains");

            if (hasfavorites == true)
            {
                System.Console.WriteLine("Set favorites.");
                dm.SetFavorites(favorites);
                MessagingCenter.Send(DataManager.favorites, "Favorites Set");
            }
        }
    }
}
