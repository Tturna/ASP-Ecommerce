using System.Text.Json;
using ASP_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Services;

public class ShoppingCartService(ApplicationDbContext dbContext)
{
    private bool _cartUpdated = true;
    private ShoppingCartModel? _cachedCart;
    
    public ShoppingCartModel? GetCart(string username)
    {
        if (!_cartUpdated) return _cachedCart;
        
        var cart = dbContext.ShoppingCarts
            .Include(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(sc => sc.User.UserName == username);
            
        if (cart == null) return null;
            
        _cachedCart = cart;
        _cartUpdated = false;
            
        return cart;
    }

    public ShoppingCartModel? AddItemToCart(string username, int productId)
    {
        var item = dbContext.Products
            .FirstOrDefault(p => p.Id == productId);

        if (item == null) throw new Exception("Product not found");
        
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc!.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(u => u.UserName == username);

        if (user == null) throw new Exception("User not found");
        
        user.ShoppingCart ??= CreateCart(user);
        
        var existingItem = user.ShoppingCart.CartItems
            .FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            user.ShoppingCart.CartItems.Add(new CartItemModel()
            {
                ShoppingCart = user.ShoppingCart,
                Product = item,
                ProductId = item.Id,
                Quantity = 1
            });
        }

        try
        {
            dbContext.SaveChanges();
            _cartUpdated = true;
            
            return user.ShoppingCart;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to add item to cart", e);
        }
    }

    /// <summary>
    /// Removes an item from the user's shopping cart. If the optional quantity is less than 0, the item is removed entirely.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public ShoppingCartModel? RemoveItemFromCart(string username, int productId, int quantity = 1)
    {
        if (quantity == 0) throw new Exception("Quantity can not be 0");
        
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc!.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(u => u.UserName == username);

        var existingItem = user?.ShoppingCart?.CartItems
            .FirstOrDefault(ci => ci.ProductId == productId);

        if (existingItem == null) throw new Exception("Item not found in cart");
        
        if (quantity > 0 && existingItem.Quantity > quantity)
        {
            existingItem.Quantity -= quantity;
        }
        else
        {
            user!.ShoppingCart!.CartItems.Remove(existingItem);
        }

        try
        {
            dbContext.SaveChanges();
            _cartUpdated = true;
            
            return user!.ShoppingCart!;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to remove item from cart", e);
        }
    }
    
    public ShoppingCartModel? EmptyCart(string username)
    {
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc!.CartItems)
            .FirstOrDefault(u => u.UserName == username);

        if (user == null) throw new Exception("User not found");
        
        user.ShoppingCart!.CartItems.Clear();

        try
        {
            dbContext.SaveChanges();
            _cartUpdated = true;
            
            return user.ShoppingCart;
        }
        catch (Exception e)
        {
            throw new Exception("Failed to empty cart", e);
        }
    }

    public CartItemData[]? GetItemsFromLocalStorageJson(string dictJson)
    {
        var idDict = JsonSerializer.Deserialize<Dictionary<int, int>>(dictJson);

        if (idDict == null) throw new Exception("Failed to deserialize JSON");

        var ids = idDict.Keys;

        var uniqueProducts = dbContext.Products
            .Where(p => ids.Contains(p.Id))
            .AsNoTracking()
            .ToArray();
        
        List<CartItemData> cartItems = [];

        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var up in uniqueProducts)
        {
            if (idDict[up.Id] < 1) continue;
            
            cartItems.Add(new CartItemData()
            {
                Product = up,
                ProductId = up.Id,
                Quantity = idDict[up.Id]
            });
        }
        
        return cartItems.ToArray();
    }

    private ShoppingCartModel CreateCart(UserModel user)
    {
        var newCart = new ShoppingCartModel()
        {
            User = user
        };
        
        dbContext.ShoppingCarts.Add(newCart);
        
        return newCart;
    }
}