using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using CannaBro.Models;
using System.Linq;

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
                Console.WriteLine(file);
                //File.Delete(file);

                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                Console.WriteLine("--USER INFO----------------");
                Console.WriteLine(lineData[0]);
                Console.WriteLine(lineData[1]);
                Console.WriteLine(lineData[2]);
                Console.WriteLine(lineData[3]);
                Console.WriteLine(lineData[4]);
                Console.WriteLine(lineData[5]);
                Console.WriteLine(lineData[6]);
                Console.WriteLine("");
            }

            var favFiles = Directory.EnumerateFiles(App.FolderPath, "*.UserFavorites.txt");
            foreach (var file in favFiles)
            {
                //File.Delete(file);

                Console.WriteLine(file);
                Console.WriteLine(File.ReadAllText(file));
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
            string input = "";

            // Check if username entry is filled and retrieve password.
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text))
            {

                // Loop through each file to check for user credentials.
                foreach (var file in files)
                {
                    // Split each line into a string array.
                    string[] lineData = File.ReadAllText(file).Split(',');

                    // Check if username is valid
                    if (usernameEntry.Text == lineData[4] || usernameEntry.Text == lineData[5])
                    {
                        // Retrieve password.
                        userPassword = lineData[6];
                        foundUser = true;

                        break;
                    }
                }
            }
            // Ask user for email and attempt to retrieve password.
            else
            {
                input = await DisplayPromptAsync("Email", "Enter the email address that you used to create your account.");

                // Look for email address if input is not empty.
                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Loop through each file to check for user credentials.
                    foreach (var file in files)
                    {
                        // Split each line into a string array.
                        string[] lineData = File.ReadAllText(file).Split(',');

                        // Check if username is valid
                        if (input == lineData[4])
                        {
                            // Retrieve password.
                            userPassword = lineData[6];
                            foundUser = true;

                            break;
                        }
                    }
                }
            }

            // Action if user does not cancel.
            if (!string.IsNullOrWhiteSpace(input))
            {
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
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");
            bool foundUser = false;
            bool hasFavorites = false;
            bool hasRecents = false;

            // Loop through each file to check for user credentials.
            foreach (var file in files)
            {
                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                // Action if username is valid
                if (usernameEntry.Text == lineData[4] || usernameEntry.Text == lineData[5])
                {
                    foundUser = true;

                    // Action if password is valid.
                    if (passwordEntry.Text == lineData[6])
                    {
                        // Clear username and password fields.
                        usernameEntry.Text = null;
                        passwordEntry.Text = null;

                        // Set current user.
                        CurrentUserData currentUser = new CurrentUserData
                        {
                            FileName = file,
                            MemberSince = DateTime.Parse(lineData[0].ToString()),
                            UID = lineData[1],
                            FirstName = lineData[2].ToString(),
                            LastName = lineData[3].ToString(),
                            Username = lineData[4].ToString(),
                            Email = lineData[5].ToString(),
                            Password = lineData[6].ToString(),
                            Initials = $"{lineData[2].ToString().Substring(0, 1)}{lineData[3].ToString().Substring(0, 1)}"
                        };

                        CurrentUserData.userID = currentUser.UID;

                        // Look for user's favorites list.
                        var favFiles = Directory.EnumerateFiles(App.FolderPath,$"*.{currentUser.UID}.txt");

                        if (favFiles.Any())
                        {
                            Console.WriteLine("User has favorites.");

                            List<string> userFav = new List<string>();

                            foreach (var fav in favFiles)
                            {

                                // Split each line into a string array.
                                string[] favData = File.ReadAllText(fav).Split(',');

                                Console.WriteLine(fav);
                                Console.WriteLine(favData[0]);
                                Console.WriteLine(favData[1]);

                                userFav.Add($"{fav},{favData[1]}");
                            }

                            // Set current user object's favorites array.
                            currentUser.Favorites = userFav.ToArray();

                            hasFavorites = true;
                        }

                        // Look for user's recents list.
                        var recentFiles = Directory.EnumerateFiles(App.FolderPath, $"*.{currentUser.UID}_recent.txt");

                        if (recentFiles.Any())
                        {
                            Console.WriteLine("User has recents.");

                            List<string> userRecents = new List<string>();

                            foreach (var recent in recentFiles)
                            {

                                // Split each line into a string array.
                                string[] recentData = File.ReadAllText(recent).Split(',');

                                Console.WriteLine(recent);
                                Console.WriteLine(recentData[0]);
                                Console.WriteLine(recentData[1]);

                                userRecents.Add($"{recent},{recentData[0]},{recentData[1]}");
                            }

                            // Set current user object's favorites array.
                            currentUser.Recents = userRecents.ToArray();

                            hasRecents = true;
                        }

                        foundUser = true;

                        // Navigate to home page.
                        _ = Navigation.PushModalAsync(new MainPage());

                        if (hasFavorites == true)
                        {
                            // Send user info to main page.
                            MessagingCenter.Send(currentUser, "Favorites");
                        }

                        if (hasRecents == true)
                        {
                            // Send user info to main page.
                            MessagingCenter.Send(currentUser, "Recents");
                        }
                        // Send user info to profile page.
                        MessagingCenter.Send(currentUser, "Current User");

                        break;
                    }
                    // Action if password is incorrect.
                    else
                    {
                        Console.WriteLine("Incorrect Password");

                        passwordEntry.TextColor = Color.IndianRed;
                        passwordErrorLabel.IsVisible = true;
                        passwordEntry.Focus();
                        signInButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "The password you entered is incorrect. Please try again.", "OK");

                        break;
                    }
                }
                // Action if username if not valid.
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

            // Action if user is not found.
            if (foundUser == false)
            {
                // Ask user if they would like to sign up.
                bool answer = await DisplayAlert("Error", "There is no account with the provided username or email. Do you wish to sign up?", "Try Again", "Sign Up");

                // Action if user wants to sign up.
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
