using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using CannaBro.Models;

namespace CannaBro
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();

            usernameEntry.Unfocused += (s, e) => { EnableSignIn(); };
            usernameEntry.TextChanged += (s, e) => { EnableSignIn(); };
            passwordEntry.Unfocused += (s, e) => { EnableSignIn(); };
            passwordEntry.TextChanged += (s, e) => { EnableSignIn(); };
            forgotButton.Clicked += ForgotButton_ClickedAsync;
            signInButton.Clicked += SignInButton_Clicked;
            signUpButton.Clicked += SignUpButton_Clicked;

            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");
            foreach (var file in files)
            {
                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                Console.WriteLine("--USER INFO----------------");
                Console.WriteLine(lineData[0]);
                Console.WriteLine(lineData[1]);
                Console.WriteLine(lineData[2]);
                Console.WriteLine(lineData[3]);
                Console.WriteLine(lineData[4]);
                Console.WriteLine(lineData[5]);
                Console.WriteLine("");
            }

        }

        private void EnableSignIn()
        {
            if (usernameErrorLabel.IsVisible == true || passwordErrorLabel.IsVisible == true)
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

        private async void ForgotButton_ClickedAsync(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");
            string userPassword = "";
            bool foundUser = false;

            // Check if username entry is filled and retrieve password.
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text))
            {

                // Loop through each file to check for user credentials.
                foreach (var file in files)
                {
                    // Split each line into a string array.
                    string[] lineData = File.ReadAllText(file).Split(',');

                    // Check if username is valid
                    if (usernameEntry.Text == lineData[4] || usernameEntry.Text == lineData[4])
                    {
                        // Retrieve password.
                        userPassword = lineData[5];
                        foundUser = true;

                        break;
                    }
                }
            }
            // Ask user for email and attempt to retrieve password.
            else
            {
                string input = await DisplayPromptAsync("Email", "Enter the email address that you used to create your account.");

                // Look for email address if input is not empty.
                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Loop through each file to check for user credentials.
                    foreach (var file in files)
                    {
                        // Split each line into a string array.
                        string[] lineData = File.ReadAllText(file).Split(',');

                        // Check if username is valid
                        if (input == lineData[3])
                        {
                            // Retrieve password.
                            userPassword = lineData[5];
                            foundUser = true;

                            break;
                        }
                    }
                }
            }

            if (foundUser == false)
            {
                // Display error.
                await DisplayAlert("Error", "Password could not be retrieved. Double check the username or email used and try again.", "OK");
            }
            else
            {
                // Display hint.
                await DisplayAlert("Hint", $"password: {userPassword}", "OK");
            }
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");
            bool foundUser = false;

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
                        // Clear username and password fields.
                        usernameEntry.Text = null;
                        passwordEntry.Text = null;

                        // Set current user.
                        CurrentUserData currentUser = new CurrentUserData
                        {
                            //currentUser.FileName = ;
                            MemberSince = DateTime.Parse(lineData[0].ToString()),
                            FirstName = lineData[1].ToString(),
                            LastName = lineData[2].ToString(),
                            Username = lineData[3].ToString(),
                            Email = lineData[4].ToString(),
                            Password = lineData[5].ToString(),
                            Initials = $"{lineData[1].ToString().Substring(0, 1)}{lineData[2].ToString().Substring(0, 1)}"
                        };

                        foundUser = true;

                        // Navigate to home page.
                        _ = Navigation.PushModalAsync(new MainPage());

                        // Send user info to profile page.
                        MessagingCenter.Send(currentUser, "Current User");

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

                        await DisplayAlert("Error", "The password you entered is incorrect. Please try again.", "OK");

                        break;
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
            }

            if (foundUser == false)
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
