using FluentValidation;
using System;
using ToDoApp.Data.Entities.Enums;

namespace ToDoApp.Data.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DoneDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime DueTime { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ToDoListId { get; set; }
        public ItemStatus ItemStatus { get; set; } = ItemStatus.Waiting;
    }
}
