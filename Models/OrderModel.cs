using System.ComponentModel.DataAnnotations;

namespace ASP_Ecommerce.Models;

public class OrderModel
{
    public int Id { get; set; }
    
    [Required]
    public required DateTime OrderDateTime { get; set; }
    
    [Required]
    public string OrderStatus { get; set; }
    
    [Required, StringLength(50)]
    public required string Address { get; set; }
    
    [Required, StringLength(50)]
    public required string City { get; set; }
    
    [Required, StringLength(50)]
    public required string Country { get; set; }
    
    [DataType(DataType.PostalCode)]
    public int PostalCode { get; set; }
    
    public required List<OrderItemModel> OrderItems { get; set; }
    
    public int UserId { get; set; }
    public required UserModel User { get; set; }
}