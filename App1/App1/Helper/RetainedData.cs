using App1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Helper
{
    public class RetainedData
    {
        public static string Email { get; set; }
        public static string UserUuid { get; set; }
        public static UserModel CurrentUser { get; set; }
    }
}
