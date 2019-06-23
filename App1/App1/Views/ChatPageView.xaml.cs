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
    public partial class ChatPageView : ContentPage
    {
        public ChatPageView(UserModel user)
        {
            this.BindingContext = new ChatPageViewModel(user);
            InitializeComponent();
            ToolbarItem toolbarItem = new ToolbarItem
            {
                Order=ToolbarItemOrder.Default,
                Text = user.FirstName+ " " + user.LastName
            };
            this.ToolbarItems.Add(toolbarItem);
            MessagingCenter.Subscribe<ScrollToBottomMessage>(this, "ScrollToBottomMessage", (arg) => {
                MessagesListView.ScrollTo(arg, ScrollToPosition.End, true);
            });
        }

        protected override bool OnBackButtonPressed()
        {
            // If you want to continue going back
            base.OnBackButtonPressed();
            return false;
        }

        private void UnfocusEntry(object sender, EventArgs e)
        {
            messageEntry.Unfocus();
        }
    }
}