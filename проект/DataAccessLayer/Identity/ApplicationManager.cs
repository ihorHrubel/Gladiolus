using DataAccessLayer.Entities;
using Microsoft.AspNet.Identity;

namespace DataAccessLayer.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}
