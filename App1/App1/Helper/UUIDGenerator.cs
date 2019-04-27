using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Helper
{
    public class UUIDGenerator
    {

        public static string UuidGenerator
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < 10; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                    builder.Append(ch);
                }
                return builder.ToString();
            }
        }
    }
}
