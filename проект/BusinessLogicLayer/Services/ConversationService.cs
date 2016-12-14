using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using DataAccessLayer.UnitsOfWork;
using AutoMapper;
using BusinessLogicLayer.Services;
using DataAccessLayer.UnitOfWork;
using System.Data.Entity;

namespace BusinessLogicLayer.Services
{
    public class ConversationService : IConversationService
    {
        private ApplicationManager<Conversation> _repositoryConversations;
        private ApplicationUnitOfWork _applicationUnitOfWork;
        private ProfileUnitOfWork _profileUnitOfWork;
        public ConversationService(ApplicationUnitOfWork applicationUnitOfWork , ProfileUnitOfWork profileUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _repositoryConversations = _applicationUnitOfWork.ApplicationManager<Conversation>();
            _profileUnitOfWork = profileUnitOfWork;
        }
        public IEnumerable<ConversationDTO> GetConversations(string id)
        {
            var conversations = _repositoryConversations.Table.Where(u => u.Users.Where(p => p.Id.Equals(id)).Count() > 0).ToList();
            if (conversations.Any())
            {
                Mapper.Initialize(m => m.CreateMap<Conversation, ConversationDTO>());
                return Mapper.Map<List<Conversation>, List<ConversationDTO>>(conversations);
            }
            return null;
        }
        public async Task<Conversation> Add(string id)
        {
            var user = await _profileUnitOfWork.UserManager.FindByIdAsync(id);
            var conversatopn = _repositoryConversations.GetById("fdg");
           
            if (user != null && conversatopn != null)
            {
                user.Conversations.Add(conversatopn);
                //conversatopn.Users.Add(user);
                //_applicationUnitOfWork.Save();
                await _profileUnitOfWork.SaveAsync();
                return conversatopn;
            }
            else
            {
                return null;
            }
        }    
        public async Task<IEnumerable<User>> Show(string id)
        {
            var con = _repositoryConversations.GetById("1");
            var user = await _profileUnitOfWork.UserManager.FindByIdAsync(id);
            return con.Users;
        }
    }
}
