using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.CustomRenders;
using App1.Droid;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MessageViewCell), typeof(CustomViewCellRenderer))]
namespace App1.Droid
{
    public class CustomViewCellRenderer
    {
    }
}