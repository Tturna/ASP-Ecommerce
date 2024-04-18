namespace ASP_Ecommerce.Models;

public class ShoppingCartModel
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public required UserModel User { get; set; }
    
    public required List<CartItemModel> CartItems { get; set; }
}