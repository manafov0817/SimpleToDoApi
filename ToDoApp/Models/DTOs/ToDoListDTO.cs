using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;

namespace ToDoApp.WebApi.Models.DTOs
{
    public class ToDoListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ToDoItem> ToDoItems { get; set; }

        // Returns true if all items are checked
        public bool IsDone { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ToDoListValidator : AbstractValidator<ToDoListDTO>
    {
        public ToDoListValidator()
        {
            RuleFor(it => it.Title).NotEmpty().Length(0, 50);
            RuleFor(it => it.ToDoItems).NotNull();
            RuleForEach(it => it.ToDoItems).NotNull();
        }
    }
}
