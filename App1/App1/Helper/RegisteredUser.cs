using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Helper
{
    public class RegisteredUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserUUID { get; set; }

        public RegisteredUser(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

    }
}
