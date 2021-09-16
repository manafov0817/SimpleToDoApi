using FluentValidation;
using System;
using System.Collections.Generic;

namespace ToDoApp.Data.Entities
{
    public class ToDoList 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }

        // Returns true if all items are checked
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
    }
   
}
