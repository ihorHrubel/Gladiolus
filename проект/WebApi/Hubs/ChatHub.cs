using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using DataAccessLayer.Entities;

namespace WebApi.Hubs
{
    public class ChatHub : Hub
    {
        static List<Userk> Userss = new List<Userk>();
        private IEnumerable<User> Users;
        private IChatHubService _chatHubService;
        public ChatHub()
        {
            _chatHubService = new ChatHubService(
            new UserService(new DataAccessLayer.UnitOfWork.ProfileUnitOfWork()),
            new UserProfileService(new DataAccessLayer.UnitsOfWork.ApplicationUnitOfWork(), new DataAccessLayer.UnitOfWork.ProfileUnitOfWork()),
            new MessageService(new DataAccessLayer.UnitsOfWork.ApplicationUnitOfWork()));
            Users = _chatHubService.UserManager.GetAllUser();
        }

        // Отправка сообщений
        /*public void Send(string conversationId, string message)
        {
            var user = _chatHubService.ProfileManager.GetUserProfiles().Where(p => p.Id.Equals(Context.User.Identity.GetUserId()));
            var messageDto = new MessageDTO()
            {
                Text = message,
                ConversationId = conversationId
            };
            _chatHubService.MessageManager.CreateMessage(Context.User.Identity.GetUserId(), messageDto);
            Clients.All.addMessage(user, message);
        }*/
        public void Send(string name ,string message)
        {
            
            Clients.All.addMessage(name , message);
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Userss.Any(x => x.ConnectionId == id))
            {
                Userss.Add(new Userk { ConnectionId = id, Name = userName });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, Userss);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Userss.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Userss.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
    public class Userk
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
    }
    /*public class ChatHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        public void Send(string message)
        {
            string name = Context.User.Identity.Name;          
            Clients.All.send(message);
        }
        
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            Clients.All.newUser(name);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }      
    }
    
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }*/

}