using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDoApp.Data.Data;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.WebApi.Repository.Concrete
{
    public class CategoryRepository : GenericRepository<Category, ToDoAppDbContext>, ICategoryRepository
    {
        public bool ExsistsById(int categoryId)
        {
            using (var context = new ToDoAppDbContext())
            {
                return context.Categories.Any(c => c.Id == categoryId);
            }
        }

        public bool ExsistsByName(string name)
        {
            using (var context = new ToDoAppDbContext())
            {
                return context.Categories.Any(c => c.Name == name);
            }
        }
    }
}
