using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using CannaBro.Models;

namespace CannaBro
{
    public partial class SignInPage : ContentPage
    {
        ThemeData theme = new ThemeData();

        public SignInPage()
        {
            InitializeComponent();

            Console.WriteLine(theme.PrimaryColor);
            Console.WriteLine(signInButton.BindingContext);
            Console.WriteLine(signInButton.BackgroundColor);

            usernameEntry.Unfocused += (s, e) => { EnableSignIn(); };
            usernameEntry.TextChanged += (s, e) => { EnableSignIn(); };
            passwordEntry.Unfocused += (s, e) => { EnableSignIn(); };
            passwordEntry.TextChanged += (s, e) => { EnableSignIn(); };
            signInButton.Clicked += SignInButton_Clicked;
            signUpButton.Clicked += SignUpButton_Clicked;
        }

        private void EnableSignIn()
        {
            if(usernameErrorLabel.IsVisible == true || passwordErrorLabel.IsVisible == true)
            {
                usernameErrorLabel.IsVisible = false;
                passwordErrorLabel.IsVisible = false;

                usernameEntry.TextColor = Color.FromHex("85d5bc");
                passwordEntry.TextColor = Color.FromHex("85d5bc");
            }

            // Enable sign in button if required fields are complete.
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                signInButton.IsEnabled = true;
            }
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");

            // Loop through each file to check for user credentials.
            foreach (var file in files)
            {
                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                // Check if username is valid
                if (usernameEntry.Text == lineData[3] || usernameEntry.Text == lineData[4])
                {
                    // Check if password is valid.
                    if (passwordEntry.Text == lineData[5])
                    {
                        // Navigate to home page.
                        _ = Navigation.PushModalAsync(new MainPage());

                        break;
                    }
                    else
                    {
                        // Display error.
                        Console.WriteLine("Incorrect Password");

                        passwordEntry.TextColor = Color.IndianRed;
                        passwordErrorLabel.IsVisible = true;
                        passwordEntry.Focus();
                        signInButton.IsEnabled = false;

                        await DisplayAlert("Error","The password you entered is incorrect. Please try again.","Word");
                        //break;
                    }
                }
                else
                {
                    // Display error.
                    Console.WriteLine("User does not exist.");

                    usernameEntry.TextColor = Color.IndianRed;
                    usernameErrorLabel.IsVisible = true;
                    usernameEntry.Focus();
                    signInButton.IsEnabled = false;
                }

                Console.WriteLine(lineData[1].ToString());
                Console.WriteLine(lineData[2].ToString());
                Console.WriteLine(lineData[3].ToString());
                Console.WriteLine(lineData[4].ToString());
                Console.WriteLine(lineData[5].ToString());
            }

            if(signInButton.IsEnabled == false)
            {
                bool answer = await DisplayAlert("Error", "There is no account with the provided username or email. Do you wish to sign up?", "Try Again", "Sign Up");

                if (answer == false)
                {
                    // Clear username and password fields.
                    usernameEntry.Text = null;
                    passwordEntry.Text = null;

                    // Navigate to sign up page.
                    _ = Navigation.PushAsync(new SignUpPage());
                }
            }
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to sign up page.
            Navigation.PushAsync(new SignUpPage());
        }
    }
}
