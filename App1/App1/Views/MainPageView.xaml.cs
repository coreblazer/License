using App1.Models;
using App1.ViewModels;
using System;
using System.Collections.Generic;
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
		public MainPageView ()
		{
			InitializeComponent ();
            BindingContext = new MainPageViewModel();           
        }
       protected void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            UserModel user = e.SelectedItem as UserModel;
            NavigationPage nav = new NavigationPage(new ChatPageView(user.UserUUID));
            Application.Current.MainPage = nav;
        }

    }
}