using Android.Graphics;
using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.Models
{
    public class UsersLocal
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserUUID { get; set; }
        public string UserEmail { get; set; }
        public Bitmap UserImage { get; set; }

        public UsersLocal(string firstName, string lastName, string userUUID, string userEmail, Bitmap userImage)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserUUID = userUUID;
            this.UserEmail = userEmail;
            this.UserImage = userImage;

        }

        public UsersLocal()
        {

        }
    }
}
