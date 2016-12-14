using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using DataAccessLayer.UnitOfWork;
using DataAccessLayer.UnitsOfWork;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserProfileService, UserProfileService>()
                .RegisterType<ApplicationUnitOfWork>()                               
                .RegisterType<ApplicationManager<UserProfile>>()
                .RegisterType<IUserService , UserService>()
                .RegisterType<IConversationService, ConversationService>()
                .RegisterType<ProfileUnitOfWork>()
                .RegisterType<IChatHubService, ChatHubService>()
                .RegisterType<IMessageService,MessageService>
                
            (new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }       
    }
}