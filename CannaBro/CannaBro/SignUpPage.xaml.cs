﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CannaBro.Models;
using Xamarin.CommunityToolkit;
using Xamarin.Forms;

namespace CannaBro
{
    public partial class SignUpPage : ContentPage
    {
        CurrentUserData currentUser = new CurrentUserData();

        public SignUpPage()
        {
            InitializeComponent();

            //signUpButton.IsEnabled = true;

            firstnameEntry.Unfocused += (s, e) => { EnableSignUp(); };
            firstnameEntry.TextChanged += (s, e) => { EnableSignUp(); };
            lastnameEntry.Unfocused += (s, e) => { EnableSignUp(); };
            lastnameEntry.TextChanged += (s, e) => { EnableSignUp(); };
            usernameEntry.Unfocused += (s, e) => { EnableSignUp(); };
            //usernameEntry.TextChanged += (s, e) => { EnableSignUp(); };
            usernameEntry.TextChanged += UsernameEntry_TextChanged;
            emailEntry.Unfocused += (s, e) => { EnableSignUp(); };
            //emailEntry.TextChanged += (s, e) => { EnableSignUp(); };
            emailEntry.TextChanged += EmailEntry_TextChanged;
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

        private void UsernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Clear error labels and color if present.
            if (usernameErrorLabel.IsVisible == true)
            {
                usernameEntry.TextColor = Color.FromHex("85d5bc");
                usernameErrorLabel.IsVisible = false;
            }

            EnableSignUp();
        }

        private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Clear error labels and color if present.
            if (emailErrorLabel.IsVisible == true)
            {
                emailEntry.TextColor = Color.FromHex("85d5bc");
                emailErrorLabel.IsVisible = false;
            }

            EnableSignUp();
        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Clear error labels and color if present.
            if (passwordErrorLabel.IsVisible == true || confirmPasswordErrorLabel.IsVisible == true)
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
                && !string.IsNullOrWhiteSpace(emailEntry.Text) && !string.IsNullOrWhiteSpace(usernameEntry.Text)
                && !string.IsNullOrWhiteSpace(passwordEntry.Text) && !string.IsNullOrWhiteSpace(confirmPasswordEntry.Text))
            {
                signUpButton.IsEnabled = true;
            }
            else
            {
                signUpButton.IsEnabled = false;
            }
        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            var files = Directory.EnumerateFiles(App.FolderPath, "*.CannaBroUsers.txt");
            bool success = false;

            // Check if any users exist.
            if (files.Any())
            {
                // Loop through each file to check for email.
                foreach (var file in files)
                {
                    Console.WriteLine("In Loop");

                    // Split each line into a string array.
                    string[] lineData = File.ReadAllText(file).Split(',');

                    // Check if email is valid.
                    if (emailEntryValidator.IsNotValid)
                    {
                        emailEntry.TextColor = Color.IndianRed;
                        emailErrorLabel.Text = "Invalid email address.";
                        emailErrorLabel.IsVisible = true;
                        emailEntry.Focus();
                        signUpButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "The email you entered is not a valid email address. Please try again.", "OK");

                        break;
                    }
                    // Check if email exits.
                    else if (lineData[3].ToString() == emailEntry.Text)
                    {
                        emailEntry.TextColor = Color.IndianRed;
                        emailErrorLabel.Text = "Email is already registered.";
                        emailErrorLabel.IsVisible = true;
                        emailEntry.Focus();
                        signUpButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "The email you entered is associated with an existing account. Please try again.", "OK");

                        break;
                    }
                    // Check if username is not taken.
                    else if (lineData[4].ToString() == usernameEntry.Text)
                    {
                        usernameEntry.TextColor = Color.IndianRed;
                        usernameErrorLabel.IsVisible = true;
                        usernameEntry.Focus();
                        signUpButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "The username you entered is associated with an existing account. Please try again.", "OK");

                        break;
                    }
                    // Check if password is sufficient length.
                    else if (passwordEntry.Text.Length < 6)
                    {
                        passwordErrorLabel.IsVisible = true;
                        passwordEntry.Focus();
                        signUpButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "Your password must be at least 6 characters in length. Please try again.", "OK");

                        break;
                    }
                    // Check if passwords match.
                    else if (passwordEntry.Text != confirmPasswordEntry.Text)
                    {
                        passwordEntry.TextColor = Color.IndianRed;
                        confirmPasswordEntry.TextColor = Color.IndianRed;
                        confirmPasswordErrorLabel.IsVisible = true;
                        passwordEntry.Focus();
                        signUpButton.IsEnabled = false;

                        // Display error.
                        await DisplayAlert("Error", "The passwords you entered do not match. Please try again.", "OK");

                        break;
                    }
                    else
                    {
                        success = true;
                    }
                    //else
                    //{
                    //    // Create and save new user.
                    //    var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CannaBroUsers.txt");
                    //    File.WriteAllText(filename, $"{DateTime.Now},{Guid.NewGuid()},{firstnameEntry.Text},{lastnameEntry.Text},{emailEntry.Text},{usernameEntry.Text},{passwordEntry.Text}");

                    //    // Collapse fields.
                    //    signUpExpander.IsExpanded = false;

                    //    // Hide back button.
                    //    NavigationPage.SetHasBackButton(this, false);

                    //    // Display confirmation.
                    //    goHomeExpander.IsExpanded = true;

                    //    // Display notification.
                    //    await DisplayAlert("Success", "Your CannaBro account has been successfully created.", "OK");

                    //    // Set current user.
                    //    //currentUser.FileName = ;
                    //    currentUser.MemberSince = DateTime.Now;
                    //    currentUser.FirstName = firstnameEntry.Text;
                    //    currentUser.LastName = lastnameEntry.Text;
                    //    currentUser.Username = usernameEntry.Text;
                    //    currentUser.Email = emailEntry.Text;
                    //    currentUser.Password = passwordEntry.Text;
                    //    currentUser.Initials = $"{firstnameEntry.Text.Substring(0, 1)}{lastnameEntry.Text.Substring(0, 1)}";

                    //    break;
                    //}
                }
            }

            // Action if no users exist or all fields have been completed correctly.
            if (!files.Any() || success == true)
            {
                // Assign user a new ID.
                string userID = Guid.NewGuid().ToString();

                // Create and save new user.
                var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.CannaBroUsers.txt");
                File.WriteAllText(filename, $"{DateTime.Now},{userID},{firstnameEntry.Text},{lastnameEntry.Text},{emailEntry.Text},{usernameEntry.Text},{passwordEntry.Text}");

                // Collapse fields.
                signUpExpander.IsExpanded = false;

                // Hide back button.
                NavigationPage.SetHasBackButton(this, false);

                // Display confirmation.
                goHomeExpander.IsExpanded = true;

                // Display notification.
                await DisplayAlert("Success", "Your CannaBro account has been successfully created.", "OK");

                // Set current user.
                currentUser.FileName = filename;
                currentUser.UID = userID;
                currentUser.MemberSince = DateTime.Now;
                currentUser.FirstName = firstnameEntry.Text;
                currentUser.LastName = lastnameEntry.Text;
                currentUser.Username = usernameEntry.Text;
                currentUser.Email = emailEntry.Text;
                currentUser.Password = passwordEntry.Text;
                currentUser.Initials = $"{firstnameEntry.Text.Substring(0, 1)}{lastnameEntry.Text.Substring(0, 1)}";
            }
        }

        private void GoHomeButton_Clicked(object sender, EventArgs e)
        {
            // Navigate to home page.
            Navigation.PushModalAsync(new MainPage());

            // Send user info to profile page.
            MessagingCenter.Send(currentUser, "Current User");
        }
    }
}
