using System;
using System.Collections.Generic;
using CannaBro.Models;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class ArticlePage : ContentPage
    {
        public ArticlePage()
        {
            InitializeComponent();

            // Recieve messages to display selected article.
            MessagingCenter.Subscribe<NewsData>(this, "Article", (sender) =>
            {
                webView.Source = sender.URL;
                this.Title = sender.Source;
            });
        }
    }
}
