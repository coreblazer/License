using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            test();
        }

        public async void test()
        {
            FirebaseClient firebase = new FirebaseClient("https://studhub-4b7ef.firebaseio.com/");
            try
            {
                //              Credentials toUpdatePerson = (await firebase
                //.Child("Users/Bogdan");
                //await firebase
                //            .Child("Users")
                //            .PostAsync(new Credentials() { email = "testttt", parola = "bicis" });
                //string test =  firebase
                //  .Child("Users/Bogdan")..ToString();

           
               string firebaseUsername = "testing@gmail.com";
                string firebasePassword = "acestaeuntest";

                // this can be found via the console

                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDoNdqifOL3PtgY0ABTnXD6bNmPlhg39aE"));
                // this grabs a one-off token using the username and password combination
                var data = await auth.SignInWithEmailAndPasswordAsync(firebaseUsername, firebasePassword);


                // finally log in
                var firebaseClient = new FirebaseClient(
                                        "https://studhub-4b7ef.firebaseio.com/",
                                        new FirebaseOptions
                                        {
                                            AuthTokenAsyncFactory = () => Task.FromResult(data.FirebaseToken)
                                        });
                var test = "succeded";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

    }
}
