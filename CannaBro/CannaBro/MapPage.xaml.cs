using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CannaBro
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            Map map = new Map();
            Content = map;

            map.IsShowingUser = true;
        }
    }
}
