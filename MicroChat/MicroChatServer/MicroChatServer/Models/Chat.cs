using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroChatServer
{
    public class Chat
    {
        public int Id { get; set; }
        private List<Message> _messages;
        public List<Message> Messages { get{ if (_messages == null) _messages = new List<Message>(); return _messages; } }
        public string ChatAdress { get; set; }
        public Chat(string adr)
        {
            ChatAdress = adr;
        }
        public Chat() { }
        public List<Message> GetNewMessages(DateTime time)
        {
            time = time == DateTime.MinValue ? DateTime.Now : time;
             var result = Messages.Where((x) => x.SendingTime < time).ToList();
            return result;
        }
    }
}
