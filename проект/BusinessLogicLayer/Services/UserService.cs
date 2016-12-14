using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Infrastructure.UserStore.BLL.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Identity;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

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
            var manager = CreateUserManager(new IdentityFactoryOptions<ApplicationUserManager>(), new OwinContext());
            User user = await manager.FindByEmailAsync(userDto.Email);
            if (user != null)
            {
                return new OperationDetails(false, "Користувач з таким email вже існує", "Email");
            }

            user = new User { Email = userDto.Email, UserName = userDto.Email };
            var result = await manager.CreateAsync(user, userDto.Password);
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
            return new OperationDetails(true, "Реєстрація успішно пройдена", "");
        }
        public IEnumerable<User> GetAllUser()
        {
            var users =  _profileUnitOfWork.UserManager.Users.AsEnumerable();
            return users;
        }
        public async Task<UserDTO> FindByEmailAndPassword(string email, string password)
        {
            var user = await _profileUnitOfWork.UserManager.FindAsync(email, password);
            if (user == null)
            {
                return null;
            }
            var userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = password                
            };
            return userDto;
        }
        public async Task<UserDTO> FindByEmail(string email)
        {
            var user = await _profileUnitOfWork.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email
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
        public async Task<ClaimsIdentity> AuthenticateByEmail(string Email , string authenticationType)
        {
            User user = await _profileUnitOfWork.UserManager.FindByEmailAsync(Email);
            var userIdentity = await _profileUnitOfWork.UserManager.CreateIdentityAsync(user, authenticationType);
            return userIdentity;
        }       
        public async Task<OperationDetails> ChangePassword (string id , string oldPassword , string newPassword)
        {
            var result = await _profileUnitOfWork.UserManager.ChangePasswordAsync(id, oldPassword, newPassword);
            if (result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            else
            {
                return new OperationDetails(true, "Пароль змінено", "");
            }
        }
        public void Dispose()
        {
            _profileUnitOfWork.Dispose();
        }
        public ApplicationUserManager CreateUserManager(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = _profileUnitOfWork.UserManager;
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            
            return manager;
        }
    }
}

