using App1.Models;
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
    public class LoginPageViewModel: BaseViewModel
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
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var data = await Connectors.Client.FireBaseAuthConfig.SignInWithEmailAndPasswordAsync(UserEmail, UserPassword);
                var firebaseClient = new FirebaseClient(
                                        "https://studhub-4b7ef.firebaseio.com/",
                                        new FirebaseOptions
                                        {
                                            AuthTokenAsyncFactory = () => Task.FromResult(data.FirebaseToken)
                                        });

                Connectors.Client.DatabaseClient = firebaseClient;
                Helper.RetainedData.Email = UserEmail;
                Helper.RetainedData.UserUuid = data.User.LocalId;
                await RetainLoggedUser();
                var userrr = Helper.RetainedData.CurrentUser;

                NavigationPage nav = new NavigationPage(new MainPageView());
                Application.Current.MainPage = nav;
                IsBusy = false;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await App.Current.MainPage.DisplayAlert("Alert", "Credentials wrong!", "OK");
                IsBusy = false;
            }
        }

        private async Task RetainLoggedUser()
        {
            //to do add uuid like this: Users/Uuid/username
            var users = await Connectors.Client.DatabaseClient
             .Child("Users")
             .OrderByKey()
             .StartAt(Helper.RetainedData.UserUuid)
             .OnceAsync<UserModel>();
            foreach (var user in users)
            {

                Helper.RetainedData.CurrentUser = user.Object;
            }
        }
        public void Register()
        {
            if (IsBusy) return;
            IsBusy = true;
            NavigationPage newNavigationPage = new NavigationPage(new RegisterPageView());
            Application.Current.MainPage = newNavigationPage;
            IsBusy = false;

        }

    }
}
