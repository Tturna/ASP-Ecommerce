namespace ASP_Ecommerce.Models;

public class CartItemModel : CartItemData
{
    public int Id { get; set; }
    
    public int ShoppingCartId { get; set; }
    public required ShoppingCartModel ShoppingCart { get; set; }
}