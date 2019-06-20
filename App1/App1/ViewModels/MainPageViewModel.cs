using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Graphics;
using Android.Util;
using App1.Helper;
using App1.Models;
using App1.Views;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class MainPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<UserModel> userlist = new ObservableCollection<UserModel>();
        public string UserName
        {
            get; set;
        }

        public string CurrentUserImage
        {
            get; set;
        }

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
        public ICommand LogoutCommand { get; set; }

        public MainPageViewModel()
        {
            Initialize();
            LogoutCommand = new Command(Logout);

            UserName = Helper.RetainedData.CurrentUser.FirstName + " " + Helper.RetainedData.CurrentUser.LastName;
            CurrentUserImage = Helper.RetainedData.CurrentUser.UserImage;
        }

        private void Logout()
        {
            var firebase = Connectors.Client.DatabaseClient;
            firebase.Dispose();
            NavigationPage nav = new NavigationPage(new LoginPageView());
            Application.Current.MainPage = nav;
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
        public Bitmap Base64ToImage(string base64String)
        {
            if (!(string.IsNullOrEmpty(base64String)))
            {
                byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
                return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
                //// Convert base 64 string to byte[]
                //byte[] imageBytes = Convert.FromBase64String(base64String);
                //// Convert byte[] to Image     
                //image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                //return image;
            }
            return null;

        }
    }
}
