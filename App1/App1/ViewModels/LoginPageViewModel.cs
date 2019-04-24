using App1.Views;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginPageViewModel
    {
        public ICommand LoginCommand { set; get; }
        public ICommand NavigateToRegisterPageCommand { set; get; }


        private string userEmail = null;
        public string UserEmail
        {
            get
            {
                return userEmail;
            }
            set
            {
                userEmail = value;
            }
        }
        private string userPassword = null;

        public string UserPassword
        {
            get
            {
                return userPassword;
            }
            set
            {
                userPassword = value;
            }
        }


        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);
            NavigateToRegisterPageCommand = new Command(Register);
        }

        public async void Login()
        {
            try
            {
                var data = await Connectors.Client.FireBaseAuthConfig.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword);
                string test = data.User.LocalId;
                var firebaseClient = new FirebaseClient(
                                        "https://studhub-4b7ef.firebaseio.com/",
                                        new FirebaseOptions
                                        {
                                            AuthTokenAsyncFactory = () => Task.FromResult(data.FirebaseToken)
                                        });

                Connectors.Client.DatabaseClient = firebaseClient;
                Helper.RetainedData.Email = test;
                NavigationPage nav = new NavigationPage(new MainPageView());
                Application.Current.MainPage = nav;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await App.Current.MainPage.DisplayAlert("Alert", "Credentials wrong!", "OK");
            }
        }
        public void Register()
        {
            NavigationPage newNavigationPage = new NavigationPage(new RegisterPageView());
            Application.Current.MainPage = newNavigationPage;

        }

    }
}
