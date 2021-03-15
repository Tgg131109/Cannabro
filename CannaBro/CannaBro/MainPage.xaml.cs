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

        public MainPage()
        {
            InitializeComponent();
            GetStrains();

            // Create tab views.
            Xamarin.Forms.NavigationPage homeNavigation = new Xamarin.Forms.NavigationPage(new HomePage());
            homeNavigation.On<iOS>().SetPrefersLargeTitles(true);
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
            //profileNavigation.On<iOS>().SetPrefersLargeTitles(true);
            favoritesNavigation.IconImageSource = "favoritesicon";
            favoritesNavigation.Title = "Favorites";
            favoritesNavigation.BarBackgroundColor = Color.FromHex("#121212");
            favoritesNavigation.BarTextColor = Color.FromHex("85d5bc");

            //Xamarin.Forms.NavigationPage mapNavigation = new Xamarin.Forms.NavigationPage(new MapPage());
            ////mapNavigation.On<iOS>().SetPrefersLargeTitles(true);
            //mapNavigation.IconImageSource = "mapicon";
            //mapNavigation.Title = "Search";
            //mapNavigation.BarBackgroundColor = Color.FromHex("#121212");
            //mapNavigation.BarTextColor = Color.FromHex("85d5bc");

            Xamarin.Forms.NavigationPage profileNavigation = new Xamarin.Forms.NavigationPage(new ProfilePage());
            //profileNavigation.On<iOS>().SetPrefersLargeTitles(true);
            profileNavigation.IconImageSource = "profileicon";
            profileNavigation.Title = "Profile";
            profileNavigation.BarBackgroundColor = Color.FromHex("#121212");
            profileNavigation.BarTextColor = Color.FromHex("85d5bc");


            // Add views to tab bar.
            Children.Add(homeNavigation);
            Children.Add(findNavigation);
            Children.Add(favoritesNavigation);
            Children.Add(profileNavigation);
        }

        private async Task GetStrains()
        {
            await dm.GetStrainsAsync();

            MessagingCenter.Send(dm.resultData, "Results");
        }
    }
}
