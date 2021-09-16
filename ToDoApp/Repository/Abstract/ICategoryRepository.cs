using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.WebApi.Repository.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        bool ExsistsByName(string name);
        bool ExsistsById(int categoryId);
    }
}
