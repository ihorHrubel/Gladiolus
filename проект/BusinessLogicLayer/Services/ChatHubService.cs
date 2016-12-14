using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ChatHubService: IChatHubService
    {
        private IUserService _userService;
        private IUserProfileService _userProfileService;
        private IMessageService _messageService;
        public ChatHubService(IUserService userService , IUserProfileService userProfileService ,  IMessageService messageService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _messageService = messageService;
        }
        public IUserService UserManager
        {
            get
            {
                return _userService;
            }
        }
        public IUserProfileService ProfileManager
        {
            get
            {
                return _userProfileService;
            }
        }
        public IMessageService MessageManager
        {
            get
            {
                return _messageService;
            }
        }
    }
}
