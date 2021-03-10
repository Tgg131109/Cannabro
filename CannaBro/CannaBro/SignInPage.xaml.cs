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
            if(emailErrorLabel.IsVisible == true || passwordErrorLabel.IsVisible == true)
            {
                emailErrorLabel.IsVisible = false;
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

        private void SignInButton_Clicked(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");

            // Loop through each file to check for email.
            foreach (var file in files)
            {
                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                // Check if username is valid
                if (usernameEntry.Text == lineData[3])
                {
                    // Check if password is valid.
                    if (passwordEntry.Text == lineData[5])
                    {
                        // Navigate to home page.
                        _ = Navigation.PushModalAsync(new MainPage());

                    }
                    else
                    {
                        // Display error.
                        Console.WriteLine("Incorrect Password");

                        passwordEntry.TextColor = Color.IndianRed;
                        passwordErrorLabel.IsVisible = true;
                        passwordEntry.Focus();

                        break;
                    }
                }
                else
                {
                    // Display error.
                    Console.WriteLine("User does not exist.");

                    usernameEntry.TextColor = Color.IndianRed;
                    emailErrorLabel.IsVisible = true;
                    usernameEntry.Focus();

                    break;
                }

                Console.WriteLine(lineData[1].ToString());
                Console.WriteLine(lineData[2].ToString());
                Console.WriteLine(lineData[3].ToString());
                Console.WriteLine(lineData[4].ToString());
                Console.WriteLine(lineData[5].ToString());

            }
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to sign up page.
            Navigation.PushAsync(new SignUpPage());
        }
    }
}
