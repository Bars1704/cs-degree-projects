using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MicroChatServer.DbContexts;

namespace MicroChatServer.Models
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController
    {
        private readonly ChatContext _chatContext;

        public MessagesController(ChatContext _context)
        {
            _chatContext = _context;
        }

        [HttpPost]
        [Route("CreateChat")]
        public void CreateChat(string ChatId) => _chatContext.AddChat(!String.IsNullOrEmpty(ChatId) ? ChatId : _chatContext.GenerateChatAdress());

        [HttpPost]
        [Route("PostMessage")]
        public void PostMessage(string NickName, string MessageText, string ChatId) => _chatContext.AddMsg(ChatId, new Message(MessageText, NickName));

        [HttpDelete]
        [Route("RemoveChat")]
        public void RemoveChat(string ChatId) => _chatContext.RemoveChat(ChatId);

        [HttpGet]
        [Route("GetChatMessages")]
        public List<Message> GetChatMessages(string ChatId) => _chatContext.GetChat(ChatId).Messages;

        [HttpGet]
        [Route("GetChatList")]
        public List<Chat> GetAllChats() => _chatContext.GetAllChats();

        [HttpGet]
        [Route("GetNewMessages")]
        public List<Message> GetNewMessages(DateTime LastMessageTime, string ChatId)
        {
            var x = _chatContext.GetChat(ChatId);
            return x == null ? new List<Message>() : x.GetNewMessages(LastMessageTime);
        }
    }
}
