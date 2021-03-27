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
<<<<<<< HEAD
        bool hasrecents = false;
        string[] favorites;
        string[] recents;
=======
        string[] favorites;
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

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
<<<<<<< HEAD

            //Xamarin.Forms.NavigationPage mapNavigation = new Xamarin.Forms.NavigationPage(new MapPage());
            //mapNavigation.IconImageSource = "mapicon";
            //mapNavigation.Title = "Map";
            //mapNavigation.BarBackgroundColor = Color.FromHex("#121212");
            //mapNavigation.BarTextColor = Color.FromHex("85d5bc");
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

            Xamarin.Forms.NavigationPage profileNavigation = new Xamarin.Forms.NavigationPage(new ProfilePage());
            profileNavigation.IconImageSource = "profileicon";
            profileNavigation.Title = "Profile";
            profileNavigation.BarBackgroundColor = Color.FromHex("#121212");
            profileNavigation.BarTextColor = Color.FromHex("85d5bc");

            // Add views to tab bar.
            Children.Add(homeNavigation);
            Children.Add(findNavigation);
            Children.Add(favoritesNavigation);
<<<<<<< HEAD
            //Children.Add(mapNavigation);
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            Children.Add(profileNavigation);

            //Recieve current user information.
            MessagingCenter.Subscribe<CurrentUserData>(this, "Favorites", (sender) =>
            {
                System.Console.WriteLine("Favorites Check");
                System.Console.WriteLine(sender.Favorites.Count());

                favorites = sender.Favorites;
                hasfavorites = true;
<<<<<<< HEAD

                MessagingCenter.Unsubscribe<CurrentUserData>(this, "Favorites");
            });

            //Recieve current user information.
            MessagingCenter.Subscribe<CurrentUserData>(this, "Recents", (sender) =>
            {
                System.Console.WriteLine("Recents Check");
                System.Console.WriteLine(sender.Recents.Count());

                recents = sender.Recents;
                hasrecents = true;

                MessagingCenter.Unsubscribe<CurrentUserData>(this, "Recents");
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            });
        }

        private async Task GetStrains()
        {
            System.Console.WriteLine("Get Strains");

            await dm.GetNewsAsync();
            MessagingCenter.Send(dm.articles, "News");

            await dm.GetStrainsAsync();
<<<<<<< HEAD
            MessagingCenter.Send(DataManager.filters, "Filters");
            MessagingCenter.Send(DataManager.strains, "Strains");

            if (hasfavorites == true)
            {
                System.Console.WriteLine("Set favorites.");
                dm.SetFavorites(favorites);
                MessagingCenter.Send(DataManager.favorites, "Favorites Set");
            }

            if (hasrecents == true)
            {
                System.Console.WriteLine("Set recents.");
                dm.SetRecents(recents);
                MessagingCenter.Send(DataManager.recents, "Recents Set");
=======
            MessagingCenter.Send(dm.filters, "Filters");
            MessagingCenter.Send(DataManager.strains, "Strains");

            if (hasfavorites == true)
            {
                System.Console.WriteLine("Set favorites.");
                dm.SetFavorites(favorites);
                MessagingCenter.Send(DataManager.favorites, "Favorites Set");
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
            }
        }
    }
}
