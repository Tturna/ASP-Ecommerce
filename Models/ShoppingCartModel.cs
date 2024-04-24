namespace ASP_Ecommerce.Models;

public class ShoppingCartModel
{
    public int Id { get; set; }

    public List<CartItemModel> CartItems { get; set; } = [];
}