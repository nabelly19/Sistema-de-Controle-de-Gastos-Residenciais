using ExpenseControl.Domain.Enums;

namespace ExpenseControl.Application.DTOs
{
    public class CreateCategoryDto
    {
        public string Description { get; set; }
        public CategoryPurpose Purpose { get; set; }
    }
}