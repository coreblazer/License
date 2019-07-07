using App1.ViewModels;
using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPageView : ContentPage
    {
        public LoginPageView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var viewModel = new LoginPageViewModel();

            this.BindingContext = viewModel;
            InitializeComponent();
            loginButton.Clicked += (sender, e) => animationView.PlayProgressSegment(0.65f, 0.0f);
            loginButton.Clicked += (sender, e) => animationView.PlayFrameSegment(100, 1);
        }
        private async void RegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPageView()));
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAsync();
            return true;
        }

        private void UnfocusEntry(object sender, EventArgs e)
        {
            Email.Unfocus();
            Password.Unfocus();
        }

        private void OnLoginButtonPresset(object sender, EventArgs e)
        {
            
        }
    }
        
}