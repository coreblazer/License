using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Helper;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace App1.ViewModels
{
    public class MainPageViewModel
    {
        public string UserName = "";

        public MainPageViewModel()
        {
            Initialize();
        }
        private void Initialize()
        {
            RetrieveUserName();
        }
        private async void RetrieveUserName()
        {
            //IFirebaseConfig config = new FirebaseConfig
            //{
            //    FirebaseAuth
            //    BasePath = "https://yourfirebase.firebaseio.com/"
            //};
            var firebase = Connectors.Client.DatabaseClient;
            var email = Helper.RetainedData.Email;
            //var users = await GetUsers()
            //var child = firebase.Child("Users/"+ Helper.RetainedData.Email);
            var dd = 0;
            //var test = await firebase
            //  .Child("Users/" + Helper.RetainedData.Email)
            //  .OrderByKey()
            //  .OnceAsync<User>();
            //string name = users.First().Username;
            //var observable = child.AsObservable<Helper.User>();
            //var sub = observable.Subscribe(f => Debug.WriteLine("eweeeeee"));
            //EventStreamResponse response = await firebase.Child("chat", (sender, args, context) => {
            //System.Console.WriteLine(args.Data);
            //});
            Debug.Write(dd);

            var child = firebase.Child("Users");

            var observable = child.AsObservable<Helper.User>();

            // delete entire conversation list
            await child.DeleteAsync();

            // subscribe to messages comming in, ignoring the ones that are from me
            var subscription = observable
                .Where(f => !string.IsNullOrEmpty(f.Key)) // you get empty Key when there are no data on the server for specified node
                .Subscribe(f => Debug.WriteLine("Received" + f.Key));
            await child.PostAsync(new Helper.User { Username="Helaaaaaau"});


        }

        public async Task<List<Helper.User>> GetUsers()
        {
            var firebase = Connectors.Client.DatabaseClient;

            return (await firebase
              .Child("Users/" + Helper.RetainedData.Email)
              .OnceAsync<Helper.User>()).Select(item => new Helper.User
              {
                  Username = item.Object.Username
              }).ToList();
        }
    }
}
