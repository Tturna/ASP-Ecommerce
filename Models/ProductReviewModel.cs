using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models;

public class ProductReviewModel
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Title { get; set; }
    
    [Required, StringLength(500)]
    public string Content { get; set; }
    
    [Required, Range(1, 5)]
    public int Rating { get; set; }
    
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    
    public int UserId { get; set; }
    public UserModel User { get; set; }
}