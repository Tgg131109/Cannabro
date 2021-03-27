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
<<<<<<< HEAD
            recentsButton.Clicked += RecentsButton_Clicked;
=======
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1

            // Recieve current user information.
            MessagingCenter.Subscribe<CurrentUserData>(this, "Current User", (sender) =>
            {
                Console.WriteLine("User Data Recieved");

                avatarView.Text = sender.Initials;
                userName.Text = $"{sender.FirstName} {sender.LastName}";
                userEmail.Text = sender.Email;
                userDate.Text = $"Member since {sender.MemberSince:MMM yyyy}";
<<<<<<< HEAD

                MessagingCenter.Unsubscribe<CurrentUserData>(this, "Current User");
            });
        }

        private void RecentsButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to recently viewed page.
            _ = Navigation.PushAsync(new RecentsPage());
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SignInPage())
            {
                BarBackgroundColor = Color.FromHex("121212"),
                BarTextColor = Color.FromHex("85d5bc")
            };

            foreach (FilterData fd in DataManager.filters)
            {
                if (fd.Selected == true)
                {
                    fd.Selected = false;
                }
            }
=======
            });
        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            // Log out and navigate to sign in page.
            Application.Current.MainPage.Navigation.PopModalAsync();
>>>>>>> 72e45dd82a36ec527d599372af22cba2e47786e1
        }
    }
}
