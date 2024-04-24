using ASP_Ecommerce.Models;
using ASP_Ecommerce.Models.ViewModels;
using ASP_Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_Ecommerce.Controllers;

public class ShoppingCartController(ApplicationDbContext dbContext) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult GetCart()
    {
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .First(u => u.UserName == User.Identity!.Name);

        var cartOverlayViewModel = new CartOverlayViewModel()
        {
            CartItems = user.ShoppingCart?.CartItems.ToArray()
        };
        
        return PartialView("_CartOverlay", cartOverlayViewModel);
    }

    // TODO: Use session storage for cart if the user is not signed in.
    [Authorize]
    public IActionResult Add(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        
        var item = dbContext.Products
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == id);
        
        if (item == null)
        {
            return NotFound();
        }
        
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .First(u => u.UserName == User.Identity!.Name);

        if (user.ShoppingCart == null)
        {
            var newCart = new ShoppingCartModel();
            user.ShoppingCart = newCart;
            
            user.ShoppingCart.CartItems.Add(new CartItemModel()
            {
                ShoppingCart = user.ShoppingCart,
                Product = item,
                Quantity = 1
            });
        }
        else
        {
            var existingItem = user.ShoppingCart.CartItems.FirstOrDefault(i => i.Product.Id == item.Id);
            
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
                    Quantity = 1
                });
            }
        }

        try
        {
            dbContext.SaveChanges();

            var cartOverlayViewModel = new CartOverlayViewModel()
            {
                CartItems = user.ShoppingCart.CartItems.ToArray()
            };
            
            return PartialView("_CartOverlay", cartOverlayViewModel);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize]
    public IActionResult Remove(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .First(u => u.UserName == User.Identity!.Name);
        
        if (user.ShoppingCart == null)
        {
            return NotFound();
        }
        
        var item = user.ShoppingCart.CartItems.FirstOrDefault(i => i.Id == id);
        
        if (item == null)
        {
            return NotFound();
        }
        
        if (item.Quantity > 1)
        {
            item.Quantity--;
        }
        else
        {
            user.ShoppingCart.CartItems.Remove(item);
        }
        
        try
        {
            dbContext.SaveChanges();

            var cartOverlayViewModel = new CartOverlayViewModel()
            {
                CartItems = user.ShoppingCart.CartItems.ToArray()
            };
            
            return PartialView("_CartOverlay", cartOverlayViewModel);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize]
    public IActionResult RemoveAll(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.CartItems)
            .ThenInclude(ci => ci.Product)
            .First(u => u.UserName == User.Identity!.Name);
        
        if (user.ShoppingCart == null)
        {
            return NotFound();
        }
        
        var item = user.ShoppingCart.CartItems.FirstOrDefault(i => i.Id == id);
        
        if (item == null)
        {
            return NotFound();
        }
        
        user.ShoppingCart.CartItems.Remove(item);
        
        try
        {
            dbContext.SaveChanges();

            var cartOverlayViewModel = new CartOverlayViewModel()
            {
                CartItems = user.ShoppingCart.CartItems.ToArray()
            };
            
            return PartialView("_CartOverlay", cartOverlayViewModel);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [Authorize]
    public IActionResult EmptyCart()
    {
        var user = dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(sc => sc.CartItems)
            .First(u => u.UserName == User.Identity!.Name);
        
        if (user.ShoppingCart == null)
        {
            return NotFound();
        }
        
        user.ShoppingCart.CartItems.Clear();
        
        try
        {
            dbContext.SaveChanges();

            var cartOverlayViewModel = new CartOverlayViewModel()
            {
                CartItems = []
            };
            
            return PartialView("_CartOverlay", cartOverlayViewModel);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}