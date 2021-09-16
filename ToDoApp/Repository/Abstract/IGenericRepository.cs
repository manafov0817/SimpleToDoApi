using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.WebApi.Repository.Abstract
{
    public interface IGenericRepository<T>
    {
        ICollection<T> GetAll();
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetById(int id);
    }
}
