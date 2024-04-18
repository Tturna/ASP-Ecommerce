namespace ASP_Ecommerce.Models;

public class CartItemModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    
    public int ShoppingCartId { get; set; }
    public required ShoppingCartModel ShoppingCart { get; set; }
    
    public int ProductId { get; set; }
    public required ProductModel Product { get; set; }
}