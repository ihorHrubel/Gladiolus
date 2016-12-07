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
                Gender = updateUserProfileModel.Gender,
                CurrentLocation = "",
                IsOnline = true,
                LastTimeOnline = DateTime.Now
            };
            return userProfileDto;
        }
    }
}