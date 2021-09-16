using FluentValidation; 

namespace ToDoApp.WebApi.Models.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(it => it.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Length(0, 50)
                .WithMessage("Minimum length must be 1 and maximum length must be 500");
        }
    }
}
