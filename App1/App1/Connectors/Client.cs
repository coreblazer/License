using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Connectors
{
    public class Client
    {
        public static FirebaseClient DatabaseClient { get; set; }
        public static FirebaseAuthProvider FireBaseAuthConfig
        {
            get => new
            FirebaseAuthProvider(new FirebaseConfig("AIzaSyDoNdqifOL3PtgY0ABTnXD6bNmPlhg39aE"));
        }



    }
}
