using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Infrastructure.UserStore.BLL.Infrastructure;
using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        IEnumerable<User> GetAllUser();
        Task<UserDTO> FindByEmail(string Email);
        Task<UserDTO> FindByEmailAndPassword(string email, string password);
        Task<ClaimsIdentity> AuthenticateByEmail(string Email, string authenticationType);
        Task<OperationDetails> ChangePassword(string id, string oldPassword, string newPassword);
    }
}
