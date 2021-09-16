using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.WebApi.Repository.Abstract
{
    public interface IToDoItemRepository : IGenericRepository<ToDoItem>
    {
        bool ExsistsByTitle(string title);
        bool CategoryIdExsists(int categoryId);
        bool ExsistsById(int toDoItemId);
    }
}
