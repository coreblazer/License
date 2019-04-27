﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class MessageModel
    {

        public string MessageUuid { get; set; }
        public string AuthorUuid { get; set; }
        public string Author { get; set; }
        public string UserUuid { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        public MessageModel(string messageUuid, string authorUuid, string author,string useruuid, string content, DateTime timestamp)
        {
            this.MessageUuid = messageUuid;
            this.AuthorUuid = authorUuid;
            this.Author = author;
            this.UserUuid = useruuid;
            this.Content = content;
            this.TimeStamp = timestamp;

        }
    }
}
