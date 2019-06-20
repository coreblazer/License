using App1.Models;
using App1.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
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