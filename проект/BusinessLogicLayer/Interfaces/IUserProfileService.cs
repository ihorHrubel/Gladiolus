using BusinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserProfileService
    {
        bool UpdateUserProfile(string id, UserProfileDTO userProfileDto);
        UserProfileDTO GetUserProfile(string id);
        IEnumerable<UserProfileDTO> GetUserProfiles();
    }
}
