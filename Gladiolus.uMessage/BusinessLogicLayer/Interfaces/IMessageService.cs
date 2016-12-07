using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IMessageService
    {
        Message CreateMessage(MessageDTO messageDto);
        IEnumerable<MessageDTO> GetMessages(string converstaionId);
        bool DeleteMessage(string id);
    }
}
