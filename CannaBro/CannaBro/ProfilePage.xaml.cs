using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using CannaBro.Models;

namespace CannaBro
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            signOutButton.Clicked += SignOutButton_Clicked;

            // Recieve current user information.
            MessagingCenter.Subscribe<CurrentUserData>(this, "Current User", (sender) =>
            {
                Console.WriteLine("User Data Recieved");

                avatarView.Text = sender.Initials;
                userName.Text = $"{sender.FirstName} {sender.LastName}";
                userEmail.Text = sender.Email;
                userDate.Text = $"Member since {sender.MemberSince:MM yyyy}";
            });
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            // Log out and navigate to sign in page.
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
