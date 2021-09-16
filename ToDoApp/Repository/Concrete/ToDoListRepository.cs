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
    public class ToDoListRepository : GenericRepository<ToDoList, ToDoAppDbContext>, IToDoListRepository
    {

        public bool ExsistsById(int toDoListId)
        {
            using (var context = new ToDoAppDbContext())
                return context.ToDoLists.Any(l => l.Id == toDoListId);
        }

        public bool ExsistsByTitle(string title)
        {
            using (var context = new ToDoAppDbContext())
                return context.ToDoLists.Any(l => l.Title == title);
        }
        public new ICollection<ToDoList> GetAll()
        {
            using (var context = new ToDoAppDbContext())
                return context.ToDoLists.Include(l => l.ToDoItems).ToList();
        }
        public new ToDoList GetById(int toDoListId)
        {
            using (var context = new ToDoAppDbContext())
                return context.ToDoLists
                                    .Where(l =>l.Id==toDoListId)
                                    .Include(l => l.ToDoItems)
                                    .FirstOrDefault() ;
        }
    }
}
