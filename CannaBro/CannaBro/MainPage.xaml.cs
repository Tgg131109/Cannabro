using System;
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
            homeNavigation.Title = "Home";
            homeNavigation.BarBackgroundColor = Color.FromHex("#121212");
            //homeNavigation.BarBackgroundColor = Color.FromHex("8CFF98");
            homeNavigation.BarTextColor = Color.FromHex("8CFF98");

            Xamarin.Forms.NavigationPage findNavigation = new Xamarin.Forms.NavigationPage(new FindPage());
            findNavigation.Title = "Find";
            findNavigation.BarBackgroundColor = Color.FromHex("#121212");

            Xamarin.Forms.NavigationPage mapNavigation = new Xamarin.Forms.NavigationPage(new MapPage());
            mapNavigation.Title = "Map";
            mapNavigation.BarBackgroundColor = Color.FromHex("#121212");

            Xamarin.Forms.NavigationPage profileNavigation = new Xamarin.Forms.NavigationPage(new ProfilePage());
            profileNavigation.Title = "Profile";
            profileNavigation.BarBackgroundColor = Color.FromHex("#121212");

            // Add views to tab bar.
            Children.Add(homeNavigation);
            Children.Add(findNavigation);
            Children.Add(mapNavigation);
            Children.Add(profileNavigation);
        }

        private async Task GetStrains()
        {
            await dm.GetStrainsAsync();

            MessagingCenter.Send(dm.resultData, "Results");
        }
    }
}
