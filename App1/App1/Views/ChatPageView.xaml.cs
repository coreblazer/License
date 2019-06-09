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
    public partial class ChatPageView : ContentPage
    {
        public ChatPageView(string userUUID)
        {
            this.BindingContext = new ChatPageViewModel(userUUID);
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            base.OnBackButtonPressed();
            return false;
        }
    }
}