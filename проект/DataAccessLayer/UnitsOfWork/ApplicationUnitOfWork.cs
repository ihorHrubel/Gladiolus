using System;
using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;

namespace DataAccessLayer.UnitsOfWork
{
  public  class ApplicationUnitOfWork:UnitOfWork.UnitOfWork
    {
        private Dictionary<string, object> repositories;
        public ApplicationManager<T> ApplicationManager<T>() where T :class,IEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(ApplicationManager<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), context);
                repositories.Add(type, repositoryInstance);
            }
            return (ApplicationManager<T>)repositories[type];
        }

    }
}
