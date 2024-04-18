using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models;

public class ProductReviewModel
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public required string Title { get; set; }
    
    [Required, StringLength(500)]
    public required string Content { get; set; }
    
    [Range(1, 5)]
    public required int Rating { get; set; }
}