using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IConversationService
    {
        IEnumerable<ConversationDTO> GetConversations(string id);
        Task<Conversation> Add(string id);
        Task<IEnumerable<User>> Show(string id);
    }
}
