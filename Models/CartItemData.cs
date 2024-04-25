namespace ASP_Ecommerce.Models;

public class CartItemData
{
    public required ProductModel Product { get; set; }
    public required int Quantity { get; set; }
    public int ProductId { get; set; }
}