using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class MessageModel
    {

        public string MessageUuid { get; set; }
        public string Author { get; set; }
        public string AuthorUuid { get; set; }
        public string Content { get; set; }
        public string TargetUuid { get; set; }
        public DateTime TimeStamp { get; set; }
        public MessageModel(string messageUuid, string author,string useruuid, string content, string targetUuid, DateTime timestamp)
        {
            this.MessageUuid = messageUuid;
            this.Author = author;
            this.AuthorUuid = useruuid;
            this.Content = content;
            this.TargetUuid = targetUuid;
            this.TimeStamp = timestamp;


        }
    }
}
