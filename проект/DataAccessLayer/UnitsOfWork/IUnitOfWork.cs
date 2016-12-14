using System;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    interface IUnitOfWork:IDisposable
    {
        void Save();
        void Dispose(bool disposing);
    }
}
