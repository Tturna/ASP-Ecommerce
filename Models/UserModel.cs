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
    
    [PersonalData, StringLength(150)]
    public string? ProfilePictureUrl { get; set; }
    
    [StringLength(100)]
    public string? MaintainerServices { get; set; }
    
    [StringLength(100)]
    public string? FeaturedMaintainerMessage { get; set; }
    
    public decimal MaintainerLatitude { get; set; }
    public decimal MaintainerLongitude { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public int? ShoppingCartId { get; set; }
    public ShoppingCartModel? ShoppingCart { get; set; }
    
    public List<OrderModel> Orders { get; set; } = [];
    public List<ProductReviewModel> ProductReviews { get; set; } = [];
    
    // [InverseProperty("Maintainer")]
    public List<ProductModel> MaintainerProducts { get; set; } = [];
    
    public UserModel() { }

    // Clone constructor
    public UserModel(UserModel source)
    {
        FirstName = source.FirstName;
        LastName = source.LastName;
        BillingAddress = source.BillingAddress;
        MaintainerAddress = source.MaintainerAddress;
        City = source.City;
        Country = source.Country;
        PostalCode = source.PostalCode;
        SaveBillingInfo = source.SaveBillingInfo;
        MaintainerServices = source.MaintainerServices;
        FeaturedMaintainerMessage = source.FeaturedMaintainerMessage;
        CreatedAt = source.CreatedAt;
        ShoppingCartId = source.ShoppingCartId;
        ShoppingCart = source.ShoppingCart;
        Orders = source.Orders;
        ProductReviews = source.ProductReviews;
        MaintainerProducts = source.MaintainerProducts;
    }
}