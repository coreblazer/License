using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserUUID { get; set; }
        public string UserEmail { get; set; }
        public string UserImage { get; set; }

        public UserModel(string firstName, string lastName, string userUUID, string userEmail, string userImage)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserUUID = userUUID;
            this.UserEmail = userEmail;
            this.UserImage = userImage;

        }

    }
}
