using App1.Helper;
using App1.Interfaces;
using App1.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace App1.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isbusy { get; set; }
        public bool IsBusy
        {
            get => isbusy;
            set
            {
                isbusy = value; OnPropertyChanged("IsBusy");
            }
        }
        private ObservableCollection<MessageModel> messageList = new ObservableCollection<MessageModel>();

        private List<string> messageUuid = new List<string>();

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

        protected void SubscribeToReceiveMessages()
        {
            var child = Connectors.Client.DatabaseClient.Child("Conversations").Child(Helper.RetainedData.CurrentUser.UserUUID);
            var observable = child.AsObservable<MessageModel>();
            var subscription = observable
                .Where(f => !string.IsNullOrEmpty(f.Key))
                .Subscribe(f =>
                {
                    if (messageUuid.Contains(f.Object.MessageUuid))
                    {
                        Debug.Write("Already have item");
                        return;
                    }
                    {
                        Debug.Write("You have received a new message.");
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
