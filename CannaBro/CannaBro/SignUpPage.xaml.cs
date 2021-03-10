using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.CommunityToolkit;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();

            //signUpButton.IsEnabled = true;

            firstnameEntry.Unfocused += (s, e) => { EnableSignUp();};
            firstnameEntry.TextChanged += (s, e) => { EnableSignUp(); };
            lastnameEntry.Unfocused += (s, e) => { EnableSignUp(); };
            lastnameEntry.TextChanged += (s, e) => { EnableSignUp(); };
            emailEntry.Unfocused += (s, e) => { EnableSignUp(); };
            //emailEntry.TextChanged += (s, e) => { EnableSignUp(); };
            emailEntry.TextChanged += EmailEntry_TextChanged;
            birthdayEntry.Unfocused += (s, e) => { EnableSignUp(); };
            birthdayEntry.TextChanged += (s, e) => { EnableSignUp(); };
            passwordEntry.Unfocused += (s, e) => { EnableSignUp(); };
            //passwordEntry.TextChanged += (s, e) => { EnableSignUp(); };
            passwordEntry.TextChanged += PasswordEntry_TextChanged;
            confirmPasswordEntry.Unfocused += (s, e) => { EnableSignUp(); };
            //confirmPasswordEntry.TextChanged += (s, e) => { EnableSignUp(); };
            confirmPasswordEntry.TextChanged += PasswordEntry_TextChanged;
            signUpButton.Clicked += SignUpButton_Clicked;
            goHomeButton.Clicked += GoHomeButton_Clicked;
        }

        protected override void OnAppearing()
        {
            signUpExpander.IsExpanded = true;
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (emailErrorLabel.IsVisible == true)
            {
                emailEntry.TextColor = Color.FromHex("85d5bc");
                emailErrorLabel.IsVisible = false;
            }

            EnableSignUp();
        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(passwordErrorLabel.IsVisible == true || confirmPasswordErrorLabel.IsVisible == true)
            {
                passwordEntry.TextColor = Color.FromHex("85d5bc");
                confirmPasswordEntry.TextColor = Color.FromHex("85d5bc");

                passwordErrorLabel.IsVisible = false;
                confirmPasswordErrorLabel.IsVisible = false;
            }

            EnableSignUp();
        }

        private void EnableSignUp()
        {
            // Enable sign in button if required fields are complete.

            if (!string.IsNullOrWhiteSpace(firstnameEntry.Text) && !string.IsNullOrWhiteSpace(lastnameEntry.Text)
                && !string.IsNullOrWhiteSpace(emailEntry.Text) && !string.IsNullOrWhiteSpace(birthdayEntry.Text)
                && !string.IsNullOrWhiteSpace(passwordEntry.Text) && !string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
            {
                signUpButton.IsEnabled = true;
            }
            else
            {
                signUpButton.IsEnabled = false;
            }
        }

        private void SignUpButton_Clicked(object sender, EventArgs e)
        {
            // Check if age is valid.

            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");

            // Loop through each file to check for email.
            foreach(var file in files)
            {
                // Split each line into a string array.
                string[] lineData = File.ReadAllText(file).Split(',');

                // Check if email exits.
                if (lineData[3].ToString() == emailEntry.Text)
                {
                    // Display error.
                    Console.WriteLine("Email exists.");

                    emailEntry.TextColor = Color.IndianRed;
                    emailErrorLabel.IsVisible = true;
                    emailEntry.Focus();
                    signUpButton.IsEnabled = false;

                    continue;
                }
                // Check if password is sufficient length.
                else if (passwordEntry.Text.Length < 6)
                {
                    // Display error.
                    Console.WriteLine("Password is too short");

                    passwordErrorLabel.IsVisible = true;
                    passwordEntry.Focus();
                    signUpButton.IsEnabled = false;

                    continue;
                }
                // Check if passwords match.
                else if (passwordEntry.Text != confirmPasswordEntry.Text)
                {
                    // Display error.
                    passwordEntry.TextColor = Color.IndianRed;
                    confirmPasswordEntry.TextColor = Color.IndianRed;
                    Console.WriteLine("Emails don't match.");

                    confirmPasswordErrorLabel.IsVisible = true;
                    passwordEntry.Focus();
                    signUpButton.IsEnabled = false;

                    continue;
                }
                else
                {
                    // Create and save new user.
                    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CannaBroUsers.txt");
                    File.WriteAllText(filename, $"{DateTime.Now},{firstnameEntry.Text},{lastnameEntry.Text},{emailEntry.Text},{birthdayEntry.Text},{passwordEntry.Text}");

                    // Collapse fields.
                    signUpExpander.IsExpanded = false;

                    // Hide back button.
                    NavigationPage.SetHasBackButton(this, false);

                    // Display confirmation.
                    goHomeExpander.IsExpanded = true;

                    //continue;
                }
            }

            //// Check if password is sufficient length.
            //if (passwordEntry.Text.Length < 6)
            //{
            //    // Display error.
            //    Console.WriteLine("Password is too short");

            //    passwordErrorLabel.IsVisible = true;
            //    passwordEntry.Focus();
            //}
            //// Check if passwords match.
            //else if (passwordEntry.Text != confirmPasswordEntry.Text)
            //{
            //    // Display error.
            //    passwordEntry.TextColor = Color.Red;
            //    confirmPasswordEntry.TextColor = Color.Red;
            //    Console.WriteLine("Emails don't match.");

            //    confirmPasswordErrorLabel.IsVisible = true;
            //    passwordEntry.Focus();
            //}
            //else
            //{
            //    // Create and save new user.
            //    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CannaBroUsers.txt");
            //    File.WriteAllText(filename, $"{DateTime.Now},{firstnameEntry.Text},{lastnameEntry.Text},{emailEntry.Text},{birthdayEntry.Text},{passwordEntry.Text}");

            //    // Collapse fields.
            //    signUpExpander.IsExpanded = false;

            //    // Hide back button.
            //    NavigationPage.SetHasBackButton(this, false);

            //    // Display confirmation.
            //    goHomeExpander.IsExpanded = true;
            //}
        }

        private void GoHomeButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to home page.
            Navigation.PushModalAsync(new MainPage());
        }
    }
}
