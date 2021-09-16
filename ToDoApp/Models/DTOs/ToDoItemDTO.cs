using System; 
using FluentValidation;

namespace ToDoApp.WebApi.Models.DTOs
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime DueTime { get; set; }
        public int CategoryId { get; set; }
        public int ToDoListId { get; set; }
    }
    public class ToDoItemValidator : AbstractValidator<ToDoItemDTO>
    {
        public ToDoItemValidator()
        {
            RuleFor(it => it.Title).NotEmpty()
                                   .WithMessage("Title cannot be empty")
                                   .Length(0, 50)
                                   .WithMessage("Minimum length must be 1 and maximum length must be 500");

            RuleFor(it => it.Content).NotEmpty()
                                     .WithMessage("Content cannot be null")
                                     .Length(0, 500)
                                     .WithMessage("Minimum length must be 1 and maximum length must be 500");

            RuleFor(it => it.CategoryId).NotNull()
                                        .WithMessage("CategoryId cannot be null");
        }
    }
}
