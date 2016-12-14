using System;
using DataAccessLayer.Context;

namespace DataAccessLayer.UnitOfWork
{
   public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly SocialContext context;
        protected bool disposed;
        public UnitOfWork(SocialContext context)
        {
            this.context = context;
        }
        public UnitOfWork()
        {
            context = new SocialContext();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
