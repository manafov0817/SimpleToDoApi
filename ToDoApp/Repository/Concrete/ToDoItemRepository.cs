using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Data;
using ToDoApp.Data.Entities;
using ToDoApp.WebApi.Repository.Abstract;

namespace ToDoApp.WebApi.Repository.Concrete
{
    public class ToDoItemRepository : GenericRepository<ToDoItem, ToDoAppDbContext>, IToDoItemRepository
    {
        public bool CategoryIdExsists(int categoryId)
        {
            using (var context = new ToDoAppDbContext())
            {
                return context.Categories.Any(c => c.Id == categoryId);
            }
        }

        public bool ExsistsById(int toDoItemId)
        {
            using (var context = new ToDoAppDbContext())
            {
                return context.ToDoItems.Any(it => it.Id == toDoItemId);
            }
        }

        public bool ExsistsByTitle(string title)
        {
            using (var context = new ToDoAppDbContext())
            {
                return context.ToDoItems.Any(it => it.Title == title);
            }
        }
    }
}
