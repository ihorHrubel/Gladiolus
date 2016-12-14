using DataAccessLayer.Entities;
using DataAccessLayer.Identity;
using DataAccessLayer.Repository;

namespace DataAccessLayer.UnitOfWork
{
    public interface IProfileUnitOfWork
    {
        ApplicationUserManager UserManager { get; }
        IApplicationManager<UserProfile> ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
    }
}
