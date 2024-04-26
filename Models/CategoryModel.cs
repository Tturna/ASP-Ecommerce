using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models;

public class CategoryModel
{
    public int Id { get; set; }
    
    [StringLength(100)]
    public required string Name { get; set; }
    
    public int? ParentId { get; set; }
    public CategoryModel? Parent { get; set; }
    public List<CategoryModel> Children { get; set; } = [];
    public List<ProductModel> Products { get; set; } = [];
}