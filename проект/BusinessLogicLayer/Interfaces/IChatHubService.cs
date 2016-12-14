using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IChatHubService
    {
        IUserService UserManager { get; }
        IUserProfileService ProfileManager { get; }
        IMessageService MessageManager { get; }
    }
}
