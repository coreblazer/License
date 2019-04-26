using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Helper;
using App1.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace App1.ViewModels
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        //public List<Helper.User> Userlist = new List<Helper.User>();
        private ObservableCollection<UserModel> userlist = new ObservableCollection<UserModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<UserModel> Userlist
        {
            get
            {
                return userlist;
            }
            set
            {
                userlist = value;
                OnPropertyChanged("Userlist");
            }
        }
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
            //to do add uuid like this: Users/Uuid/username
            var firebase = Connectors.Client.DatabaseClient;
            var users = await firebase
             .Child("Users")
             .OrderByKey()
             .OnceAsync<UserModel>();
            foreach (var user in users)
            {
                Userlist.Add(user.Object);
            }
            //var child = firebase.Child("Users");

            //var observable = child.AsObservable<UserModel>();

            // delete entire conversation list

            // subscribe to messages comming in, ignoring the ones that are from me
            //var subscription = observable
            //    .Where(f => !string.IsNullOrEmpty(f.Key)) // you get empty Key when there are no data on the server for specified node
            //    .Subscribe(f => Userlist.Add(f.Object));


        }

        //public async Task<List<Helper.User>> GetUsers()
        //{
        //    var firebase = Connectors.Client.DatabaseClient;

        //    return (await firebase
        //      .Child("Users/" + Helper.RetainedData.Email)
        //      .OnceAsync<Helper.User>()).Select(item => new Helper.User
        //      {
        //          Username = item.Object.Username
        //      }).ToList();
        //}

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
