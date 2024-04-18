using System.ComponentModel.DataAnnotations;
using ASP_Ecommerce.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace ASP_Ecommerce.Models;

public class UserModel : IdentityUser<int>
{
    // public int Id { get; set; }
    
    // [Required, StringLength(50)]
    // public string Username { get; set; }
    
    // [Required, StringLength(50)]
    // [DataType(DataType.EmailAddress)]
    // public string Email { get; set; }
    
    // [Required]
    // public byte[] PasswordHash { get; set; }
    
    // public byte[] PasswordSalt { get; set; }
    
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
    
    // [StringLength(15)]
    // [DataType(DataType.PhoneNumber)]
    // public string? PhoneNumber { get; set; }
    
    public bool SaveBillingInfo { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
    
    public int? ShoppingCartId { get; set; }
    public ShoppingCartModel? ShoppingCart { get; set; }
    
    public List<OrderModel> Orders { get; set; } = [];
    public List<ProductReviewModel> ProductReviews { get; set; } = [];
}