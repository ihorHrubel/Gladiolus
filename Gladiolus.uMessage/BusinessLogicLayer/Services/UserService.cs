using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Infrastructure.UserStore.BLL.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private ProfileUnitOfWork _profileUnitOfWork { get; set; }

        public UserService(ProfileUnitOfWork profileUnitOfWork)
        {
            _profileUnitOfWork = profileUnitOfWork;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await _profileUnitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email , UserName = userDto.Email };
                var result = await _profileUnitOfWork.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                _profileUnitOfWork.ClientManager.Insert(
                    new UserProfile
                    {
                        Id = user.Id,                        
                        CurrentLocation = userDto.Profile.CurrentLocation,
                        IsOnline = userDto.Profile.IsOnline,
                        LastTimeOnline = userDto.Profile.LastTimeOnline,
                        Name = userDto.Profile.Name,
                        Surname = userDto.Profile.Surname,
                        User = user               
                    }               
                );

                await _profileUnitOfWork.SaveAsync();
                return new OperationDetails(true, "Реєсрація успішно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Користувач з такми email вже існує", "Email");
            }
        }
        public async Task<UserDTO> Find(string Email, string Password)
        {
            var user = await _profileUnitOfWork.UserManager.FindAsync(Email, Password);
            if (user == null)
            {
                return null;
            }
            var userDto = new UserDTO
            {
                Email = user.Email,
                Password = Password                
            };
            return userDto;
        }
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            User user = await _profileUnitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await _profileUnitOfWork.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ExternalBearer);
            return claim;
        }
        public void Dispose()
        {
            _profileUnitOfWork.Dispose();
        }

    }
}

