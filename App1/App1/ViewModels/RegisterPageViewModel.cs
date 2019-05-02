﻿using App1.Helper;
using App1.Models;
using App1.Views;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class RegisterPageViewModel: BaseViewModel
    {
        public ICommand RegisterCommand { set; get; }

        private string firstName { get; set; }
        private string lastName { get; set; }
        private string email { get; set; }
        private string password { get; set; }
        private string confirmPassword { get; set; }
        private bool passwordCorrectFormat { get; set; }
        private bool passwordAndConfirmPasswords { get; set; }
        private string backgroundColorForInvalidEntries { get; set; }


        public string Firstname
        {
            get => firstName;
            set => firstName = value;
        }

        public string Lastname
        {
            get => lastName;
            set => lastName = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set => confirmPassword = value;
        }

        public string BackgroundColorForInvalidEntries
        {
            get => backgroundColorForInvalidEntries;
            set
            {
                confirmPassword = value;
                OnPropertyChanged("BackgroundColorForInvalidEntries");
            }
        }

        public bool PasswordCorrectFormat
        {
            get => passwordCorrectFormat;
            set
            {
                passwordCorrectFormat = value;
                OnPropertyChanged("PasswordCorrectFormat");
            }
        }
        public bool PasswordAndConfirmPasswords
        {
            get => passwordAndConfirmPasswords;
            set
            {
                passwordAndConfirmPasswords = value;
                OnPropertyChanged("PasswordAndConfirmPasswords");
            }
        }

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(RegisterNewUser);
        }

        private async void RegisterNewUser()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {

                await Connectors.Client.FireBaseAuthConfig.CreateUserWithEmailAndPasswordAsync(Email, Password);

                InsertNewUser();
                NavigationPage nav = new NavigationPage(new LoginPageView());
                Application.Current.MainPage = nav;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await App.Current.MainPage.DisplayAlert("Alert", "Credentials wrong!", "OK");
                IsBusy = false;
            }
        }

        private async void InsertNewUser() //for inserting new user informations in the db
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var loginNewlyCreatedUser = await Connectors.Client.FireBaseAuthConfig.SignInWithEmailAndPasswordAsync(Email, Password);
                var firebaseClient = new FirebaseClient(
                                        "https://studhub-4b7ef.firebaseio.com/",
                                        new FirebaseOptions
                                        {
                                            AuthTokenAsyncFactory = () => Task.FromResult(loginNewlyCreatedUser.FirebaseToken)
                                        });

                UserModel userToBeAdded = new UserModel(Firstname, Lastname, loginNewlyCreatedUser.User.LocalId, Email);
                MessageModel message = new MessageModel("abv", "abc", Helper.RetainedData.UserUuid, "abc", "abc", DateTime.Now);
                await firebaseClient
                    .Child("Users")
                    .Child(loginNewlyCreatedUser.User.LocalId)
                    .PutAsync(userToBeAdded);
                await firebaseClient
                    .Child("Conversations")
                    .Child(loginNewlyCreatedUser.User.LocalId)
                    .Child(Helper.UUIDGenerator.UuidGenerator)
                    .PutAsync(message);
                IsBusy = false;
            }
            catch (Exception e)
            {
                Debug.Write("got error" + e);
                IsBusy = false;
            }

        }
            
        private void CheckPasswordAndConfirmPasswordValues()
        {
            if (!(Password.Equals(ConfirmPassword)))
            {
                PasswordAndConfirmPasswords = false;

            }
        }
    }
}
