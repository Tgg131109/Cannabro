using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using CannaBro.Models;

namespace CannaBro
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            // Recieve messages to update list data.
            MessagingCenter.Subscribe<List<StrainData>>(this, "Results", (sender) =>
            {
                Console.WriteLine("Home page recieved data");
                resultsList.ItemsSource = sender.ToList();
            });
        }
    }
}
