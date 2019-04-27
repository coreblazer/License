﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class MessageModel
    {
        public string MessageUuid { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

        public MessageModel(string messageUuid, string author, string content, DateTime timestamp)
        {
            this.MessageUuid = messageUuid;
            this.Author = author;
            this.Content = content;
            this.TimeStamp = timestamp;
        }
    }
}
