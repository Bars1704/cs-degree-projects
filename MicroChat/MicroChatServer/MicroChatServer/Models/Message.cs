using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroChatServer
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SenderUsrName { get; set; }
        public DateTime SendingTime { get; set; }
        public Message() { }
        public Message(string text, string sender)
        {
            Text = text;
            SenderUsrName = sender;
            SendingTime = DateTime.UtcNow;
        }
    }
}
