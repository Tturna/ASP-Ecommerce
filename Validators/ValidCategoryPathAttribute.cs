using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Validators;

public class ValidCategoryPathAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string categoryPath)
        {
            return new ValidationResult("Category path must be a string.");
        }

        if (categoryPath[0] == '/')
        {
            categoryPath = categoryPath[1..];
        }
        
        if (categoryPath.Length == 0)
        {
            return new ValidationResult("Category path cannot be empty.");
        }

        if (categoryPath.Length > 100)
        {
            return new ValidationResult("Category path cannot be longer than 100 characters.");
        }
        
        if (categoryPath.Any(c => !char.IsLetterOrDigit(c) && c != '/'))
        {
            return new ValidationResult("Category path can only contain letters, digits, and slashes.");
        }
        
        if (categoryPath.Count(c => c == '/') > 2)
        {
            return new ValidationResult("Category can only have up to 2 subcategories.");
        }
        
        return ValidationResult.Success;
    }
}