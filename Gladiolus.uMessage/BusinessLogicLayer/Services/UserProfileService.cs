using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using DataAccessLayer.UnitOfWork;
using DataAccessLayer.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserProfileService : IUserProfileService
    {
        private ApplicationManager<UserProfile> _repositoryUserProfiles;
        private ApplicationUnitOfWork _unitOfWork;
        private ProfileUnitOfWork _profileUnitOfWork { get; set; }

        public UserProfileService(ApplicationUnitOfWork unitOfWork , ProfileUnitOfWork profileUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _profileUnitOfWork = profileUnitOfWork;
            _repositoryUserProfiles = _unitOfWork.ApplicationManager<UserProfile>();
        }
        public bool UpdateUserProfile(string id , UserProfileDTO userProfileDto)
        {
            var userProfile = _repositoryUserProfiles.GetById(id);
            if(userProfile != null)
            {
                userProfile.Name = userProfileDto.Name;
                userProfile.Surname = userProfileDto.Surname;
                userProfile.IsOnline = userProfileDto.IsOnline;
                userProfile.LastTimeOnline = userProfileDto.LastTimeOnline;
                userProfile.BirthDate = userProfileDto.BirthDate;
                userProfile.Gender = userProfileDto.Gender;
                userProfile.CurrentLocation = userProfileDto.CurrentLocation;
                _repositoryUserProfiles.Update(userProfile);
                return true;
            }
            else
            {
                return false;
            }            

        }
        public UserProfileDTO GetUserProfile(string id)
        {
            var userProfile = _repositoryUserProfiles.GetById(id);
            if (userProfile != null)
            {
                Mapper.Initialize(p => p.CreateMap<UserProfile, UserProfileDTO>());
                return Mapper.Map<UserProfile, UserProfileDTO>(userProfile);
            }
            return null;
        }
        public IEnumerable<UserProfileDTO> GetUserProfiles()
        {
            var userProfiles = _repositoryUserProfiles.Table.ToList();
            if(userProfiles.Any())
            {
                Mapper.Initialize(p => p.CreateMap<UserProfile, UserProfileDTO>());
                return Mapper.Map<List<UserProfile>, List<UserProfileDTO>>(userProfiles);
            }
            return null;
        }
    }
}
