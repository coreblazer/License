using App1.Models;
using App1.ViewModels;
using Firebase.Database.Query;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : ContentPage
    {
        MainPageViewModel viewModel;

        public MainPageView(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            this.viewModel = viewModel;
        }

        protected async void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            UserModel user = e.SelectedItem as UserModel;
            await Navigation.PushModalAsync(new NavigationPage(new ChatPageView(user.UserUUID)));

        }

        private async void OnPhotoTapped(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });
            if (file == null)
                return;
            var stream1 = file.GetStream();
            var bytes = new byte[stream1.Length];
            await stream1.ReadAsync(bytes, 0, (int)stream1.Length);
            string base64 = System.Convert.ToBase64String(bytes);
            Helper.RetainedData.CurrentUser.UserImage = base64;
            await Connectors.Client.DatabaseClient
                  .Child("Users")
                  .Child(Helper.RetainedData.CurrentUser.UserUUID)
                  .PutAsync(Helper.RetainedData.CurrentUser);
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

        }
        //protected override void OnAppearing()
        //{

        //    base.OnAppearing();
        //    ViewCell viewCell = new ViewCell();
        //    UsersList.ItemsSource = viewModel.Userlist;

        //    DataTemplate dataTemplate = new DataTemplate(() =>
        //    {
        //        StackLayout stackLayout = new StackLayout { Margin = 20, Orientation = StackOrientation.Vertical };

        //        foreach (UserModel user in UsersList.ItemsSource)
        //        {
        //            if (!(string.IsNullOrEmpty(user.UserImage)))
        //            {
        //                byte[] Base64Stream = Convert.FromBase64String(user.UserImage);
        //                CircleImage UserImage = new CircleImage { WidthRequest = 55, HeightRequest = 55, Aspect = Aspect.AspectFill, IsVisible = true };
        //                UserImage.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
        //                stackLayout.Children.Add(UserImage);                  
        //            }
        //            Label FirstName = new Label { Text = user.FirstName };
        //            Label LastName = new Label { Text = user.LastName };
        //            stackLayout.Children.Add(FirstName);
        //            stackLayout.Children.Add(LastName);
        //        }
        //        viewCell.View = stackLayout ;
        //        return viewCell;
        //    });
        //    UsersList.ItemTemplate = dataTemplate;

        //}



        protected override void OnDisappearing()
        {

            base.OnDisappearing();

        }

    }
}