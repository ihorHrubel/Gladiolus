namespace DataAccessLayer.Repository
{
   public interface IApplicationManager<T>
    {
         T GetById(object id);
         void Insert(T entity);
         void Update(T entity);
          void Delete(T entity);
    }
}
