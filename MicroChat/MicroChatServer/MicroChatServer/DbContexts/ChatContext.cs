using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroChatServer.DbContexts
{
    public class ChatContext : DbContext
    {
        private readonly List<char> createChars;
        private readonly Random rand;

        public DbSet<Chat> Chats { get; set; }
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {
            rand = new Random();
            createChars = new List<char>();
            for (char i = 'A'; i <= 'z'; i++)
            {
                if (Char.IsLetter(i))
                    createChars.Add(i);
            }

            Database.EnsureCreated();
        }
        public List<Chat> GetAllChats() => Chats.Include(x => x.Messages).ToList();
        public void AddMsg(string Adress, Message message)
        {
            var currchat = Chats.Include(x => x.Messages).FirstOrDefault((x) => x.ChatAdress == Adress);
            if (currchat == null) return;
            Attach(currchat);
            currchat.Messages.Add(message);
            Update(currchat);
            SaveChanges();
        }
        public void AddChat(string ChatAdress)
        {
            var chat = new Chat(ChatAdress);
            Chats.Add(chat);
            SaveChanges();
        }
        public void AddChat() => AddChat(GenerateChatAdress());
        public Chat GetChat(string ChatAdress)
        {
            return Chats.Include(x => x.Messages).FirstOrDefault((x) => x.ChatAdress == ChatAdress);
        }
        public string GenerateChatAdress(int length = 10)
        {
            char[] newStr = new char[length];
            for (int i = 0; i < length; i++)
            {
                newStr[i] = createChars[rand.Next(0, createChars.Count)];
            }
            var resultAdress = new string(newStr);
            return GetChat(resultAdress) != null ? resultAdress : GenerateChatAdress(length);
        }
        public void RemoveChat(string chatName)
        {
            Chats.Remove(GetChat(chatName));
            SaveChanges();
        }
    }
}
