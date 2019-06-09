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
	public partial class RegisterPageView : ContentPage
	{
		public RegisterPageView()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            this.BindingContext = new RegisterPageViewModel();
		}

        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPageView());
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return true;
        }

    }
}