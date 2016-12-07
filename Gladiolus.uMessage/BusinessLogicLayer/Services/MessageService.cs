using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using DataAccessLayer.UnitsOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class MessageService : IMessageService
    {
        private ApplicationManager<Message> _repositoryMessages;
        private ApplicationUnitOfWork _applicationUnitOfWork;
        public MessageService(ApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _repositoryMessages = _applicationUnitOfWork.ApplicationManager<Message>();
        }
        public Message CreateMessage(MessageDTO messageDto)
        {
            var message = new Message
            {
                Id = messageDto.Id,
                SendDateTime = messageDto.SendDateTime,
                Text = messageDto.Text,
                ConversationId = messageDto.ConversationId,
                UserId = messageDto.UserId
            };
            _repositoryMessages.Insert(message);
            return message;
        }
        public IEnumerable<MessageDTO> GetMessages(string converstaionId)
        {
            var messages = _repositoryMessages.Table.Where(m => m.ConversationId.Equals(converstaionId)).ToList();
            if (messages.Any())
            {
                Mapper.Initialize(m => m.CreateMap<Message, MessageDTO>());
                return Mapper.Map<List<Message>, List<MessageDTO>>(messages);
            }
            return null;
        }
        public bool DeleteMessage(string id)
        {
            var message = _repositoryMessages.GetById(id);
            if(message != null)
            {
                _repositoryMessages.Delete(message);
                return true;
            }
            return false;
        }
    }
}
