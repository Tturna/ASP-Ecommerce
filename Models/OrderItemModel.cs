namespace ASP_Ecommerce.Models;

public class OrderItemModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    
    public int OrderId { get; set; }
    public required OrderModel Order { get; set; }
    
    public int ProductId { get; set; }
    public required ProductModel Product { get; set; }
}