using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ModelFactory
    {
        public ModelFactory()
        {

        }
        public UserProfileDTO Parse(UpdateUserProfileModel updateUserProfileModel)
        {
            var userProfileDto = new UserProfileDTO()
            {
                Name = updateUserProfileModel.Name,
                Surname = updateUserProfileModel.Surname,
                BirthDate = updateUserProfileModel.BirthDate,
                Gender = updateUserProfileModel.Gender
            };
            return userProfileDto;
        }
        public MessageDTO Parse(MessageViewModel messageViewModel)
        {
            var messageDto = new MessageDTO()
            {               
                Text = messageViewModel.Text,
                ConversationId = messageViewModel.ConversationId           
            };
            return messageDto;
        }
    }
}