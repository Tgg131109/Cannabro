using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CannaBro.Models;

namespace CannaBro
{
    public partial class App : Application
    {
        public static string FolderPath { get; private set; }

        public App()
        {
            InitializeComponent();

            //ThemeData themeData = new ThemeData();
            //themeData.PrimaryColor = Color.FromHex("8CFF98");

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            MainPage = new NavigationPage(new SignInPage())
            {
                BarBackgroundColor = Color.FromHex("121212"),
                BarTextColor = Color.FromHex("8CFF98")
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
