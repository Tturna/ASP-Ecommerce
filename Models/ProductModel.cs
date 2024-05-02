using System.ComponentModel.DataAnnotations;
using ASP_Ecommerce.Validators;

namespace ASP_Ecommerce.Models;

public class ProductModel
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string ShortDescription { get; set; } = string.Empty;

    [StringLength(500)]
    public string? LongDescription { get; set; }
    
    [StringLength(500)]
    public string? TechDescription { get; set; }
    
    [StringLength(500)]
    public string? WarrantyDescription { get; set; }
    
    [Required]
    [Range(0, 1_000_000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Range(0, 1_000_000)]
    [DataType(DataType.Currency)]
    public decimal? OldPrice { get; set; } = null;
    
    [Required, StringLength(200)]
    public string ImageUrl { get; set; } = string.Empty;

    [Required, StringLength(100), ValidCategoryPath]
    public string CategoryPath { get; set; } = string.Empty;
    
    public int? CategoryId { get; set; }
    public CategoryModel? Category { get; set; }

    public int? MaintainerId { get; set; }
    public UserModel? Maintainer { get; set; }

    public List<OrderItemModel> OrderItems { get; set; } = [];
    
    public List<ProductReviewModel> ProductReviews { get; set; } = [];
}