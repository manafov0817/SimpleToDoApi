using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.WebApi.Repository.Abstract
{
    public interface IToDoListRepository : IGenericRepository<ToDoList>
    {
        bool ExsistsByTitle(string title);
        bool ExsistsById(int taskId);
        new ICollection<ToDoList> GetAll();
        new ToDoList GetById(int id);
    }
}
