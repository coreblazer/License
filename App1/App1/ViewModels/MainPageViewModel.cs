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
    public class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<UserModel> userlist = new ObservableCollection<UserModel>();


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
        private async void Initialize()
        {
            await RetrieveUsers();
        }
        private async Task RetrieveUsers()
        {
            //to do add uuid like this: Users/Uuid/username
            var firebase = Connectors.Client.DatabaseClient;
            var users = await firebase
             .Child("Users")
             .OrderByKey()
             .OnceAsync<UserModel>();
            foreach (var user in users)
            {
                if (user.Object.UserUUID != Helper.RetainedData.UserUuid)
                {
                    Userlist.Add(user.Object);

                }
            }
        }
    }
}
