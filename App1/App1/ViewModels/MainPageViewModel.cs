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
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<UserModel> userlist = new ObservableCollection<UserModel>();

        private List<string> messageUuid = new List<string>();

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

        private ObservableCollection<MessageModel> messageList = new ObservableCollection<MessageModel>();



        public ObservableCollection<MessageModel> MessageList
        {
            get
            {
                return messageList;
            }
            set
            {
                messageList = value;
                OnPropertyChanged("MessageList");
            }
        }
        public MainPageViewModel()
        {
            Initialize();
        }
        private async void Initialize()
        {
            await RetrieveUserName();
            SubscribeToReceiveMessages();
        }
        private async Task RetrieveUserName()
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
            var child = firebase.Child("Users");

            //var observable = child.AsObservable<UserModel>();

            // delete entire conversation list

            // subscribe to messages comming in, ignoring the ones that are from me
            //var subscription = observable
            //    .Where(f => !string.IsNullOrEmpty(f.Key)) // you get empty Key when there are no data on the server for specified node
            //    .Subscribe(f => Userlist.Add(f.Object));
        }

        private void SubscribeToReceiveMessages()
        {
            string uuid = Userlist.Where(index => index.UserEmail == RetainedData.Email).FirstOrDefault().UserUUID;
            var child = Connectors.Client.DatabaseClient.Child("Conversations").Child(uuid);

            var observable = child.AsObservable<MessageModel>();
            var subscription = observable
                .Where(f => !string.IsNullOrEmpty(f.Key))
                .Subscribe(f => {
                    if (messageUuid.Contains(f.Object.MessageUuid))
                    {
                        Debug.Write("Already have item");
                        return;
                    }
                    {
                        messageUuid.Add(f.Object.MessageUuid);
                        Debug.Write("You have received a new message " + f.Object.Content);
                    }
                });
        } 

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
