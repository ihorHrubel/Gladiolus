using DataAccessLayer.Entities;
using DataAccessLayer.Identity;
using DataAccessLayer.Repository;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
   public class ProfileUnitOfWork : UnitOfWork, IProfileUnitOfWork
    {
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IApplicationManager<UserProfile> clientManager;

        public ProfileUnitOfWork()
        {
            userManager = new ApplicationUserManager(new UserStore<User>(context));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            clientManager = new ApplicationManager<UserProfile>(context);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IApplicationManager<UserProfile> ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
