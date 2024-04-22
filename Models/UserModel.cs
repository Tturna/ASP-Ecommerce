using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASP_Ecommerce.Models;

public class UserModel : IdentityUser<int>
{
    [PersonalData, StringLength(50)]
    public string? FirstName { get; set; }
    
    [PersonalData, StringLength(50)]
    public string? LastName { get; set; }
    
    [PersonalData, StringLength(50)]
    public string? BillingAddress { get; set; }
    
    [PersonalData, StringLength(50)]
    public string? MaintainerAddress { get; set; }
    
    [PersonalData, StringLength(50)]
    public string? City { get; set; }
    
    [PersonalData, StringLength(50)]
    public string? Country { get; set; }
    
    [PersonalData, DataType(DataType.PostalCode)]
    public int? PostalCode { get; set; }
    
    public bool SaveBillingInfo { get; set; }
    
    [StringLength(100)]
    public string? MaintainerServices { get; set; }
    
    [StringLength(100)]
    public string? FeaturedMaintainerMessage { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public int? ShoppingCartId { get; set; }
    public ShoppingCartModel? ShoppingCart { get; set; }
    
    public List<OrderModel> Orders { get; set; } = [];
    public List<ProductReviewModel> ProductReviews { get; set; } = [];
    
    // [InverseProperty("Maintainer")]
    public List<ProductModel> MaintainerProducts { get; set; } = [];
}