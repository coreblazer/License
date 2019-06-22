using App1.Models;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ChatPageViewModel : BaseViewModel
    {
        private string author { get; set; }
        private string content { get; set; }
        public string TargetUser { get; set; }
        private List<MessageModel> sorted = new List<MessageModel>();
        private List<string> messageUuids = new List<string>();
        public string UserUuid { get; set; }

        public UserModel User { get; set; }
        public ICommand SendMessageCommand { get; set; }

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }


        public ChatPageViewModel(UserModel user)
        {
            User = user;
            TargetUser = User.FirstName + " " + User.LastName;
            base.SubscribeToReceiveMessages();
            SubscribeToReceiveMessages();
            Initialize();
        }

        public async void Initialize()
        {
            SendMessageCommand = new Command(SendMessage);
            await RetrieveMessageHistory();
            SubscribeToReceiveMessageFromCurrentUser();
        }

        public async void SendMessage()
        {
            Author = Helper.RetainedData.CurrentUser.FirstName + " " + Helper.RetainedData.CurrentUser.LastName;
            string messageUuid = Helper.UUIDGenerator.UuidGenerator;
            MessageModel message = new MessageModel(messageUuid, Author, Helper.RetainedData.CurrentUser.UserUUID, Content, User.UserUUID, DateTime.Now);
            MessageList.Add(message);
            await Connectors.Client.DatabaseClient
                    .Child("Conversations")
                    .Child(User.UserUUID)
                    .Child(messageUuid)
                    .PutAsync(message);
            await Connectors.Client.DatabaseClient
        .Child("Conversations")
        .Child(Helper.RetainedData.CurrentUser.UserUUID)
        .Child(messageUuid)
        .PutAsync(message);
            Content = string.Empty;
        }
        protected void SubscribeToReceiveMessageFromCurrentUser()
        {
            var uuid = Helper.RetainedData.CurrentUser.UserUUID;
            var child = Connectors.Client.DatabaseClient.Child("Conversations").Child(Helper.RetainedData.CurrentUser.UserUUID);
            var observable = child.AsObservable<MessageModel>();
            var subscription = observable
                .Where(f => !string.IsNullOrEmpty(f.Key))
                .Subscribe(f =>
                {
                    if (f.Object.AuthorUuid == User.UserUUID)
                    {
                        if (!(messageUuids.Contains(f.Object.MessageUuid)))
                        {
                            //sorted.Add(f.Object);
                            MessageList.Add(f.Object);

                            messageUuids.Add(f.Object.MessageUuid);
                        }
                    }

                });
            sorted = sorted.OrderBy(x => x.TimeStamp).ToList();//necessary for ordering the messages when arriving
            foreach (var item in sorted)
            {

            }
            sorted.Clear();

        }
        public async Task RetrieveMessageHistory()
        {
            //TODO Change columns to something like this: Conversations/CurrentUserUuid/AuthorUuid/messageuuid
            var firebase = Connectors.Client.DatabaseClient;
            var messages = await firebase
             .Child("Conversations")
             .Child(Helper.RetainedData.CurrentUser.UserUUID)
             .OrderByKey()
             .OnceAsync<MessageModel>();
            foreach (var message in messages)
            {
                if (message.Object.TargetUuid == User.UserUUID || message.Object.AuthorUuid == User.UserUUID)
                {
                    sorted.Add(message.Object);

                    messageUuids.Add(message.Object.MessageUuid);
                }

            }
            sorted = sorted.OrderBy(o => o.TimeStamp).ToList();//necessary for ordering the messages when arriving
            foreach (var item in sorted)
            {
                MessageList.Add(item);

            }
            sorted.Clear();

        }


    }
}

