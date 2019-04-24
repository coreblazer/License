using App1.Helper;
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
    public class RegisterPageViewModel
    {
        public ICommand RegisterCommand { set; get; }

        private string firstName { get; set; }
        private string lastName { get; set; }
        private string email { get; set; }
        private string password { get; set; }

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

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(RegisterNewUser);
        }

        private async void RegisterNewUser()
        {
            try
            {
                var userRegistration = await Connectors.Client.FireBaseAuthConfig.CreateUserWithEmailAndPasswordAsync(Email, Password);
                InsertNewUser();
                NavigationPage nav = new NavigationPage(new LoginPageView());
                Application.Current.MainPage = nav;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await App.Current.MainPage.DisplayAlert("Alert", "Credentials wrong!", "OK");
            }
        }

        private async void InsertNewUser() //for inserting new user informations in the db
        {
            try
            {
                var loginNewlyCreatedUser = await Connectors.Client.FireBaseAuthConfig.SignInWithEmailAndPasswordAsync(Email, Password);
                var firebaseClient = new FirebaseClient(
                                        "https://studhub-4b7ef.firebaseio.com/",
                                        new FirebaseOptions
                                        {
                                            AuthTokenAsyncFactory = () => Task.FromResult(loginNewlyCreatedUser.FirebaseToken)
                                        });
                RegisteredUser userToBeAdded = new RegisteredUser(Firstname, Lastname);
                await firebaseClient
                    .Child("Users")
                    .Child(UserUUIDGenerator())
                    .PutAsync(userToBeAdded);
                var boolean = true;
            }catch(Exception e)
            {
                Debug.Write("got error" + e);
            }

        }

        public string UserUUIDGenerator()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 10; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }



    }
}
